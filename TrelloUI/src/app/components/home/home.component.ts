import { ActivatedRoute, Router } from '@angular/router';
import { Component, OnInit, Input, OnDestroy } from '@angular/core';
import { BookingSubjectService } from '../../services/booking-subject.service';
import { BookingSubjectType } from '../../models/booking-subject-type';
import { BookingSubject } from '../../models/booking-subject';
import { FormGroup, FormBuilder } from '@angular/forms';
import { UserService } from 'src/app/services/user.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})
export class HomeComponent implements OnInit, OnDestroy {
  bookingType = BookingSubjectType.Hotel;
  bookingSubject: BookingSubject[];
  currentBookingSubject: BookingSubject;

  constructor(private bookingService: BookingSubjectService, private userService: UserService,
     private router: Router, private route: ActivatedRoute) {
    this.bookingSubject = [];
  }

  ngOnDestroy(): void {
    // throw new Error("Method not implemented.");
  }
  ngOnInit(): void {
    // throw new Error("Method not implemented.");
    this.route.params.subscribe(param => {
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
       this.bookingService.changeBookingType(this.bookingType);
       if (this.router.url.endsWith('true')) {
         this.userService.changeLoggedInStatus(true);
       }
        this.loadBookingSubjects();
      }
      if (this.userService.isUserLoggedIn()) {
        this.userService.changeLoggedInStatus(true);
       }
     });
     this.bookingService.searchParamsTypeObservable.subscribe($event => {
        this.loadBookingSubjects($event);
     });
  }

  loadBookingSubjects($event?: any) {
    // const test = this.getBookingTypeFromRoute();
     console.log('Typeee', this.bookingType);
     this.bookingService.getBookingSubjects(this.bookingType, $event).subscribe((res: BookingSubject[]) => {
       this.bookingSubject = res;
       console.log('Res', res);
       this.currentBookingSubject = res[0];
     });
   }

  updateIndividualBookingSubject($event: any) {
    this.currentBookingSubject = $event;
  }

}
