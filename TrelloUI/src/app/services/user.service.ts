import { Injectable } from '@angular/core';
import { Observable, BehaviorSubject } from 'rxjs';
import { map } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { HttpHeaders, HttpClient } from '@angular/common/http';

const httpOptions = {
  headers: new HttpHeaders({
    'Content-Type': 'application/json'
  })
};
@Injectable({
  providedIn: 'root'
})
export class UserService {

  private baseUrl = `${environment.apiUrl}/auth`;
  defaultLoggedInStatus = new BehaviorSubject(false);
  loggedInStatus = this.defaultLoggedInStatus.asObservable();
  defaultPhotoUrl = new BehaviorSubject('../../assets/img/user.png');
  defPhoto = '../../assets/img/user.png';
  defaultPhotoUrlObservable = this.defaultPhotoUrl.asObservable();
  constructor(private http: HttpClient) { }

  changeLoggedInStatus(status: boolean) {
    this.defaultLoggedInStatus.next(status);
  }
  changeDefaultPhoto(url: string) {
    this.defaultPhotoUrl.next(url);
  }

  registerUser(userFoRegisterationDto: any): Observable<any[]> {
   return this.http.post<any[]>(`${this.baseUrl}/register`, userFoRegisterationDto, httpOptions).pipe();
  }
  loginUser(userForLoginDto: any): Observable<any[]> {
   return this.http.post<any[]>(`${this.baseUrl}/login`, userForLoginDto, httpOptions).pipe(
     map((res: any) => {
        const token = res.token;
        const user = JSON.stringify(res.user);
        this.changeLoggedInStatus(true);
        localStorage.setItem('token', token);
        localStorage.setItem('user', user);
        return res.user;
     })
   );
  }

  getLoggedInUser() {
    const user = JSON.parse(localStorage.getItem('user'));
    return user;
  }
  isUserLoggedIn() {
    const user = JSON.parse(localStorage.getItem('user'));
    return !!user;
  }
}
