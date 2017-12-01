import { CourseItemCreateComponent } from './course-items/course-item-create.component';
import { CourseItemsComponent } from './course-items/course-items.component';

import { UserComponent } from './user.component';

export * from './course-items/course-item-create.component';
export * from './course-items/course-items.component';

export * from './user.component';

export const USER_COMPONENTS = [
    CourseItemCreateComponent,
    CourseItemsComponent,

    UserComponent
];
