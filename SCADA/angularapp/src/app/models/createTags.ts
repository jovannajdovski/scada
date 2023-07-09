export interface AnalogInputCreateDTO {
    description: string;
    scanTime: number;
    lowLimit: number;
    highLimit: number;
  unit: string;
  AddressId: number;
  }

  export interface AnalogOutputCreateDTO {
    description: string;
    initialValue: number;
    lowLimit: number;
    highLimit: number;
    unit: string;
    AddressId: number;
  }

  export interface DigitalInputCreateDTO {
    description: string;
    scanTime: number;
    AddressId: number;
  }

  export interface DigitalOutputCreateDTO {
    description: string;
    initialValue: boolean;
    AddressId: number;
  }
