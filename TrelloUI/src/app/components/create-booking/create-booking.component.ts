import { Component, OnInit, Input } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { BookingSubjectType } from 'src/app/models/booking-subject-type';
import { BookingSubjectService } from 'src/app/services/booking-subject.service';
import { AlertifyService } from 'src/app/services/alertify.service';

@Component({
  selector: 'app-create-booking',
  templateUrl: './create-booking.component.html',
  styleUrls: ['./create-booking.component.scss']
})
export class CreateBookingComponent implements OnInit {

  @Input() passDisplay;
  bookingToCreate: any = {};
  createBookingGroup: FormGroup;
  bookingType: any = [
    {display: 'Hotel', value: BookingSubjectType.Hotel},
    {display: 'Car Rental', value: BookingSubjectType.CarRental},
    {display: 'Flight', value: BookingSubjectType.Flight},
    {display: 'Tours', value: BookingSubjectType.Tour}
  ];
  constructor(private fb: FormBuilder, private bookingService: BookingSubjectService, private alertify: AlertifyService) { }

  ngOnInit() {
    this.initialiseForm();
  }
  initialiseForm(): any {
    this.createBookingGroup = this.fb.group({
      name: ['', Validators.required],
      mainDescription: ['', [Validators.required, Validators.minLength(272), Validators.maxLength(276)]],
      subDescription: ['', [Validators.required, Validators.minLength(272), Validators.maxLength(276)]],
      bookingFeatures: ['', Validators.required],
      noOfBookingSubjectsLeft: ['', Validators.required],
      city: ['', Validators.required],
      country: ['', Validators.required],
      totalBookingSubjects: ['', Validators.required],
      bookingSubjectType: [BookingSubjectType.Hotel, Validators.required],
    });
  }
  createBooking() {
   if (this.createBookingGroup.valid) {
     const location: any = {};
     const description: any = {};
     location.city = this.createBookingGroup.value.city;
     location.country = this.createBookingGroup.value.city;
    const userId = this.getLoggedInUserId();
     const features: string[] = this.createBookingGroup.value.bookingFeatures.split(',');
     description.mainDescription = this.createBookingGroup.value.mainDescription;
     description.subDescription = this.createBookingGroup.value.subDescription;
     location.country = this.createBookingGroup.value.city;
     const noOfVoters = 0;
    const creatingBookingVm = { ...this.createBookingGroup.value, location, description, noOfVoters, features, userId };
    this.bookingService.createBooking(creatingBookingVm).subscribe(res => {
      this.alertify.success('Successfully created booking');
    }, err => {
      this.alertify.error('Something just happened right now');
    }, () => {
      this.createBookingGroup.reset();
    });
   }
  }
  getLoggedInUserId(): number {
    const id = JSON.parse(localStorage.getItem('user')).id;
    return id;
  }
}
