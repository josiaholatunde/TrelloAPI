import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-test-header',
  templateUrl: './test-header.component.html',
  styleUrls: ['./test-header.component.css']
})
export class TestHeaderComponent implements OnInit {
  routePath: any;

  constructor(private route: ActivatedRoute) {
     this.route.url.subscribe(res => {
       this.routePath = res[1].path;
     });
   }

  ngOnInit() {
  }

}
