import { NgForm } from '@angular/forms';
import { Message } from 'src/app/models/message';
import { ActivatedRoute } from '@angular/router';
import { Component, OnInit, ViewChild } from '@angular/core';
import { MessageService } from 'src/app/services/messages.service';
import { AlertifyService } from 'src/app/services/alertify.service';
import { UserService } from 'src/app/services/user.service';

@Component({
  selector: 'app-chat-detail',
  templateUrl: './chat-detail.component.html',
  styleUrls: ['./chat-detail.component.scss']
})
export class ChatDetailComponent implements OnInit {
  messages: Message[];
  recipientId: number;
  messageToCreateDto: any = {};
  @ViewChild('chatControl') chatControl: NgForm;

  constructor(private messageService: MessageService, private route: ActivatedRoute,
    private userService: UserService, private alertify: AlertifyService) { }

  ngOnInit() {
    this.route.params.subscribe(param => {
      if (param['id']) {
        this.recipientId = +param['id'];
        this.messageService.getMessageThread(this.recipientId).subscribe(res => {
          this.messages = res;
        });
      }
      if (this.userService.isUserLoggedIn()) {
        this.userService.changeLoggedInStatus(true);
       }
    });
  }
  sendMessage() {
    this.messageToCreateDto.recipientId = this.recipientId;
    this.messageService.sendMessage(this.messageToCreateDto).subscribe((res: Message) => {
      this.messages.push(res);
      this.alertify.success('Message was sent successfully');
    }, err => {
      this.alertify.error(err);
    }, () => this.chatControl.reset());

  }

}
