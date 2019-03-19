import { ActivatedRoute } from '@angular/router';
import { Component, OnInit } from '@angular/core';
import { UserService } from 'src/app/services/user.service';
import { UserRole } from 'src/app/models/UserRole';
import { BookingSubjectService } from 'src/app/services/booking-subject.service';
import { Comment } from 'src/app/models/Comment';

@Component({
  selector: 'app-comments-list',
  templateUrl: './comments-list.component.html',
  styleUrls: ['./comments-list.component.scss']
})
export class CommentsListComponent implements OnInit {
  loggedInUser: any;
  bookingId: number;
  comments: Comment[];
  constructor(private userService: UserService, private bookingService: BookingSubjectService, private route: ActivatedRoute) { }

  ngOnInit() {
    if (this.userService.isUserLoggedIn()) {
      this.loggedInUser = this.userService.getLoggedInUser();
        if (this.loggedInUser) {
          if (this.loggedInUser.userRole === UserRole.Admin) {
            this.userService.changeLoggedInStatus(true);
          }
        }
    }
    this.route.params.subscribe(param => {
      if (param['id']) {
        this.bookingId = +param['id'];
      }
    })
    this.getComments();
  }
  getComments(): any {
    this.bookingService.getComments(this.bookingId).subscribe((res: Comment[]) => {
      this.comments = res;
    });
  }
  

}
