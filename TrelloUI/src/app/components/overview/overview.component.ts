import { BookingSubject } from './../../models/booking-subject';
import { Component, OnInit, Input } from '@angular/core';

@Component({
  selector: 'app-overview',
  templateUrl: './overview.component.html',
  styleUrls: ['./overview.component.scss']
})
export class OverviewComponent implements OnInit {

  @Input() bookingSubject: BookingSubject;
  starsCount: any[] = [];
  constructor() { }

  ngOnInit() {
    for(let i = 0; i < this.bookingSubject.avgRating; i++) {
      this.starsCount.push(i);
    }
  }

}
