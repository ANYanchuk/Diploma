import { Component, Input, OnInit } from '@angular/core';
import { UserRole } from '../models/user.model';

const ERRANDS = [
  {
    name: 'Організувати день відкритих дверей',
    dateFrom: '28-04-2001',
    dateTo: '28-04-2001',
    type: 'Коллективне',
    status: 'Перевіряється',
    description:
      'Провести організацію дня відкритих дверей з нагоди перемоги України у війні та напливу студентів, які не складали ЗНО в цьому році. Підготувати презентації з наступніх предметів для спеціальностей кафедри: аглоритми та структури даних, тестування, комп’ютерні мережі. Завантажити презентації у звіт',
    executors: [
      { id: 1, firstName: 'Андрій', lastName: 'Янчук', role: UserRole.LEAD },
      { id: 1, firstName: 'Влад', lastName: 'Григорович', role: UserRole.LEAD },
    ],
    report: {
      text: 'Текст звіту',
      file: 'Звіт.docx',
    },
  },
  {
    name: 'Назва доручення',
    dateFrom: '28-04-2002',
    dateTo: '28-04-2002',
    type: 'Індивідуальне',
    status: 'Виконується',
    description: 'Опис доручення',
    executors: [{ id: 1, firstName: 'Андрій', lastName: 'Янчук', role: UserRole.LEAD }],
  },
];

@Component({
  selector: 'app-errands',
  templateUrl: './errands.component.html',
  styleUrls: ['./errands.component.css'],
})
export class ErrandsComponent implements OnInit {
  errands = ERRANDS;
  constructor() {}

  ngOnInit(): void {}
}
