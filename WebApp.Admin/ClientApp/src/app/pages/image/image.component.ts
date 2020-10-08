import { HttpErrorResponse } from '@angular/common/http';
import { Component, OnInit, TemplateRef } from '@angular/core';
import { Router } from '@angular/router';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { ToastrService } from 'ngx-toastr';
import { Observable } from 'rxjs';
import { map, tap } from 'rxjs/operators';
import { Image } from 'src/app/models/image/image.model';
import { ImageService } from 'src/app/services/image.service';

@Component({
  selector: 'app-image',
  templateUrl: './image.component.html'
})
export class ImageComponent implements OnInit {

  image: Image;
  keyword: string;
  itemsAsync: Observable<any[]>;
  modalRef: BsModalRef;
  page: number;
  pageSize: number;
  total: number;

  constructor(
    public imageService: ImageService,
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
    this.getAllImages(this.page);
  }

  getAllImages(page: number) {
    this.itemsAsync = this.imageService.getAllImages(this.keyword, page, this.pageSize)
    .pipe(
      tap(response => {
        this.total = response.total;
        this.page = page;
      }),
      map(response => response.items)
    );
  }

  add() {
    this.router.navigate(['/images/add']);
  }

  edit(id: any) {
    this.router.navigate(['/images/edit/' + id]);
  }

  deleteConfirm(template: TemplateRef<any>, data: any) {
    this.image = Object.assign({}, data);
    this.modalRef = this.modalService.show(template);
  }

  confirm(): void {
    if (this.image) {
      this.imageService.deleteImage(this.image.id)
        .subscribe(
          () => {
            this.getAllImages(this.page);
            this.toastr.success(`Xóa image thành công`);
          },
          (error: HttpErrorResponse) => {
            this.toastr.error(`Xóa Image không thành công!`);
          }
        );
    }
    this.image = undefined;
    this.modalRef.hide();
  }

  close(): void {
    this.image = undefined;
    this.modalRef.hide();
  }

  search() {
    this.getAllImages(this.page);
  }

  refresh() {
    this.keyword = '';
    this.getAllImages(this.page);
  }

}
