import { Component, inject, OnInit } from '@angular/core';
import { Member } from '../../../../models/member';
import { AccountService } from '../../../services/account.service';
import { MembersService } from '../../../services/members.service';
import { TabsModule } from 'ngx-bootstrap/tabs';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-member-edit',
  imports: [TabsModule, FormsModule ],
  templateUrl: './member-edit.component.html',
  styleUrl: './member-edit.component.css'
})
export class MemberEditComponent implements OnInit{

  public member?: Member;
  private accountService = inject(AccountService);
  private memberService = inject(MembersService);
  

  ngOnInit(): void {
    const user = this.accountService.currentUser();

    if (!user) return;
  
    const member = this.memberService
    .getMember(user.username)
    .subscribe({
      next: member => {
        this.member = member;
      },
      error: error => {
        console.log(error);
      },
      complete: () => {
        console.log("get member complete");
      }
    })
  }

}
