import { Component } from '@angular/core';
import { Api } from '../../services/api';

@Component({
  selector: 'app-payments',
  standalone: false,
  templateUrl: './payments.html',
  styleUrl: './payments.css',
})
export class Payments {
  amount: number = 0;
  description: string = '';

  constructor(private api: Api) {}

  createPayment() {
    const paymentData = {
      amount: this.amount,
      description: this.description
    };

    this.api.createPayment(paymentData).subscribe({
      next: (response) => {
        if (response.paymentUrl) {
          window.location.href = response.paymentUrl;
        }
      },
      error: (err) => console.error('Error:', err)
    });
  }
}
