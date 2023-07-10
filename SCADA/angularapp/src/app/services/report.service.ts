import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Alarm, AlarmPriority } from '../models/alarm';
import { AnalogInput, DigitalInput, TagValue } from '../models/tags';
import { AlarmReportDTO, AnalogInputReportDTO, DigitalInputReportDTO } from '../models/report';

@Injectable({
  providedIn: 'root'
})
export class ReportService {

  private baseUrl = '/api/Report';

  constructor(private http: HttpClient) { }

  getAllAlarms(startTime: Date, endTime: Date, isAscending: boolean): Observable<AlarmReportDTO[]> {
    const params = new HttpParams()
      .set('startTime', startTime.toString())
      .set('endTime', endTime.toString())
      .set('isAscending', isAscending.toString());

    return this.http.get<AlarmReportDTO[]>(`${this.baseUrl}/alarms`, { params });
  }

  getAlarmsByPriority(priority: number, isAscending: boolean): Observable<AlarmReportDTO[]> {
    console.log(priority);
    const params = new HttpParams()
      .set('priority', priority)
      .set('isAscending', isAscending.toString());

    return this.http.get<AlarmReportDTO[]>(`${this.baseUrl}/alarms/priority`, { params });
  }

  getAllTagValues(startTime: Date, endTime: Date, isAscending: boolean): Observable<TagValue[]> {
    const params = new HttpParams()
      .set('startTime', startTime.toString())
      .set('endTime', endTime.toString())
      .set('isAscending', isAscending.toString());

    return this.http.get<TagValue[]>(`${this.baseUrl}/tagvalues`, { params });
  }

  getLastAnalogInputs(isAscending: boolean): Observable<AnalogInputReportDTO[]> {
    const params = new HttpParams().set('isAscending', isAscending.toString());
    return this.http.get<AnalogInputReportDTO[]>(`${this.baseUrl}/analoginputs/last`, { params });
  }

  getLastDigitalInputs(isAscending: boolean): Observable<DigitalInputReportDTO[]> {
    const params = new HttpParams().set('isAscending', isAscending.toString());
    return this.http.get<DigitalInputReportDTO[]>(`${this.baseUrl}/digitalinputs/last`, { params });
  }

  getTagValuesById(id: number, isAscending: boolean): Observable<TagValue[]> {
    const params = new HttpParams().set('isAscending', isAscending.toString());
    return this.http.get<TagValue[]>(`${this.baseUrl}/tagvalues/${id}`, { params });
  }
}
