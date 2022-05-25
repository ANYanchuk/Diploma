import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TrueDistributionReportComponent } from './true-distribution-report.component';

describe('TrueDistributionReportComponent', () => {
  let component: TrueDistributionReportComponent;
  let fixture: ComponentFixture<TrueDistributionReportComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ TrueDistributionReportComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(TrueDistributionReportComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
