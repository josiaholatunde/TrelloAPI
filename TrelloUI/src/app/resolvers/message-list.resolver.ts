import { BookingSubject } from './../models/booking-subject';
import { Injectable } from '@angular/core';
import { Resolve, ActivatedRouteSnapshot, Router } from '@angular/router';
import { catchError } from 'rxjs/operators';
import { of, Observable } from 'rxjs';
import { Message } from '@angular/compiler/src/i18n/i18n_ast';
import { MessageService } from '../services/messages.service';
import { AlertifyService } from '../services/alertify.service';

@Injectable({
  providedIn: 'root'
})
export class MessageListResolver implements Resolve<Message[]> {
  pageNumber = 1;
  pageSize = 5;
  messageContainer = 'Unread';
  constructor(private messageService: MessageService, private alertify: AlertifyService, private router: Router) {}

  resolve(route: ActivatedRouteSnapshot): Observable<Message[]> {
    return this.messageService.getMessagesForUser(this.messageContainer, this.pageNumber, this.pageSize).pipe(
      catchError(err => {
        this.alertify.error('Problem retrieving messages');
        this.router.navigate(['bookings/home']);
        return of(null);
      })
    );
  }
}
