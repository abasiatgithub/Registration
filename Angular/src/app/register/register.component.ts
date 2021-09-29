import { Component, OnInit } from '@angular/core';
import {
  Validators,
  FormGroup,
  FormControl,
  FormBuilder,
} from '@angular/forms';
import { AccountService } from '../account.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css'],
  providers: [AccountService],
})
export class RegisterComponent implements OnInit {
  constructor(
    private builder: FormBuilder,
    private api: AccountService,
    private router: Router
  ) {}

  ngOnInit(): void {}

  username = new FormControl('', [Validators.required]);
  email = new FormControl('', [Validators.required]);
  password = new FormControl('', [Validators.required]);

  registerForm: FormGroup = this.builder.group({
    username: this.username,
    email: this.email,
    password: this.password,
  });

  save() {
    this.api.postUser(this.registerForm.value).subscribe((res) => {
      this.router.navigate(['/Profile', res]);
    });
  }
}
