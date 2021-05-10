import { Router } from '@angular/router';
import { CategoryService } from './../../../services/category.service';
import { Component, ElementRef, EventEmitter, Input, OnInit, Output, ViewChild } from '@angular/core';
import { Category } from 'src/app/Dtos/category';
import { NgDynamicBreadcrumbService } from 'ng-dynamic-breadcrumb';

@Component({
    selector: 'app-submenu',
    templateUrl: './submenu.component.html',
    styleUrls: ['./../../../../assets/scss/layout/__submenu.scss']
})
export class SubmenuComponent implements OnInit {

    categories: Category[] = [];
    @Input() isSubMenuShown = true;
    @Output() isSubMenuShownOutput = new EventEmitter<boolean>();

    constructor(
        private categoryService: CategoryService,
        private ngDynamicBreadcrumbService: NgDynamicBreadcrumbService) { }

    ngOnInit() {
        this.getAllCategories();
    }

    public getAllCategories() {
        this.categoryService.getAllCategories().subscribe((data) => {
            this.categories = data.categories;
            console.log(data.categories);
        });
    }

    public closeSubMenu() {
        this.isSubMenuShown = false;
        this.isSubMenuShownOutput.emit(this.isSubMenuShown);
    }

    public reloadBreadcrumbs(categoryId: number) {

        this.categoryService.getCategoryChildrens(categoryId).subscribe((data) => {
            // console.log(data.parent.name);
            // alert("1");
            // const breadcrumb = { categoryName: data.parent.name };
            // this.ngDynamicBreadcrumbService.updateBreadcrumbLabels(breadcrumb);
        });
    }

}
