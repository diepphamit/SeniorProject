import { HttpErrorResponse } from '@angular/common/http';
import { Component, OnInit, TemplateRef } from '@angular/core';
import { Router } from '@angular/router';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { ToastrService } from 'ngx-toastr';
import { Observable } from 'rxjs';
import { map, tap } from 'rxjs/operators';
import { Topic } from 'src/app/models/topic/topic.model';
import { TopicService } from 'src/app/services/topic.service';

@Component({
  selector: 'app-topic',
  templateUrl: './topic.component.html'
})
export class TopicComponent implements OnInit {

  topic: Topic;
  keyword: string;
  itemsAsync: Observable<any[]>;
  modalRef: BsModalRef;
  page: number;
  pageSize: number;
  total: number;

  constructor(
    public topicService: TopicService,
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
    this.getAllTopics(this.page);
  }

  getAllTopics(page: number) {
    this.itemsAsync = this.topicService.getAllTopics(this.keyword, page, this.pageSize)
    .pipe(
      tap(response => {
        this.total = response.total;
        this.page = page;
      }),
      map(response => response.items)
    );
  }

  add() {
    this.router.navigate(['/topics/add']);
  }

  edit(id: any) {
    this.router.navigate(['/topics/edit/' + id]);
  }

  deleteConfirm(template: TemplateRef<any>, data: any) {
    this.topic = Object.assign({}, data);
    this.modalRef = this.modalService.show(template);
  }

  confirm(): void {
    if (this.topic) {
      this.topicService.deleteTopic(this.topic.id)
        .subscribe(
          () => {
            this.getAllTopics(this.page);
            this.toastr.success(`Xóa topic thành công`);
          },
          (error: HttpErrorResponse) => {
            this.toastr.error(`Xóa Topic không thành công!`);
          }
        );
    }
    this.topic = undefined;
    this.modalRef.hide();
  }

  close(): void {
    this.topic = undefined;
    this.modalRef.hide();
  }

  search() {
    this.getAllTopics(this.page);
  }

  refresh() {
    this.keyword = '';
    this.getAllTopics(this.page);
  }
}
