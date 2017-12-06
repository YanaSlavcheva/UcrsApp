import { Component } from '@angular/core';
import { HttpErrorResponse } from '@angular/common/http';

import { CoursesDataService, RouterService } from '../../../services/index';

import { Course } from '../../../domain/index';

@Component({
    moduleId: module.id,
    selector: 'course-create',
    templateUrl: 'course-create.component.html'
})

export class CourseCreateComponent {
    constructor(private coursesDataService: CoursesDataService, private routerService: RouterService) { }

    public course: Course = new Course();
    public errorMessage: string = null;

    public create(): void {
        this.coursesDataService.create(this.course).subscribe(
            () => this.routerService.navigateByUrl('/user/courses'),
            (error: HttpErrorResponse) => this.errorMessage = error.error || 'Create Course item failed.');
    }
}
