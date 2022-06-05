import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ReportFormatsComponent } from './report-formats.component';

describe('ReportFormatsComponent', () => {
  let component: ReportFormatsComponent;
  let fixture: ComponentFixture<ReportFormatsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ReportFormatsComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ReportFormatsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
