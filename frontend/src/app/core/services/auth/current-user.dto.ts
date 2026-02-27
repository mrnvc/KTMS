export interface CurrentUserDto {
  userId: number;
  email: string;
  isAdmin: boolean;
  isCoach: boolean;
  isContestant: boolean;
  tokenVersion: number;
}
