import { Component, OnInit, ViewEncapsulation } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { SearchService } from '../../services/search.service';
import { Discussion, searchObject } from '../../models/discussion';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-search',
  templateUrl: './search.component.html',
  styleUrls: ['./search.component.scss'],
  encapsulation: ViewEncapsulation.None,
})
export class SearchComponent implements OnInit {

  searchString: string = "";
  searchResult: searchObject;

  constructor(private searchService: SearchService, private route: ActivatedRoute) {
    this.searchString = this.route.snapshot.paramMap.get("searchString");
    this.searchService.search(this.searchString).then(data => {
      this.searchResult = data
      console.log(this.searchResult);
      console.log(data);
    });
  }

  ngOnInit(): void {

  }
}
