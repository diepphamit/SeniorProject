import { HttpErrorResponse } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { FlashcardForEdit } from 'src/app/models/flashcard/flashcardForEdit.model';
import { FlashcardService } from 'src/app/services/flashcard.service';
import { TopicService } from 'src/app/services/topic.service';

@Component({
  selector: 'app-edit-flashcard',
  templateUrl: './edit-flashcard.component.html'
})
export class EditFlashcardComponent implements OnInit {

  editFlashcardForm: FormGroup;
  flashcard: FlashcardForEdit;
  id: any;
  topics: Observable<any[]>;

  constructor(
    private fb: FormBuilder,
    private router: Router,
    private route: ActivatedRoute,
    private flashcardService: FlashcardService,
    private topicService: TopicService,
    private toastr: ToastrService,
    // private authService: AuthService
  ) {
    this.editFlashcardForm = this.fb.group({
      word: ['', Validators.required],
      meaning: ['', Validators.required],
      type: ['', Validators.required],
      example: ['', Validators.required],
      isSystem: [true, Validators.required],
      topicId: [1, Validators.required]
    });
  }

  ngOnInit() {
    // if (this.authService.getRoles().filter(x => x.includes('UPDATE_BRANCH')).length === 0) {
    //   this.router.navigate(['/flashcards']);
    // }
    this.getAllTopics();
    this.route.params.subscribe(params => {
      this.id = params.id;
      if (this.id) {
        this.flashcardService.getFlashcardById(this.id).subscribe(
          result => {
            this.flashcard = result;
            this.editFlashcardForm.controls.word.setValue(result.word);
            this.editFlashcardForm.controls.meaning.setValue(result.meaning);
            this.editFlashcardForm.controls.type.setValue(result.type);
            this.editFlashcardForm.controls.example.setValue(result.example);
            this.editFlashcardForm.controls.topicId.setValue(result.topicId);
          },
          () => {
            this.toastr.error('Không tìm thấy flashcard này');
          });
      }
    });
  }

  getAllTopics() {
    this.topics = this.topicService.getAllTopics('', 1, 1000)
    .pipe(
      map(response => response.items)
    );
  }

  editFlashcard() {
    this.flashcard = Object.assign({}, this.editFlashcardForm.value);
    this.flashcard.topicId = Number(this.flashcard.topicId);
    this.flashcardService.editFlashcard(this.id, this.flashcard).subscribe(
      () => {
        this.router.navigate(['/flashcards']).then(() => {
          this.toastr.success('Cập nhật flashcard thành công');
        });
      },
      (error: HttpErrorResponse) => {
        this.toastr.error('Cập nhật flashcard không thành công!');
      }
    );
  }

  get f() { return this.editFlashcardForm.controls; }
}
