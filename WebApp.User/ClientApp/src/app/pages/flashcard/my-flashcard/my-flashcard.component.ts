import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Observable } from 'rxjs';
import { map, tap } from 'rxjs/operators';
import { CURRENT_USER } from 'src/app/constants/db-keys';
import { FlashcardService } from 'src/app/services/flashcard.service';

@Component({
  selector: 'app-my-flashcard',
  templateUrl: './my-flashcard.component.html',
  styleUrls: ['./my-flashcard.component.css']
})
export class MyFlashcardComponent implements OnInit {

  flashcardsAsync: Observable<any[]>;
  page: number;
  pageSize: number;
  total: number;

  constructor(private flashcardService: FlashcardService,
    private route: ActivatedRoute) { }

  ngOnInit() {
    this.page = 1;
    this.pageSize = 8;

    this.getFlashcardsByUserId(this.page);
  }

  get getuserId() {
    const user = JSON.parse(localStorage.getItem(CURRENT_USER));

    return user.id;
  }

  getFlashcardsByUserId(page: number) {
    this.flashcardsAsync = this.flashcardService.getFlashcardsByUserId(Number(this.getuserId), page, this.pageSize)
          .pipe(
            tap(response => {
              this.total = response.total;
              this.page = page;
            }),
            map(response => response.items)
          );


  }

}
