import { Component, OnInit } from '@angular/core';
import { errands } from '../../mock-data/errands';
import { UserRole } from '../../models/user.model';

const users = [
  { id: 1, firstName: 'Андрій', lastName: 'Янчук', role: UserRole.LEAD },
  { id: 1, firstName: 'Владислав', lastName: 'Григорович', role: UserRole.LECTURER },
];

@Component({
  selector: 'app-true-distribution-report',
  templateUrl: './true-distribution-report.component.html',
  styleUrls: ['./true-distribution-report.component.css'],
})
export class TrueDistributionReportComponent implements OnInit {
  users = users;
  errands = errands;
  constructor() {}

  ngOnInit(): void {}
}
