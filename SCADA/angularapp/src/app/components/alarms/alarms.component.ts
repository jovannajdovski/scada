import { HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { AlarmPriority, AlarmTypeCreate } from '../../models/alarm';

@Component({
  selector: 'app-alarms',
  templateUrl: './alarms.component.html',
  styleUrls: ['./alarms.component.css']
})
export class AlarmsComponent {
  showPopup: boolean = false;
  AlarmPriority = AlarmPriority;
  AlarmType = AlarmTypeCreate;
  alarms: Alarm[] = [];

  constructor(private http: HttpClient, private router: Router) {
    this.loadAlarms();
  }
  logout(): void {
    this.router.navigate(['/login']);
  }
  loadAlarms() {
    this.http.get<Alarm[]>('/api/alarms').subscribe(data => {
      this.alarms = data;
    });
  }


  openPopup() {
    this.showPopup = true;
  }

  closePopup() {
    this.showPopup = false;
  }



  removeAlarm(id: number) {
    this.http.delete(`/api/alarms/delete/${id}`).subscribe(() => {
      this.loadAlarms();
    });
  }

  createAlarmForm: any = {
    AnalogInputId: 0,
    Priority: 0,
    Type: 0,
    Limit: 0
  };


  createAlarm() {
    const url = '/api/alarms';
    this.http.post<Alarm>(url, this.createAlarmForm).subscribe((response) => {
      console.log('Alarm created successfully:', response);
      this.loadAlarms();
      this.showPopup = false;
    }, (error) => {
      console.error('Error creating alarm:', error);
    });
    // Reset the form inputs
    this.createAlarmForm= {
      AnalogInputId: 0,
      Priority: 0,
      Type: 0,
      Limit: 0
    };
  }
}
export interface Alarm {
  id: number;
  type: AlarmTypeCreate;
  priority: AlarmPriority;
  limit: number;
  description: string;
}
