import { Component, Input, OnInit } from '@angular/core';
import { Errand } from '../../models/errand.model';

@Component({
  selector: 'app-progress-report-entry',
  templateUrl: './progress-report-entry.component.html',
  styleUrls: ['./progress-report-entry.component.scss'],
})
export class ProgressReportEntryComponent implements OnInit {
  @Input() entry: Errand;
  dataSource = [];
  columns = ['executorName', 'status', 'reportType', 'completedDate'];
  constructor() {}

  ngOnInit(): void {
    this.dataSource = this.entry.users;
  }
}
