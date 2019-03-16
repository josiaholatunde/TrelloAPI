import { Message } from './message';

export interface PaginationResult<T> {
  result: T;
  pagination: any;
}
export class MessagePaginationResult implements PaginationResult<Message[]> {
  result: Message[];
  pagination: any;
}
