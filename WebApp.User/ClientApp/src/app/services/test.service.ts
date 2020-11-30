import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable()
export class TestService {
  baseUrl = environment.apiUrl + 'Test';

  constructor(private http: HttpClient) {
  }

  getTests(userId: any, page: number, pageSize: number): Observable<any> {
    return this.http.get(`${this.baseUrl}/GetTest?userId=${userId}&page=${page}&pageSize=${pageSize}`);
  }

  getHistories(userId: number, page: number, pageSize: number): Observable<any> {
    return this.http.get(`${this.baseUrl}/GetHistoryTestsByUserId?userId=${userId}&page=${page}&pageSize=${pageSize}`);
  }

  // getAllTests(keyword: string, page: number, pageSize: number): Observable<any> {
  //   return this.http.get(`${this.baseUrl}?keyword=${keyword}&page=${page}&pageSize=${pageSize}`);
  // }

  // getTestById(id: any): Observable<any> {
  //   return this.http.get(`${this.baseUrl}/${id}`);
  // }
  getTestDetailById(id: any): Observable<any> {
    return this.http.get(`${this.baseUrl}/getTestDetailById?id=${id}`);
  }

  createTest(test: any) {
    return this.http.post(`${this.baseUrl}`, test);
  }

  // editTest(id: any, test: any) {
  //   return this.http.put(`${this.baseUrl}/${id}`, test);
  // }

  // deleteTest(id: any) {
  //   return this.http.delete(`${this.baseUrl}/${id}`);
  // }
}
