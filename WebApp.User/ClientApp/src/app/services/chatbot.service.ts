import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable()
export class ChatbotService {
  baseUrl = 'http://localhost:8000/chatbot';

  constructor(private http: HttpClient) {
  }

  createChatbot(chatbot: any) {
    return this.http.post(this.baseUrl, chatbot);
  }
}
