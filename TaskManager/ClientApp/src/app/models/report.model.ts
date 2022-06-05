import {Errand} from "./errand.model";

export interface Report {
  id: number;
  errandId: number;
  errand: Errand;
  lastChanged: Date;
  files: any[];
  comment: string;
}
