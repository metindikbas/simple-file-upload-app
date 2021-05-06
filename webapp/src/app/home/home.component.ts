import { Component, OnInit } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { AlertifyService } from '../_services/alertify.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css'],
})
export class HomeComponent implements OnInit {
  constructor(private toastr: ToastrService) {}

  ngOnInit(): void {}

  helloworld(): void {
    this.toastr.success('Profile updated successfully');
  }
}
