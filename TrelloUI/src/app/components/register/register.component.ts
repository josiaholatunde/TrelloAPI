import { AlertifyService } from './../../services/alertify.service';
import { Component, OnInit, ViewChild } from '@angular/core';
import { UserService } from 'src/app/services/user.service';
import { NgForm } from '@angular/forms';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss']
})
export class RegisterComponent implements OnInit {

  @ViewChild('registerationFormControl') registerationFormControl: NgForm;
  userForRegisterDto: any = {};
  constructor(private userService: UserService, private alertify: AlertifyService) { }

  ngOnInit() {
  }
  register() {
    this.userService.registerUser(this.userForRegisterDto).subscribe(res => {
      this.alertify.success('Registeration was successful');
    }, err => this.alertify.error(err), () => {
      this.registerationFormControl.reset();
    });
  }

}
