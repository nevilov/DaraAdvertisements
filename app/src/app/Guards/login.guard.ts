import { CookieService } from 'ngx-cookie-service';
import { Component, Injectable } from "@angular/core";
import { CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot, Router } from "@angular/router";
import { Observable } from "rxjs";
import { retry } from 'rxjs/operators';

@Injectable({
    providedIn: 'root'
})

export class LoginGuard implements CanActivate {

    constructor(private cookieService: CookieService, private router: Router) {

    }

    canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): Observable<boolean> | boolean {
        var cookieRole = this.cookieService.get('UserRole');
        var userRole = cookieRole != "" ? this.cookieService.get('UserRole') != "" : "noRole";
        if (userRole == "noRole") {
            this.router.navigate(['/']);
            return false;
        } else {
            return true;
        }
    }
}
