import { Routes } from '@angular/router';
import { LoginPage } from './pages/login/login.page';
import { DashboardPage } from './pages/dashboard/dashboard.page';
import { authGuard } from './shared/guards/auth.guard';
import { unauthGuard } from './shared/guards/unauth.guard';

export const routes: Routes = [
    {
        path: 'login',
        component: LoginPage,
        canActivate: [unauthGuard]
    },
    {
        path: 'dashboard',
        component: DashboardPage,
        canActivate: [authGuard]
    }
];
