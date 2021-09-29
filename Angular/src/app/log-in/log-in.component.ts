import { Component, OnInit } from '@angular/core';
import {
  Validators,
  FormGroup,
  FormControl,
  FormBuilder,
} from '@angular/forms';
import { Router } from '@angular/router';
import { AccountService } from '../account.service';

@Component({
  selector: 'app-log-in',
  templateUrl: './log-in.component.html',
  styleUrls: ['./log-in.component.css'],
  providers: [AccountService],
})
export class LogInComponent implements OnInit {
  loginForm: FormGroup;
  showWrongMessage: boolean = false;

  constructor(
    private api: AccountService,
    private fb: FormBuilder,
    private router: Router
  ) {
    this.loginForm = this.fb.group({
      username: ['', Validators.required],
      password: ['', Validators.required],
    });
  }

  ngOnInit(): void {
    this.loginForm = this.fb.group({
      username: ['', Validators.required],
      password: ['', Validators.required],
    });
  }

  onSubmit() {
    let username = this.loginForm.controls.username.value;
    let password = this.loginForm.controls.password.value;
    this.api.getUser(username, password).subscribe((data) => {
      if (data == 0) {
        console.log(data);
        this.showWrongMessage = true;
      } else {
        this.router.navigate(['/Profile', data]);
      }
    });
  }
}
