import { ErrandType, ReportFormat } from '../models/errand.model';

export interface CreateErrandDto {
  users: { id: number }[];
  title: string;
  body: string;
  type: ErrandType;
  deadline: Date;
  reportFormat: ReportFormat;
}
