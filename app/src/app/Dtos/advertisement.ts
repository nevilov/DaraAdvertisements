import { Category } from './category';
import { User } from "./user";

export interface Advertisement {
  id: string;
  title: string;
  description: string;
  cover: string;
  status: string;
  price: number;
  category: Category;
  owner: User;
}

export interface ListOfItems<T> {
  limit: number,
  offset: number,
  items: T[]
}
