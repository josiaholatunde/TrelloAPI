export interface Comment {
  Id: string;
  description: string;
  dateCommented: Date;
  rating: number;
  isRecommended: boolean;
  photoUrl: string;
  fullName?: string;
}
