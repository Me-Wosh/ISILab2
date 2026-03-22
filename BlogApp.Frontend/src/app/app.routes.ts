import { Routes } from '@angular/router';
import { PostsListComponent } from '../posts-list/posts-list.component';
import { PostDetailsComponent } from './post-details/post-details.component';

export const routes: Routes = [
    {
        path: '',
        redirectTo: 'posts',
        pathMatch: 'full'
    },
    {
        path: 'posts',
        component: PostsListComponent
    },
    {
        path: 'posts/:id',
        component: PostDetailsComponent
    }
];
