export interface User {
  id: number;
  firstName: string;
  lastName: string;
}

export enum UserRole {
  LECTURER = 'Викладач',
  LEAD = 'Завідувач',
}
