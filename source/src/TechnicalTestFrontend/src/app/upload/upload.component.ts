import { Component } from '@angular/core';
import { FileUploader } from 'ng2-file-upload';

@Component({
  selector: 'app-upload',
  templateUrl: './upload.component.html',
  styleUrls: ['./upload.component.scss']
})
export class UploadComponent {
  URL = 'http://localhost:51133/api/Customer/Upload';

  public uploader: FileUploader = new FileUploader({ url: this.URL });
  public hasBaseDropZoneOver: boolean = false;
  public hasAnotherDropZoneOver: boolean = false;

  public fileOverBase(e: any): void {
      this.hasBaseDropZoneOver = e;
  }

  public fileOverAnother(e: any): void {
      this.hasAnotherDropZoneOver = e;
  }
}
