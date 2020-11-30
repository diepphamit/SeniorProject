import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Observable } from 'rxjs';
import { map, tap } from 'rxjs/operators';
import { CURRENT_USER } from 'src/app/constants/db-keys';
import { FlashcardService } from 'src/app/services/flashcard.service';
import { TopicService } from 'src/app/services/topic.service';
import { UserFlashcardService } from 'src/app/services/user-flashcard.service';
import { UserFlashcardForCreate } from './Dto/UserFlashcardForCreate.model';

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
    private userFlashcardUservice: UserFlashcardService,
    private route: ActivatedRoute) { }

  ngOnInit() {
    this.page = 1;
    this.pageSize = 9;
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
    const user = JSON.parse(localStorage.getItem(CURRENT_USER));
    let userId = 1;
    if (user != null ) {
      userId = user.id;
    }
    this.flashcardsAsync = this.flashcardService.getFlashcardsByTopicId(Number(this.id), userId, page, this.pageSize)
          .pipe(
            tap(response => {
              this.total = response.total;
              this.page = page;
            }),
            map(response => response.items)
          );
  }

  getTopic(id: any) {
    this.topicService.getTopicById(id).subscribe(data => this.topic = data);
  }

  addFavoriteFlashcard(id: number) {
    const userFlashcard = new UserFlashcardForCreate(id, this.getuserId);
    this.userFlashcardUservice.createUserFlashcard(userFlashcard).subscribe(data => this.getFlashcardsByTopicId(this.page));
  }

  removeFavoriteFlashcard(id: any) {

  }

  get getuserId() {
    const user = JSON.parse(localStorage.getItem(CURRENT_USER));

    return user.id;
  }

  get getUser() {
    return JSON.parse(localStorage.getItem(CURRENT_USER));
  }

  playAudio(link) {
    const audio = new Audio(link);
    audio.load();
    audio.play();
  }

}
