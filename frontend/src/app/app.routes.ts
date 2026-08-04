import { DashboardPage } from './pages/dashboard/dashboard.page';
import { RegisterPage } from './pages/register/register.page';
import { unauthGuard } from './shared/guards/unauth.guard';
import { authGuard } from './shared/guards/auth.guard';
import { LoginPage } from './pages/login/login.page';
import { Routes } from '@angular/router';

export const routes: Routes = [
    {
        path: 'login',
        component: LoginPage,
        canActivate: [unauthGuard]
    },
    {
        path: 'register',
        component: RegisterPage,
        canActivate: [unauthGuard]
    },
    {
        path: 'dashboard',
        component: DashboardPage,
        canActivate: [authGuard]
    }
];
