export interface RegisterRequest {
  roleId: number;
  cityId: number;
  genderId: number;
  name: string;
  surname: string;
  phoneNumber: string;
  email: string;
  dateOfBirth: string; // DateOnly → YYYY-MM-DD
  username: string;
  password: string;
}
