import { BrowserModule } from '@angular/platform-browser';
import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import  {HttpClientModule, HttpClient } from '@angular/common/http'; 
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { FormsModule } from '@angular/forms'; 
import { CustomerComponent } from './customer/customer.component';
import { NgxPaginationModule} from 'ngx-pagination';  
import { Ng2SearchPipeModule } from 'ng2-search-filter';
import { UploadComponent } from './upload/upload.component'; 
import { FileUploadModule  } from 'ng2-file-upload';

@NgModule({
  declarations: [
    AppComponent,
    CustomerComponent,
    UploadComponent
  ],
  imports: [
    BrowserModule, CommonModule,
    AppRoutingModule,FormsModule,  
    HttpClientModule,NgxPaginationModule,Ng2SearchPipeModule,
    FileUploadModule      
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
