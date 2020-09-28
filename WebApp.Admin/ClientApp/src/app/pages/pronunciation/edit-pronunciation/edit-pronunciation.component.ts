import { HttpErrorResponse } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { PronunciationForUpdate } from 'src/app/models/pronunciation/pronunciationForUpdate.model';
import { PronunciationService } from 'src/app/services/pronunciation.service';

@Component({
  selector: 'app-edit-pronunciation',
  templateUrl: './edit-pronunciation.component.html'
})
export class EditPronunciationComponent implements OnInit {

  editPronunciationForm: FormGroup;
  pronunciation: PronunciationForUpdate;
  id: any;

  constructor(
    private fb: FormBuilder,
    private router: Router,
    private route: ActivatedRoute,
    private pronunciationService: PronunciationService,
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

  editPronunciation() {
    this.pronunciation = Object.assign({}, this.editPronunciationForm.value);

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
