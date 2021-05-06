import { getTestBed, TestBed } from '@angular/core/testing';
import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';

import { FilesApiService } from './files-api.service';
import { FileDto } from '../_models/FileDto';
import { FileViewDto } from '../_models/FileViewDto';

describe('FilesApiService', () => {
  let service: FilesApiService;
  let httpMock: HttpTestingController;
  let injector: TestBed;

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [HttpClientTestingModule],
      providers: [FilesApiService],
    });
    injector = getTestBed();
    service = injector.inject(FilesApiService);
    httpMock = injector.inject(HttpTestingController);
  });

  afterEach(() => {
    httpMock.verify();
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });

  describe('Get all files', () => {
    it('should return an Observable<{ files: FileDto[]; totalCount: number }>', () => {
      const pageSize = 2;
      const pageNumber = 1;
      const contentType = 'image/png';
      const queryParams = `?ContentTypeFilter=${encodeURIComponent(contentType)}&PageNumber=${encodeURIComponent(
        pageNumber
      )}&PageSize=${encodeURIComponent(pageSize)}`;
      const dummyFiles: FileDto[] = [
        {
          fileName: 'file',
          fileSizeInMb: 1,
          id: '1',
          uploadDate: new Date(),
        },
        {
          fileName: 'file2',
          fileSizeInMb: 2,
          id: '2',
          uploadDate: new Date(),
        },
      ];

      service.getFiles(contentType, pageNumber, pageSize).subscribe((files) => {
        expect(files.files).toBe(dummyFiles);
        expect(files.totalCount).toEqual(dummyFiles.length);
      });

      const req = httpMock.expectOne(`${service.baseUrl}${queryParams}`);
      expect(req.request.method).toBe('GET');
      req.flush({ items: dummyFiles, totalCount: dummyFiles.length });
    });
  });

  describe('Get file by id', () => {
    it('should return an Observable<FileViewDto>', () => {
      const dummyFile = { fileName: 'file2', fileSizeInMb: 2, id: '2', uploadDate: new Date(), contentData: '' };

      service.getFile(dummyFile.id).subscribe((file: FileViewDto) => {
        expect(file.fileName).toBe(dummyFile.fileName);
        expect(file.contentData).toEqual(dummyFile.contentData);
      });

      const req = httpMock.expectOne(`${service.baseUrl}${dummyFile.id}`);
      expect(req.request.method).toBe('GET');
      req.flush({
        id: dummyFile.id,
        fileName: dummyFile.fileName,
        uploadDate: dummyFile.uploadDate,
        fileSizeInMb: dummyFile.fileSizeInMb,
        contentData: dummyFile.contentData,
      });
    });
  });
});
