import { Component, OnInit } from '@angular/core';
import { AlertifyService } from 'src/app/services/alertify.service';
import { BookingSubjectService } from 'src/app/services/booking-subject.service';
import { UserService } from 'src/app/services/user.service';
import { BookingSubject } from 'src/app/models/booking-subject';
import { UserRole } from 'src/app/models/UserRole';

@Component({
  selector: 'app-my-bookings',
  templateUrl: './my-bookings.component.html',
  styleUrls: ['./my-bookings.component.scss']
})
export class MyBookingsComponent implements OnInit {
  bookingSubjects: BookingSubject[];
  loggedInUser: any;
  defaultPhotoUrl = '../../../assets/img/super-hero.png';

  constructor(private alertify: AlertifyService, private bookingService: BookingSubjectService,
     private userService: UserService) { }

  ngOnInit() {
    if (this.userService.isUserLoggedIn()) {
      this.loggedInUser = this.userService.getLoggedInUser();
        if (this.loggedInUser) {
          if (this.loggedInUser.userRole === UserRole.Admin) {
            this.userService.changeLoggedInStatus(true);
          }
        }
    }
    this.loadMyBookings();
  }
  loadMyBookings() {
    const loggedInUser = this.userService.getLoggedInUser();
    this.bookingService.getMyBookings(loggedInUser.id).subscribe((res: BookingSubject[]) => {
      this.bookingSubjects = res;
    });
  }
  deleteBooking(booking: any) {
    this.alertify.confirm('Are you sure you want to delete this booking? ', () => {
      // api call to delete bookings
      this.bookingService.deleteBooking(booking.id, this.loggedInUser.id).subscribe(() => {
        this.alertify.success('Successfully deleted photo');
      }, err => this.alertify.error('Error occurred'),
      () => {
        const index = this.bookingSubjects.indexOf(booking);
        this.bookingSubjects.splice(index, 1);
      });
    });
  }

}
