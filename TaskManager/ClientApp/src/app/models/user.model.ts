export interface User {
  Id: number;
  FirstName: string;
  LastName: string;
  MiddleName: string;
  PhoneNumber: string;
  Email: string;
  Role: UserRole;
}

export enum UserRole {
  LECTURER = 'Викладач',
  LEAD = 'Завідувач',
}
