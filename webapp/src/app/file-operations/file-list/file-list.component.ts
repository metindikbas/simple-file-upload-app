import { Component, OnInit, ViewChild } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { Subscription } from 'rxjs';
import { UploadSettingKeys } from 'src/app/_enums/UploadSettingKeys';
import { UploadSettingsService } from 'src/app/_services/upload-settings.service';
import { FileViewComponent } from '../file-view/file-view.component';

@Component({
  selector: 'app-file-list',
  templateUrl: './file-list.component.html',
  styleUrls: ['./file-list.component.css'],
})
export class FileListComponent implements OnInit {
  @ViewChild('fileViewDialog', { static: true }) fileViewDialog: FileViewComponent;
  contentTypes: string[];

  constructor(private uploadSettingsService: UploadSettingsService, private dialog: MatDialog) {}

  ngOnInit(): void {
    this.uploadSettingsService.getUploadSettingValue().subscribe((uploadSettings) => {
      const allowedContentTypes = uploadSettings.find((x) => x.key === UploadSettingKeys.AllowedContentTypes)?.value;
      this.contentTypes = allowedContentTypes
        ? allowedContentTypes.includes(',')
          ? allowedContentTypes.split(',')
          : [allowedContentTypes]
        : this.uploadSettingsService.defaultContentTypes;
    });
  }

  showDialog(id: string): void {
    this.dialog.open(FileViewComponent, { data: { fileId: id } });
  }
}
