import { Component, inject, OnInit } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { CommonModule } from '@angular/common';
import { HttpClient } from '@angular/common/http';
import { TooltipModule } from 'ngx-bootstrap/tooltip';


@Component({
  selector: 'app-root',
  imports: [TooltipModule],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css',
})
export class AppComponent implements OnInit {
  constructor(private http: HttpClient) {}

  title = 'client';
  users: any;

  ngOnInit() {
    this.http.get('https://localhost:5001/api/users').subscribe({
      next: (response) => (this.users = response),
      error: (err) => console.log(err),
      complete: () => console.log('Request has completed'),
    });
  }
}

interface User {
  name: string;
  age: number;
}
