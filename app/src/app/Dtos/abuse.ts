export interface Abuse {
  abuseAdvId: number;
  abuseText: string;
  authorId: string;
  id: number;
  priority: number;
  removedDate: Date;
}

export interface NewAbuse {
  advId: number;
  abuseText: string;
}

export interface ListOfItems<T> {
  limit: number;
  offset: number;
  total: number;
  items: T[];
}
