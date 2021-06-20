import { ComponentFixture, TestBed } from '@angular/core/testing';

import { IssueAdminPanelComponent } from './issue-admin-panel.component';

describe('IssueAdminPanelComponent', () => {
  let component: IssueAdminPanelComponent;
  let fixture: ComponentFixture<IssueAdminPanelComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ IssueAdminPanelComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(IssueAdminPanelComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
