export interface Abuse {
    abuseAdvId: number;
    abuseText: string;
    authorId: string;
    id: number;
    priority: number;
}

export interface NewAbuse {
    advId: number;
    abuseText: string;
}

export interface ListOfItems<T> {
    limit: number,
    offset: number,
    items: T[]
  }