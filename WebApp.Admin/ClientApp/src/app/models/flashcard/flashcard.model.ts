export class Flashcard {
  id: number;
  word: string;
  meaning: string;
  type: string;
  example: string;
  isSystem: boolean;
  topicId: number;

  constructor(id?: number, word?: string, meaning?: string, type?: string,
    example?: string, isSystem?: boolean, topicId?: number) {
      this.id = id;
      this.word = word;
      this.meaning = meaning;
      this.type = type;
      this.example = example;
      this.isSystem = isSystem;
      this.topicId = topicId;
    }
}
