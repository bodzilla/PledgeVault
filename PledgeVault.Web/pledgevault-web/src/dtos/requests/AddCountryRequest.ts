import GovernmentType from "../../enums/GovernmentType";

export interface AddCountryRequest {
  name: string;
  dateEstablished: Date | null;
  governmentType: GovernmentType;
  summary: string | null;
}

export default AddCountryRequest;
