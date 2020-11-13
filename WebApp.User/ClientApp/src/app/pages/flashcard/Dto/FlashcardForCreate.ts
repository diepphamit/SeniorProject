export class FlashcardForCreate {
  public file: File;
  public userId: number;

  constructor(file?: File, userId?: number) {
    this.file = file;
    this.userId = userId;
  }
}
