import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import {
  FormBuilder,
  FormGroup,
  ReactiveFormsModule,
  Validators
} from '@angular/forms';
import { Router, RouterLink } from '@angular/router';
import { AuthService } from '../../services/auth.service';
import { ToastService } from '../../services/toast.service';

@Component({
  selector: 'app-login',
  imports: [
    CommonModule,
    ReactiveFormsModule,
    RouterLink
  ],
  templateUrl: './login.html',
  styleUrl: './login.css'
})
export class LoginComponent {

  loginForm: FormGroup;

  constructor(
  private fb: FormBuilder,
  private authService: AuthService,
  private router: Router,
  private toastService: ToastService
) {

  this.loginForm = this.fb.group({
    email: ['', [Validators.required, Validators.email]],
    password: ['', Validators.required]
  });

}
onSubmit(): void {

  if (this.loginForm.invalid) {
    this.toastService.warning(
        'Please fill in all required fields correctly.'
    );
    return;
  }

  this.authService.login(this.loginForm.value).subscribe({

    next: () => {

      this.router.navigate(['/']);

    },

    error: (error) => {

      console.error(error);

      this.toastService.error('Invalid email or password.');

    }

  });

}
 testClick() {
  console.log('Register link clicked');
}

}


