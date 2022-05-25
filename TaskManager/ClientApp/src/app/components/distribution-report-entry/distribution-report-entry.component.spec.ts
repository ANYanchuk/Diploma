import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DistributionReportEntryComponent } from './distribution-report-entry.component';

describe('DistributionReportEntryComponent', () => {
  let component: DistributionReportEntryComponent;
  let fixture: ComponentFixture<DistributionReportEntryComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ DistributionReportEntryComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(DistributionReportEntryComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
