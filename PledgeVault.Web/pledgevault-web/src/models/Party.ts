import { IResponse } from "../contracts/IResponse";

export interface Party extends IResponse {
  name: string;
  dateEstablished: Date | null;
  logoUrl: string;
  siteUrl: string;
  summary: string;
}
