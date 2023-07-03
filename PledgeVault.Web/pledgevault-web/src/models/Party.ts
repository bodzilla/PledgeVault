import { IResponse } from "../contracts/IResponse";
import { Country } from "./Country";
import { Politician } from "./Politician";

export interface Party extends IResponse {
  id: number;
  name: string;
  dateEstablished: Date | null;
  countryId: number;
  country: Country | null;
  logoUrl: string;
  siteUrl: string;
  summary: string;
  politicians: Politician[];
}
