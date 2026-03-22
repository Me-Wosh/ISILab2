import { HttpClient } from '@angular/common/http';
import { Component, inject, OnInit, signal } from '@angular/core';
import { Post } from '../../models/post.model';
import { ActivatedRoute, Router } from '@angular/router';
import { DatePipe } from '@angular/common';

@Component({
    selector: 'app-post-details',
    imports: [DatePipe],
    templateUrl: './post-details.component.html'
})
export class PostDetailsComponent implements OnInit {

    private readonly httpClient = inject(HttpClient);
    private readonly route = inject(ActivatedRoute);
    private readonly router = inject(Router);

    protected readonly post = signal<Post | null>(null);

    ngOnInit(): void {
        const id = this.route.snapshot.paramMap.get('id');

        if (!id) {
            return;
        }

        this.httpClient.get<Post | null>(`http://localhost:5118/api/posts/${id}`)
            .subscribe(post => this.post.set(post));
    }

    protected goBack(): void {
        this.router.navigate(['..']);
    }
}
