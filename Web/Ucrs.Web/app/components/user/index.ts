import { CourseCreateComponent } from './courses/course-create.component';
import { CourseUpdateComponent } from './courses/course-update.component';
import { CoursesComponent } from './courses/courses.component';

import { UserComponent } from './user.component';

export * from './courses/course-create.component';
export * from './courses/course-update.component';
export * from './courses/courses.component';

export * from './user.component';

export const USER_COMPONENTS = [
    CourseCreateComponent,
    CourseUpdateComponent,
    CoursesComponent,

    UserComponent
];
