import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ProgressReportEntryComponent } from './progress-report-entry.component';

describe('ProgressReportEntryComponent', () => {
  let component: ProgressReportEntryComponent;
  let fixture: ComponentFixture<ProgressReportEntryComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ProgressReportEntryComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ProgressReportEntryComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
