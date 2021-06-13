import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AdminProjectPanelPageComponent } from './admin-project-panel-page.component';

describe('AdminProjectPanelPageComponent', () => {
  let component: AdminProjectPanelPageComponent;
  let fixture: ComponentFixture<AdminProjectPanelPageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ AdminProjectPanelPageComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(AdminProjectPanelPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
