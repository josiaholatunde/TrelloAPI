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
       console.log('I changed', res[1].path);
       this.routePath = res[1].path;
     });
     console.log('constructor ran');
   }

  ngOnInit() {
  }

}
