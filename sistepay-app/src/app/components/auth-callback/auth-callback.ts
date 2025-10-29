import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Api } from '../../services/api';

@Component({
  selector: 'app-auth-callback',
  standalone: false,
  templateUrl: './auth-callback.html',
  styleUrl: './auth-callback.css',
})
export class AuthCallback implements OnInit {
  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private api: Api
  ) {}

  ngOnInit() {
    this.route.queryParams.subscribe(params => {
      const code = params['code'];
      if (code) {
        this.api.getToken(code).subscribe({
          next: (response) => {
            localStorage.setItem('access_token', response.access_token);
            this.router.navigate(['/dashboard']);
          },
          error: (err) => console.error('Error obteniendo token:', err)
        });
      }
    });
  }
}
