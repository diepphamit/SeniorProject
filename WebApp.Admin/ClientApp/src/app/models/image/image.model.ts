export class Image {
  public id: number;
  public imageUrl: string;
  public flashcardId: number;
  public flashcardName: string;

  constructor(id?: number, imageUrl?: string, flashcardId?: number, flashcardName?: string) {
    this.id = id;
    this.imageUrl = imageUrl;
    this.flashcardId = flashcardId;
    this.flashcardName = flashcardName;
  }
}
