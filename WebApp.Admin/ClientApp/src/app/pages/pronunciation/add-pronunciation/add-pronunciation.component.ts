import { HttpErrorResponse } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { PronunciationForCreate } from 'src/app/models/pronunciation/pronunciationForCreate.model';
import { FlashcardService } from 'src/app/services/flashcard.service';
import { PronunciationService } from 'src/app/services/pronunciation.service';

@Component({
  selector: 'app-add-pronunciation',
  templateUrl: './add-pronunciation.component.html'
})
export class AddPronunciationComponent implements OnInit {

  createPronunciationForm: FormGroup;
  pronunciation: PronunciationForCreate;
  flashcards: Observable<any[]>;
  constructor(
    private fb: FormBuilder,
    private router: Router,
    private pronunciationService: PronunciationService,
    private flascardService: FlashcardService,
    private toastr: ToastrService,
    //private authService: AuthService
  ) {
    this.createPronunciationForm = this.fb.group({
      flashcardId: [1, Validators.required],
      phonetic: ['', Validators.required],
      link: ['', Validators.required]
    });
  }

  ngOnInit() {
    // if (this.authService.getRoles().filter(x => x.includes('CREATE_BRANCH')).length === 0) {
    //   this.router.navigate(['/pronunciations']);
    // }
    this.getAllFlashcards();
  }

  getAllFlashcards() {
    this.flashcards = this.flascardService.getAllFlashcards('', 1, 1000)
    .pipe(
      map(response => response.items)
    );
  }

  addPronunciation() {
    this.pronunciation = Object.assign({}, this.createPronunciationForm.value);
    this.pronunciation.flashcardId = Number(this.pronunciation.flashcardId);
    this.pronunciationService.createPronunciation(this.pronunciation).subscribe(
      () => {
        this.router.navigate(['/pronunciations']).then(() => {
          this.toastr.success('Tạo pronunciation thành công');
        });
      },
      (error: HttpErrorResponse) =>
        this.toastr.error('Tạo pronunciation không thành công!')
      );
  }

  get f() { return this.createPronunciationForm.controls; }

}
