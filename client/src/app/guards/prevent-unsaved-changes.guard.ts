import { CanDeactivateFn } from '@angular/router';
import { MemberEditComponent } from '../components/members/member-edit/member-edit.component';

export const preventUnsavedChangesGuard: CanDeactivateFn<MemberEditComponent> = (component) => {
  return component.memberEditForm?.dirty ? confirm('Are you sure you want to continue? Any unsaved changes will be lost') : true;
  return true;
};
