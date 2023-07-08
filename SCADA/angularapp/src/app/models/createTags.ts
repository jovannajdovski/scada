export interface AnalogInputCreateDTO {
    description: string;
    scanTime: number;
    lowLimit: number;
    highLimit: number;
    unit: string;
  }

  export interface AnalogOutputCreateDTO {
    description: string;
    initialValue: number;
    lowLimit: number;
    highLimit: number;
    unit: string;
  }

  export interface DigitalInputCreateDTO {
    description: string;
    scanTime: number;
  }

  export interface DigitalOutputCreateDTO {
    description: string;
    initialValue: boolean;
  }