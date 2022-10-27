import { Component,EventEmitter , Injector, OnInit } from '@angular/core';
import { AppComponentBase } from '@shared/app-component-base';
import { ProjectServiceProxy ,GetProjectDto , CreateProjectDto } from '@shared/service-proxies/service-proxies';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';

@Component({
  selector: 'app-project',
  templateUrl: './project.component.html',
  styleUrls: ['./project.component.css']
})
export class ProjectComponent extends AppComponentBase implements OnInit {
  onSave = new EventEmitter<any>();
  saving =false;
  page :number = 1;
  pageSize : number = 10;
  projects : GetProjectDto[] = [];
  createProject : CreateProjectDto;
  constructor(
    injector : Injector,
    private _projectServiceProxy : ProjectServiceProxy,
    private _bsModalRef : BsModalRef,
  ) { 
    super(injector)
  }

  ngOnInit(): void {
    this.getProjects();

  }
  create() {
    this.createProject.id = 0;
    let input = this.createProject;
    this._projectServiceProxy.create(this.createProject).subscribe(
      ()=>{
        this.notify.info(this.l('Saved Succesful'));
        this._bsModalRef.hide();
        this.onSave.emit();
      },
      ()=>{
        this.saving=false;
      }
      
    )
  }
  
 getProjects()
  {
    this._projectServiceProxy.paginatedList(this.page , this.pageSize)
    .subscribe((res) => {
      this.projects = res;
    });
  }

  protected delete(project : GetProjectDto) : void{
    abp.message.confirm(
      this.l(project.name + ' İsimli proje silinecek onaylıyor musunuz?'),
      undefined,
      (result: boolean) => {
        if (result) {
          this._projectServiceProxy.delete(project.id).subscribe(() => {
            abp.notify.success(this.l('Başarıyla Silindi'));
            this.refresh();
          });
        }
      }
    );
  }
  refresh(){
    this.getProjects
  }
}
