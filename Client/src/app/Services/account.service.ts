import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, ReplaySubject } from 'rxjs';
import { map } from 'rxjs/operators'
import { KeyValuePair } from '../Interfaces/KeyVakuePair';
import { User } from '../Interfaces/User';

@Injectable({
  providedIn: 'root'
})
export class AccountService {
  baseUrl = 'https://localhost:5001/api/';
  private currentUserSource = new ReplaySubject<User>(1);
  currentUser$ = this.currentUserSource.asObservable();
  username: string;
  owners = [
    "Ben",
    "Mic",
    "Aviram",
    "Moti"
  ]

  constructor(private http: HttpClient) { }

  getUsers(): Observable<KeyValuePair[]>{
    return this.http.get<KeyValuePair[]>(this.baseUrl + 'Users')
    .pipe();
  }

  login(model: any) {
    return this.http.post(this.baseUrl + 'account/login', model).pipe(
      map((response: User) => {
        const user = response;
        if (user) {
          localStorage.setItem('user', JSON.stringify(user));
          this.currentUserSource.next(user);
        }
      })
    );
  }

  setCurrentUser(user: User) {
    this.currentUserSource.next(user);
  }

  logout() {
    localStorage.removeItem('user');
    this.currentUserSource.next(null);
  }

  getUserName(): string {
    this.currentUser$.subscribe(val =>this.username = val.username);
    return this.username;
}

register(model: any) {
  return this.http.post(this.baseUrl + 'account/register', model).pipe(
    map((response: User) => {
      const user = response;
      if (user) {
        localStorage.setItem('user', JSON.stringify(user));
        this.currentUserSource.next(user);
      }
    })
  );
}


}
