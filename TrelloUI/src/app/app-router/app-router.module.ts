import { TestComponent } from './../components/test/test.component';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Routes, RouterModule } from '@angular/router';
import { AppComponent } from '../app.component';
import { BookingSubjectResolver } from '../resolvers/booking-subject.resolver';
import { HomeComponent } from '../components/home/home.component';
import { LoginComponent } from '../components/login/login.component';
import { RegisterComponent } from '../components/register/register.component';
import { MyBookingsComponent } from '../components/my-bookings/my-bookings.component';
import { BookingEditComponent } from '../components/booking-edit/booking-edit.component';
import { PhotoUploadComponent } from '../components/photo-upload/photo-upload.component';
import { MessageComponent } from '../components/message/message.component';
import { MessageListResolver } from '../resolvers/message-list.resolver';
import { ChatDetailComponent } from '../components/chat-detail/chat-detail.component';


const routes: Routes = [
  {
    path: 'bookings/edit/:id',
    component: BookingEditComponent,
    // resolve: [ BookingSubjectResolver ]
  },
  {
    path: 'users/messages',
    component: MessageComponent,
    resolve: { messages: MessageListResolver }
    // resolve: [ BookingSubjectResolver ]
  },
  {
    path: 'users/messages/:id',
    component: ChatDetailComponent,
    // resolve: { messages: MessageListResolver }
    // resolve: [ BookingSubjectResolver ]
  },
  {
    path: 'bookings/:name',
    component: HomeComponent,
    // resolve: [ BookingSubjectResolver ]
  },
  {
    path: 'photo/upload',
    component: PhotoUploadComponent,
    // resolve: [ BookingSubjectResolver ]
  },
  {
    path: 'bookings/:name/:status',
    component: HomeComponent,
    // resolve: [ BookingSubjectResolver ]
  },
  {
    path: 'home',
    component: HomeComponent,
    resolve: [ BookingSubjectResolver ]
  },
  {
    path: 'my-bookings',
    component: MyBookingsComponent,
   // resolve: [ BookingSubjectResolver ]
  },
  {
    path: 'login',
    component: LoginComponent,
    // resolve: [ BookingSubjectResolver ]
  },
  {
    path: 'register',
    component: RegisterComponent,
    // resolve: [ BookingSubjectResolver ]
  },
  {
    path: 'test',
    component: TestComponent
   // resolve: [ BookingSubjectResolver ]
  },
  {path: '', redirectTo: 'home', pathMatch: 'full'},
  {path: '**', redirectTo: 'home', pathMatch: 'full'},

];

@NgModule({
  declarations: [],
  imports: [
    CommonModule,
    RouterModule.forRoot(routes)
  ],
  exports: [RouterModule]
})
export class AppRouterModule { }
