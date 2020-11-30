export class UserFlashcardForCreate {
  public flashcardId: number;
  public userId: number;

  constructor(flashcardId?: number, userId?: number) {
    this.flashcardId = flashcardId;
    this.userId = userId;
  }
}
