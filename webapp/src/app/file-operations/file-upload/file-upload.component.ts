import { Component, OnInit } from '@angular/core';
import { Validators } from '@angular/forms';
import { FileUploader } from 'ng2-file-upload';
import { NgxFileDropEntry, FileSystemFileEntry, FileSystemDirectoryEntry } from 'ngx-file-drop';
import { ToastrService } from 'ngx-toastr';
import { Observable } from 'rxjs';
import { UploadSettingKeys } from 'src/app/_enums/UploadSettingKeys';
import { ErrorModel } from 'src/app/_models/ErrorModel';
import { UploadSettingsService } from 'src/app/_services/upload-settings.service';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-file-upload',
  templateUrl: './file-upload.component.html',
  styleUrls: ['./file-upload.component.css'],
})
export class FileUploadComponent implements OnInit {
  private baseUrl = environment.serverApiUrl;
  allowedContentTypes: string;
  public files: NgxFileDropEntry[] = [];

  uploader: FileUploader;
  hasBaseDropZoneOver = false;

  constructor(private toastr: ToastrService, private uploadSettingsService: UploadSettingsService) {}

  ngOnInit(): void {
    this.initializeUploader();
  }

  initializeUploader(): void {
    this.uploadSettingsService.getUploadSettingValue().subscribe((uploadSettings) => {
      const allowedContentTypes = uploadSettings.find((x) => x.key === UploadSettingKeys.AllowedContentTypes)?.value;
      const allowedMaxFileSize = uploadSettings.find((x) => x.key === UploadSettingKeys.MaxAllowedSingleFileSizeInBytes)?.value;
      const mimeTypeFilter = allowedContentTypes
        ? allowedContentTypes.includes(',')
          ? allowedContentTypes.split(',')
          : [allowedContentTypes]
        : this.uploadSettingsService.defaultContentTypes;
      this.allowedContentTypes = mimeTypeFilter.map((type) => type.split('/')[1]).join(', ');
      this.uploader = new FileUploader({
        url: this.baseUrl + 'files/upload',
        isHTML5: true,
        allowedMimeType: mimeTypeFilter,
        removeAfterUpload: true,
        autoUpload: false,
        maxFileSize: allowedMaxFileSize ? Number.parseInt(allowedMaxFileSize) : this.uploadSettingsService.defaultMaxFileSizeInBytes,
      });
      this.uploader.onAfterAddingFile = (file) => {
        file.withCredentials = false;
      };

      this.uploader.onSuccessItem = (item, response, status, headers) => {
        this.toastr.success(`File ${item._file.name} is uploaded.`);
      };
      this.uploader.onErrorItem = (item, response, status, headers) => {
        const errorModel: ErrorModel = JSON.parse(response);
        this.toastr.error(
          `An error occured while uploading file ${item._file.name}! Reason: ${errorModel.errors ? errorModel.errors.join(',') : null}`
        );
      };
      this.uploader.onWhenAddingFileFailed = (item, filter, options) => {
        if (filter.name === 'fileSize') {
          this.toastr.error(`Maximum file size exceeds for image ${item.name}`);
        }
        if (filter.name === 'mimeType') {
          this.toastr.error(`Unsupported type for image ${item.name}`);
        }
      };
    });
  }

  fileOverBase(e: any): void {
    this.hasBaseDropZoneOver = e;
  }
}
