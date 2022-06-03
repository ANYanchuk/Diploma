import { Component, OnInit } from '@angular/core';
import { UserRole } from '../../models/user.model';

const progressReportEntries = [
  {
    name: 'Зробити дипломний проєкт',
    dateFrom: '23-05-2022',
    dateTo: '25-05-2022',
    type: 'Коллективне',
    reports: [
      {
        createdDate: '23-05-2022',
        type: 'Файловий',
        text: 'Текст звіту',
        file: 'Звіт.docx',
        user: { id: 1, firstName: 'Андрій', lastName: 'Янчук', role: UserRole.LEAD },
        status: 'Виконано',
      },
      {
        createdDate: '25-05-2022',
        type: 'Файловий',
        text: 'Текст звіту',
        file: 'Звіт.docx',
        user: { id: 1, firstName: 'Владислав', lastName: 'Григорович', role: UserRole.LEAD },
        status: 'Перевіряється',
      },
    ],
  },
  {
    name: 'Перевірити дипломний проєкт',
    dateFrom: '25-05-2022',
    dateTo: '25-05-2022',
    type: 'Коллективне',
    reports: [
      {
        user: { id: 1, firstName: 'Ірина', lastName: 'Ушакова', role: UserRole.LEAD },
        status: 'Виконується',
      },
      {
        user: { id: 1, firstName: 'Людмила', lastName: 'Знахур', role: UserRole.LEAD },
        status: 'Виконується',
      },
    ],
  },
];

@Component({
  selector: 'app-progress-report',
  templateUrl: './progress-report.component.html',
  styleUrls: ['./progress-report.component.scss'],
})
export class ProgressReportComponent implements OnInit {
  progressReportEntries = progressReportEntries;

  constructor() {}

  ngOnInit(): void {}
}
