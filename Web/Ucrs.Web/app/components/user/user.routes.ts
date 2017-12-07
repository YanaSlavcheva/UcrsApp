import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import {
    CourseCreateComponent,
    CourseUpdateComponent,
    CoursesComponent,

    UserComponent
} from './index';

import { AuthGuardService } from '../../services/index';

const ACCOUNT_ROUTES: Routes = [
    {
        path: '',
        component: UserComponent,
        canActivate: [AuthGuardService],
        canActivateChild: [AuthGuardService],
        children: [
            { path: '', redirectTo: '/user/courses', pathMatch: 'full' },

            { path: 'course-create', component: CourseCreateComponent },
            { path: 'course-update/:id', component: CourseUpdateComponent },
            { path: 'courses', component: CoursesComponent }
        ]
    }
];

@NgModule({
    imports: [RouterModule.forChild(ACCOUNT_ROUTES)],
    exports: [RouterModule]
})

export class UserRoutingModule { }
