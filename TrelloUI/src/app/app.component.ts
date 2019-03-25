import { Component, OnInit, OnDestroy } from '@angular/core';
import { BookingSubjectService } from './services/booking-subject.service';
import { BookingSubjectType } from './models/booking-subject-type';
import { BookingSubject } from './models/booking-subject';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit, OnDestroy {
  isLoggedInStatus = false;
  passOn: any;
  bookingType = BookingSubjectType.Hotel;
  isLoginClicked = false;
  isRegisterClicked = false;
  isModalClicked = false;
  display = false;
  routeParams: { display: string; value: string; imgIcon: string; }[];
  book: any;
  bookingSubject: BookingSubject[];
  currentBookingSubject: BookingSubject;
  constructor(private bookingService: BookingSubjectService, private route: ActivatedRoute,
    private router: Router) {
  }
  ngOnInit(): void {

    if (this.router.url === '/login') {
      this.isRegisterClicked = false;
      this.isLoginClicked = true;
    } else if (this.router.url === '/register') {
      this.isLoginClicked = false;
      this.isRegisterClicked = true;
    } else {
      this.isLoginClicked = false;
      this.isRegisterClicked = false;
      this.isLoggedInStatus = false;
    }
    this.routeParams = [
      {display: 'Hotel', value: BookingSubjectType[BookingSubjectType.Hotel], imgIcon: 'fa-homei'},
      {display: 'Flight', value: BookingSubjectType[BookingSubjectType.Flight], imgIcon: 'fa-fighter-jeti'},
      {display: 'Car rental', value: BookingSubjectType[BookingSubjectType.CarRental], imgIcon: 'fa-cari'},
      {display: 'Tours', value: BookingSubjectType[BookingSubjectType.Tour], imgIcon: 'fa-touri'},
    ];
  }





  updateBookingType($event) {
    this.bookingType = $event;
  }


  updateBookingSubject($event: any) {
    this.bookingSubject = $event;
    this.currentBookingSubject = this.bookingSubject[0];
  }
  updateBookingSubjectViaSearch($event) {
    this.currentBookingSubject = $event;
    this.bookingService.changeBookingSubject(this.currentBookingSubject);

  }
  loadBookingSubjects($event?: any) {
     this.bookingService.getBookingSubjects(this.bookingType, $event).subscribe((res: BookingSubject[]) => {
       this.bookingSubject = res;
       this.currentBookingSubject = res[0];
     });
   }

  getBookingSubject($event: any) {
    this.bookingService.changeSearchParams($event);
  }
  reset($event) {
    this.isLoginClicked = false;
    this.isRegisterClicked = false;
  }
  update ($event) {
    this.isLoginClicked = false;
    this.isRegisterClicked = false;
    this.isLoggedInStatus = true;
  }
  log($event) {
    this.passOn = $event;
  }
  openModal($event) {
    this.isModalClicked = true;
    this.display = true;
    this.bookingService.displayModal(true);
    this.bookingService.displayNavBar(false);

  }

  ngOnDestroy(): void {
    this.book.unsubscribe();
 }

}
