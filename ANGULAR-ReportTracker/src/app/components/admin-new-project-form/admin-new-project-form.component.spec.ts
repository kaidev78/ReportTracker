import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AdminNewProjectFormComponent } from './admin-new-project-form.component';

describe('AdminNewProjectFormComponent', () => {
  let component: AdminNewProjectFormComponent;
  let fixture: ComponentFixture<AdminNewProjectFormComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ AdminNewProjectFormComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(AdminNewProjectFormComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
