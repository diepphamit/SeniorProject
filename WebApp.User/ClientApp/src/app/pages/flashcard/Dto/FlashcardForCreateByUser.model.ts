export class FlashcardForCreateByUser {
  public file: File;
  public userId: number;
  public word: string;
  public meaning: string;
  public type: string;
  public example: string;
  public topicId: number;
  public phonetic: string;
  public pronunciationLink: string;

  constructor(file?: File, userId?: number,
    word?: string, meaning?: string,
    type?: string, example?: string,
    topicId?: number, phonetic?: string,
    pronunciationLink?: string) {
    this.file = file;
    this.userId = userId;
    this.word = word;
    this.meaning = meaning;
    this.type = type;
    this.example = example;
    this.topicId = topicId;
    this.phonetic = phonetic;
    this.pronunciationLink = pronunciationLink;
  }
}
