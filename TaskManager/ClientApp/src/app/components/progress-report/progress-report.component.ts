import { Component, OnInit } from '@angular/core';
import { errands } from '../mock-data/errands';

@Component({
  selector: 'app-progress-report',
  templateUrl: './progress-report.component.html',
  styleUrls: ['./progress-report.component.css'],
})
export class ProgressReportComponent implements OnInit {
  errands = errands;

  constructor() {}

  ngOnInit(): void {}
}
