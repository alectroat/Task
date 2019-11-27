import { Component, OnInit } from '@angular/core';
import { first } from 'rxjs/operators';
import { Router } from '@angular/router';
import { User } from '@/_models';
import { AuthenticationService } from '@/_services';

@Component({ templateUrl: 'dashboard.component.html' })
export class DashboardComponent implements OnInit {
    currentUser: User;
    constructor(
        private router: Router,
        private authenticationService: AuthenticationService
    ) {
        this.currentUser = this.authenticationService.currentUserValue;
    }

    ngOnInit() {}

    logout() {
        this.authenticationService.logout();
        this.router.navigate(['/login']);
    }
}