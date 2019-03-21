import { AppRouterModule } from './app-router/app-router.module';
import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import {AutoCompleteModule} from 'primeng/autocomplete';
import {BrowserAnimationsModule} from '@angular/platform-browser/animations';
import {ButtonModule} from 'primeng/button';
import {DialogModule} from 'primeng/dialog';
import { JwtModule } from '@auth0/angular-jwt';
import {TabViewModule} from 'primeng/tabview';
import { FileUploadModule } from 'ng2-file-upload';
import {TimeAgoPipe} from 'time-ago-pipe';
import {PaginatorModule} from 'primeng/paginator';

import { AppComponent } from './app.component';
import { SidebarComponent } from './components/sidebar/sidebar.component';
import { GalleryComponent } from './components/gallery/gallery.component';
import { OverviewComponent } from './components/overview/overview.component';
import { DetailComponent } from './components/detail/detail.component';
import { CtaComponent } from './components/cta/cta.component';
import { HeaderComponent } from './components/header/header.component';
import { HttpClientModule } from '@angular/common/http';
import { BookingSubjectService } from './services/booking-subject.service';
import { BookingSubjectResolver } from './resolvers/booking-subject.resolver';
import { HomeComponent } from './components/home/home.component';
import { TestComponent } from './components/test/test.component';
import {CarouselModule} from 'primeng/carousel';
import { LoginRegisterPageComponent } from './components/login-register-page/login-register-page.component';
import { LoginComponent } from './components/login/login.component';
import { RegisterComponent } from './components/register/register.component';
import { AlertifyService } from './services/alertify.service';
import { UserService } from './services/user.service';
import { ErrorInterceptor, ErrorInterceptorProvider } from './services/error.interceptor';
import { CreateBookingComponent } from './components/create-booking/create-booking.component';
import { UserCommentComponent } from './components/user-comment/user-comment.component';
import { TestHeaderComponent } from './components/test-header/test-header.component';
import { MyBookingsComponent } from './components/my-bookings/my-bookings.component';
import { BookingEditComponent } from './components/booking-edit/booking-edit.component';
import { PhotoUploadComponent } from './components/photo-upload/photo-upload.component';
import { MessageListResolver } from './resolvers/message-list.resolver';
import { MessageComponent } from './components/message/message.component';
import { MessageService } from './services/messages.service';
import { ChatDetailComponent } from './components/chat-detail/chat-detail.component';
import { CommentsListComponent } from './components/comments-list/comments-list.component';
import { AuthGuard } from './guards/auth.guard';


export function tokenGetter() {
 return localStorage.getItem('token');
}


@NgModule({
  declarations: [
    AppComponent,
    SidebarComponent,
    GalleryComponent,
    OverviewComponent,
    DetailComponent,
    CtaComponent,
    HeaderComponent,
    HomeComponent,
    TestComponent,
    LoginRegisterPageComponent,
    LoginComponent,
    RegisterComponent,
    CreateBookingComponent,
    UserCommentComponent,
    TestHeaderComponent,
    MyBookingsComponent,
    BookingEditComponent,
    PhotoUploadComponent,
    MessageComponent,
    TimeAgoPipe,
    ChatDetailComponent,
    CommentsListComponent
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    FormsModule,
    AppRouterModule,
    CarouselModule,
    AutoCompleteModule,
    BrowserAnimationsModule,
    ButtonModule,
    DialogModule,
    ReactiveFormsModule,
    TabViewModule,
    PaginatorModule,
    FileUploadModule,
    JwtModule.forRoot({
     config: {
        tokenGetter: tokenGetter,
        whitelistedDomains: ['localhost:5000'],
        blacklistedRoutes: ['localhost:5000/api/auth']
      }
    })
  ],
  providers: [
    BookingSubjectService,
    BookingSubjectResolver,
    AlertifyService,
    UserService,
    ErrorInterceptorProvider,
    MessageListResolver,
    MessageService,
    AuthGuard
   ],
  bootstrap: [AppComponent]
})
export class AppModule { }
