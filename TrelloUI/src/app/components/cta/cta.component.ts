import { BookingSubject } from 'src/app/models/booking-subject';
import { Component, OnInit, Input } from '@angular/core';
import { AlertifyService } from 'src/app/services/alertify.service';

@Component({
  selector: 'app-cta',
  templateUrl: './cta.component.html',
  styleUrls: ['./cta.component.scss']
})
export class CtaComponent implements OnInit {

  @Input() bookingSubject: BookingSubject;
  constructor(private alertifyService: AlertifyService) { }

  ngOnInit() {
  }
  bookSubject() {
    this.alertifyService.success('This button does not do anything at the moment');
  }

}
