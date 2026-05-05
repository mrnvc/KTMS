export interface Judge {
  id: number;
  userId: number;

  name: string;
  surname: string;
  phoneNumber: string;
  email: string;

  license: string;
  rank?: string | null;
}