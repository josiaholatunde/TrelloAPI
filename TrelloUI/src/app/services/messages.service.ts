import { Message } from 'src/app/models/message';
import { PaginationResult, MessagePaginationResult } from './../models/pagination-result';
import { Injectable } from '@angular/core';
import { HttpClient, HttpParams, HttpHeaders } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { Observable } from 'rxjs';
import { UserService } from './user.service';
import { map } from 'rxjs/operators';

const httpOptions = {
  headers: new HttpHeaders({
    'Content-Type': 'application/json'
  })
};

@Injectable()
export class MessageService {
  private baseUrl = `${environment.apiUrl}`;
  bookingType: any;
  constructor(private http: HttpClient,private userService: UserService) { }

  getMessagesForUser(messageContainer?: string, pageNumber?: any, pageSize?: any): Observable<MessagePaginationResult> {
    const userId = this.userService.getLoggedInUser().id;
    const paginationResult = new MessagePaginationResult();
    let params = new HttpParams();
    if (messageContainer !== null) {
      params = params.append('messageContainer', messageContainer);
    }
    if (pageSize !== null && pageNumber !== null) {
      params = params.append('pageSize', pageSize);
      params = params.append('pageNumber', pageNumber);
    }
    return this.http.get<any[]>(`${this.baseUrl}/users/${userId}/message`, { observe: 'response', params})
        .pipe(
          map(res => {
            paginationResult.result = res.body;
            if (res.headers.get('Pagination') !== null) {
              paginationResult.pagination = JSON.parse(res.headers.get('Pagination'));
            }
            return paginationResult;
          })
        );
  }
  getMessageThread(recipientId: any) {
    const userId = this.userService.getLoggedInUser().id;
    return this.http.get<any[]>(`${this.baseUrl}/users/${userId}/message/thread/${recipientId}`).pipe();
  }

   sendMessage(messageToCreateDto: any) {
    const userId = this.userService.getLoggedInUser().id;
    return this.http.post<Message>(`${this.baseUrl}/users/${userId}/message`, messageToCreateDto).pipe();
  }
  getNotificationCount(): any {
    const userId = this.userService.getLoggedInUser().id;
    return this.http.get<any[]>(`${this.baseUrl}/users/${userId}/message/unread`).pipe();
  }

}
