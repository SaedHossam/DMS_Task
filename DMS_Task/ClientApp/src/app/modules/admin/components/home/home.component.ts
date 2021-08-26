import { Component, OnInit } from '@angular/core';
import { AdminService } from "../../../../shared/services/admin.service";
import { AdminInsights } from "../../../../models/admin-insights";

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {
  insights: AdminInsights;
  jobsByType: any;
  jobsByTypeOptions: any;

  jobsByCountry: any;
  jobsByCountryOptions: any;

  jobsByCategory: any;
  jobsByCategoryOptions: any;

  totalJobsByDate: any;
  totalJobsByDateOptions: any;

  constructor(private adminService: AdminService) { }

  ngOnInit(): void {
    
      this.insights = null;

      const jobTypesLabels = [];
      const jobTypesData = [];

      for (let item of [1, 2, 3]) {
        jobTypesLabels.push(item);
        jobTypesData.push(item);
      }

      this.jobsByType = {
        labels: jobTypesLabels,
        datasets: [
          {
            data: jobTypesData,
            backgroundColor: [
              "#FF6384",
              "#36A2EB",
              "#FFCE56",
              "#FF9020",
              "#22CFCF"
            ],
            hoverBackgroundColor: [
              "#FF6384",
              "#36A2EB",
              "#FFCE56",
              "#FF9020",
              "#22CFCF"
            ]
          }
        ]
      };
      this.jobsByTypeOptions = {
        title: {
          display: true,
          text: 'This is Static Data',
          fontSize: 16
        },
        legend: {
          position: 'bottom'
        }
      };

      const jobsCountryLabels = [];
      const jobCountryData = [];

      for (let item of [1, 2, 3]) {
        jobsCountryLabels.push(item);
        jobCountryData.push(item);
      }

      this.jobsByCountry = {
        labels: jobsCountryLabels,
        datasets: [
          {
            data: jobCountryData,
            backgroundColor: [
              "#FF6384",
              "#36A2EB",
              "#FFCE56",
              "#FF9020",
              "#22CFCF"
            ],
            hoverBackgroundColor: [
              "#FF6384",
              "#36A2EB",
              "#FFCE56",
              "#FF9020",
              "#22CFCF"
            ]
          }
        ]
      };
      this.jobsByCountryOptions = {
        title: {
          display: true,
          text: 'This is Static Data',
          fontSize: 16
        },
        legend: {
          position: 'bottom'
        }
      };

      const jobsCategoryLabels = [];
      const jobCategoryData = [];

      for (let item of [1, 2, 3]) {
        jobsCategoryLabels.push(item);
        jobCategoryData.push(item);
      }

      this.jobsByCategory = {
        labels: jobsCategoryLabels,
        datasets: [
          {
            data: jobCategoryData,
            backgroundColor: [
              "#FF6384",
              "#36A2EB",
              "#FFCE56",
              "#FF9020",
              "#22CFCF"
            ],
            hoverBackgroundColor: [
              "#FF6384",
              "#36A2EB",
              "#FFCE56",
              "#FF9020",
              "#22CFCF"
            ]
          }
        ]
      };
      this.jobsByCategoryOptions = {
        title: {
          display: true,
          text: 'This is Static Data',
          fontSize: 16
        },
        legend: {
          position: 'bottom'
        }
      };

      const totalJobsByDateLabels = [];
      const totalJobsByDateData = [];

      for (let item of [1, 2, 3]) {
        totalJobsByDateLabels.push(item);
        totalJobsByDateData.push(item);
      }

      this.totalJobsByDate = {
        labels: totalJobsByDateLabels,
        datasets: [
          {
            data: totalJobsByDateData,
            borderColor: '#42A5F5',
            fill: false
          }
        ],
      };
      this.totalJobsByDateOptions = {
        title: {
          display: true,
          text: 'This is Static Data',
          fontSize: 16
        },
        legend: {
          display: false,
          position: 'bottom'
        },
        scales: {
          yAxes: [{
            ticks: {
              suggestedMin: 0,
              suggestedMax: 10
            }
          }]
        }
      };
    
  }
}
