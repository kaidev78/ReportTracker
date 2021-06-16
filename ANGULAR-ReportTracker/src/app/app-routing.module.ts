import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

//modules
import { LoginPageComponent } from './components/login-page/login-page.component';
import { AdminPanelPageComponent } from './components/admin-panel-page/admin-panel-page.component';
import { CustomerMainPageComponent } from './components/customer-main-page/customer-main-page.component';
import { SearchPageComponent } from './components/search-page/search-page.component';
import { RegisterPageComponent } from './components/register-page/register-page.component';

const routes: Routes = [
  {path: '', component: LoginPageComponent },
  {path: 'admin-panel', component: AdminPanelPageComponent},
  {path: 'home', component: CustomerMainPageComponent},
  {path: 'search', component: SearchPageComponent},
  {path: 'register', component: RegisterPageComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
