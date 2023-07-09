import { HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';

@Component({
  selector: 'app-admin',
  templateUrl: './admin.component.html',
  styleUrls: ['./admin.component.css']
})
export class AdminComponent {
  constructor(private http: HttpClient) { }
  showPopup: boolean = false;

  openPopup() {
    this.showPopup = true;
  }

  closePopup() {
    this.showPopup = false;
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
    this.closePopup();
  }
}
