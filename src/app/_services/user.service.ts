import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { User } from '@/_models';
import { apiConfig } from '@/_helpers';

@Injectable({ providedIn: 'root' })
export class UserService {
    public host: apiConfig = new apiConfig;
    public readonly endpoint = this.host.endpoint;
    private readonly _getAllUrl = this.endpoint + "api/Event/UserEvents";
    private readonly _registerUrl = this.endpoint + "api/Register/NewRegistration";

    constructor(private http: HttpClient) { }

    getAll(UserId: string) {
        return this.http.post<string>(this._getAllUrl, { userId: UserId} );
    }

    register(user: User) {
        return this.http.post(this._registerUrl, user);
    }

    //delete(id: number) {
    //    return this.http.delete(`${config.apiUrl}/users/${id}`);
    //}
}