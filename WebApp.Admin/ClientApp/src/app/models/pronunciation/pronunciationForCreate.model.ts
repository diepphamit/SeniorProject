export class PronunciationForCreate {
  flashcardId: number;
  phonetic: string;
  link: string;

  constructor(id?: number, flashcardId?: number,
    phonetic?: string, link?: string) {
      this.flashcardId = flashcardId;
      this.phonetic = phonetic;
      this.link = link;
    }
}
