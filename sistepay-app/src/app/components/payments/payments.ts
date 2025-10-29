import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Api } from '../../services/api';

@Component({
  selector: 'app-payments',
  standalone: false,
  templateUrl: './payments.html',
  styleUrl: './payments.css',
})
export class Payments implements OnInit {
  orderId: string = '';
  amount: number = 0;
  returnUrl: string = '';
  processing: boolean = false;
  success: boolean = false;

  constructor(
    private api: Api,
    private route: ActivatedRoute
  ) {}

  ngOnInit() {
    // Obtener parámetros de la URL
    this.route.queryParams.subscribe(params => {
      this.orderId = params['order_id'] || '';
      this.amount = parseFloat(params['amount']) || 0;
      this.returnUrl = params['return_url'] || '';
    });
  }

  processPayment() {
    this.processing = true;

    const paymentData = {
      orderId: this.orderId,
      amount: this.amount,
      status: 'approved'
    };

    this.api.createPayment(paymentData).subscribe({
      next: (response) => {
        this.processing = false;
        this.success = true;
        
        // Redirigir de vuelta al checkout con éxito
        setTimeout(() => {
          if (this.returnUrl) {
            window.location.href = this.returnUrl + '&payment_status=approved&transaction_id=' + response.transactionId;
          }
        }, 2000);
      },
      error: (err) => {
        console.error('Error:', err);
        this.processing = false;
        alert('Error procesando el pago');
      }
    });
  }
}
