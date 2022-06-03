import { User } from './user.model';

export interface Errand {
  id: number;
  title: string;
  body: string;
  reportFormat: ReportFormat;
  type: ErrandType;
  users: User[];
  started: Date;
  deadline: Date;
  state: ErrandState;
  report: any | null;
  reviewComment: string | null;
}

export enum ErrandType {
  INDIVIDUAL = 'Індивідуальне',
  COLLECTIVE = 'Колективне',
}

export enum ErrandState {
  COMPLETED = 'Виконано',
  IN_PROGRESS = 'Видано',
}

export enum ReportFormat {
  TEXT = 'Текст',
  FILE = 'Файл',
}
