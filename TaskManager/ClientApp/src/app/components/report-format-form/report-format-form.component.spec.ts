import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ReportFormatFormComponent } from './report-format-form.component';

describe('ReportFormatFormComponent', () => {
  let component: ReportFormatFormComponent;
  let fixture: ComponentFixture<ReportFormatFormComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ReportFormatFormComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ReportFormatFormComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
