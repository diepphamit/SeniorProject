import { HttpErrorResponse } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { PronunciationForUpdate } from 'src/app/models/pronunciation/pronunciationForUpdate.model';
import { FlashcardService } from 'src/app/services/flashcard.service';
import { PronunciationService } from 'src/app/services/pronunciation.service';

@Component({
  selector: 'app-edit-pronunciation',
  templateUrl: './edit-pronunciation.component.html'
})
export class EditPronunciationComponent implements OnInit {

  editPronunciationForm: FormGroup;
  pronunciation: PronunciationForUpdate;
  id: any;
  flashcards: Observable<any[]>;

  constructor(
    private fb: FormBuilder,
    private router: Router,
    private route: ActivatedRoute,
    private pronunciationService: PronunciationService,
    private flascardService: FlashcardService,
    private toastr: ToastrService,
    // private authService: AuthService
  ) {
    this.editPronunciationForm = this.fb.group({
      flashcardId: [Validators.required],
      phonetic: ['', Validators.required],
      link: ['', Validators.required]
    });
  }

  ngOnInit() {
    // if (this.authService.getRoles().filter(x => x.includes('UPDATE_BRANCH')).length === 0) {
    //   this.router.navigate(['/pronunciations']);
    // }
    this.getAllFlashcards();
    this.route.params.subscribe(params => {
      this.id = params.id;
      if (this.id) {
        this.pronunciationService.getPronunciationById(this.id).subscribe(
          result => {
            this.pronunciation = result;
            this.editPronunciationForm.controls.flashcardId.setValue(result.flashcardId);
            this.editPronunciationForm.controls.phonetic.setValue(result.phonetic);
            this.editPronunciationForm.controls.link.setValue(result.link);
          },
          () => {
            this.toastr.error('Không tìm thấy pronunciation này');
          });
      }
    });
  }

  getAllFlashcards() {
    this.flashcards = this.flascardService.getAllFlashcards('', 1, 1000)
    .pipe(
      map(response => response.items)
    );
  }

  editPronunciation() {
    this.pronunciation = Object.assign({}, this.editPronunciationForm.value);
    this.pronunciation.flashcardId = Number(this.pronunciation.flashcardId);
    this.pronunciationService.editPronunciation(this.id, this.pronunciation).subscribe(
      () => {
        this.router.navigate(['/pronunciations']).then(() => {
          this.toastr.success('Cập nhật pronunciation thành công');
        });
      },
      (error: HttpErrorResponse) => {
        this.toastr.error('Cập nhật pronunciation không thành công!');
      }
    );
  }

  get f() { return this.editPronunciationForm.controls; }

}
