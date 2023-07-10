export interface DigitalInput {
    id: number;
    description: string;
    scanTime: number;
    isScanning: boolean;
  }

  export interface DigitalOutput {
    id: number;
    description: string;
    initialValue: boolean;
  }

  export interface AnalogInput {
    id: number;
    description: string;
    scanTime: number;
    isScanning: boolean;
    lowLimit: number;
    highLimit: number;
    unit: string;
  }

  export interface AnalogOutput {
    id: number;
    description: string;
    initialValue: number;
    lowLimit: number;
    highLimit: number;
    unit: string;
  }

  export interface TagValue {
    id: number;
    tagId: number;
    date: Date;
    type: string;
    value?: string;
  }