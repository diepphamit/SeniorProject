export class FlashcardForEdit {
  word: string;
  meaning: string;
  type: string;
  example: string;
  isSystem: boolean;
  topicId: number;

  constructor(word?: string, meaning?: string, type?: string,
    example?: string, isSystem?: boolean, topicId?: number) {
      this.word = word;
      this.meaning = meaning;
      this.type = type;
      this.example = example;
      this.isSystem = isSystem;
      this.topicId = topicId;
    }
}
