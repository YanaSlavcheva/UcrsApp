import { Component, OnInit } from '@angular/core';
import { HttpErrorResponse } from '@angular/common/http';

import { CourseItemsDataService } from '../../../services/index';

import { CourseItem } from '../../../domain/index';

@Component({
    moduleId: module.id,
    selector: 'course-items',
    templateUrl: 'course-items.component.html'
})

export class CourseItemsComponent implements OnInit {
    constructor(private courseItemsDataService: CourseItemsDataService) { }

    public courseItems: CourseItem[] = [];
    public errorMessage: string = null;

    ngOnInit() {
        this.courseItemsDataService.getAll().subscribe((data: CourseItem[]) => this.courseItems = data);
    }

    public markAsDone(courseItem: CourseItem): void {
        this.courseItemsDataService.markAsDone(courseItem.id).subscribe(
            () => {
                this.errorMessage = null;

                courseItem.isDone = true;
            },
            (error: HttpErrorResponse) => this.errorMessage = error.error || 'Mark Course as done failed.');
    }
}
