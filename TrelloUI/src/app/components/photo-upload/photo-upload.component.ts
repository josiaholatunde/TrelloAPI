import { ActivatedRoute, Router } from '@angular/router';
import { Component, OnInit } from '@angular/core';
import { FileUploader } from 'ng2-file-upload';
import { environment } from 'src/environments/environment';
import { AlertifyService } from 'src/app/services/alertify.service';
import { BookingSubjectService } from 'src/app/services/booking-subject.service';
import { UserService } from 'src/app/services/user.service';
import { UserRole } from 'src/app/models/UserRole';

@Component({
  selector: 'app-photo-upload',
  templateUrl: './photo-upload.component.html',
  styleUrls: ['./photo-upload.component.scss']
})
export class PhotoUploadComponent implements OnInit {

  uploader: FileUploader;
  hasBaseDropZoneOver = false;
  baseUrl = environment.apiUrl;
  bookingId: number;
  photos: any[];
  userid: any;
  hasGallery: boolean;
  loggedInUser: any;
  constructor(private route: ActivatedRoute, private router: Router, private userService: UserService,
    private booking: BookingSubjectService,
    private alertify: AlertifyService) {
    this.photos = [];
  }

  ngOnInit() {
    if (this.userService.isUserLoggedIn()) {
      this.loggedInUser = this.userService.getLoggedInUser();
        if (this.loggedInUser) {
          if (this.loggedInUser.userRole === UserRole.Admin) {
            this.userService.changeLoggedInStatus(true);
          }
        }
    }
    if (this.router.url.endsWith('upload')) {
      this.hasGallery = false;
      this.initialiseUploader(this.hasGallery);

    } else {
      this.route.params.subscribe(param => {
        if (param['id']) {
          this.hasGallery = true;
          this.bookingId = +param['id'];
          this.initialiseUploader(this.hasGallery);
          this.booking.getGalleryPhotos(this.bookingId).subscribe(photos => this.photos = photos);
        }
      });
    }
  }

  fileOverBase(e: any): void {
    this.hasBaseDropZoneOver = e;
  }

  initialiseUploader(hasGallery?: boolean) {
    this.userid = JSON.parse(localStorage.getItem('user')).id;
    this.uploader = new FileUploader({
      url: hasGallery ? `${this.baseUrl}/bookings/${this.bookingId}/photos/${this.userid}` :
      `${this.baseUrl}/users/${this.userid}/photos`,
      authToken: `Bearer ${localStorage.getItem('token')}`,
      isHTML5: true,
      allowedFileType: ['image'],
      autoUpload: false,
      maxFileSize: 10 * 10 * 1024,
      removeAfterUpload: true
    });
    this.uploader.onAfterAddingFile = (file) => {
      file.withCredentials = false;
    };
    this.uploader.onSuccessItem = (item, response, status, headers) => {
      if (response) {
        const res: any = JSON.parse(response);
        const photo: any =  {
          id: res.id,
          url: res.url,
          description: res.url,
          dateAdded: res.dateAdded
        };
        if (hasGallery) {
          this.photos.push(photo);
          this.alertify.success('Uploaded gallery image usccessfully');
        } else {
          // change user default photo
          this.userService.changeDefaultPhoto(photo.url);
          this.alertify.success('Upated profile photo successfully');
        }
      }
    };
  }

  deleteGalleryPhoto(photo: any) {
    this.alertify.confirm('Are you sure you want to delete this gallery photo', () => {
      this.booking.deleteGalleryPhoto(this.bookingId, this.userid, photo.id).subscribe(() => {
        this.alertify.success('Successfully deleted photo');
      }, err => this.alertify.error(err),
      () => {
        const index = this.photos.indexOf(photo);
        this.photos.splice(index, 1);
      });
    });
  }

}
