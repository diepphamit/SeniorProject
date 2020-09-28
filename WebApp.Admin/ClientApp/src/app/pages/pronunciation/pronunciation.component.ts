import { HttpErrorResponse } from '@angular/common/http';
import { Component, OnInit, TemplateRef } from '@angular/core';
import { Router } from '@angular/router';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { ToastrService } from 'ngx-toastr';
import { Observable } from 'rxjs';
import { map, tap } from 'rxjs/operators';
import { Pronunciation } from 'src/app/models/pronunciation/pronunciation.model';
import { PronunciationService } from 'src/app/services/pronunciation.service';

@Component({
  selector: 'app-pronunciation',
  templateUrl: './pronunciation.component.html'
})
export class PronunciationComponent implements OnInit {

  pronunciation: Pronunciation;
  keyword: string;
  itemsAsync: Observable<any[]>;
  modalRef: BsModalRef;
  page: number;
  pageSize: number;
  total: number;

  constructor(
    public pronunciationService: PronunciationService,
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
    this.getAllPronunciations(this.page);
  }

  getAllPronunciations(page: number) {
    this.itemsAsync = this.pronunciationService.getAllPronunciations(this.keyword, page, this.pageSize)
    .pipe(
      tap(response => {
        this.total = response.total;
        this.page = page;
      }),
      map(response => response.items)
    );
  }

  add() {
    this.router.navigate(['/pronunciations/add']);
  }

  edit(id: any) {
    this.router.navigate(['/pronunciations/edit/' + id]);
  }

  deleteConfirm(template: TemplateRef<any>, data: any) {
    this.pronunciation = Object.assign({}, data);
    this.modalRef = this.modalService.show(template);
  }

  confirm(): void {
    if (this.pronunciation) {
      this.pronunciationService.deletePronunciation(this.pronunciation.id)
        .subscribe(
          () => {
            this.getAllPronunciations(this.page);
            this.toastr.success(`Xóa pronunciation thành công`);
          },
          (error: HttpErrorResponse) => {
            this.toastr.error(`Xóa Pronunciation không thành công!`);
          }
        );
    }
    this.pronunciation = undefined;
    this.modalRef.hide();
  }

  close(): void {
    this.pronunciation = undefined;
    this.modalRef.hide();
  }

  search() {
    this.getAllPronunciations(this.page);
  }

  refresh() {
    this.keyword = '';
    this.getAllPronunciations(this.page);
  }

}
