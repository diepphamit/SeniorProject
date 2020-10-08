import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { BsDatepickerModule } from 'ngx-bootstrap/datepicker';
import { ControlMessagesComponent } from 'src/app/helper/control-messages.component';
import { AppModule } from 'src/app/app.module';
import { ControlModule } from 'src/app/helper/control.module';
import { NgxPaginationModule } from 'ngx-pagination';
import { ImageRoutingModule } from './image-routing.module';
import { ImageComponent } from './image.component';
import { AddImageComponent } from './add-image/add-image.component';
import { NgxFileDropModule } from 'ngx-file-drop';

@NgModule({
    imports: [
        CommonModule,
        FormsModule,
        ReactiveFormsModule,
        ImageRoutingModule,
        ControlModule,
        NgxPaginationModule,
        NgxFileDropModule,
        BsDatepickerModule.forRoot()
    ],
    declarations: [
        ImageComponent,
        AddImageComponent
    ]
})
export class ImageModule { }
