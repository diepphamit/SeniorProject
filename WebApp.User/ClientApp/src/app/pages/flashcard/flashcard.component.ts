import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Observable } from 'rxjs';
import { map, tap } from 'rxjs/operators';
import { FlashcardService } from 'src/app/services/flashcard.service';
import { TopicService } from 'src/app/services/topic.service';

@Component({
  selector: 'app-flashcard',
  templateUrl: './flashcard.component.html',
  styleUrls: ['./flashcard.component.css']
})
export class FlashcardComponent implements OnInit {

  id: any;
  flashcardsAsync: Observable<any[]>;
  topic: Observable<any>;
  page: number;
  pageSize: number;
  total: number;

  constructor(private flashcardService: FlashcardService,
    private topicService: TopicService,
    private route: ActivatedRoute) { }

  ngOnInit() {
    this.page = 1;
    this.pageSize = 8;
    // this.flashcardService.getFlashcardsByTopicId(1, 1, 1000).subscribe(data => console.log(data));
    this.route.params.subscribe(params => {
      this.id = params.topicId;
      if (this.id) {
        this.getFlashcardsByTopicId(this.page);
        this.getTopic(this.id);
      }
    });
  }

  getFlashcardsByTopicId(page: number) {
    this.flashcardsAsync = this.flashcardService.getFlashcardsByTopicId(Number(this.id), page, this.pageSize)
          .pipe(
            tap(response => {
              this.total = response.total;
              this.page = page;
            }),
            map(response => response.items)
          );

    this.flashcardsAsync.subscribe(data => console.log(data));
  }

  getTopic(id: any) {
    this.topicService.getTopicById(id).subscribe(data => this.topic = data);
  }

}
