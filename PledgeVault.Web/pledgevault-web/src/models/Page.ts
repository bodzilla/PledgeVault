import { IResponse } from "../contracts/IResponse";

export interface Page<T extends IResponse> {
  pageNumber: number;
  pageSize: number;
  totalItems: number;
  totalPages: number;
  data: T[];
}
