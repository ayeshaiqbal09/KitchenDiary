import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';
import { ConfirmDialog } from '../models/confirm-dialog.model';

@Injectable({
  providedIn: 'root'
})
export class ConfirmDialogService {

  private dialogSubject = new BehaviorSubject<ConfirmDialog | null>(null);

  dialog$ = this.dialogSubject.asObservable();

  private resolver: ((result: boolean) => void) | null = null;

  open(dialog: ConfirmDialog): Promise<boolean> {

    this.dialogSubject.next(dialog);

    return new Promise<boolean>((resolve) => {
      this.resolver = resolve;
    });

  }

  confirm(): void {

    this.resolver?.(true);

    this.close();

  }

  cancel(): void {

    this.resolver?.(false);

    this.close();

  }

  private close(): void {

    this.dialogSubject.next(null);

    this.resolver = null;

  }

}