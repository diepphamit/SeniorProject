import { HttpErrorResponse } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { TopicForCreate } from 'src/app/models/topic/topicForCreate.model';
import { TopicService } from 'src/app/services/topic.service';

@Component({
  selector: 'app-add-topic',
  templateUrl: './add-topic.component.html'
})
export class AddTopicComponent implements OnInit {

  createTopicForm: FormGroup;
  topic: TopicForCreate;
  constructor(
    private fb: FormBuilder,
    private router: Router,
    private topicService: TopicService,
    private toastr: ToastrService,
    //private authService: AuthService
  ) {
    this.createTopicForm = this.fb.group({
      name: ['', Validators.required]
    });
  }

  ngOnInit() {
    // if (this.authService.getRoles().filter(x => x.includes('CREATE_BRANCH')).length === 0) {
    //   this.router.navigate(['/topics']);
    // }
  }

  addTopic() {
    this.topic = Object.assign({}, this.createTopicForm.value);
    this.topicService.createTopic(this.topic).subscribe(
      () => {
        this.router.navigate(['/topics']).then(() => {
          this.toastr.success('Tạo topic thành công');
        });
      },
      (error: HttpErrorResponse) =>
        this.toastr.error('Tạo topic không thành công!')
      );
  }

  get f() { return this.createTopicForm.controls; }

}
