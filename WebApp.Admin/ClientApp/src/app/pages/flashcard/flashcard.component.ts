import { HttpErrorResponse } from '@angular/common/http';
import { Component, OnInit, TemplateRef } from '@angular/core';
import { Router } from '@angular/router';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { ToastrService } from 'ngx-toastr';
import { Observable } from 'rxjs';
import { map, tap } from 'rxjs/operators';
import { Flashcard } from 'src/app/models/flashcard/flashcard.model';
import { FlashcardService } from 'src/app/services/flashcard.service';

@Component({
  selector: 'app-flashcard',
  templateUrl: './flashcard.component.html'
})
export class FlashcardComponent implements OnInit {

  flashcard: Flashcard;
  keyword: string;
  itemsAsync: Observable<any[]>;
  modalRef: BsModalRef;
  page: number;
  pageSize: number;
  total: number;

  constructor(
    public flashcardService: FlashcardService,
    private router: Router,
    private modalService: BsModalService,
    private toastr: ToastrService
  ) { }

  ngOnInit() {
    // if (this.getId !== 1) {
    //   this.router.navigate(['/home']);
    // }
    this.keyword = '';
    this.page = 1;
    this.pageSize = 10;
    this.getAllFlashcards(this.page);
  }

  getAllFlashcards(page: number) {
    this.itemsAsync = this.flashcardService.getAllFlashcards(this.keyword, page, this.pageSize)
    .pipe(
      tap(response => {
        this.total = response.total;
        this.page = page;
      }),
      map(response => response.items)
    );
  }

  add() {
    this.router.navigate(['/flashcards/add']);
  }

  edit(id: any) {
    this.router.navigate(['/flashcards/edit/' + id]);
  }

  deleteConfirm(template: TemplateRef<any>, data: any) {
    this.flashcard = Object.assign({}, data);
    this.modalRef = this.modalService.show(template);
  }

  confirm(): void {
    if (this.flashcard) {
      this.flashcardService.deleteFlashcard(this.flashcard.id)
        .subscribe(
          () => {
            this.getAllFlashcards(this.page);
            this.toastr.success(`Xóa flashcard thành công`);
          },
          (error: HttpErrorResponse) => {
            this.toastr.error(`Xóa Flashcard không thành công!`);
          }
        );
    }
    this.flashcard = undefined;
    this.modalRef.hide();
  }

  close(): void {
    this.flashcard = undefined;
    this.modalRef.hide();
  }

  search() {
    this.getAllFlashcards(this.page);
  }

  refresh() {
    this.keyword = '';
    this.getAllFlashcards(this.page);
  }

}
