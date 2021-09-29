import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { user } from './models/user';

@Injectable()
export class AccountService {
  constructor(private http: HttpClient) {}

  private APIurl = 'https://localhost:44302/api/account';

  getUsers() {
    return this.http.get(`${this.APIurl}`);
  }

  getUserById(id: number): Observable<user> {
    return this.http.get<user>(`${this.APIurl}/${id}`);
  }

  getUser(username: string, password: string) {
    return this.http.get(`${this.APIurl}/${username}/${password}`);
  }

  postUser(user: user) {
    return this.http.post(`${this.APIurl}`, user);
  }

  putUser(user: user) {
    return this.http.put(`${this.APIurl}`, user);
  }
}
