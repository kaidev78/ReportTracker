import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ProductIssuePageComponent } from './product-issue-page.component';

describe('ProductIssuePageComponent', () => {
  let component: ProductIssuePageComponent;
  let fixture: ComponentFixture<ProductIssuePageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ProductIssuePageComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ProductIssuePageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
