import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { User } from '@/_models';
import { apiConfig } from '@/_helpers';

@Injectable({ providedIn: 'root' })
export class EventService {
    public host: apiConfig = new apiConfig;

    public readonly endpoint = this.host.endpoint;
    private readonly _saveUrl = this.endpoint + "api/Event/CreateNewEvent";
    private readonly _UserEventsUrl = this.endpoint + "api/Event/UserEvents";
    private readonly _EventByIdUrl = this.endpoint + "api/Event/GetEventById"
    private readonly _DeleteEventByIdUrl = this.endpoint + "api/Event/DeleteEventById"

    constructor(private http: HttpClient) { }

    UserEvents(UserId: string) {
        return this.http.post<string>(this._UserEventsUrl, { UserId: UserId });
    }

    EventById(EventId: any) {
        return this.http.post<string>(this._EventByIdUrl, { EventId: EventId});
    }

    DeleteEventById(EventId: any, UserId: string) {
        return this.http.post<string>(this._DeleteEventByIdUrl, { EventId: EventId, UserId: UserId });
    }    

    save(event: any) {
        return this.http.post(this._saveUrl, event);
    }

    delete(id: number) {
        return this.http.delete(`${config.apiUrl}/users/${id}`);
    }
}