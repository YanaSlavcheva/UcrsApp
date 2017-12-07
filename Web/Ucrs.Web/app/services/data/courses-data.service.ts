import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { Observable } from 'rxjs/Observable';

import { Course } from '../../domain/course';

@Injectable()
export class CoursesDataService {
    private static readonly URLS = {
        ALL: 'api/courses/all',
        CREATE: 'api/courses/create',
        REGISTER_FOR_COURSE: 'api/courses/registerforcourse/',
        GET_UPDATE: 'api/courses/getupdate/',
        UPDATE: 'api/courses/update'
    };

    constructor(private httpClient: HttpClient) { }

    public getAll(): Observable<Course[]> {
        return this.httpClient.get<Course[]>(CoursesDataService.URLS.ALL);
    }

    public create(course: Course): Observable<any> {
        return this.httpClient.post(CoursesDataService.URLS.CREATE, course);
    }

    public registerForCourse(id: number): Observable<any> {
        return this.httpClient.post(`${CoursesDataService.URLS.REGISTER_FOR_COURSE}${id}`, null, { responseType: 'text' });
    }

    public getUpdate(id: number): Observable<any> {
        return this.httpClient.get(`${CoursesDataService.URLS.GET_UPDATE}${id}`);
    }

    public update(course: Course): Observable<any> {
        return this.httpClient.post(CoursesDataService.URLS.UPDATE, course);
    }
}
