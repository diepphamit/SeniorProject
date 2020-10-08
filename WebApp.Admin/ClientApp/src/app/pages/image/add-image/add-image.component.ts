import { HttpErrorResponse } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { FileSystemDirectoryEntry, FileSystemFileEntry, NgxFileDropEntry } from 'ngx-file-drop';
import { LoadingBarService } from 'ngx-loading-bar/LoadingBarService';
import { ToastrService } from 'ngx-toastr';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { ImageForCreate } from 'src/app/models/image/imageForCreate.model';
import { AuthService } from 'src/app/services/auth.service';
import { FlashcardService } from 'src/app/services/flashcard.service';
import { ImageService } from 'src/app/services/image.service';

@Component({
  selector: 'app-add-image',
  templateUrl: './add-image.component.html'
})
export class AddImageComponent implements OnInit {

  addImageForm: FormGroup;
  image: ImageForCreate;

  upload: ImageForCreate = new ImageForCreate;
  public files: NgxFileDropEntry[] = [];

  flashcards: Observable<any[]>;
  constructor(
    private fb: FormBuilder,
    private router: Router,
    private imageService: ImageService,
    private flashcardService: FlashcardService,
    private toastr: ToastrService,
    // private authService: AuthService
  ) {
    this.addImageForm = this.fb.group({
      // imageUrl: ['', Validators.required],
      flashcardId: ['', Validators.required]
    });
  }

  ngOnInit() {
    // if (this.authService.getRoles().filter(x => x.includes('CREATE_PICTURE')).length === 0) {
    //   this.router.navigate(['/images']);
    // }

    this.getAllFlashcards();
  }

  getAllFlashcards() {
    this.flashcards = this.flashcardService.getAllFlashcards('', 1, 1000).pipe(
      map(response => response.items)
    );
  }

  addImage() {
    // this.upload = Object.assign({}, this.addImageForm.value);
    // this.image.flashcardId = Number(this.image.flashcardId);
    this.upload.flashcardId = Number(this.addImageForm.value.flashcardId);
    console.log(this.upload);
    this.imageService.createImage(this.upload).subscribe(
      () => {
        this.router.navigate(['/images']).then(() => {
          this.toastr.success('Tạo hình ảnh thành công');
        });
      },
      (error: HttpErrorResponse) => {
        this.toastr.error('Tạo hình ảnh không thành công!');
      }
    );
  }

  get f() { return this.addImageForm.controls; }

  public dropped(files: NgxFileDropEntry[]) {
    this.files = files;
    for (const droppedFile of files) {

      // Is it a file?
      if (droppedFile.fileEntry.isFile) {
        const fileEntry = droppedFile.fileEntry as FileSystemFileEntry;
        fileEntry.file((file: File) => {
           this.upload.file = file;
        });
      } else {
        const fileEntry = droppedFile.fileEntry as FileSystemDirectoryEntry;
        console.log(droppedFile.relativePath, fileEntry);
      }
    }


  }

  public fileOver(event) {
    console.log(event);
  }

  public fileLeave(event) {
    console.log(event);
  }

}
