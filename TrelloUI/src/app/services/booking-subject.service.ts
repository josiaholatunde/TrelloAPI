import { BookingSubject } from './../models/booking-subject';
import { Injectable } from '@angular/core';
import { HttpClient, HttpParams, HttpHeaders } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { BookingSubjectType } from '../models/booking-subject-type';
import { Observable, BehaviorSubject, Subject } from 'rxjs';
import { Comment } from '../models/Comment';

const httpOptions = {
  headers: new HttpHeaders({
    'Content-Type': 'application/json'
  })
};

@Injectable()
export class BookingSubjectService {

  private baseUrl = `${environment.apiUrl}/bookings`;
  defaultBookingType = new BehaviorSubject(BookingSubjectType.Hotel);
  defaultSubject = new Subject<BookingSubject>();
  defaultBookingTypeObservable = this.defaultBookingType.asObservable();
  searchParamsType = new BehaviorSubject('');
  searchParamsTypeObservable = this.searchParamsType.asObservable();
  shouldDisplayNavBar = new Subject<boolean>();
  shouldDisplayModal = new Subject<boolean>();
  bookingType: any;
  constructor(private http: HttpClient) { }

  displayNavBar(value: boolean) {
    this.shouldDisplayNavBar.next(value);
  }
  displayModal(value: boolean) {
    this.shouldDisplayModal.next(value);
  }
  changeBookingSubject(bookingSubject: BookingSubject) {
    this.defaultSubject.next(bookingSubject);
  }
  changeBookingType(bookingType: BookingSubjectType) {
   this.defaultBookingType.next(bookingType);
  }
  changeSearchParams(searchParams: string) {
    this.searchParamsType.next(searchParams);
   }

  getBookingSubjects(bookingType: BookingSubjectType, searchParams?: any): Observable<any[]> {
    let params = new HttpParams();
    if (searchParams) {
      params = params.append('nameOrLocation', searchParams);
    }
    if (bookingType != null) {
      params = params.append('bookingType', bookingType.toString());
    }
   return this.http.get<any[]>(this.baseUrl, { observe: 'body', params}).pipe();
  }
  createBooking(bookingDto: any) {
    return this.http.post<any[]>(this.baseUrl, bookingDto, httpOptions).pipe();
  }

  deleteBooking(bookingId: any, userId: number) {
    return this.http.delete<any[]>(`${this.baseUrl}/${bookingId}/${userId}`, httpOptions).pipe();
  }
  createComment(commentDto: any, bookingId: BookingSubjectType) {
    return this.http.post<any[]>(`${this.baseUrl}/${bookingId}/comments`, commentDto, httpOptions).pipe();
  }
  getMyBookings(userId: any) {
    return this.http.get<any[]>(`${this.baseUrl}/myBookings/${userId}`).pipe();
  }
  getBooking(bookingId: any) {
    return this.http.get<any[]>(`${this.baseUrl}/${bookingId}`).pipe();
  }
  getGalleryPhotos(bookingId: any) {
    return this.http.get<any[]>(`${this.baseUrl}/${bookingId}/photos`).pipe();
  }
  updateBooking(bookingForUpdateVm: any, id: number) {
    return this.http.put<any[]>(`${this.baseUrl}/${id}`, bookingForUpdateVm).pipe();
  }
  deleteGalleryPhoto(bookingId: any, userId: number, photoId: number) {
    return this.http.delete<any[]>(`${this.baseUrl}/${bookingId}/photos/${photoId}/${userId}`, httpOptions).pipe();
  }

  getComments(bookingId: number) {
    return this.http.get<Comment[]>(`${this.baseUrl}/${bookingId}/comments`).pipe();

  }

}
