import { Component, OnInit  } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { HttpErrorResponse } from '@angular/common/http';

import { CoursesDataService, RouterService } from '../../../services/index';

import { Course } from '../../../domain/index';

@Component({
    moduleId: module.id,
    selector: 'course-update',
    templateUrl: 'course-update.component.html'
})

export class CourseUpdateComponent implements OnInit {
    constructor(
        private coursesDataService: CoursesDataService,
        private routerService: RouterService,
        private activatedRoute: ActivatedRoute)
    { }

    public course: Course = new Course();
    public errorMessage: string = null;
    private subscriptions: any[] = [];
    private routeParams: any;

    ngOnInit(): void {
        this.subscriptions.push(this.activatedRoute.params.subscribe(params => {
            this.routeParams = params;

            this.onRouteChange();
        }));
    }

    public update(): void {
        this.coursesDataService.update(this.course).subscribe(
            () => this.routerService.navigateByUrl('/user/courses'),
            (error: HttpErrorResponse) => this.errorMessage = error.error || 'Update Course failed.');
    }

    private onRouteChange() {
        const id = this.routeParams['id'];
        this.coursesDataService.getUpdate(id).subscribe(
            data => this.course = data,
            (error: HttpErrorResponse) => this.errorMessage = error.error || 'Get Course information failed.');
    }
}
