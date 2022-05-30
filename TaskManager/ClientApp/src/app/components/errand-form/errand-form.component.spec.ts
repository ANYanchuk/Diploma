import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ErrandFormComponent } from './errand-form.component';

describe('ErrandFormComponent', () => {
  let component: ErrandFormComponent;
  let fixture: ComponentFixture<ErrandFormComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ErrandFormComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ErrandFormComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
