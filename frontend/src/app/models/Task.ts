export interface Task {
  id?: string;
  userId?: string;
  title: string;
  details: string;
  creationDate?: string;
  editDate?: string;
  completed: boolean;
}