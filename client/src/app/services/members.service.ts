import { HttpClient } from '@angular/common/http';
import { inject, Injectable, signal } from '@angular/core';
import { environment } from '../../environments/environment';
import { Member } from '../../models/member';
import { Observable, of, tap } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class MembersService {
  private http = inject(HttpClient);
  baseUrlAddress = environment.apiUrl;
  members = signal<Member[]>([]);

  getMembers(): Observable<Member[]> {
    return this.http.get<Member[]>(this.baseUrlAddress + 'users').pipe(
      tap((members) => {
        this.members.set(members);
      })
      // Subscribers can now handle errors, or you could add a catchError here
      // to provide centralized error handling for this specific call.
    );
  }

  deleteMember(id: number) {
    return this.http.delete(this.baseUrlAddress + 'users/');
  }

  getMember(username: string): Observable<Member> {
    const member = this.members().find((x) => x.userName === username);
    return member === undefined
      ? this.http.get<Member>(this.baseUrlAddress + 'users/' + username)
      : of(member);
  }

  updateMember(member: Member) {
    return this.http.put(this.baseUrlAddress + 'users', member).pipe(
      // The tap operator is primarily for side effects
      // – performing actions without modifying the emitted value itself.
      tap(() => {
        // The function inside tap will execute upon successful HTTP PUT.
        this.members.update((currentMembers) =>
          currentMembers.map((m) =>
            m.userName === member.userName ? { ...m, ...member } : m
          )
        );
      })
    );
  }
}
