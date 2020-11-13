import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Observable } from 'rxjs';
import { map, tap } from 'rxjs/operators';
import { CURRENT_USER } from 'src/app/constants/db-keys';
import { TestService } from 'src/app/services/test.service';

@Component({
  selector: 'app-history-test',
  templateUrl: './history-test.component.html',
  styleUrls: ['./history-test.component.css']
})
export class HistoryTestComponent implements OnInit {

  itemsAsync: Observable<any[]>;
  page: number;
  pageSize: number;
  total: number;

  constructor(
    public testService: TestService,
    private router: Router
  ) { }

  ngOnInit() {
    // if (this.getId !== 1) {
    //   this.router.navigate(['/home']);
    // }
    this.page = 1;
    this.pageSize = 10;
    this.getAllHistories(this.page);
  }

  get getuserId() {
    const user = JSON.parse(localStorage.getItem(CURRENT_USER));

    return user.id;
  }

  getAllHistories(page: number) {
    this.itemsAsync = this.testService.getHistories(this.getuserId, page, this.pageSize)
    .pipe(
      tap(response => {
        this.total = response.total;
        this.page = page;
      }),
      map(response => response.items)
    );
  }

  viewDetail(id: number) {
    this.router.navigate(['test/history/view/' + id]);
  }
}
