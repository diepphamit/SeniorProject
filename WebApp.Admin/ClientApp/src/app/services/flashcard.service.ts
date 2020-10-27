import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable()
export class FlashcardService {
  baseUrl = 'https://localhost:44386/api/' + 'flashcard';

  constructor(private http: HttpClient) {
  }

  getAllFlashcards(keyword: string, page: number, pageSize: number): Observable<any> {
    return this.http.get(`${this.baseUrl}?keyword=${keyword}&page=${page}&pageSize=${pageSize}`);
  }

  getFlashcardById(id: any): Observable<any> {
    return this.http.get(`${this.baseUrl}/${id}`);
  }

  createFlashcard(flashcard: any) {
    return this.http.post(this.baseUrl, flashcard);
  }

  editFlashcard(id: any, flashcard: any) {
    return this.http.put(`${this.baseUrl}/${id}`, flashcard);
  }

  deleteFlashcard(id: any) {
    return this.http.delete(`${this.baseUrl}/${id}`);
  }
}
