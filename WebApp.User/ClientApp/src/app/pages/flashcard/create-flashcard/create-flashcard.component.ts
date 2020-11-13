import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { FileSystemDirectoryEntry, FileSystemFileEntry, NgxFileDropEntry } from 'ngx-file-drop';
import { CURRENT_USER } from 'src/app/constants/db-keys';
import { FlashcardService } from 'src/app/services/flashcard.service';
import { FlashcardForCreate } from '../Dto/FlashcardForCreate';
import { FlashcardForCreateByUser } from '../Dto/FlashcardForCreateByUser.model';

@Component({
  selector: 'app-create-flashcard',
  templateUrl: './create-flashcard.component.html',
  styleUrls: ['./create-flashcard.component.css']
})
export class CreateFlashcardComponent implements OnInit {

  public files: NgxFileDropEntry[] = [];
  public fileUpload: any;
  public flashcard: FlashcardForCreate = new FlashcardForCreate();
  public flashcardForUser: FlashcardForCreateByUser = new FlashcardForCreateByUser();
  createFlashcardForm: FormGroup;

  constructor(private flashcardService: FlashcardService, private fb: FormBuilder) { }

  ngOnInit() {
    this.createFlashcardForm = this.fb.group({
      word: ['', Validators.required],
      meaning: ['', Validators.required],
      type: ['', Validators.required],
      example: ['', Validators.required],
      topicId: [1, Validators.required],
      phonetic: ['', Validators.required],
      pronunciationLink: [1, Validators.required],
    });
  }

  get getuserId() {
    const user = JSON.parse(localStorage.getItem(CURRENT_USER));

    return user.id;
  }

  createFlashcardByuserId() {
    this.flashcardForUser =  Object.assign({}, this.createFlashcardForm.value);
    this.flashcardForUser.file = this.fileUpload;
    this.flashcardForUser.userId = this.getuserId;

    this.flashcardService.createFlashcardByUser(this.flashcardForUser).subscribe(data => console.log(data));
  }



  public dropped(files: NgxFileDropEntry[]) {
    this.files = files;
    for (const droppedFile of files) {

      // Is it a file?
      if (droppedFile.fileEntry.isFile) {
        const fileEntry = droppedFile.fileEntry as FileSystemFileEntry;
        fileEntry.file((file: File) => {
          //  this.upload.file = file;
          this.flashcard.file = file;
          this.flashcard.userId = Number(this.getuserId);
          this.flashcardService.createFlashcardByAI(this.flashcard).subscribe(data => console.log(data));

        });
      } else {
        const fileEntry = droppedFile.fileEntry as FileSystemDirectoryEntry;
        console.log(droppedFile.relativePath, fileEntry);
      }
    }
  }

  public dropped1(files: NgxFileDropEntry[]) {
    this.files = files;
    for (const droppedFile of files) {

      // Is it a file?
      if (droppedFile.fileEntry.isFile) {
        const fileEntry = droppedFile.fileEntry as FileSystemFileEntry;
        fileEntry.file((file: File) => {
          //  this.upload.file = file;
           this.fileUpload = file;
          //  console.log(this.flashcardForUser);
          // this.flashcard.userId = Number(this.getuserId);
          // this.flashcardService.createFlashcardByAI(this.flashcard).subscribe(data => console.log(data));

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
