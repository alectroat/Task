import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { first } from 'rxjs/operators';

import { AlertService, EventService, AuthenticationService } from '@/_services';

@Component({ templateUrl: 'event.component.html' })
export class EventComponent implements OnInit {
    eventForm: FormGroup;
    loading = false;
    submitted = false;

    fileData: File = null;
    previewUrl: any = '../../src/app/_content/images/default-image.png';
    fileUploadProgress: string = null;
    uploadedFilePath: string = null;
    UserId: string = "";
    progressBar: boolean = false;

    private __id: string = "";;

    constructor(
        private formBuilder: FormBuilder,
        private router: Router,
        private activatedRoute: ActivatedRoute,
        private authenticationService: AuthenticationService,
        private eventService: EventService,
        private alertService: AlertService
    ) {
        // redirect to home if already logged in
        if (this.authenticationService.currentUserValue) {
            //this.router.navigate(['/']);
            this.UserId = this.authenticationService.currentUserValue.UserId;
            //console.log(this.currentUser.UserId);
        }
    }

    fileProgress(fileInput: any) {
        this.fileData = <File>fileInput.target.files[0];
        this.preview();
    }

    preview() {
        // Show preview 
        var mimeType = this.fileData.type;
        if (mimeType.match(/image\/*/) == null) {
            return;
        }

        var reader = new FileReader();
        reader.readAsDataURL(this.fileData);
        reader.onload = (_event) => {
            this.previewUrl = reader.result;
        }
    }

    ngOnInit() {
        this.eventForm = this.formBuilder.group({
            Title: ['', Validators.required],
            Description: ['', Validators.required],
            Date: [''],
            Start: [{ hour: 0, minute: 0 }],
            End: [{ hour: 0, minute: 0 }],
            Location: [''],
            NotifyBefore: [''],
            NotificationMedium: ['Email'],            
            UserId: [this.UserId],
            EventId: [''],
        });

        this.activatedRoute.queryParams.subscribe(params => {
            if (params.id) {
                this.__id = params.id;
                this.progressBar = true;
                this.eventService.EventById(this.__id).subscribe(res => {
                    var d = JSON.parse(JSON.parse(JSON.stringify(res)).Data);
                    console.log(d);
                    var SH = +d.Start.split(":")[0];
                    var SM = +d.Start.split(":")[1];
                    var EH = +d.End.split(":")[0];
                    var EM = +d.End.split(":")[1];
                    this.eventForm.patchValue(
                        {
                            Title: d.Title,
                            Description: d.Description,
                            Date: d.Date,
                            Start: { hour: SH, minute: SM },
                            End: { hour: EH, minute: EM },
                            Location: d.Location,
                            NotifyBefore: d.NotifyBefore,
                            NotificationMedium: d.NotificationMedium,
                            UserId: d.UserId,
                            EventId: d.EventId
                        });
                    this.progressBar = false;
                },
                error => {
                });
            } 
        });
    }

    // convenience getter for easy access to form fields
    get f() { return this.eventForm.controls; }

    onSubmit() {
        //this.registerForm.patchValue({Image: this.previewUrl});
        //this.registerForm.controls['Image'].setValue(this.previewUrl);
        //this.registerForm.Image=this.previewUrl;
        console.log(this.eventForm.value);
        this.submitted = true;

        // reset alerts on submit
        this.alertService.clear();

        // stop here if form is invalid
        if (this.eventForm.invalid) {
            return;
        }

        this.loading = true;
        this.eventService.save(this.eventForm.value)
            .pipe(first())
            .subscribe(
                data => {
                    //this.alertService.success('Registration successful', true);
                    this.router.navigate(['/home']);
                },
                error => {
                    this.alertService.error(error);
                    this.loading = false;
                });
    }
}