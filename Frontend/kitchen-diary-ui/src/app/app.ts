import { Component, signal } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { Navbar } from './components/navbar/navbar';
import { ToastComponent } from './components/toast/toast';
import { ConfirmDialogComponent } from './components/confirm-dialog/confirm-dialog';
import { Loading } from './services/loading';
import { LoadingSpinner } from './shared/loading-spinner/loading-spinner';
import { AsyncPipe } from '@angular/common';

@Component({
  selector: 'app-root',
  imports: [RouterOutlet, Navbar, ToastComponent, ConfirmDialogComponent, LoadingSpinner, AsyncPipe],
  templateUrl: './app.html',
  styleUrl: './app.css'
})
export class App {

  protected readonly title = signal('kitchen-diary-ui');

  loading$;

  constructor(
    public loadingService: Loading
  ) {
    this.loading$ = this.loadingService.loading$;
  }

}

