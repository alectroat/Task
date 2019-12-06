import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { apiConfig } from '@/_helpers';

import { User } from '@/_models';

@Injectable({ providedIn: 'root' })
export class AuthenticationService {
    private currentUserSubject: BehaviorSubject<User>;
    public currentUser: Observable<User>;

    public host: apiConfig = new apiConfig;
    public readonly endpoint = this.host.endpoint;
    private readonly _loginUrl = this.endpoint + "api/Login/AuthenticUser";

    constructor(private http: HttpClient) {
        this.currentUserSubject = new BehaviorSubject<User>(JSON.parse(localStorage.getItem('currentUser')));
        this.currentUser = this.currentUserSubject.asObservable();
    }

    public get currentUserValue(): User {
        return this.currentUserSubject.value;
    }
    //Email:Email, Password:Password 
    login(Email, Password) {
        return this.http.post<any>(this._loginUrl, { Email: Email, Password: Password })
            .pipe(map(user => {
                if (user.Data==null){
                    return [];
                }
                // store user details and jwt token in local storage to keep user logged in between page refreshes
                localStorage.setItem('currentUser', JSON.stringify(user.Data));
                this.currentUserSubject.next(user.Data);
                return user.Data;
            }));
    }

    logout() {
        // remove user from local storage and set current user to null
        localStorage.removeItem('currentUser');
        this.currentUserSubject.next(null);
    }
}