import { HttpClient } from '@angular/common/http';
import { Component, inject } from '@angular/core';

@Component({
  selector: 'app-test-errors',
  imports: [],
  templateUrl: './test-errors.component.html',
  styleUrl: './test-errors.component.css',
})
export class TestErrorsComponent {
  baseUrl = 'https://localhost:5001/api/';
  private http = inject(HttpClient);

  get404Error() {
    this.http.get(this.baseUrl + 'TestError/not-found').subscribe({
      next: (res) => {
        console.log(res);
      },
      error: (error) => {
        console.log(error);
      },
      complete: () => {
        console.log("completed 'not-found' test");
      },
    });
  }
  get500Error() {
    this.http.get(this.baseUrl + 'TestError/server-error').subscribe({
      next: (res) => {
        console.log(res);
      },
      error: (error) => {
        console.log(error);
      },
      complete: () => {
        console.log("completed 'server-error' test");
      },
    });
  }
  get401Error() {
    this.http.get(this.baseUrl + 'TestError/auth').subscribe({
      next: (res) => {
        console.log(res);
      },
      error: (error) => {
        console.log(error);
        
      },
      complete: () => {
        console.log("completed 'auth' test");
      },
    });
  }
  get400Error() {
    this.http.get(this.baseUrl + 'TestError/bad-request').subscribe({
      next: (res) => {
        console.log(res);
      },
      error: (error) => {
        console.log(error);
      },
      complete: () => {
        console.log("completed 'bad-request' test");
      },
    });
  }
  get400ValidationError() {
    this.http.post(this.baseUrl + 'account/register', {}).subscribe({
      next: (res) => {
        console.log(res);
      },
      error: (error) => {
        console.log(error);
      },
      complete: () => {
        console.log("completed 'register-validation' test");
      },
    });
  }
}
