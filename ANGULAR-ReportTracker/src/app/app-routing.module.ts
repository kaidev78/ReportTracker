import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

//modules
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
import { AdminProjectListPanelComponent } from './components/admin-project-list-panel/admin-project-list-panel.component';
import { AdminNewProjectFormComponent } from './components/admin-new-project-form/admin-new-project-form.component';
import { CreateIssueFormComponent } from './components/create-issue-form/create-issue-form.component';
import { IssueDisplayPageComponent } from './components/issue-display-page/issue-display-page.component';
import { IssueAdminPanelComponent } from './components/issue-admin-panel/issue-admin-panel.component';

const routes: Routes = [
  {path: '', component: LoginPageComponent },
  {path: 'admin-panel', component: AdminPanelPageComponent, children: [
    {path: 'project-list', component: AdminProjectListPanelComponent},
    {path: 'create-project', component: AdminNewProjectFormComponent}
  ]},
  {path: 'home', component: CustomerMainPageComponent},
  {path: 'search', component: SearchPageComponent},
  {path: 'register', component: RegisterPageComponent},
  {path: 'issues', component: ProductIssuePageComponent},
  {path: 'create-issue', component: CreateIssueFormComponent},
  {path: 'issue-display', component: IssueDisplayPageComponent},
  {path: 'issue-admin-panel', component: IssueAdminPanelComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
