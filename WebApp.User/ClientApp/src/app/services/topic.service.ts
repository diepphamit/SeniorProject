import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable()
export class TopicService {
  baseUrl = 'https://localhost:44386/api/' + 'topic';

  constructor(private http: HttpClient) {
  }

  getAllTopics(keyword: string, page: number, pageSize: number): Observable<any> {
    return this.http.get(`${this.baseUrl}?keyword=${keyword}&page=${page}&pageSize=${pageSize}`);
  }

  getPopularTopics(keyword: string, page: number, pageSize: number): Observable<any> {
    return this.http.get(`${this.baseUrl}/GetPopularTopics?keyword=${keyword}&page=${page}&pageSize=${pageSize}`);
  }

  getTopicById(id: any): Observable<any> {
    return this.http.get(`${this.baseUrl}/${id}`);
  }

  // createTopic(topic: any) {
  //   return this.http.post(this.baseUrl, topic);
  // }

  // editTopic(id: any, topic: any) {
  //   return this.http.put(`${this.baseUrl}/${id}`, topic);
  // }

  // deleteTopic(id: any) {
  //   return this.http.delete(`${this.baseUrl}/${id}`);
  // }
}
