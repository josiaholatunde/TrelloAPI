export interface Message {
  id: number;
  senderId: number;
  senderKnownAs: string;
  senderPhotoUrl: string;
  recipientId: number;
  recipientKnownAs: string;
  recipientPhotoUrl: string;
  messageContent: string;
  isRead: boolean;
  dateRead: Date;
  dateSent: Date;
  senderDeleted?: boolean;
  recipientDeleted?: boolean;
}
