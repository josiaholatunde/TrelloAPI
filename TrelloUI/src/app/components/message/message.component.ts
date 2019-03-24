import { AlertifyService } from 'src/app/services/alertify.service';
import { ActivatedRoute, Router } from '@angular/router';
import { Component, OnInit } from '@angular/core';
import { MessageService } from 'src/app/services/messages.service';
import { Pagination } from 'src/app/models/pagination';
import { Message } from 'src/app/models/message';
import { UserService } from 'src/app/services/user.service';
import { UserRole } from 'src/app/models/UserRole';
import { NgxSpinnerService } from 'ngx-spinner';

@Component({
  selector: 'app-message',
  templateUrl: './message.component.html',
  styleUrls: ['./message.component.scss']
})
export class MessageComponent implements OnInit {
  messages: Message[];
  pagination: Pagination;
  messageContainer = 'Unread';
  loggedInUser: any;

  constructor(private route: ActivatedRoute, private router: Router, private userService: UserService, private spinner: NgxSpinnerService,
    private alertify: AlertifyService,  private messageService: MessageService) { }

  ngOnInit() {
    if (this.userService.isUserLoggedIn()) {
      this.loggedInUser = this.userService.getLoggedInUser();
        if (this.loggedInUser) {
          if (this.loggedInUser.userRole === UserRole.Admin) {
            this.userService.changeLoggedInStatus(true);
          }
        }
    }
    this.route.data.subscribe(data => {
      this.spinner.hide();
      this.messages = data['messages'].result;
      this.pagination = data['messages'].pagination;
    });
  }

  loadMessages(messageContainer?: string) {
    if (messageContainer) {
      this.messageContainer = messageContainer;
    }
    this.messageService.getMessagesForUser(this.messageContainer, this.pagination.currentPage, this.pagination.itemsPerPage)
    .subscribe(res => {
      this.spinner.show();
      this.messages = res.result;
      this.pagination = res.pagination;
    })
  }
  pageChanged(event: any) {
    this.pagination.currentPage = event.page;
    this.spinner.show();
    this.loadMessages();
  }
  deleteMessage(m: Message) {
    this.alertify.confirm('Are you sure you want to delete this message? ', () => {
      // Do something;
    });
  }

}
