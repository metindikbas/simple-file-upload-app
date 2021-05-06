import { Component, EventEmitter, Input, OnDestroy, OnInit, Output } from '@angular/core';
import { PageEvent } from '@angular/material/paginator';
import { Subscription } from 'rxjs';
import { FilesApiService } from 'src/app/_services/files-api.service';

@Component({
  selector: 'app-paginated-files-table',
  templateUrl: './paginated-files-table.component.html',
  styleUrls: ['./paginated-files-table.component.css'],
})
export class PaginatedFilesTableComponent implements OnInit, OnDestroy {
  files: any = [];
  totalCount = 0;
  pageSize = 3;
  pageNumber = 1;
  pageSizeOptions = [1, 3, 5, 10];
  private filesSubscription: Subscription = new Subscription();
  @Input() fileContentType: string;
  @Output() onShowFileViewDialog: EventEmitter<string> = new EventEmitter<string>();

  constructor(private fileApiService: FilesApiService) {}

  ngOnInit(): void {
    this.getFilesFromApi();
  }

  ngOnDestroy(): void {
    this.filesSubscription.unsubscribe();
  }

  onPageChanged(event: PageEvent): void {
    this.pageNumber = event.pageIndex + 1;
    this.pageSize = event.pageSize;
    this.getFilesFromApi();
  }

  getFilesFromApi(): void {
    this.fileApiService.getFiles(this.fileContentType, this.pageNumber, this.pageSize).subscribe((res) => {
      this.files = res.files;
      this.totalCount = res.totalCount;
    });
  }

  showViewModal(id: string): void {
    this.onShowFileViewDialog.emit(id);
  }
}
