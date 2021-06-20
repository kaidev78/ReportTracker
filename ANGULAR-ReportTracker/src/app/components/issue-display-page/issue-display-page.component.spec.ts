import { ComponentFixture, TestBed } from '@angular/core/testing';

import { IssueDisplayPageComponent } from './issue-display-page.component';

describe('IssueDisplayPageComponent', () => {
  let component: IssueDisplayPageComponent;
  let fixture: ComponentFixture<IssueDisplayPageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ IssueDisplayPageComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(IssueDisplayPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
