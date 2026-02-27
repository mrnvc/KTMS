export interface Tournament {
  location: string;
  title: string;
  date: string;          // "2024-04-05"
  startTime: string;     // "09:30:00"
  endTime?: string | null;
  description?: string | null;
  registrationFee: string;
  status: string;        // "Active" | "Completed" | ...
}
