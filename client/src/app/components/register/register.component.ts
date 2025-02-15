import { Component, inject, input, output } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { AccountService } from '../../services/account.service';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-register',
  imports: [FormsModule],
  templateUrl: './register.component.html',
  styleUrl: './register.component.css',
})
export class RegisterComponent {
  private toastr = inject(ToastrService);
  cancelRegistration = output<void>();
  accountService = inject(AccountService);
  model: any = {};

  register() {
    this.accountService
      .register(this.model.username, this.model.password)
      .subscribe({
        next: (x) => {
          console.log(x);
          this.cancel();
        },
        error: (err) => {
          console.log(err);
          this.toastr.error(err.error)
        },
      });

    console.log(this.model);
  }

  cancel() {
    this.cancelRegistration.emit();
  }
}
