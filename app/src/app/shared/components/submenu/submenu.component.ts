import { CategoryService } from './../../../services/category.service';
import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { Category } from 'src/app/Dtos/category';

@Component({
  selector: 'app-submenu',
  templateUrl: './submenu.component.html',
  styleUrls: ['./../../../../assets/scss/layout/__submenu.scss'],
})
export class SubmenuComponent implements OnInit {
  categories: Category[] = [];
  @Input() isSubMenuShown = true;
  @Output() isSubMenuShownOutput = new EventEmitter<boolean>();

  constructor(private categoryService: CategoryService) {}

  ngOnInit() {
    this.getAllCategories();
  }

  public getAllCategories() {
    this.categoryService.getAllCategories().subscribe((data) => {
      this.categories = data.categories;
    });
  }

  public closeSubMenu() {
    this.isSubMenuShown = false;
    this.isSubMenuShownOutput.emit(this.isSubMenuShown);
  }
}
