import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable()
export class ChatbotService {
  baseUrl =  environment.apiUrl + 'chatbot';

  constructor(private http: HttpClient) {
  }

  createChatbot(chatbot: any) {
    return this.http.post('http://localhost:5002/api/chatbot', chatbot);
  }

  createFlashcardByChatbot(flashcardForCreateDB: any) {
    return this.http.post('http://localhost:5002/api/Flashcard/CreateFlashcardByChatBot', flashcardForCreateDB);
  }
}
