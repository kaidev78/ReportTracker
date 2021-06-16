/* Modules */
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { AppRoutingModule } from './app-routing.module';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';

/* Components */
import { AppComponent } from './app.component';
import { HeaderComponent } from './components/header/header.component';
import { LoginPageComponent } from './components/login-page/login-page.component';
import { AdminPanelPageComponent } from './components/admin-panel-page/admin-panel-page.component';
import { AdminProjectPanelPageComponent } from './components/admin-project-panel-page/admin-project-panel-page.component';
import { CustomerMainPageComponent } from './components/customer-main-page/customer-main-page.component';
import { CustomerNavbarComponent } from './components/customer-navbar/customer-navbar.component';
import { SearchPageComponent } from './components/search-page/search-page.component';
import { ProductIssuePageComponent } from './components/product-issue-page/product-issue-page.component';
import { RegisterPageComponent } from './components/register-page/register-page.component';

@NgModule({
  declarations: [
    AppComponent,
    HeaderComponent,
    LoginPageComponent,
    AdminPanelPageComponent,
    AdminProjectPanelPageComponent,
    CustomerMainPageComponent,
    CustomerNavbarComponent,
    SearchPageComponent,
    ProductIssuePageComponent,
    RegisterPageComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    FormsModule,
    HttpClientModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
