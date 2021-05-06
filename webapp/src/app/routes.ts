import { Routes } from '@angular/router';
import { FileListComponent } from './file-operations/file-list/file-list.component';
import { FileUploadComponent } from './file-operations/file-upload/file-upload.component';
import { HomeComponent } from './home/home.component';

export const AppRoutes: Routes = [
  {
    path: '',
    component: HomeComponent,
  },
  {
    path: '',
    runGuardsAndResolvers: 'always',
    children: [
      {
        path: 'files',
        component: FileListComponent,
      },
      {
        path: 'files/upload',
        component: FileUploadComponent,
      },
    ],
  },
  {
    path: '**',
    redirectTo: '',
    pathMatch: 'full',
  },
];
