import { AlarmPriority, AlarmType } from "./alarm";

export interface AnalogInputReportDTO {
    id: number;
    description: string;
    lowLimit: number;
    highLimit: number;
    unit: string;
    date: Date;
    value: string;
}

export interface DigitalInputReportDTO {
    id: number;
    description: string;
    date: Date;
    value: string;
}

export interface AlarmReportDTO {
    id: number;
    type: AlarmType;
    priority: AlarmPriority;
    limit: number;
    date: Date;
    analogInputId: number;
    analogInputDescription: string;
    analogInputLowLimit: number;
    analogInputHighLimit: number;
    analogInputUnit: string;
  }

  export interface TagValueReportDTO {
    id: number;
    tagId: number;
    tagType: string;
    description: string;
    date: Date;
    type: string;
    value?: string;
  }