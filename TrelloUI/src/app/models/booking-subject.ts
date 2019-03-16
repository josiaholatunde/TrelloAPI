import { BookingSubjectType } from './booking-subject-type';
import { GalleryPicture } from './GalleryPicture';
import { Feature } from './Feature';
import { Comment } from './Comment';

export interface BookingSubject {
  id: number;
  name: string;
  city: string;
  country: string;
  noOfVoters: number;
  mainDescription: string;
  subDescription: string;
  noOfBookingSubjectsLeft: number;
  totalBookingSubjects: number;
  noOfRecommendations: number;
  bookingType: BookingSubjectType;
  comments: Comment[];
  features: Feature[];
  galleryPictures: GalleryPicture[];
  avgRating?: number;
}

