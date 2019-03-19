import { Injectable } from '@angular/core';
import { CanActivate, ActivatedRoute, Router } from '@angular/router';
import { UserService } from '../services/user.service';
import { AlertifyService } from '../services/alertify.service';

@Injectable({
  providedIn: 'root'
}) 
export class AuthGuard implements CanActivate {

  constructor(private userService: UserService, private route: Router, private alertifyService: AlertifyService) {}
  canActivate(): boolean  {
    if (this.userService.getLoggedInUser()) {
      return true;
    }
    this.alertifyService.error('You must be logged in before you can access any page');
    this.route.navigate(['login']);
    return false;
  }


}