import { Category } from './category';
import { User } from "./user";

export class Advertisement {
  Title: string;
  Description: string;
  Cover: string;
  Status: string;
  Price: number;
  Category: Category;
  Owner: User;
}
