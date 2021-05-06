import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { UploadSettingKeys } from '../_enums/UploadSettingKeys';
import { UploadSettingDto } from '../_models/UploadSettingDto';

@Injectable({
  providedIn: 'root',
})
export class UploadSettingsService {
  private baseUrl = environment.serverApiUrl + 'uploadsettings';
  public defaultContentTypes = ['image/png', 'image/jpg', 'image/jpeg'];
  public defaultMaxFileSizeInBytes = 5 * 1024 * 1024;

  constructor(private http: HttpClient) {}

  getUploadSettingValue(): Observable<UploadSettingDto[]> {
    return this.http.get<UploadSettingDto[]>(this.baseUrl);
  }
}
