export interface UpdateJudgeRequest {
  name: string;
  surname: string;
  phoneNumber: string;
  email: string;
  dateOfBirth: string;
  username: string;

  cityId: number;
  genderId: number;

  license: string;
  rank?: string | null;
}