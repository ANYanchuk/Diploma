import { TestBed } from '@angular/core/testing';

import { ReportFormatsService } from './report-formats.service';

describe('ReportFormatsService', () => {
  let service: ReportFormatsService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(ReportFormatsService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
