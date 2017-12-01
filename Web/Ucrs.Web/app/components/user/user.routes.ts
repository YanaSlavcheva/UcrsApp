import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import {
    CourseItemCreateComponent,
    CourseItemsComponent,

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
            { path: '', redirectTo: '/user/course-items', pathMatch: 'full' },

            { path: 'course-item-create', component: CourseItemCreateComponent },
            { path: 'course-items', component: CourseItemsComponent }
        ]
    }
];

@NgModule({
    imports: [RouterModule.forChild(ACCOUNT_ROUTES)],
    exports: [RouterModule]
})

export class UserRoutingModule { }
