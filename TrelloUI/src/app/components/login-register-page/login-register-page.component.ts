import { ActivatedRoute } from '@angular/router';
import { Component, OnInit } from '@angular/core';
import { BookingSubjectService } from 'src/app/services/booking-subject.service';
import { BookingSubjectType } from 'src/app/models/booking-subject-type';
import { BookingSubject } from 'src/app/models/booking-subject';

@Component({
  selector: 'app-login-register-page',
  templateUrl: './login-register-page.component.html',
  styleUrls: ['./login-register-page.component.scss']
})
export class LoginRegisterPageComponent implements OnInit {

  bookingType = BookingSubjectType.Hotel;
  bookingSubject: BookingSubject[];
  currentBookingSubject: BookingSubject;
  constructor(private bookingService: BookingSubjectService, private route: ActivatedRoute) {
    this.bookingSubject = [];
  }

  ngOnInit() {
  }
  getBookingSubject($event: any) {
    this.loadBookingSubjects($event);
  }
  loadBookingSubjects($event?: any) {
    this.getBookingTypeFromRoute();
    this.bookingService.getBookingSubjects(this.bookingType, $event).subscribe((res: BookingSubject[]) => {
      this.bookingSubject = res;
      this.currentBookingSubject = res[0];
    });
  }

  getBookingTypeFromRoute() {
    this.route.paramMap.subscribe(param => {
      if (param['name']) {
       if (param['name'] === 'Hotel') {
         this.bookingType = BookingSubjectType.Hotel;
       } else  if (param['name'] === 'CarRental') {
         this.bookingType = BookingSubjectType.CarRental;
       } else  if (param['name'] === 'Tour') {
         this.bookingType = BookingSubjectType.Tour;
       }  else  if (param['name'] === 'Flight') {
         this.bookingType = BookingSubjectType.Flight;
       } else {
         this.bookingType = BookingSubjectType.Hotel;
       }
      }
     });
  }

  updateBookingSubject($event: any) {
    this.bookingSubject = $event;
    this.currentBookingSubject = this.bookingSubject[0];
  }

}
