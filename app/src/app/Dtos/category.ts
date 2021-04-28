export interface Category {
    id: number,
    name: string,
    parentId: number,
    parentName: string,
    childCategories: Category[]
}

export interface ListOfCategories {
    categories: Category[],
    parent: Category,
}