import { UserRole } from '../models/user.model';

export const errands = [
  {
    name: 'Організувати день відкритих дверей',
    dateCreated: '28-04-2001',
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
    name: 'Провести лекцію',
    dateCreated: '28-04-2001',
    dateFrom: '28-04-2002',
    dateTo: '28-04-2002',
    type: 'Індивідуальне',
    status: 'Виконується',
    description: 'Провести лекцію для школярів',
    executors: [{ id: 1, firstName: 'Андрій', lastName: 'Янчук', role: UserRole.LEAD }],
  },
  {
    name: 'Заповнити анкету',
    dateCreated: '28-04-2001',
    dateFrom: '28-04-2002',
    dateTo: '28-04-2002',
    type: 'Індивідуальне',
    status: 'Виконується',
    description: 'Заповнити анкету',
    executors: [{ id: 1, firstName: 'Андрій', lastName: 'Янчук', role: UserRole.LEAD }],
  },
];
