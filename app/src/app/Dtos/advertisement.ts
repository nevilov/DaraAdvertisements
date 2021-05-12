import { Category } from './category';
import { User } from './user';

export interface Advertisement {
    id: number;
    title: string;
    description: string;
    cover: string;
    images: any;
    status: string;
    price: number;
    category: Category;
    owner: User;
    createdDate: Date;
    location: string;
    geoLat: number;
    geoLon: number;
}

export interface NewAdvertisement {
    title: string;
    description: string;
    price: number;
    cover: string;
    categoryId: number;
    location: string;
    geoLat: number;
    geoLon: number;
}

export interface ListOfItems<T> {
    limit: number;
    offset: number;
    total: number;
    items: T[];
}
