import { DashboardPage } from './pages/dashboard/dashboard.page';
import { RegisterPage } from './pages/register/register.page';
import { unauthGuard } from './shared/guards/unauth.guard';
import { authGuard } from './shared/guards/auth.guard';
import { LoginPage } from './pages/login/login.page';
import { GamesPage } from './pages/games/games.page';
import { Routes } from '@angular/router';

export const routes: Routes = [
    {
        path: '',
        pathMatch: 'full',
        redirectTo: 'dashboard'
    },
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
    },
    {
        path: 'games',
        component: GamesPage,
    },
    {
        path: '**',
        pathMatch: 'full',
        redirectTo: 'dashboard'
    }
];
