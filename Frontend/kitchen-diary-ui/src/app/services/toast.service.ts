import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';
import { Toast, ToastType } from '../models/toast.model';

@Injectable({
  providedIn: 'root'
})
export class ToastService {

  private toastSubject = new BehaviorSubject<Toast | null>(null);

  toast$ = this.toastSubject.asObservable();

  private timeoutId: any;

  show(message: string, type: ToastType) {

  if (this.timeoutId) {
    clearTimeout(this.timeoutId);
  }

  const toast: Toast = {
    id: Date.now(),
    message,
    type,
    visible: true
  };

  this.toastSubject.next(toast);

  this.timeoutId = setTimeout(() => {

    this.toastSubject.next({
      ...toast,
      visible: false
    });

    setTimeout(() => {
      this.clear();
    }, 300);

  }, 2700);
}
  success(message: string) {
    this.show(message, 'success');
  }

  error(message: string) {
    this.show(message, 'error');
  }

  warning(message: string) {
    this.show(message, 'warning');
  }

  info(message: string) {
    this.show(message, 'info');
  }

  clear() {
    this.toastSubject.next(null);
  }
}