export interface UpdateContestantRequest {
  name: string;
  surname: string;
  phoneNumber: string;
  email: string;
  dateOfBirth: string;
  username: string;

  cityId: number;
  genderId: number;
  beltId: number;
  clubId: number;
}