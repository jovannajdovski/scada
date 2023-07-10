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
    HIGH_PRIORITY = 'high',
    NORMAL_PRIORITY = 'normal',
    LOW_PRIORITY = 'low'
  }
  
  export enum AlarmType {
    LOW = 'low',
    HIGH = 'high'
  }