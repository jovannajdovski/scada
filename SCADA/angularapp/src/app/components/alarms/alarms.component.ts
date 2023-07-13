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

  createAlarmForm: CreateAlarm = {
    analogInputId: 0,
    priority: 0,
    type: 0,
    limit: 0
  };

  createAlarm() {
    const url = '/api/alarms';
    if (this.createAlarmForm.priority.toString() === "1") {
      this.createAlarmForm.priority = 1;
    }
    if (this.createAlarmForm.priority.toString() === "0") {
      this.createAlarmForm.priority = 0;
    }
    if (this.createAlarmForm.priority.toString() === "2") {
      this.createAlarmForm.priority = 2;
    }
    if (this.createAlarmForm.type.toString() === "1") {
      this.createAlarmForm.type = 1;
    }
    if (this.createAlarmForm.type.toString() === "0") {
      this.createAlarmForm.type = 0;
    }
    this.http.post<Alarm>(url, this.createAlarmForm).subscribe((response) => {
      console.log('Alarm created successfully:', response);
      this.loadAlarms();
      this.showPopup = false;
    }, (error) => {
      console.error('Error creating alarm:', error);
    });
    // Reset the form inputs
    this.createAlarmForm = {
      analogInputId: 0,
      priority: 0,
      type: 0,
      limit: 0
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

export interface CreateAlarm {
  analogInputId: number;
  type: number;
  priority: number;
  limit: number;
}
