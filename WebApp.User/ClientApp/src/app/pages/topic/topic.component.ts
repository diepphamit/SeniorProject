import { HttpErrorResponse } from '@angular/common/http';
import { Component, OnInit, TemplateRef } from '@angular/core';
import { Router } from '@angular/router';
import { Observable } from 'rxjs';
import { map, tap } from 'rxjs/operators';
// import { Topic } from 'src/app/models/topic/topic.model';
import { TopicService } from 'src/app/services/topic.service';

@Component({
  selector: 'app-topic',
  templateUrl: './topic.component.html',
  styleUrls: ['./topic.component.css']
})
export class TopicComponent implements OnInit {

  keyword: string;
  topicsAsync: Observable<any[]>;
  popularTopicsAsync: Observable<any[]>;
  page: number;
  pageSize: number;
  total: number;

  constructor(
    public topicService: TopicService,
    private router: Router
  ) { }

  ngOnInit() {
    this.keyword = '';
    this.page = 1;
    this.pageSize = 8;
    this.getAllTopics(this.page);
    this.getPopularTopics();
  }

  getAllTopics(page: number) {
    this.topicsAsync = this.topicService.getAllTopics(this.keyword, page, this.pageSize)
    .pipe(
      tap(response => {
        this.total = response.total;
        this.page = page;
      }),
      map(response => response.items)
    );
  }

  getPopularTopics() {
    this.popularTopicsAsync = this.topicService.getPopularTopics('', 1, 1000)
    .pipe(
      map(response => response.items)
    );
  }
}
