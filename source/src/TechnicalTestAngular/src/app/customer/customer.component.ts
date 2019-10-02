import { Component, OnInit } from '@angular/core';  
import { CustomerService } from '../Service/customer.service';  
import { Customer } from "../Model/customer";  
import { Observable } from "rxjs"; 

@Component({
  selector: 'app-customer',
  templateUrl: './customer.component.html',
  styleUrls: ['./customer.component.scss']
})
export class CustomerComponent implements OnInit {
  private _allCustomer: Observable<Customer[]>;
  public get allCustomer(): Observable<Customer[]> {
    return this._allCustomer;
  }
  public set allCustomer(value: Observable<Customer[]>) {
    this._allCustomer = value;
  }
  constructor(public customerService: CustomerService) { }

  loadDisplay() {
    this._allCustomer = this.customerService.GetCustomer();

  }
  ngOnInit() {
    this.loadDisplay();
  }
}
