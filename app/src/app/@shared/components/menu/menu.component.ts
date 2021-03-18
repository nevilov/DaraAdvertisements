import { SubmenuComponent } from './../submenu/submenu.component';
import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';

@Component({
  selector: 'app-menu',
  templateUrl: './menu.component.html',
  styleUrls: ['./../../../../assets/scss/layout/__menu.scss']
})
export class MenuComponent implements OnInit {

  @ViewChild("submenu", { read: ElementRef }) SubmenuComponent!: ElementRef;
  @ViewChild("btnShowsSubmenu", { read: ElementRef }) span!: ElementRef;
  isSubmenuShown: boolean = false;

  public showSubmenu() {
    if (this.isSubmenuShown) {
      this.SubmenuComponent.nativeElement.style.display = 'none';
      this.span.nativeElement.style.transform = "rotate(180deg)";
      this.isSubmenuShown = false;
    } else {
      this.SubmenuComponent.nativeElement.style.display = 'block';
      this.span.nativeElement.style.transform = "rotate(360deg)";
      this.isSubmenuShown = true;
    }



    // this.tref.nativeElement.textContent
    // this.tref.nativeElement.textContent
    // document.querySelector("app-submenu").setAttribute("style", "background:red; dispplay: block;");
  }

  constructor() { }

  ngOnInit() {
  }

}
