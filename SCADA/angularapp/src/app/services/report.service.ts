import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Alarm, AlarmPriority } from '../models/alarm';
import { AnalogInput, DigitalInput, TagValue } from '../models/tags';

@Injectable({
  providedIn: 'root'
})
export class ReportService {

  private baseUrl = '/api/Report';

  constructor(private http: HttpClient) { }

  getAllAlarms(startTime: Date, endTime: Date, isAscending: boolean): Observable<Alarm[]> {
    const params = new HttpParams()
      .set('startTime', startTime.toString())
      .set('endTime', endTime.toString())
      .set('isAscending', isAscending.toString());

    return this.http.get<Alarm[]>(`${this.baseUrl}/alarms`, { params });
  }

  getAlarmsByPriority(priority: AlarmPriority, isAscending: boolean): Observable<Alarm[]> {
    const params = new HttpParams()
      .set('priority', priority)
      .set('isAscending', isAscending.toString());

    return this.http.get<Alarm[]>(`${this.baseUrl}/alarms/priority`, { params });
  }

  getAllTagValues(startTime: Date, endTime: Date, isAscending: boolean): Observable<TagValue[]> {
    const params = new HttpParams()
      .set('startTime', startTime.toString())
      .set('endTime', endTime.toString())
      .set('isAscending', isAscending.toString());

    return this.http.get<TagValue[]>(`${this.baseUrl}/tagvalues`, { params });
  }

  getLastAnalogInputs(isAscending: boolean): Observable<AnalogInput[]> {
    const params = new HttpParams().set('isAscending', isAscending.toString());
    return this.http.get<AnalogInput[]>(`${this.baseUrl}/analoginputs/last`, { params });
  }

  getLastDigitalInputs(isAscending: boolean): Observable<DigitalInput[]> {
    const params = new HttpParams().set('isAscending', isAscending.toString());
    return this.http.get<DigitalInput[]>(`${this.baseUrl}/digitalinputs/last`, { params });
  }

  getTagValuesById(id: number, isAscending: boolean): Observable<TagValue[]> {
    const params = new HttpParams().set('isAscending', isAscending.toString());
    return this.http.get<TagValue[]>(`${this.baseUrl}/tagvalues/${id}`, { params });
  }
}
