import { HttpErrorResponse } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { TopicForUpdate } from 'src/app/models/topic/topicForUpdate.model';
import { TopicService } from 'src/app/services/topic.service';

@Component({
  selector: 'app-edit-topic',
  templateUrl: './edit-topic.component.html'
})
export class EditTopicComponent implements OnInit {

  editTopicForm: FormGroup;
  topic: TopicForUpdate;
  id: any;

  constructor(
    private fb: FormBuilder,
    private router: Router,
    private route: ActivatedRoute,
    private topicService: TopicService,
    private toastr: ToastrService,
    // private authService: AuthService
  ) {
    this.editTopicForm = this.fb.group({
      name: ['', Validators.required]
    });
  }

  ngOnInit() {
    // if (this.authService.getRoles().filter(x => x.includes('UPDATE_BRANCH')).length === 0) {
    //   this.router.navigate(['/topics']);
    // }

    this.route.params.subscribe(params => {
      this.id = params.id;
      if (this.id) {
        this.topicService.getTopicById(this.id).subscribe(
          result => {
            this.topic = result;
            this.editTopicForm.controls.name.setValue(result.name);
          },
          () => {
            this.toastr.error('Không tìm thấy topic này');
          });
      }
    });
  }

  editTopic() {
    this.topic = Object.assign({}, this.editTopicForm.value);

    this.topicService.editTopic(this.id, this.topic).subscribe(
      () => {
        this.router.navigate(['/topics']).then(() => {
          this.toastr.success('Cập nhật topic thành công');
        });
      },
      (error: HttpErrorResponse) => {
        this.toastr.error('Cập nhật topic không thành công!');
      }
    );
  }

  get f() { return this.editTopicForm.controls; }

}
