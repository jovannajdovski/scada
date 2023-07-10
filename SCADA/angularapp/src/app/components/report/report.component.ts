import { Component } from '@angular/core';
import { Alarm, AlarmPriority } from 'src/app/models/alarm';
import { AnalogInput, DigitalInput, TagValue } from 'src/app/models/tags';
import { ReportService } from 'src/app/services/report.service';

@Component({
  selector: 'app-report',
  templateUrl: './report.component.html',
  styleUrls: ['./report.component.css']
})
export class ReportComponent {
  AlarmPriority = AlarmPriority;

  alarms: Alarm[] = [];
  tagValues: TagValue[] = [];
  analogInputs: AnalogInput[] = [];
  digitalInputs: DigitalInput[] = [];
  reportType: string = '';

  constructor(private reportService: ReportService) { }

  showPopup: boolean = false;
  reportPopupType: string = '';

  closePopup() {
    this.showPopup = false;
  }

  openDialog(tagType: string) {
    this.reportPopupType = tagType;
    this.showPopup = true;
  }

  getAllAlarmsForm: any = {
    startTime: new Date(),
    endTime: new Date(),
    isAscending: false
  };
  getAlarmsByPriorityForm: any = {
    priority: AlarmPriority.NORMAL_PRIORITY,
    isAscending: false
  };
  getAllTagValuesForm: any = {
    startTime: new Date(),
    endTime: new Date(),
    isAscending: false
  };
  getLastAnalogInputsForm: any = {
    isAscending: false
  };
  getLastDigitalInputsForm: any = {
    isAscending: false
  };
  getTagValuesByIdForm: any = {
    id: 0,
    isAscending: false
  };
  


  getAllAlarms() {
    
    let startTime = this.getAllAlarmsForm.startTime;
    let endTime = this.getAllAlarmsForm.endTime;
    let isAscending =  this.getAllAlarmsForm.isAscending;

    this.reportService.getAllAlarms(startTime, endTime, isAscending).subscribe(
      (data) => {
        this.alarms = data;
        this.reportType = 'alarms';
        this.showPopup = false;
      },
      (error) => {
        console.log(error);
      }
    );
  }

  getAlarmsByPriority() {

    let priority = this.getAlarmsByPriorityForm.priority;
    let isAscending =  this.getAlarmsByPriorityForm.isAscending;

    this.reportService.getAlarmsByPriority(priority, isAscending).subscribe(
      (data) => {
        this.alarms = data;
        this.reportType = 'alarmsByPriority';
        this.showPopup = false;
      },
      (error) => {
        console.log(error);
      }
    );
  }

  getAllTagValues() {

    let startTime = this.getAllTagValuesForm.startTime;
    let endTime = this.getAllTagValuesForm.endTime;
    let isAscending =  this.getAllTagValuesForm.isAscending;

    this.reportService.getAllTagValues(startTime, endTime, isAscending).subscribe(
      (data) => {
        this.tagValues = data;
        this.reportType = 'tagValues';
        this.showPopup = false;
      },
      (error) => {
        console.log(error);
      }
    );
  }

  getLastAnalogInputs() {

    let isAscending =  this.getLastAnalogInputsForm.isAscending;

    this.reportService.getLastAnalogInputs(isAscending).subscribe(
      (data) => {
        this.analogInputs = data;
        this.reportType = 'lastAnalogInputs';
        this.showPopup = false;
      },
      (error) => {
        console.log(error);
      }
    );
  }

  getLastDigitalInputs() {

    let isAscending =  this.getLastDigitalInputsForm.isAscending;

    this.reportService.getLastDigitalInputs(isAscending).subscribe(
      (data) => {
        this.digitalInputs = data;
        this.reportType = 'lastDigitalInputs';
        this.showPopup = false;
      },
      (error) => {
        console.log(error);
      }
    );
  }

  getTagValuesById() {

    let id = this.getTagValuesByIdForm.id;
    let isAscending =  this.getTagValuesByIdForm.isAscending;

    this.reportService.getTagValuesById(id, isAscending).subscribe(
      (data) => {
        this.tagValues = data;
        this.reportType = 'tagValuesById';
        this.showPopup = false;
      },
      (error) => {
        console.log(error);
      }
    );
  }
}
