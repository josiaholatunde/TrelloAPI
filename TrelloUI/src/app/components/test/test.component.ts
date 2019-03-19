import { BookingSubject } from 'src/app/models/booking-subject';
import { Component, OnInit } from '@angular/core';
import { BookingSubjectService } from 'src/app/services/booking-subject.service';
import { BookingSubjectType } from 'src/app/models/booking-subject-type';
import { AlertifyService } from 'src/app/services/alertify.service';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-test',
  templateUrl: './test.component.html',
  styleUrls: ['./test.component.scss']
})
export class TestComponent implements OnInit {

  cars: any[];
  searchParams: string;
  bookingSubjects: BookingSubject[];
  bookingType: any;
  currentBookingSubject: BookingSubject;
  constructor(private bookingService: BookingSubjectService, private alertify: AlertifyService,
    private route: ActivatedRoute) {
    this.route.url.subscribe(res => {
      this.bookingType = BookingSubjectType[res[1].path];
    });
   }

  ngOnInit() {
    this.loadBookingSubjects();

  }
  filterCountrySingle(event) {
    const query = event.query;
    // type from route params
    const bookingType = BookingSubjectType.Hotel;
    this.bookingService.getBookingSubjects(bookingType, query).subscribe((res: BookingSubject[]) => {
      this.bookingSubjects = res;
    });
  }
  selectCar() {
  this.alertify.success('i work');
  }

  loadBookingSubjects($event?: any) {
    // const test = this.getBookingTypeFromRoute();
     this.bookingService.getBookingSubjects(this.bookingType, $event).subscribe((res: BookingSubject[]) => {
       this.bookingSubjects = res;
       this.currentBookingSubject = res[0];
     });
   }

}
