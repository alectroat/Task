import { Routes, RouterModule } from '@angular/router';

import { DashboardComponent } from './dashboard';
import { HomeComponent } from './home';
import { LoginComponent } from './login';
import { RegisterComponent } from './register';
import { EventComponent } from './event';
import { AuthGuard } from './_helpers';

const routes: Routes = [
    { path: '', component: DashboardComponent, canActivate: [AuthGuard] },
    { path: 'home', component: HomeComponent},
    { path: 'login', component: LoginComponent },
    { path: 'register', component: RegisterComponent },
    { path: 'event', component: EventComponent },
    { path: 'event/edit', component: EventComponent },
    // otherwise redirect to home
    { path: '**', redirectTo: '' }
];

export const appRoutingModule = RouterModule.forRoot(routes);