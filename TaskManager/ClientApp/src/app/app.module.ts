import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule, Routes } from '@angular/router';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './components/nav-menu/nav-menu.component';
import { MatSidenavModule } from '@angular/material/sidenav';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatNativeDateModule, MatOptionModule, MatRippleModule } from '@angular/material/core';
import { MatToolbarModule } from '@angular/material/toolbar';
import { LoginComponent } from './components/login/login.component';
import { MatCardModule } from '@angular/material/card';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { ErrandsComponent } from './components/errands/errands.component';
import { FiltersComponent } from './components/filters/filters.component';
import { AuthGuard } from './guards/auth.guard';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatSelectModule } from '@angular/material/select';
import { MatExpansionModule } from '@angular/material/expansion';
import { ErrandComponent } from './components/errand/errand.component';
import { MatMenuModule } from '@angular/material/menu';
import { DistributionReportComponent } from './components/distribution-report/distribution-report.component';
import { DistributionReportEntryComponent } from './components/distribution-report-entry/distribution-report-entry.component';
import {MatTableModule} from "@angular/material/table";

const routes: Routes = [
  {
    path: 'login',
    component: LoginComponent,
  },
  {
    path: 'errands',
    canActivate: [AuthGuard],
    component: ErrandsComponent,
  },
  {
    path: 'distribution-report',
    canActivate: [AuthGuard],
    component: DistributionReportComponent,
  },
];

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    LoginComponent,
    ErrandsComponent,
    FiltersComponent,
    ErrandComponent,
    DistributionReportComponent,
    DistributionReportEntryComponent,
  ],
  imports: [
    BrowserAnimationsModule,
    BrowserModule.withServerTransition({appId: 'ng-cli-universal'}),
    HttpClientModule,
    FormsModule,
    RouterModule.forRoot(routes),
    MatSidenavModule,
    MatButtonModule,
    MatIconModule,
    MatRippleModule,
    MatToolbarModule,
    MatCardModule,
    MatFormFieldModule,
    MatInputModule,
    MatDatepickerModule,
    MatNativeDateModule,
    MatSelectModule,
    MatOptionModule,
    MatExpansionModule,
    MatMenuModule,
    MatTableModule,
  ],
  providers: [],
  bootstrap: [AppComponent],
})
export class AppModule {}
