import { AnalogInput } from "./tags";

export interface Alarm {
    id: number;
    type: AlarmType;
    priority: AlarmPriority;
    limit: number;
    analogInput: AnalogInput;
    date: Date;
  }
  
  export enum AlarmPriority {
    HIGH_PRIORITY = 2,
    NORMAL_PRIORITY = 1,
    LOW_PRIORITY = 0
  }
  
  export enum AlarmType {
    LOW = 'low',
    HIGH = 'high'
  }