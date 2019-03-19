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
import { CommentsListComponent } from '../components/comments-list/comments-list.component';
import { AuthGuard } from '../guards/auth.guard';


const routes: Routes = [
  {
    path: 'home',
    component: HomeComponent,
  },
      {
        path: 'bookings/edit/:id',
        component: BookingEditComponent,
        canActivate: [AuthGuard]
      },
      {
        path: 'users/messages',
        component: MessageComponent,
        resolve: { messages: MessageListResolver },
        canActivate: [AuthGuard]
      },
      {
        path: 'users/messages/:id',
        component: ChatDetailComponent,
        canActivate: [AuthGuard]
      },
      {
        path: 'bookings/:name',
        component: HomeComponent,
      },
      {
        path: 'photo/upload',
        component: PhotoUploadComponent,
        canActivate: [AuthGuard]
      },
      {
        path: 'bookings/:name/:id/comments',
        component: CommentsListComponent,
      },
      {
        path: 'bookings/:name/:status',
        component: HomeComponent,
        canActivate: [AuthGuard]
      },
      {
        path: 'my-bookings',
        component: MyBookingsComponent,
        canActivate: [AuthGuard]
      },
      {
        path: 'login',
        component: LoginComponent,
      },
      {
        path: 'register',
        component: RegisterComponent,
      },

  {
    path: '',
    redirectTo: 'home',
    pathMatch: 'full'
  },
    {
      path: '**',
      redirectTo: 'home',
      pathMatch: 'full'
    },
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
