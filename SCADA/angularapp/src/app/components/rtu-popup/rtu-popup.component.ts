import { Component, EventEmitter, Output } from '@angular/core';

@Component({
  selector: 'app-rtu-popup',
  templateUrl: './rtu-popup.component.html',
  styleUrls: ['./rtu-popup.component.css']
})
export class RtuPopupComponent {


  formData: any = {};

  @Output() submit: EventEmitter<any> = new EventEmitter();
  @Output() close: EventEmitter<any> = new EventEmitter();

  errorMessage: string = '';

  onSubmit() {
    console.log("On submit");
    if (this.validateFields()) {
      this.submit.emit(this.formData);
    }
    
  }

  validateFields(): boolean {
    const number1 = parseFloat(this.formData.LowLimit);
    const number2 = parseFloat(this.formData.HighLimit);
    const number3 = parseInt(this.formData.AddressId);
    if (isNaN(number1) || isNaN(number2)) {
      this.errorMessage = 'Number 1 and Number 2 must be valid double numbers.';
      return false;
    }
    if (isNaN(number3) || number3 < 10 || number3 > 100) {
      this.errorMessage = 'Number 3 must be a valid integer between 10 and 100.';
      return false;
    }
    this.errorMessage = '';
    return true;
  }

  onClose() {
    this.close.emit();
  }
}
