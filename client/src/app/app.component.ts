import { Component, inject, OnInit } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { CommonModule } from '@angular/common';
import { TooltipModule } from 'ngx-bootstrap/tooltip';
import { NavbarComponent } from "./navbar/navbar.component";
import { AccountService } from './services/account.service';
import { User } from '../models/user';


@Component({
  selector: 'app-root',
  imports: [RouterOutlet, TooltipModule, NavbarComponent],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css',
})
export class AppComponent implements OnInit {

  private accountService = inject(AccountService);

  ngOnInit() {
    this.setCurrentUser();
  }

  setCurrentUser() {
    const userString = localStorage.getItem("user");
    if (!userString) return;
    const currentUser: User = JSON.parse(userString);
    this.accountService.currentUser.set(currentUser);
  } 
}
