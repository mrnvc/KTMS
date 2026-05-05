export interface CreateJudgeRequest {
  name: string;
  surname: string;
  phoneNumber: string;
  email: string;
  dateOfBirth: string;
  username: string;
  password: string;

  cityId: number;
  genderId: number;

  license: string;
  rank?: string | null;
}