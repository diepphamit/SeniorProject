import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { map, tap } from 'rxjs/operators';
import { FlashcardService } from 'src/app/services/flashcard.service';
import { TopicService } from 'src/app/services/topic.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html'
})
export class HomeComponent implements OnInit {

  topicsAsync: Observable<any[]>;
  flashcardsAsync: Observable<any[]>;

  constructor(
    private topicService: TopicService,
    private flashcardService: FlashcardService) { }

  ngOnInit() {

    this.getAllFlashcard();
    this.getAllTopic();
  }

  getAllTopic() {
    this.topicsAsync = this.topicService.getPopularTopics('', 1, 1000)
    .pipe(
      map(response => response.items)
    );
  }

  getAllFlashcard() {
    this.flashcardsAsync = this.flashcardService.getAllFlashcards('', 1, 1000)
    .pipe(
      map(response => response.items)
    );
  }

}
