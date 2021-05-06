import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

import { AppComponent } from './app.component';
import { FileOperationsModule } from './file-operations/file-operations.module';
import { NavComponent } from './nav/nav.component';
import { RouterModule } from '@angular/router';
import { AppRoutes } from './routes';
import { HomeComponent } from './home/home.component';
import { AlertifyService } from './_services/alertify.service';
import { ToastrModule } from 'ngx-toastr';
import { UploadSettingsService } from './_services/upload-settings.service';
import { FilesApiService } from './_services/files-api.service';

@NgModule({
  declarations: [AppComponent, NavComponent, HomeComponent],
  imports: [
    BrowserModule,
    BrowserAnimationsModule,
    RouterModule.forRoot(AppRoutes),
    FileOperationsModule,
    ToastrModule.forRoot({
      positionClass: 'toast-bottom-right',
    }),
  ],
  providers: [AlertifyService, FilesApiService, UploadSettingsService],
  bootstrap: [AppComponent],
})
export class AppModule {}
