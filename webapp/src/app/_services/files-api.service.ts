import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, Subject } from 'rxjs';
import { environment } from 'src/environments/environment';
import { PaginatedViewModel } from '../_models/PaginatedViewModel';
import { map } from 'rxjs/operators';
import { FileDto } from '../_models/FileDto';
import { FileViewDto } from '../_models/FileViewDto';

@Injectable({
  providedIn: 'root',
})
export class FilesApiService {
  baseUrl = environment.serverApiUrl + 'files/';

  constructor(private http: HttpClient) {}

  getFiles(fileContentType: string, pageNumber: number, pageSize: number): Observable<{ files: FileDto[]; totalCount: number }> {
    const queryParams = `?ContentTypeFilter=${encodeURIComponent(fileContentType)}&PageNumber=${encodeURIComponent(
      pageNumber
    )}&PageSize=${encodeURIComponent(pageSize)}`;
    return this.http.get<PaginatedViewModel<FileDto[]>>(this.baseUrl + queryParams).pipe(
      map((response) => {
        return {
          files: response.items,
          totalCount: response.totalCount,
        };
      })
    );
  }

  getFile(id: string): Observable<FileViewDto> {
    return this.http.get<FileViewDto>(this.baseUrl + id).pipe(
      map((response) => {
        return response;
      })
    );
  }
}
