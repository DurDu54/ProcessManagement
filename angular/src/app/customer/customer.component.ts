import { Component, Injector, OnInit } from '@angular/core';
import { AppComponentBase } from '@shared/app-component-base';
import { PagedRequestDto } from '@shared/paged-listing-component-base';
import { CustomerServiceProxy, GetCustomerDto } from '@shared/service-proxies/service-proxies';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-customer',
  templateUrl: './customer.component.html',
  styleUrls: ['./customer.component.css']
})
export class CustomerComponent extends AppComponentBase implements OnInit {

  pageNumber : number=1;
  pageSize : number=2;
  totalItems :number;
  customerList:GetCustomerDto[];
  constructor(
    injector:Injector,
    private _customerProxy : CustomerServiceProxy,
  ) { 
    super(injector);
  }

  ngOnInit(): void {
    this.GetCustomer();
    this._customerProxy.getList()
    .subscribe((res)=>{
      this.totalItems=res.length;
    })
  }
  public getDataPage(page: number): void {
    this.pageNumber=page;
    this.GetCustomer();

    
}
  public GetCustomer(){
    this._customerProxy.getPaginatedList(this.pageSize,this.pageNumber)
    .subscribe((res) => {
      this.customerList = res;
    })
  }
}
