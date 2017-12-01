import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { Observable } from 'rxjs/Observable';

import { CourseItem } from '../../domain/course-item';

@Injectable()
export class CourseItemsDataService {
    private static readonly URLS = {
        ALL: 'api/courseitems/all',
        CREATE: 'api/courseitems/create',
        MARK_AS_DONE: 'api/courseitems/markasdone/'
    };

    constructor(private httpClient: HttpClient) { }

    public getAll(): Observable<CourseItem[]> {
        return this.httpClient.get<CourseItem[]>(CourseItemsDataService.URLS.ALL);
    }

    public create(courseItem: CourseItem): Observable<any> {
        return this.httpClient.post(CourseItemsDataService.URLS.CREATE, courseItem);
    }

    public markAsDone(id: number): Observable<any> {
        return this.httpClient.post(`${CourseItemsDataService.URLS.MARK_AS_DONE}${id}`, null, { responseType: 'text' });
    }
}
