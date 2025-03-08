import { Component, inject, OnInit } from '@angular/core';
import { MembersService } from '../../../services/members.service';
import { Member } from '../../../../models/member';
import { MemberCardComponent } from '../member-card/member-card.component';
@Component({
  selector: 'app-member-list',
  imports: [MemberCardComponent],
  templateUrl: './member-list.component.html',
  styleUrl: './member-list.component.css',
})
export class MemberListComponent implements OnInit {
  private memberService = inject(MembersService);

  public members: Member[] = [];

  ngOnInit(): void {
    this.loadMembers();
  }

  private loadMembers() {
    this.memberService.getMembers().subscribe({
      next: (members) => {
        this.members = members;
      },
      error: () => {},
      complete: () => {},
    });
  }
}
