import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable()
export class TestService {
  baseUrl = 'https://localhost:5001/api/' + 'Test';

  constructor(private http: HttpClient) {
  }

  getTests(keyword: string, page: number, pageSize: number): Observable<any> {
    return this.http.get(`${this.baseUrl}/GetTest?keyword=${keyword}&page=${page}&pageSize=${pageSize}`);
  }

  // getAllTests(keyword: string, page: number, pageSize: number): Observable<any> {
  //   return this.http.get(`${this.baseUrl}?keyword=${keyword}&page=${page}&pageSize=${pageSize}`);
  // }

  // getTestById(id: any): Observable<any> {
  //   return this.http.get(`${this.baseUrl}/${id}`);
  // }

  createTest(test: any) {
    return this.http.post('https://localhost:5001/api/Test', test);
  }

  // editTest(id: any, test: any) {
  //   return this.http.put(`${this.baseUrl}/${id}`, test);
  // }

  // deleteTest(id: any) {
  //   return this.http.delete(`${this.baseUrl}/${id}`);
  // }
}
