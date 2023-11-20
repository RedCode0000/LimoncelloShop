import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { CookieService } from 'ngx-cookie-service';
import { CookieServiceAPI } from './services/cookie-service-api.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {
  title = 'LimoncelloShop';

  private cookie_name = '';
  private all_cookies: any = '';
  badgeValue: number = 5;

  constructor(private router: Router, private cookieService: CookieService, private cookieServiceAPI: CookieServiceAPI) { }

  getName() {
    return localStorage.getItem('firstName') + ' ' + localStorage.getItem('lastName');
  }

  loggedin() {
    //return LoginService.loggedin();
    return "";
  }

  logout(): void {
    localStorage.clear();
    this.router.navigateByUrl('');
  }

  getRole(): string {
    return localStorage.getItem('role')!;
  }

  setCookie(): void {
    this.cookieServiceAPI.createCookie().subscribe({
      next: (x) => {
        this.cookieService.set(x.key, x.value, { expires: 30 })
        this.showCookieDeclaration();
      },
      error: (error) => {
        console.error('Error creating cookie:', error);
      }
    });
  }

  deleteCookie() {
    this.cookieService.delete('Lemonbro\'s');
  }

  deleteAll() {
    this.cookieService.deleteAll();
  }

  showCookieDeclaration() {
    document.getElementById("cookieDeclaration")!.style.bottom = "0px";
  }

  handleCookieResponse(x: any): void {
    if (x != '') {
      console.log('Cookie received:', x);
    } else {
      this.setCookie();
      console.log('Cookie set');
    }
  }

  getCookie(): void {
    var x = this.cookieService.get('Lemonbros');
    this.handleCookieResponse(x);

  }

  closeCookieDeclaration() {
    document.getElementById("cookieDeclaration")!.style.bottom = "-200px";
  }


  ngOnInit(): void {
    // this.cookie_name = this.cookieService.get('name');
    // this.all_cookies = this.cookieService.getAll();  // get all cookies object
    this.getCookie();
  }
}





