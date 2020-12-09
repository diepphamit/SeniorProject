export class FlashcardForCreateByChatbot {
  public data: string;
  public userId: number;

  constructor(data?: string, userId?: number) {
    this.data = data;
    this.userId = userId;
  }
}
