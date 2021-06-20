import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CreateIssueFormComponent } from './create-issue-form.component';

describe('CreateIssueFormComponent', () => {
  let component: CreateIssueFormComponent;
  let fixture: ComponentFixture<CreateIssueFormComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ CreateIssueFormComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(CreateIssueFormComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
