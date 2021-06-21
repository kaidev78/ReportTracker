import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AdminIssueEditComponent } from './admin-issue-edit.component';

describe('AdminIssueEditComponent', () => {
  let component: AdminIssueEditComponent;
  let fixture: ComponentFixture<AdminIssueEditComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ AdminIssueEditComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(AdminIssueEditComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
