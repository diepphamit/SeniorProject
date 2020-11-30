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
  falshcardHome: Observable<any>;
  popularFalshcards: any[];

  constructor(
    private topicService: TopicService,
    private flashcardService: FlashcardService) { }

  ngOnInit() {

    this.getAllFlashcard();
    this.getAllTopic();
    this.getFlashcardHome();
    this.getPopularFlashcards();
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

  getPopularFlashcards() {
    this.flashcardService.getPopularFlashcards().subscribe(data => {
      this.popularFalshcards = data;
    });
  }

  getFlashcardHome() {
    this.flashcardService.getFlashcardHome().subscribe(data => this.falshcardHome = data);
  }

  playAudio(link) {
    const audio = new Audio(link);
    audio.load();
    audio.play();
  }

}
