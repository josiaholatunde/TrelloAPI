import { ActivatedRoute } from '@angular/router';
import { Component, OnInit } from '@angular/core';
import { FormGroup, Validators, FormBuilder } from '@angular/forms';
import { BookingSubjectType } from 'src/app/models/booking-subject-type';
import { BookingSubjectService } from 'src/app/services/booking-subject.service';
import { BookingSubject } from 'src/app/models/booking-subject';
import { UserService } from 'src/app/services/user.service';
import { UserRole } from 'src/app/models/UserRole';
import { AlertifyService } from 'src/app/services/alertify.service';

@Component({
  selector: 'app-booking-edit',
  templateUrl: './booking-edit.component.html',
  styleUrls: ['./booking-edit.component.scss']
})
export class BookingEditComponent implements OnInit {

  editBookingGroup: FormGroup;
  bookingType: any = [
    {display: 'Hotel', value: BookingSubjectType.Hotel},
    {display: 'Car Rental', value: BookingSubjectType.CarRental},
    {display: 'Flight', value: BookingSubjectType.Flight},
    {display: 'Tours', value: BookingSubjectType.Tour}
  ];
  currentBooking: BookingSubject;
  bookingId: number;
  constructor(private fb: FormBuilder, private route: ActivatedRoute, private userService: UserService, private alertify: AlertifyService,
    private bookingService: BookingSubjectService) { }

  ngOnInit() {
    this.initialiseForm();
    if (this.userService.isUserLoggedIn()) {
      const loggedInUser = this.userService.getLoggedInUser();
        if (loggedInUser) {
          if (loggedInUser.userRole === UserRole.Admin) {
            this.userService.changeLoggedInStatus(true);
          }
        }
    }
    this.route.params.subscribe(param => {
      if (param['id']) {
        this.bookingId = +param['id'];
        this.bookingService.getBooking(this.bookingId).subscribe((res: any) => {
          this.currentBooking = res;
          this.editBookingGroup.setValue({
            name: this.currentBooking.name,
            mainDescription: this.currentBooking.mainDescription,
            subDescription: this.currentBooking.subDescription,
            bookingFeatures: this.currentBooking.features,
            noOfBookingSubjectsLeft: this.currentBooking.noOfBookingSubjectsLeft,
            city: this.currentBooking.city,
            country: this.currentBooking.country,
            totalBookingSubjects: this.currentBooking.totalBookingSubjects,
            bookingSubjectType: this.currentBooking.bookingType
          });
        });
      }
    });
  }
  initialiseForm(): any {
    this.editBookingGroup = this.fb.group({
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

  editBooking() {
    if (this.editBookingGroup.valid) {

      const location: any = {};
      const description: any = {};
      location.city = this.editBookingGroup.value.city;
      location.country = this.editBookingGroup.value.city;
      const userId = this.userService.getLoggedInUser().id;
      const features: string[] = this.editBookingGroup.value.bookingFeatures.split(',');
      description.mainDescription = this.editBookingGroup.value.mainDescription;
      description.subDescription = this.editBookingGroup.value.subDescription;
      location.country = this.editBookingGroup.value.city;
      const id = this.bookingId;
      const noOfVoters = 0;
     const updateBookingVm = { ...this.editBookingGroup.value, location, description, noOfVoters, features, userId, id };
     this.bookingService.updateBooking(updateBookingVm, id).subscribe(res => {
       this.alertify.success('Successfully updated booking');
     }, err => {
       this.alertify.error('Something just happened right now');
     }, () => {
       this.editBookingGroup.reset(this.editBookingGroup.value);
     });
    }
  }

}
