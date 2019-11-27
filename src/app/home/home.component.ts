import { Component, OnInit } from '@angular/core';
import { first } from 'rxjs/operators';
import { Router } from '@angular/router';
import { User } from '@/_models';
import { EventService, AuthenticationService } from '@/_services';

@Component({ templateUrl: 'home.component.html' })
export class HomeComponent implements OnInit {
    currentUser: User;
    events = [];
    UserId: string = "";
    progressBar: boolean = true;

    editIcon: any = '../../src/app/_content/images/edit.png';
    deleteIcon: any = '../../src/app/_content/images/delete.png';
    timeIcon: any = '../../src/app/_content/images/time.png';
    locationIcon: any = '../../src/app/_content/images/location.png';

    constructor(
        private router: Router,
        private authenticationService: AuthenticationService,
        private eventService: EventService
    ) {
        this.currentUser = this.authenticationService.currentUserValue;
        this.UserId = this.authenticationService.currentUserValue.UserId;
    }

    ngOnInit() {
        this.progressBar = true;
        this.loadUserEvents();
    }

    delete(EventId: any) {
        var result = confirm("Want to delete?");
        if (result) {
            this.eventService.DeleteEventById(EventId, this.UserId)
                .pipe(first())
                .subscribe(res => {
                    this.events = JSON.parse(JSON.parse(JSON.stringify(res)).Data);                   
                });
        }
        
    }

    private loadUserEvents() {
        this.eventService.UserEvents(this.UserId)
            .pipe(first())
            .subscribe(res => {
                this.events = JSON.parse(JSON.parse(JSON.stringify(res)).Data);
                this.progressBar = false;
            });
    }

    logout() {
        this.authenticationService.logout();
        this.router.navigate(['/login']);
    }
}