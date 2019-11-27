import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';

import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { FormsModule } from '@angular/forms';

import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { MatButtonModule, MatFormFieldModule, MatInputModule, MatRippleModule, MatDatepickerModule, MatNativeDateModule, MatIconModule, MatProgressSpinnerModule, MatProgressBarModule  } from '@angular/material';
import { AngularFontAwesomeModule } from 'angular-font-awesome';
// used to create fake backend
import { fakeBackendProvider } from './_helpers';

import { appRoutingModule } from './app.routing';
import { JwtInterceptor, ErrorInterceptor } from './_helpers';
import { AppComponent } from './app.component';
import { DashboardComponent } from './dashboard';
import { HomeComponent } from './home';
import { LoginComponent } from './login';
import { RegisterComponent } from './register';
import { EventComponent } from './event';
import { AlertComponent } from './_components';

@NgModule({
    imports: [
        BrowserAnimationsModule,
        BrowserModule,
        AngularFontAwesomeModule,
        ReactiveFormsModule,
        HttpClientModule,
        MatFormFieldModule,
        MatInputModule,
        MatDatepickerModule,
        MatNativeDateModule,
        MatProgressSpinnerModule,
        MatProgressBarModule,
        MatIconModule,
        FormsModule,
        NgbModule,
        appRoutingModule
    ],
    declarations: [
        AppComponent,
        DashboardComponent,
        HomeComponent,
        LoginComponent,
        RegisterComponent,
        EventComponent,
        AlertComponent
    ],
    providers: [
        { provide: HTTP_INTERCEPTORS, useClass: JwtInterceptor, multi: true },
        { provide: HTTP_INTERCEPTORS, useClass: ErrorInterceptor, multi: true },

        // provider used to create fake backend
        fakeBackendProvider
    ],
    bootstrap: [AppComponent]
})
export class AppModule { };