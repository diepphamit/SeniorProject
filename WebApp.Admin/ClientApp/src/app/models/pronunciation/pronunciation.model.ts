export class Pronunciation {
  id: number;
  flashcardId: number;
  flashcardName: string;
  phonetic: string;
  link: string;

  constructor(id?: number, flashcardId?: number, flashcardName?: string,
    phonetic?: string, link?: string){
      this.id = id;
      this.flashcardId = flashcardId;
      this.flashcardName = flashcardName;
      this.phonetic = phonetic;
      this.link = link;
    }
}
