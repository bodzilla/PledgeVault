import { IResponse } from "../contracts/IResponse";
import ResourceType from "../enums/ResourceType";
import { Pledge } from "./Pledge";

export interface Resource extends IResponse {
  id: number;
  title: string;
  siteUrl: string;
  resourceType: ResourceType;
  summary: string;
  pledgeId: number;
  pledge: Pledge;
}
