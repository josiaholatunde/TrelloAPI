import { MessageService } from './../../services/messages.service';
import { Component, OnInit, EventEmitter, Output, Input, OnChanges, SimpleChanges, OnDestroy } from '@angular/core';
import { BookingSubjectType } from 'src/app/models/booking-subject-type';
import { ActivatedRoute, Router } from '@angular/router';
import { BookingSubjectService } from 'src/app/services/booking-subject.service';
import { BookingSubject } from 'src/app/models/booking-subject';
import { UserService } from 'src/app/services/user.service';
import { AlertifyService } from 'src/app/services/alertify.service';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.scss']
})
export class HeaderComponent implements OnInit, OnChanges {
  searchParams: any;
  @Output() bookingTypeSearchInputChange = new EventEmitter();
  @Output() bookingSubjectChange = new EventEmitter();
  @Output() bookingSubjectChangeViaSearchInput = new EventEmitter();

  @Input() isLoggedInStatusProperty;
  @Input() propertyToPassOn: any;
  @Input() bookingTypePassed: any;
  currentBookingType: BookingSubjectType;
  bookingSubjects: BookingSubject[];
  isLoggedIn = false;
  loggedInUser: any;
  bookingType: string;
  paramName: any;
  type: any;
  testProps: string;
  routePath: string;
  defaultPhotoUrl: string;
  notificationCount: number;
  bookingSubjectSuggestion: any[];

  constructor(private route: ActivatedRoute, private router: Router, private bookingService: BookingSubjectService,
    private alertifyService: AlertifyService, private userService: UserService, private messageService: MessageService) {
   }

  getBookingSubject(bookingType: any) {
   // this.isLoggedInAndRegisterChange.emit(false);
    this.bookingService.getBookingSubjects(bookingType).subscribe((res: BookingSubject[]) => {
     this.bookingSubjectChange.emit(res);
    }, err => this.alertifyService.error(err));
  }

  ngOnInit() {
    this.bookingService.defaultBookingTypeObservable.subscribe(type => {
      this.bookingType = BookingSubjectType[type];
    });
    this.getUnreadNotifications();
    this.userService.defaultPhotoUrlObservable.subscribe(ph => this.defaultPhotoUrl = ph);

    this.setBookingType();
    this.getBookingSubject(this.bookingType);
    this.userService.defaultLoggedInStatus.subscribe(status => {
      this.loggedInUser = this.userService.getLoggedInUser();
      this.isLoggedIn = status;
    });

    this.route.paramMap.subscribe(param => {
      if (param['status']) {
        this.isLoggedIn = true;
      }
    });
    if (this.router.url.endsWith('true')) {
        this.isLoggedIn = true;
        const user = JSON.parse(localStorage.getItem('user'));
        this.loggedInUser = user;
      }
  }

  ngOnChanges(changes: SimpleChanges): void {
     this.userService.defaultLoggedInStatus.subscribe(status => {
      this.loggedInUser = this.userService.getLoggedInUser();
      this.isLoggedIn = status;
    });
  }

  setBookingType(): any {
    if (this.paramName === 'Hotel') {
      this.bookingType = BookingSubjectType[BookingSubjectType.Hotel];
    } else  if (this.paramName === 'CarRental') {
      this.bookingType = BookingSubjectType[BookingSubjectType.CarRental];
    } else  if (this.paramName === 'Tour') {
      this.bookingType = BookingSubjectType[BookingSubjectType.Tour];
    }  else  if (this.paramName === 'Flight') {
      this.bookingType = BookingSubjectType[BookingSubjectType.Flight];
    } else {
      this.bookingType = BookingSubjectType[BookingSubjectType.Hotel];
    }
  }

  filterBookingSubject(event) {
    const query = event.query;
    // type from route params
    this.bookingService.defaultBookingType.subscribe(bookingType => {
      this.bookingService.getBookingSubjects(bookingType, query).subscribe((res: BookingSubject[]) => {
        const bookingSubjectSuggestion = [];
        res.forEach(r => {
          bookingSubjectSuggestion.push(r.bookingSubjectResultString);
        });
        this.bookingSubjectSuggestion = bookingSubjectSuggestion;
        this.bookingSubjects = res;

      });
    });

  }
  getBookingSubjects() {
    this.bookingTypeSearchInputChange.emit(this.searchParams.name);
  }
  logout() {
    this.isLoggedIn = false;
    this.userService.changeLoggedInStatus(false);
    localStorage.removeItem('token');
    localStorage.removeItem('user');
    this.router.navigate(['home']);
    this.alertifyService.success('Logged out successfully');
  }
  getDefaultPhoto() {
    return this.userService.defPhoto;
  }
  getUnreadNotifications() {
    this.messageService.getNotificationCount().subscribe(res => this.notificationCount = res);
  }

  changeBooking() {
    const bookingToChangeTo = this.bookingSubjects.find(b => b.bookingSubjectResultString === this.searchParams);
    this.bookingSubjectChangeViaSearchInput.emit(bookingToChangeTo);
    this.router.navigate(['home']);
  }

}
