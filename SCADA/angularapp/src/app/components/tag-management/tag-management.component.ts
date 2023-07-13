import { HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { AnalogInputCreateDTO, AnalogOutputCreateDTO, DigitalInputCreateDTO, DigitalOutputCreateDTO } from 'src/app/models/createTags';
import { AnalogInput, AnalogOutput, DigitalInput, DigitalOutput } from 'src/app/models/tags';

@Component({
  selector: 'app-tag-management',
  templateUrl: './tag-management.component.html',
  styleUrls: ['./tag-management.component.css']
})
export class TagManagementComponent {

  showAddTagPopup: boolean = false;
  showRtuPopup: boolean = false;
  showUpdateOutputValuePopup: boolean = false;
  tagType: string = '';

  digitalInputs: DigitalInput[] = [];
  digitalOutputs: DigitalOutput[] = [];
  analogInputs: AnalogInput[] = [];
  analogOutputs: AnalogOutput[] = [];

  constructor(private http: HttpClient, private router: Router) {
    this.loadDigitalInputs();
    this.loadDigitalOutputs();
    this.loadAnalogInputs();
    this.loadAnalogOutputs();
  }
  logout(): void {
    this.router.navigate(['/login']);
  }
  loadDigitalInputs() {
    this.http.get<DigitalInput[]>('/api/Tag/DigitalInputs').subscribe(data => {
      this.digitalInputs = data;
    });
  }

  loadDigitalOutputs() {
    this.http.get<DigitalOutput[]>('/api/Tag/DigitalOutputs').subscribe(data => {
      this.digitalOutputs = data;
    });
  }

  loadAnalogInputs() {
    this.http.get<AnalogInput[]>('/api/Tag/AnalogInputs').subscribe(data => {
      this.analogInputs = data;
    });
  }

  loadAnalogOutputs() {
    this.http.get<AnalogOutput[]>('/api/Tag/AnalogOutputs').subscribe(data => {
      console.log(data);
      this.analogOutputs = data;
    });
  }


  openRtuPopup() {
    this.showRtuPopup = true;
  }

  closeRtuPopup() {
    this.showRtuPopup = false;
  }

  addRTU(formData: any) {
    this.http.post('/scada/rtu', formData).subscribe(
      response => {
        console.log("Successfull");
      },
      error => {
        console.error('An error occurred during creating rtu:', error);
      }
    );
    this.closeRtuPopup();
  }
  openAddTagPopup(tagType: string) {
    this.tagType = tagType;
    this.showAddTagPopup = true;
  }


  closeAddTagPopup() {
    this.showAddTagPopup = false;
  }

  openUpdateOutputValuePopup(tagType: string) {
    this.tagType = tagType;
    this.showUpdateOutputValuePopup = true;
  }

  closeUpdateOutputValuePopup() {
    this.showUpdateOutputValuePopup = false;
  }


  deleteDigitalInput(id: number) {
    this.http.delete(`/api/Tag/DigitalInputs/${id}`).subscribe(() => {
      this.loadDigitalInputs();
    });
  }

  deleteDigitalOutput(id: number) {
    this.http.delete(`/api/Tag/DigitalOutputs/${id}`).subscribe(() => {
      this.loadDigitalOutputs();
    });
  }

  deleteAnalogInput(id: number) {
    this.http.delete(`/api/Tag/AnalogInputs/${id}`).subscribe(() => {
      this.loadAnalogInputs();
    });
  }

  deleteAnalogOutput(id: number) {
    this.http.delete(`/api/Tag/AnalogOutputs/${id}`).subscribe(() => {
      this.loadAnalogOutputs();
    });
  }


  turnDigitalInput(id: number) {
    const digitalInput = this.digitalInputs.find(d => d.id === id);
    if (!digitalInput) {
      return;
    }
  
    const isScanning = !digitalInput.isScanning;
    const scan: any = {
      isScanning: isScanning
    };
    this.http.put(`/api/Tag/DigitalInputs/${id}/IsScanning`, scan).subscribe(() => {
      digitalInput.isScanning = isScanning;
    });
  }

  turnAnalogInput(id: number) {
    const analogInput = this.analogInputs.find(d => d.id === id);
    if (!analogInput) {
      return;
    }
  
    const isScanning = !analogInput.isScanning;
    const scan: any = {
      isScanning: isScanning
    };
    this.http.put(`/api/Tag/AnalogInputs/${id}/IsScanning`, scan).subscribe(() => {
      analogInput.isScanning = isScanning;
    });
  }


  analogInputForm: AnalogInputCreateDTO = {
    description: '',
    scanTime: 0,
    lowLimit: 0,
    highLimit: 0,
    unit: '',
    AddressId: 10
  };
  analogOutputForm: AnalogOutputCreateDTO = {
    description: '',
    initialValue: 0,
    lowLimit: 0,
    highLimit: 0,
    unit: '',
    AddressId: 10
  };
  digitalInputForm: DigitalInputCreateDTO = {
    description: '',
    scanTime: 0,
    AddressId: 10
  };
  digitalOutputForm: DigitalOutputCreateDTO = {
    description: '',
    initialValue: false,
    AddressId: 10
  };

  createAnalogInput() {
    const url = '/api/Tag/AnalogInputs';
    this.http.post<AnalogInput>(url, this.analogInputForm).subscribe((response) => {
      console.log('Analog Input tag created successfully:', response);
      this.analogInputs.push(response); // Update the analogInputs list with the new tag
      this.showAddTagPopup = false;
    }, (error) => {
      console.error('Error creating Analog Input tag:', error);
    });
    // Reset the form inputs
    this.analogInputForm = {
      description: '',
      scanTime: 0,
      lowLimit: 0,
      highLimit: 0,
      unit: '',
      AddressId: 0
    };
  }

  createAnalogOutput() {
    const url = '/api/Tag/AnalogOutputs';
    this.http.post<AnalogOutput>(url, this.analogOutputForm).subscribe((response) => {
      console.log('Analog Output tag created successfully:', response);
      this.analogOutputs.push(response); // Update the analogOutputs list with the new tag
      this.showAddTagPopup = false;
    }, (error) => {
      console.error('Error creating Analog Output tag:', error);
    });
    // Reset the form inputs
    this.analogOutputForm = {
      description: '',
      initialValue: 0,
      lowLimit: 0,
      highLimit: 0,
      unit: '',
      AddressId: 0
    };
  }

  createDigitalInput() {
    const url = '/api/Tag/DigitalInputs';
    this.http.post<DigitalInput>(url, this.digitalInputForm).subscribe((response) => {
      console.log('Digital Input tag created successfully:', response);
      this.digitalInputs.push(response); // Update the digitalInputs list with the new tag
      this.showAddTagPopup = false;
    }, (error) => {
      console.error('Error creating Digital Input tag:', error);
    });
    // Reset the form inputs
    this.digitalInputForm = {
      description: '',
      scanTime: 0,
      AddressId: 0
    };
  }

  createDigitalOutput() {
    const url = '/api/Tag/DigitalOutputs';
    this.http.post<DigitalOutput>(url, this.digitalOutputForm).subscribe((response) => {
      console.log('Digital Output tag created successfully:', response);
      this.digitalOutputs.push(response); // Update the digitalOutputs list with the new tag
      this.showAddTagPopup = false;
    }, (error) => {
      console.error('Error creating Digital Output tag:', error);
    });
    // Reset the form inputs
    this.digitalOutputForm = {
      description: '',
      initialValue: false,
      AddressId: 0
    };
  }
  changeDigitalOutput(digitalOutput: DigitalOutput) {
    if (digitalOutput.lastValue === 'true' || digitalOutput.lastValue === 'false') {
      const payload = {
        id: digitalOutput.id,
        value: digitalOutput.lastValue === 'true'? 'false':'true'
      };
      this.http.post("/api/Tag/DigitalOutputs/value", payload)
        .subscribe(
          (data: any) => {
            digitalOutput.lastValue = digitalOutput.lastValue === 'true' ? 'false' : 'true';
          },
          (error: any) => {
            console.error("Error:", error);
          }
        ); 
    }
  }
  plus(analogOutput: AnalogOutput) {
    const value: number = parseFloat(analogOutput.lastValue);

    if (!isNaN(value) && value < analogOutput.highLimit) {
      const payload = {
        id: analogOutput.id,
        value: (value + 1).toString()
      };
      this.http.post("/api/Tag/AnalogOutputs/value", payload)
        .subscribe(
          (data: any) => {
            analogOutput.lastValue = (value + 1).toString();
          },
          (error: any) => {
            console.error("Error:", error);
          }
        ); 
    }
  }
  minus(analogOutput: AnalogOutput) {
    const value: number = parseFloat(analogOutput.lastValue);

    if (!isNaN(value) && value > analogOutput.lowLimit) {
      const payload = {
        id: analogOutput.id,
        value: (value - 1).toString()
      };
      this.http.post("/api/Tag/AnalogOutputs/value", payload)
        .subscribe(
          (data: any) => {
            analogOutput.lastValue = (value - 1).toString();
          },
          (error: any) => {
            console.error("Error:", error);
          }
        );
    }
  }

  toReports() {
    this.router.navigate(['/reports']);
  }

  toAlarms() {
    this.router.navigate(['/alarms']);
  }
}
