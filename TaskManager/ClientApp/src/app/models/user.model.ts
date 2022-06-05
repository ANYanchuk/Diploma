import { Errand } from './errand.model';

export interface User {
  id: number;
  firstName: string;
  lastName: string;
  middleName: string;
  phoneNumber: string;
  email: string;
  role: UserRole;
  errands: Errand[] | null;
}

export enum UserRole {
  LECTURER = 'Викладач',
  LEAD = 'Завідувач',
}
