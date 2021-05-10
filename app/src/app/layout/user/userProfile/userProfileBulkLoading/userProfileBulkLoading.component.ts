import {Component, OnInit} from "@angular/core";
import {AdvertisementService} from "../../../../services/advertisements.service";
import {Router} from "@angular/router";
import {ToastrService} from "ngx-toastr";

@Component({
  selector: 'app-userProfileBulkLoading',
  templateUrl: './userProfileBulkLoading.component.html',
  styleUrls: ['./userProfileBulkLoading.component.scss'],
})

export class UserProfileBulkLoadingComponent implements OnInit{

  public excelFile!: File;
  public isDisabled: boolean = false;
  constructor(private advertisementService: AdvertisementService,
              private router: Router,
              private toastrService: ToastrService) {
  }
  onUploadFile(event: any){
    this.excelFile =  event.target.files[0];
  }

  ngOnInit(): void {
  }

  onSendExcel(){
    this.isDisabled = true;
    this.advertisementService.importExcel(this.excelFile).subscribe(a => {
      this.toastrService.success('Данные успешно загружены');
      return  this.router.navigateByUrl('/');
    });
  }
}
