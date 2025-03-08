import { HttpClient } from '@angular/common/http';
import { inject, Injectable, signal } from '@angular/core';
import { User } from '../../models/user';
import { map } from 'rxjs';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class AccountService {

  currentUser = signal<User | null>(null);
  
  private http: HttpClient = inject(HttpClient);
  
  private url: string = environment.apiUrl

  public login(username: string, password: string) {
    const loginUrl = this.url + "account/login"
    return this.http.post<User>(loginUrl, { username, password })
    .pipe(
      map(user => {
        if (user) {
          
          localStorage.setItem("user", JSON.stringify(user));
          this.currentUser.set(user);
        }
      }));
    }
    
    public logout() {
    localStorage.removeItem("user");
    this.currentUser.set(null);
  }

  public register(username: string, password: string) {
    const loginUrl = this.url + "account/register"
    return this.http.post<User>(loginUrl, { username, password })
    .pipe(
      map(user => {
        if (user) {
          localStorage.setItem("user", JSON.stringify(user));
          this.currentUser.set(user);
        }

        return user;
      }));
    }
  
}
