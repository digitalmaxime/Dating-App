import { Component, inject, OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { AccountService } from '../services/account.service';
import { BsDropdownModule } from 'ngx-bootstrap/dropdown';
import { Router, RouterLink, RouterLinkActive } from '@angular/router';
import { Toast, ToastrService } from 'ngx-toastr';
import { TitleCasePipe } from '@angular/common';

@Component({
  selector: 'app-navbar',
  imports: [FormsModule, BsDropdownModule, RouterLink, RouterLinkActive, TitleCasePipe],
  templateUrl: './navbar.component.html',
  styleUrl: './navbar.component.css',
})
export class NavbarComponent implements OnInit{
  private router = inject(Router);
  private toastr = inject(ToastrService);
ngOnInit() {
  console.log(this.accountService.currentUser())
}
  public accountService: AccountService = inject(AccountService);
  username: string = 'username';
  password: string = '';

  public login() {
    this.accountService.login(this.username, this.password).subscribe({
      next: (_) => this.router.navigateByUrl('/members'),
      error: (err) => {
        console.log(err);
        this.toastr.error(err.error)
      },
      complete: () => {
        console.log('completed POST /login');
      },
    });
  }

  public logout() {
    this.accountService.logout();
    this.router.navigateByUrl('/');
  }
}
