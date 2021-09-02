import { Component, OnInit } from '@angular/core';
import { DomSanitizer } from '@angular/platform-browser';
import { MatIconRegistry } from '@angular/material/icon';
import { CompanyService } from 'src/app/services/company.service';
import { Company } from 'src/app/models';

const LOCATION_ICON = `<svg xmlns="http://www.w3.org/2000/svg" height="24px" viewBox="0 0 24 24" width="24px" fill="#FFFFFF"><path d="M0 0h24v24H0z" fill="none"/><path d="M12 2C8.13 2 5 5.13 5 9c0 5.25 7 13 7 13s7-7.75 7-13c0-3.87-3.13-7-7-7zm0 9.5c-1.38 0-2.5-1.12-2.5-2.5s1.12-2.5 2.5-2.5 2.5 1.12 2.5 2.5-1.12 2.5-2.5 2.5z"/></svg>`;

@Component({
  selector: 'app-company-overview',
  templateUrl: './company-overview.component.html',
  styleUrls: ['./company-overview.component.scss']
})
export class CompanyOverviewComponent implements OnInit {
  companies: Company[] = [];

  constructor(
    iconRegistry: MatIconRegistry,
    sanitizer: DomSanitizer,
    private companyService: CompanyService
  ) {
    iconRegistry.addSvgIconLiteral(
      'location',
      sanitizer.bypassSecurityTrustHtml(LOCATION_ICON)
    );
  }

  ngOnInit(): void {
    this.companyService.getCompaniesWithActiveVacancies().subscribe(data => {
      this.companies = data.companies;
      console.log(this.companies);
    });
  }
}
