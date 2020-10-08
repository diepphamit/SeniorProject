import { HttpErrorResponse } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { FlashcardForCreate } from 'src/app/models/flashcard/flashcardForCreate.model';
import { FlashcardService } from 'src/app/services/flashcard.service';
import { TopicService } from 'src/app/services/topic.service';

@Component({
  selector: 'app-add-flashcard',
  templateUrl: './add-flashcard.component.html'
})
export class AddFlashcardComponent implements OnInit {

  createFlashcardForm: FormGroup;
  flashcard: FlashcardForCreate;
  topics: Observable<any[]>;
  constructor(
    private fb: FormBuilder,
    private router: Router,
    private flashcardService: FlashcardService,
    private topicService: TopicService,
    private toastr: ToastrService,
    //private authService: AuthService
  ) {
    this.createFlashcardForm = this.fb.group({
      word: ['', Validators.required],
      meaning: ['', Validators.required],
      type: ['', Validators.required],
      example: ['', Validators.required],
      isSystem: [true, Validators.required],
      topicId: [1, Validators.required]
    });
  }

  ngOnInit() {
    // if (this.authService.getRoles().filter(x => x.includes('CREATE_BRANCH')).length === 0) {
    //   this.router.navigate(['/flashcards']);
    // }
    this.getAllTopics();
  }

  getAllTopics() {
    this.topics = this.topicService.getAllTopics('', 1, 1000)
      .pipe(
        map(response => response.items)
      );
  }

  addFlashcard() {
    this.flashcard = Object.assign({}, this.createFlashcardForm.value);
    this.flashcard.isSystem = true;
    this.flashcard.topicId = Number(this.flashcard.topicId);
    this.flashcardService.createFlashcard(this.flashcard).subscribe(
      () => {
        this.router.navigate(['/flashcards']).then(() => {
          this.toastr.success('Tạo flashcard thành công');
        });
      },
      (error: HttpErrorResponse) =>
        this.toastr.error('Tạo flashcard không thành công!')
      );
  }

  get f() { return this.createFlashcardForm.controls; }
}
