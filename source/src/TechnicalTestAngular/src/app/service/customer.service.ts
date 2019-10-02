import { Injectable } from '@angular/core';  
import { HttpClient,HttpHeaders } from "@angular/common/http";  
import { Observable } from "rxjs";  
import { Customer } from "../model/customer";  

@Injectable({
  providedIn: 'root'
})
export class CustomerService {

  Url = 'http://localhost:51133';  
  constructor(private http:HttpClient) { }  
  GetCustomer():Observable<Customer[]>  
  {  
    return this.http.get<Customer[]>(this.Url + '/Api/Customer');  
  }  
}
