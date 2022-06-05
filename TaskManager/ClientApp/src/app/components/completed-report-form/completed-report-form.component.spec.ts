import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CompletedReportFormComponent } from './completed-report-form.component';

describe('CompletedReportFormComponent', () => {
  let component: CompletedReportFormComponent;
  let fixture: ComponentFixture<CompletedReportFormComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ CompletedReportFormComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(CompletedReportFormComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
