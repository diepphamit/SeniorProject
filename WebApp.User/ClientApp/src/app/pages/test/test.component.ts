import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { Observable } from 'rxjs';
import { map, tap } from 'rxjs/operators';
import { TestService } from 'src/app/services/test.service';

@Component({
  selector: 'app-test',
  templateUrl: './test.component.html',
  styleUrls: ['./test.component.css']
})
export class TestComponent implements OnInit {

  itemsAsync: Observable<any[]>;
  dataTest: any[];
  count = 0;
  keyword: string;
  page: number;
  pageSize: number;
  dog: true;
  total: number;
  score = -1;
  constructor(private testService: TestService) {
  }

  ngOnInit() {
    this.keyword = '';
    this.page = 1;
    this.pageSize = 10;
    this.getTests(this.page);
    //this.count = 0;
  }

  getTests(page: number) {
    this.itemsAsync = this.testService.getTests(this.keyword, page, this.pageSize)
      .pipe(
        tap(response => {
          this.total = response.total;
          this.page = page;
        }),
        map(response => response.items)
      );

    this.itemsAsync.subscribe(data => {
      this.dataTest = data;
    });
  }

  addTest() {
    const testForCreate = {
      userId: 1,
      createAt: new Date(),
      testDetails: this.dataTest
    };
    this.testService.createTest(testForCreate).subscribe(data => {
      this.dataTest = data['data']['testDetails'];
      this.score = data['data']['score'];
      this.count = 1;

    });
  }

  addMyAnswer(flashcardId, myAnswer) {
    this.dataTest.forEach(element => {
      if (element.flashcardId === flashcardId) {
        element.myAnswer = myAnswer;
      }
    });
  }

  getColor(myAnswer, flashcardId, answerNumber, count) {
    if (count !== 0) {
      if (answerNumber === flashcardId) {
        return 'green';
      }

      if (myAnswer === answerNumber && flashcardId !== answerNumber) {
        return 'red';
      }

      return 'black';
    }

    return 'black';
  }

}
