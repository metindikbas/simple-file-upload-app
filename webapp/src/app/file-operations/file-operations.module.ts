import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FileUploadComponent } from './file-upload/file-upload.component';
import { FileListComponent } from './file-list/file-list.component';
import { NgxFileDropModule } from 'ngx-file-drop';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule } from '@angular/forms';
import { FileUploadModule } from 'ng2-file-upload';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { MatPaginatorModule } from '@angular/material/paginator';
import { PaginatedFilesTableComponent } from '../_components/paginated-files-table/paginated-files-table.component';
import { MatDialogModule } from '@angular/material/dialog';
import { MatTooltipModule } from '@angular/material/tooltip';
import { FileViewComponent } from './file-view/file-view.component';
import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';

@NgModule({
  declarations: [FileUploadComponent, FileListComponent, PaginatedFilesTableComponent, FileViewComponent],
  imports: [
    CommonModule,
    FormsModule,
    HttpClientModule,
    NgxFileDropModule,
    FileUploadModule,
    MatProgressSpinnerModule,
    MatPaginatorModule,
    MatDialogModule,
    MatTooltipModule,
    MatButtonModule,
    MatCardModule,
  ],
})
export class FileOperationsModule {}
