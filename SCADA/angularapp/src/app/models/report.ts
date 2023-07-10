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
    Id: number;
    Type: AlarmType;
    Priority: AlarmPriority;
    Limit: number;
    Date: Date;
    AnalogInputId: number;
    AnalogInputDescription: string;
    AnalogInputLowLimit: number;
    AnalogInputHighLimit: number;
    AnalogInputUnit: string;
  }