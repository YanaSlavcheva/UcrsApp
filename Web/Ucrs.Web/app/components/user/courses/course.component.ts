import { Component, OnInit } from '@angular/core';
import { HttpErrorResponse } from '@angular/common/http';

import { CoursesDataService } from '../../../services/index';

import { Course } from '../../../domain/index';

@Component({
    moduleId: module.id,
    selector: 'courses',
    templateUrl: 'courses.component.html'
})

export class CoursesComponent implements OnInit {
    constructor(private coursesDataService: CoursesDataService) { }

    public courses: Course[] = [];
    public errorMessage: string = null;

    ngOnInit() {
        this.coursesDataService.getAll().subscribe((data: Course[]) => this.courses = data);
    }

    public markAsDone(course: Course): void {
        this.coursesDataService.markAsDone(course.id).subscribe(
            () => {
                this.errorMessage = null;

                course.isDone = true;
            },
            (error: HttpErrorResponse) => this.errorMessage = error.error || 'Mark Course as done failed.');
    }
}
