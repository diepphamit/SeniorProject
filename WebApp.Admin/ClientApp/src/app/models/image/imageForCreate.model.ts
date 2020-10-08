export class ImageForCreate {
  public file: File;
  public flashcardId: number;

  constructor(file?: File, flashcardId?: number) {
    this.file = file;
    this.flashcardId = flashcardId;
  }
}
