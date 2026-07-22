import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ConfirmDialog } from '../../models/confirm-dialog.model';
import { ConfirmDialogService } from '../../services/confirm-dialog.service';

@Component({
  selector: 'app-confirm-dialog',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './confirm-dialog.html',
  styleUrl: './confirm-dialog.css'
})
export class ConfirmDialogComponent implements OnInit {

  dialog: ConfirmDialog | null = null;

  constructor(private confirmService: ConfirmDialogService) {}

  ngOnInit(): void {

    this.confirmService.dialog$.subscribe(dialog => {

      this.dialog = dialog;

    });

  }

  confirm(): void {
    this.confirmService.confirm();
  }

  cancel(): void {
    this.confirmService.cancel();
  }

}