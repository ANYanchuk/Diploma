import { Component, OnInit } from '@angular/core';
import { UserRole } from '../../models/user.model';
import { errands } from '../../mock-data/errands';

const users = [
  { id: 1, firstName: 'Андрій', lastName: 'Янчук', role: UserRole.LEAD },
  { id: 1, firstName: 'Владислав', lastName: 'Григорович', role: UserRole.LECTURER },
];

@Component({
  selector: 'app-distribution-report',
  templateUrl: './distribution-report.component.html',
  styleUrls: ['./distribution-report.component.scss'],
})
export class DistributionReportComponent implements OnInit {
  users = users;
  errands = errands;

  constructor() {}

  ngOnInit(): void {}
}
