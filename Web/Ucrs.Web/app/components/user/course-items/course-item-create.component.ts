import { Component } from '@angular/core';
import { HttpErrorResponse } from '@angular/common/http';

import { CourseItemsDataService, RouterService } from '../../../services/index';

import { CourseItem } from '../../../domain/index';

@Component({
    moduleId: module.id,
    selector: 'course-item-create',
    templateUrl: 'course-item-create.component.html'
})

export class CourseItemCreateComponent {
    constructor(private courseItemsDataService: CourseItemsDataService, private routerService: RouterService) { }

    public courseItem: CourseItem = new CourseItem();
    public errorMessage: string = null;

    public create(): void {
        this.courseItemsDataService.create(this.courseItem).subscribe(
            () => this.routerService.navigateByUrl('/user/course-items'),
            (error: HttpErrorResponse) => this.errorMessage = error.error || 'Create Course item failed.');
    }
}
