import { Component, inject, OnInit } from '@angular/core';
import { MembersService } from '../../../services/members.service';
import { ActivatedRoute } from '@angular/router';
import { Member } from '../../../../models/member';
import { TabsModule } from 'ngx-bootstrap/tabs';
import {
  GalleryItem,
  GalleryModule,
  ImageItem,
  ImageItemData,
} from 'ng-gallery';

@Component({
  selector: 'app-member-detail',
  imports: [TabsModule, GalleryModule],
  templateUrl: './member-detail.component.html',
  styleUrl: './member-detail.component.css',
})
export class MemberDetailComponent implements OnInit {
  private memberService = inject(MembersService);
  private route = inject(ActivatedRoute);
  member: Member | undefined;
  images: GalleryItem[] = [];

  ngOnInit(): void {
    this.loadMember();
  }

  private loadMember(): void {
    const username = this.route.snapshot.paramMap.get('username');
    if (!username) return;

    this.memberService.getMember(username).subscribe({
      next: (member: Member) => {
        this.member = member;
        this.images = member.photos.map((photo) => {
          const data: ImageItemData = {
            type: 'image',
            src: photo.url,
            thumb: photo.url
          };
          return new ImageItem(data);
        });
      },
      error: (error) => console.log(error),
    });
  }
}
