import { Component } from '@angular/core';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { Router, RouterLink } from '@angular/router';
import { AuthService } from '../../services/auth.service';
import { RegisterRequest } from '../../models/register';
import { ToastService } from '../../services/toast.service';

@Component({
  selector: 'app-register',
  imports: [ReactiveFormsModule, RouterLink],
  templateUrl: './register.html',
  styleUrl: './register.css'
})
export class RegisterComponent {

  registerForm;

  constructor(
    private fb: FormBuilder,
    private authService: AuthService,
    private router: Router,
    private toastService: ToastService
  ) {

    this.registerForm = this.fb.group({
      fullName: ['', Validators.required],
      email: ['', [Validators.required, Validators.email]],
      password: ['', [Validators.required, Validators.minLength(8)]],
      confirmPassword: ['', Validators.required]
    });

  }

  onSubmit() {
    console.log('Register button clicked');


    if (this.registerForm.invalid) {

    this.registerForm.markAllAsTouched();
    this.toastService.warning(
        'Please fill in all required fields correctly.'
    );

    return;

}

    if (this.registerForm.value.password !== this.registerForm.value.confirmPassword) {

      this.toastService.error('Passwords do not match.');

      return;

    }

    const request: RegisterRequest = {
      fullName: this.registerForm.value.fullName!,
      email: this.registerForm.value.email!,
      password: this.registerForm.value.password!
    };

    this.authService.register(request).subscribe({
      next: () => {
        console.log('Registration successful');
        
        this.toastService.success('Registration successful!');

        this.router.navigate(['/login']);

      },
      error: (err) => {

        console.error('Registration Error:', err);

        if (err.status === 409) {

          this.toastService.warning(
            'An account with this email already exists. Please log in.'
          );
          
        }
        
        

        
        

          this.toastService.error(
            'Registration failed. Please try again.'
          );

        

      }
    });

  }

}