import { Component, Input, OnInit } from '@angular/core';

@Component({
  selector: 'app-progress-report-entry',
  templateUrl: './progress-report-entry.component.html',
  styleUrls: ['./progress-report-entry.component.scss'],
})
export class ProgressReportEntryComponent implements OnInit {
  @Input() entry: any;
  dataSource = [];
  columns = ['executorName', 'status', 'reportType', 'completedDate'];
  constructor() {}

  ngOnInit(): void {
    this.dataSource = this.entry.reports || [];
  }
}
