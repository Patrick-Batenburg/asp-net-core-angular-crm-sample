import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { ToolbarComponent } from './components/toolbar';
import { FooterComponent } from './components/footer';
import { MaterialModule } from './modules';
import { CompanyOverviewComponent } from './components/company-overview/company-overview/company-overview.component';

@NgModule({
  declarations: [AppComponent, ToolbarComponent, FooterComponent, CompanyOverviewComponent],
  imports: [
    BrowserModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    MaterialModule,
    HttpClientModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule {}
