import { IResponse } from "../contracts/IResponse";
import SexType from "../enums/SexType";
import { Party } from "./Party";
import { Pledge } from "./Pledge";

export interface Politician extends IResponse {
  id: number;
  name: string;
  sexType: SexType;
  dateOfBirth: Date;
  dateOfDeath: Date | null;
  countryOfBirth: string;
  partyId: number;
  party: Party | null;
  position: string;
  isPartyLeader: boolean;
  photoUrl: string;
  summary: string;
  pledges: Pledge[];
}
