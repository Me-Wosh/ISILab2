import { HttpClient } from '@angular/common/http';
import { Component, inject, OnInit, signal } from '@angular/core';
import { Post } from '../models/post.model';
import { Router } from '@angular/router';
import { DatePipe } from '@angular/common';

@Component({
    selector: 'app-posts-list',
    imports: [DatePipe],
    templateUrl: './posts-list.component.html'
})
export class PostsListComponent implements OnInit {

    private readonly httpClient = inject(HttpClient);
    private readonly router = inject(Router);

    protected readonly posts = signal<Post[]>([]);

    ngOnInit(): void {
        this.httpClient.get<Post[]>('http://localhost:5118/api/posts')
            .subscribe(posts => this.posts.set(posts));
    }

    protected viewPost(postId: number): void {
        this.router.navigate(['posts', postId]);
    }
}
