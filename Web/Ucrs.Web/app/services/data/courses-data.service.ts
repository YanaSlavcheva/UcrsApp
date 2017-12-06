import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { Observable } from 'rxjs/Observable';

import { Course } from '../../domain/course';

@Injectable()
export class CoursesDataService {
    private static readonly URLS = {
        ALL: 'api/courses/all',
        CREATE: 'api/courses/create',
        MARK_AS_DONE: 'api/courses/markasdone/'
    };

    constructor(private httpClient: HttpClient) { }

    public getAll(): Observable<Course[]> {
        return this.httpClient.get<Course[]>(CoursesDataService.URLS.ALL);
    }

    public create(course: Course): Observable<any> {
        return this.httpClient.post(CoursesDataService.URLS.CREATE, course);
    }

    public markAsDone(id: number): Observable<any> {
        return this.httpClient.post(`${CoursesDataService.URLS.MARK_AS_DONE}${id}`, null, { responseType: 'text' });
    }
}
