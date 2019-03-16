import { BookingSubject } from './../models/booking-subject';
import { Injectable } from '@angular/core';
import { Resolve, ActivatedRouteSnapshot } from '@angular/router';
import { BookingSubjectService } from '../services/booking-subject.service';
import { BookingSubjectType } from '../models/booking-subject-type';
import { catchError } from 'rxjs/operators';
import { of, Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class BookingSubjectResolver implements Resolve<BookingSubject[]> {
  bookingType = BookingSubjectType.Hotel;
  constructor(private bookingService: BookingSubjectService) {}

  resolve(route: ActivatedRouteSnapshot): Observable<BookingSubject[]> {
    return this.bookingService.getBookingSubjects(this.bookingType).pipe(
      catchError(err => {
        return of(null);
      })
    );
  }
}
