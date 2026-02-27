import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import {FormsModule, ReactiveFormsModule} from '@angular/forms';
import {TranslatePipe} from '@ngx-translate/core';
import {DialogHelperService} from './services/dialog-helper.service';
import {PaginatorBarComponent} from './components/paginator-bar/paginator-bar.component';
import {TableSkeletonComponent} from './components/table-skeleton/table-skeleton.component';
import {LoadingBarComponent} from './components/loading-bar/loading-bar.component';
import {ConfirmDialogComponent} from './components/confirm-dialog/confirm-dialog.component';
import {materialModules} from './material-modules';

@NgModule({
  declarations: [
    PaginatorBarComponent,
    ConfirmDialogComponent,
    LoadingBarComponent,
    TableSkeletonComponent
  ],
  imports: [
    CommonModule,
    ReactiveFormsModule,
    FormsModule,
    TranslatePipe,
    ...materialModules
  ],
  providers: [
    DialogHelperService
  ],
  exports:[
    PaginatorBarComponent,
    CommonModule,
    ReactiveFormsModule,
    TranslatePipe,
    FormsModule,
    LoadingBarComponent,
    TableSkeletonComponent,
    materialModules
  ]
})
export class SharedModule { }
