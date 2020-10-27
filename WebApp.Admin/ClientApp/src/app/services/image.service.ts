import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable()
export class ImageService {
  baseUrl = 'https://localhost:44386/api/' + 'image';

  constructor(private http: HttpClient) {
  }

  getAllImages(keyword: string, page: number, pageSize: number): Observable<any> {
    return this.http.get(`${this.baseUrl}?keyword=${keyword}&page=${page}&pageSize=${pageSize}`);
  }

  getImageById(id: any): Observable<any> {
    return this.http.get(`${this.baseUrl}/${id}`);
  }

  createImage(image: any) {
    const formData = new FormData();
    formData.append('file', image.file, image.file.name);
    formData.append('flashcardId', image.flashcardId);

    return this.http.post(this.baseUrl, formData);
  }

  editImage(id: any, image: any) {
    return this.http.put(`${this.baseUrl}/${id}`, image);
  }

  deleteImage(id: any) {
    return this.http.delete(`${this.baseUrl}/${id}`);
  }
}
