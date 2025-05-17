import { Component, HostListener, inject, OnInit, ViewChild } from '@angular/core';
import { Member } from '../../../../models/member';
import { AccountService } from '../../../services/account.service';
import { MembersService } from '../../../services/members.service';
import { TabsModule } from 'ngx-bootstrap/tabs';
import { FormsModule, NgForm } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { AlertComponent } from 'ngx-bootstrap/alert';

@Component({
  selector: 'app-member-edit',
  imports: [TabsModule, FormsModule, AlertComponent ],
  templateUrl: './member-edit.component.html',
  styleUrl: './member-edit.component.css'
})
export class MemberEditComponent implements OnInit{

  public member?: Member;
  private accountService = inject(AccountService);
  private memberService = inject(MembersService);
  private toastr = inject(ToastrService);
  @ViewChild("memberEditForm") memberEditForm?: NgForm;
  @HostListener('window:beforeunload', ['$event']) unloadNotification($event: any) {
    if (this.memberEditForm?.dirty) {
      $event.returnValue = true;
    }
  };

  appendIntro() {
    if (this.member) {
      this.member.introduction += "hihi";
    }
  }

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

  updateMember() {
    console.log(this.member);
    this.toastr.success("Profile updated successfully");
    this.memberEditForm?.reset(this.member);
  }

}
