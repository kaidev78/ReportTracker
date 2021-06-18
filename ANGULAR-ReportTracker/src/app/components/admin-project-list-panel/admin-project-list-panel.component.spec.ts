import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AdminProjectListPanelComponent } from './admin-project-list-panel.component';

describe('AdminProjectListPanelComponent', () => {
  let component: AdminProjectListPanelComponent;
  let fixture: ComponentFixture<AdminProjectListPanelComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ AdminProjectListPanelComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(AdminProjectListPanelComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
