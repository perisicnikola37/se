import { MouseEvent, ChangeEvent } from "react";
import { Order } from "../types/globalTypes";
export interface StatisticsResponse {
  expenses: { [date: string]: number };
  incomes: { [date: string]: number };
}

export interface DataPoint {
  name: string;
  incomes: number;
  expenses: number;
}

export interface IncomeInterface {
  id: number;
  description: string;
  amount: number;
  createdAt: string;
  incomeGroup: {
    id: number;
    name: string;
    description: string;
    createdAt: string;
    incomes: IncomeInterface[] | null;
  } | null;
  user: {
    username: string;
  } | null;
}

export interface IncomeSimplified {
  description: string;
  amount: number;
  createdAt: string;
}

export interface ReminderInterface {
  id: number;
  reminderDay: string;
  type: string;
  createdAt: string;
  active: boolean;
}

export interface CreateReminderData {
  ReminderDay: string;
  Type: string;
  Active: boolean;
}

export interface CreateBlogData {
  Text: string;
  Description: string;
}

export interface ExpenseInterface {
  id: number;
  description: string;
  amount: number;
  createdAt: string;
  expenseGroup: {
    id: number;
    name: string;
    description: string;
    createdAt: string;
    expenses: ExpenseInterface[] | null;
  } | null;
  user: {
    username: string;
  } | null;
}

export interface SimplifiedObjectInterface {
  id: number;
  description: string;
  amount: number;
  createdAt: string;
  incomeGroupId: number;
  userId: number;
  userUsername: string;
}

export interface ObjectGroupInterface {
  id: number;
  name: string;
  description: string;
  incomes: SimplifiedObjectInterface[] | null;
}

export interface User {
  id: number;
  username: string;
  email: string;
  createdAt: string;
  accountTypeEnum: number;
  accountType: string;
  expenses: Expense[];
  incomes: Income[];
  incomeGroups: IncomeInterface[];
  expenseGroups: ExpenseInterface[];
  blogs: Blog[];
  resetToken: string | null;
  resetTokenExpiration: string | null;
}

export interface Expense {
  id: number;
  description: string;
  amount: number;
  createdAt: string;
  expenseGroupId: number;
  expenseGroup: Expense | null;
  userId: number;
}

export interface ExpenseSimplified {
  description: string;
  amount: number;
  createdAt: string;
}

export interface Income {
  id: number;
  description: string;
  amount: number;
  createdAt: string;
  incomeGroupId: number;
  incomeGroup: Income | null;
  userId: number;
}

export interface Blog {
  id: number;
  description: string;
  author: string;
  text: string;
  createdAt: string;
  userId: number;
}

export interface ForgotPasswordHookInterface {
  isLoading: boolean;
  success: boolean;
  error: string | null;
  forgotPassword: (email: string) => Promise<void>;
}

export interface LoginData {
  email: string;
  password: string;
}

export interface UserSimplified {
  id: number;
  username: string;
  email: string;
  accountType: string;
  formattedCreatedAt: string;
  createdAt: string;
  token: string;
}

export interface LoginResponse {
  success: boolean;
  message: string;
  user: UserSimplified;
}

export interface ErrorResponse {
  status?: number;
  data?: {
    errorMessage: string;
  }[];
}
export interface LoginResult {
  user: User | null;
  success: boolean;
  message: string;
}

export interface RegistrationData {
  username: string;
  email: string;
  accountType: string;
  password: string;
}

export interface RegistrationResponse {
  success: boolean;
  message: string;
  username: string;
  token: string;
  id: number;
  email: string;
  accountType: string;
  formattedCreatedAt: string;
}

export interface UserUsername {
  username: string;
}

export interface BlogInterface {
  id: number;
  description: string;
  author: string;
  text: string;
  createdAt: string;
  userId: number;
  user: UserUsername;
}

export type BlogType = {
  id: number;
  text: string;
  author: string;
  createdAt?: Date;
  description: string;
};

export interface FetchObjectsParams {
  pageSize: number;
  description?: string | null;
  minAmount?: number | null;
  maxAmount?: number | null;
  incomeGroupId?: string | null;
}

export interface ObjectData {
  name: string;
  description: string;
}

export interface CreateObjectDataInterface {
  description: string;
  amount: number;
  incomeGroupId?: number;
  expenseGroupId?: number;
}

export interface GetObjectInterface {
  description: string;
  amount: number;
  incomeGroupId?: number;
  expenseGroupId?: number;
}

export interface UpdateObjectGroupInterface {
  id: number;
  name: string;
  description: string;
}

export interface UpdateObjectInterface {
  id: number;
  description: string;
  amount: number;
  incomeGroupId?: number;
  expenseGroupId?: number;
}

export interface ObjectGroupDataInterface {
  name: string;
  description: string;
  expenses?: Array<{ id: number; description: string; amount: number }>;
  incomes?: Array<{ id: number; description: string; amount: number }>;
}

export interface ExportToEmailHookResult {
  isLoading: boolean;
  success: boolean;
  error: string | null;
  exportToEmail: (email: string) => Promise<void>;
}

export interface MailchimpSubscribeHookResult {
  isLoading: boolean;
  success: boolean;
  error: string | null;
  subscribeToMailchimp: (email: string) => Promise<void>;
}

export interface MailChimpProps {
  imageUrl: string;
}

export interface EmailExportProps {
  imageUrl: string;
}

export interface ResetPasswordHookInterface {
  isLoading: boolean;
  success: boolean;
  error: string | null;
  resetPassword: (newPassword: string) => Promise<void>;
}

export interface EnhancedTableProps {
  numSelected: number;
  onRequestSort: (event: MouseEvent<unknown>, property: keyof Data) => void;
  onSelectAllClick: (event: ChangeEvent<HTMLInputElement>) => void;
  order: Order;
  orderBy: keyof Data;
  rowCount: number;
}

export interface Data {
  id: number;
  name: string;
  description: string;
}
