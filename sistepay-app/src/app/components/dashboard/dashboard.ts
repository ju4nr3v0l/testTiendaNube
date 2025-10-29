import { Component, OnInit } from '@angular/core';
import { Api } from '../../services/api';

@Component({
  selector: 'app-dashboard',
  standalone: false,
  templateUrl: './dashboard.html',
  styleUrl: './dashboard.css',
})
export class Dashboard implements OnInit {
  storeInfo: any = null;
  orders: any[] = [];

  constructor(private api: Api) {}

  ngOnInit() {
    this.api.getStoreInfo().subscribe({
      next: (data) => this.storeInfo = data,
      error: (err) => console.error('Error:', err)
    });

    this.api.getOrders().subscribe({
      next: (data) => this.orders = data,
      error: (err) => console.error('Error:', err)
    });
  }
}
