import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { Observable } from 'rxjs';
import { map, tap } from 'rxjs/operators';
import { TestService } from 'src/app/services/test.service';

@Component({
  selector: 'app-detail-test',
  templateUrl: './detail-test.component.html',
  styleUrls: ['./detail-test.component.css']
})
export class DetailTestComponent implements OnInit {

  test: any;
  id: any;

  constructor(private testService: TestService, private route: ActivatedRoute) {
  }

  ngOnInit() {
    this.route.params.subscribe(params => {
      this.id = params.id;
      if (this.id) {
        // this.topicService.getTopicById(this.id).subscribe(
        //   result => {
        //     this.topic = result;
        //     this.editTopicForm.controls.name.setValue(result.name);
        //   },
        //   () => {
        //     this.toastr.error('Không tìm thấy topic này');
        //   });
        this.getTestDetail(this.id);
      }
    });
  }

  getTestDetail(id: any) {
    this.testService.getTestDetailById(id).subscribe(data => this.test = data);
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
