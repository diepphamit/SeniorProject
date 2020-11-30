import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable()
export class FlashcardService {
  baseUrl = environment.apiUrl + 'flashcard';

  constructor(private http: HttpClient) {
  }

  getAllFlashcards(keyword: string, page: number, pageSize: number): Observable<any> {
    return this.http.get(`${this.baseUrl}?keyword=${keyword}&page=${page}&pageSize=${pageSize}`);
  }

  getFlashcardById(id: any): Observable<any> {
    return this.http.get(`${this.baseUrl}/${id}`);
  }

  getPopularFlashcards(): Observable<any> {
    return this.http.get(`${this.baseUrl}/GetPopularFlashcards`);
  }

  getFlashcardHome(): Observable<any> {
    return this.http.get(`${this.baseUrl}/GetFlashcardHome`);
  }

  getFlashcardsByTopicId(topicId: number, userId: number, page: number, pageSize: number): Observable<any> {
    return this.http.get(`${this.baseUrl}/GetFlashcardsByTopicId?topicId=${topicId}&userId=${userId}&page=${page}&pageSize=${pageSize}`);
  }

  getFlashcardsByUserId(userId: number, page: number, pageSize: number): Observable<any> {
    return this.http.get(`${this.baseUrl}/GetFlashcardsByUserId?userId=${userId}&page=${page}&pageSize=${pageSize}`);
  }

  createFlashcardByAI(flashcard) {
    const formData = new FormData();
    formData.append('image', flashcard.file, flashcard.file.name);
    formData.append('userId', flashcard.userId);
    // return this.http.post(`${this.baseUrl}/CreateFlashcardAI`, formData);
    return this.http.post('http://localhost:5002/api/flashcard/CreateFlashcardAI', formData);
  }

  createFlashcardByUser(flashcard) {
    const formData = new FormData();
    formData.append('file', flashcard.file, flashcard.file.name);
    formData.append('userId', flashcard.userId);
    formData.append('word', flashcard.word);
    formData.append('meaning', flashcard.meaning);
    formData.append('topicId', flashcard.topicId);
    formData.append('phonetic', flashcard.phonetic);
    formData.append('example', flashcard.example);
    formData.append('type', flashcard.type);
    formData.append('pronunciationLink', flashcard.pronunciationLink);
    return this.http.post(`${this.baseUrl}/CreateFlashcardByUserId`, formData);
  }

  // createFlashcard(flashcard: any) {
  //   return this.http.post(this.baseUrl, flashcard);
  // }

  // editFlashcard(id: any, flashcard: any) {
  //   return this.http.put(`${this.baseUrl}/${id}`, flashcard);
  // }

  // deleteFlashcard(id: any) {
  //   return this.http.delete(`${this.baseUrl}/${id}`);
  // }
}
