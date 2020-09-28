import { HttpErrorResponse } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { PronunciationForCreate } from 'src/app/models/pronunciation/pronunciationForCreate.model';
import { PronunciationService } from 'src/app/services/pronunciation.service';

@Component({
  selector: 'app-add-pronunciation',
  templateUrl: './add-pronunciation.component.html'
})
export class AddPronunciationComponent implements OnInit {

  createPronunciationForm: FormGroup;
  pronunciation: PronunciationForCreate;
  constructor(
    private fb: FormBuilder,
    private router: Router,
    private pronunciationService: PronunciationService,
    private toastr: ToastrService,
    //private authService: AuthService
  ) {
    this.createPronunciationForm = this.fb.group({
      flashcardId: [Validators.required],
      phonetic: ['', Validators.required],
      link: ['', Validators.required]
    });
  }

  ngOnInit() {
    // if (this.authService.getRoles().filter(x => x.includes('CREATE_BRANCH')).length === 0) {
    //   this.router.navigate(['/pronunciations']);
    // }
  }

  addPronunciation() {
    this.pronunciation = Object.assign({}, this.createPronunciationForm.value);
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
