import { Component, OnInit, Output, EventEmitter, ViewChild } from '@angular/core';
import { UserService } from 'src/app/services/user.service';
import { AlertifyService } from 'src/app/services/alertify.service';
import { Router } from '@angular/router';
import { NgForm } from '@angular/forms';
import { NgxSpinnerService } from 'ngx-spinner';


@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {
@ViewChild('loginFormControl') loginFormControl: NgForm;
  userForLogin: any = {};
  @Output() hideLoginAndRegisterChange = new EventEmitter();
  constructor(private userService: UserService, private alertify: AlertifyService, private router: Router,
     private spinner: NgxSpinnerService) { }

  ngOnInit() {
  }
  login() {
    this.spinner.show();
    this.userService.loginUser(this.userForLogin).subscribe(res => {
      this.spinner.hide();
      this.alertify.success('Login was successful');
      this.loginFormControl.reset();
      this.hideLoginAndRegisterChange.emit(false);
    }, err => this.alertify.error(err),
    () => {
      this.router.navigate(['bookings/Hotel', 'true']);
      this.userService.changeLoggedInStatus(true);
    });
  }

}
