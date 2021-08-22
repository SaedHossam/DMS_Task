import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.css']
})
export class ProfileComponent implements OnInit {
  public errorMessage: string = '';
  public website: string;
  public email: string;
  public emailLink: string;
  public facebook: string;
  public linkedin: string;

  constructor(private ac: ActivatedRoute) {
  }
  isLoading: boolean;
  ngOnInit(): void {

    this.ac.params.subscribe(p => {
      if (p.id === undefined) {
        this.isLoading = true;
        
      } else {
        this.isLoading = true;
        
      }

    })























  }

  toAbsoluteLink(link: string) {
    if (link == null || link.length == 0) {
      return '';
    }

    var result;
    var startingUrl = "http://";
    var httpsStartingUrl = "https://";
    if (this.startWith(link, startingUrl) || this.startWith(link, httpsStartingUrl)) {
      result = link;
    }
    else {
      result = startingUrl + link;
    }
    return result;
  }

  startWith(string: string, subString: string) {
    return string.indexOf(subString) == 0;
  };

}
