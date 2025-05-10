import { CanActivateFn } from '@angular/router';
import { AccountService } from '../services/account.service';
import { inject } from '@angular/core';
import { ToastrService } from 'ngx-toastr';

export const authGuard: CanActivateFn = (route, state) => {
  const accountService = inject(AccountService);
  const toastr = inject(ToastrService);

  let isAuthenticated = accountService.currentUser();

  if (isAuthenticated) return true;

  toastr.error('you shall not pass!');
  
  return false;
};
