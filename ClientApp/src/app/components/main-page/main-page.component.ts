import { Component, OnInit } from '@angular/core';
import { Section } from 'src/app/models/discussion';
import { SectionsService } from 'src/app/services/sections.service';

@Component({
  selector: 'app-main-page',
  templateUrl: './main-page.component.html',
  styleUrls: ['./main-page.component.scss']
})
export class MainPageComponent implements OnInit {

  sections: Section[] = [];

  constructor(private sectionsService: SectionsService) { }

  ngOnInit(): void {
    this.sectionsService.getSections().then(sections =>
      {
        this.sections = sections;
        console.log(this.sections);
      });
  }

}
