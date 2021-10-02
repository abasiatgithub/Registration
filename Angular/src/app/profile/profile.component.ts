import { Component, OnInit } from '@angular/core';
import {
  Validators,
  FormGroup,
  FormControl,
  FormBuilder,
} from '@angular/forms';
import { Router, ParamMap, ActivatedRoute } from '@angular/router';
import { AccountService } from '../account.service';
import { user } from '../models/user';

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.css'],
  providers: [AccountService],
})
export class ProfileComponent implements OnInit {
  constructor(
    private fb: FormBuilder,
    private router: Router,
    private api: AccountService,
    private route: ActivatedRoute
  ) {}

  user: user = new user();
  errorUpadate: boolean = false;
  profileForm = new FormGroup({
    userId: new FormControl(null),
    firstname: new FormControl(null),
    lastname: new FormControl(null),
    username: new FormControl(null, Validators.required),
    password: new FormControl(null),
    email: new FormControl(null, Validators.required),
    phonenumber: new FormControl(null),
    address: new FormControl(null),
  });

  ngOnInit(): void {
    this.api.getUserById(this.route.snapshot.params.id).subscribe((data) => {
      this.user = data;
      console.log(data);
      this.profileForm.patchValue({
        userId: data.userId,
        firstname: data.firstName,
        lastname: data.lastname,
        username: data.username,
        password: data.password,
        email: data.email,
        phonenumber: data.phoneNumber,
        address: data.addres,
      });
    });
  }

  onSubmit() {
    console.log(this.profileForm.value);

    this.api.putUser(this.profileForm.value).subscribe((res) => {
      if (res == 0) {
        this.errorUpadate = true;
      } else {
        this.router.navigate(['/Home']);
      }
    });
  }
}
