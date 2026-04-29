export interface CreateContestantRequest {
  name: string;
  surname: string;
  phoneNumber: string;
  email: string;
  dateOfBirth: string;
  username: string;
  password: string;

  cityId: number;
  genderId: number;
  beltId: number;
  clubId: number;
}