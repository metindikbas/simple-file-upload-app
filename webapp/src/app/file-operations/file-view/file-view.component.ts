import { Component, Inject, OnInit } from '@angular/core';
import { MatDialog, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { FileViewDto } from 'src/app/_models/FileViewDto';
import { FilesApiService } from 'src/app/_services/files-api.service';

@Component({
  selector: 'app-file-view',
  templateUrl: './file-view.component.html',
  styleUrls: ['./file-view.component.css'],
})
export class FileViewComponent implements OnInit {
  file: FileViewDto;

  constructor(private dialog: MatDialog, @Inject(MAT_DIALOG_DATA) public data: any, private fileApiService: FilesApiService) {}

  ngOnInit(): void {
    this.fileApiService.getFile(this.data.fileId).subscribe((file) => {
      this.file = file;
    });
  }
}
