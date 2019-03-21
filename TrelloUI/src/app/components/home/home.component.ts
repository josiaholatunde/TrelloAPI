import { ActivatedRoute, Router } from '@angular/router';
import { Component, OnInit, Input, OnDestroy, OnChanges } from '@angular/core';
import { BookingSubjectService } from '../../services/booking-subject.service';
import { BookingSubjectType } from '../../models/booking-subject-type';
import { BookingSubject } from '../../models/booking-subject';
import { NgxSpinnerService } from 'ngx-spinner';
import { UserService } from 'src/app/services/user.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})
export class HomeComponent implements OnInit, OnChanges {

  bookingType = BookingSubjectType.Hotel;
  bookingSubject: BookingSubject[];
  currentBookingSubject: BookingSubject;
  showMessage = false;

  constructor(private bookingService: BookingSubjectService, private userService: UserService, private spinner: NgxSpinnerService,
     private router: Router, private route: ActivatedRoute) {
    this.bookingSubject = [];
  }

  ngOnInit(): void {
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
        this.spinner.show();
        this.loadBookingSubjects();
      }
      if (this.userService.isUserLoggedIn()) {
        this.userService.changeLoggedInStatus(true);
       }
     });
     this.bookingService.searchParamsTypeObservable.subscribe($event => {
       this.showMessage = false;
        this.spinner.show();
        this.loadBookingSubjects($event);
     });
     this.bookingService.defaultSubject.subscribe(bk => {
      this.currentBookingSubject = bk;
     });
  }

  ngOnChanges(changes: import('@angular/core').SimpleChanges): void {
    this.bookingService.defaultSubject.subscribe(sub => {
      if (sub) {
        this.currentBookingSubject = sub;
      }
    });
  }

  loadBookingSubjects($event?: any) {
    // const test = this.getBookingTypeFromRoute();
     this.bookingService.getBookingSubjects(this.bookingType, $event).subscribe((res: BookingSubject[]) => {
       this.spinner.hide();
       this.bookingSubject = res;
       this.currentBookingSubject = res[0];
       if (this.bookingSubject.length === 0) {
         this.showMessage = true;
       } else {
        this.showMessage = false;
       }
     });
   }

  updateIndividualBookingSubject($event: any) {
    this.currentBookingSubject = $event;
  }

}
