import { IResponse } from "../contracts/IResponse";
import GovernmentType from "../enums/GovernmentType";
import { Party } from "./Party";

export interface Country extends IResponse {
  name: string;
  dateEstablished: Date | null;
  governmentType: GovernmentType;
  summary: string;
  parties: Party[];
}
