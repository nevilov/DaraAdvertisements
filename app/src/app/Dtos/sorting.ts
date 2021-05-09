interface OrderBy {
  sortBy: string;
  sortDirection: string;
}

export interface SortItem {
  title: string;
  orderBy: OrderBy;
}
