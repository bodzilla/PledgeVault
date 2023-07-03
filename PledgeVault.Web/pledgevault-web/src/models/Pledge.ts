import { IResponse } from "../contracts/IResponse";
import PledgeCategoryType from "../enums/PledgeCategoryType";
import PledgeStatusType from "../enums/PledgeStatusType";
import { Politician } from "./Politician";
import { Resource } from "./Resource";

export interface Pledge extends IResponse {
  id: number;
  title: string;
  datePledged: Date;
  dateFulfilled: Date | null;
  pledgeCategoryType: PledgeCategoryType;
  pledgeStatusType: PledgeStatusType;
  politicianId: number;
  politician: Politician | null;
  summary: string;
  fulfilledSummary: string;
  score: number;
  resources: Resource[];
}
