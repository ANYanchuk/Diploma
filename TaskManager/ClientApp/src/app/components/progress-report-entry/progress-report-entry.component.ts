import { Component, Input, OnInit } from '@angular/core';

@Component({
  selector: 'app-progress-report-entry',
  templateUrl: './progress-report-entry.component.html',
  styleUrls: ['./progress-report-entry.component.css'],
})
export class ProgressReportEntryComponent implements OnInit {
  @Input() errand: any;
  constructor() {}

  ngOnInit(): void {}
}
