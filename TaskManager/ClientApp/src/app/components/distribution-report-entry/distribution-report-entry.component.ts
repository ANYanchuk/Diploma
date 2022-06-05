import { Component, Input, OnInit } from '@angular/core';

@Component({
  selector: 'app-distribution-report-entry',
  templateUrl: './distribution-report-entry.component.html',
  styleUrls: ['./distribution-report-entry.component.scss'],
})
export class DistributionReportEntryComponent implements OnInit {
  @Input() user: any;

  @Input() distr = false;

  columnsNonDistr = [
    'name',
    'dateFrom',
    'dateTo',
    'dateCompleted',
    'status',
    'report',
  ];
  columnsDistr = ['name', 'description'];

  columns: any[] = [];

  constructor() {}

  ngOnInit(): void {
    if (this.distr) {
      this.columns = this.columnsDistr;
    } else {
      this.columns = this.columnsNonDistr;
    }
  }
}
