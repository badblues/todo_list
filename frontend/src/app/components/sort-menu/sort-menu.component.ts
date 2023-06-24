import { Component } from '@angular/core';
import { UiService } from 'src/app/services/ui.service';

@Component({
  selector: 'app-sort-menu',
  templateUrl: './sort-menu.component.html',
  styleUrls: ['./sort-menu.component.css']
})
export class SortMenuComponent {
  sortItems: string[] = ["oldest", "newest", "alphabetical"];
  selectedSort = this.sortItems[0];
  isOpen: boolean = false;

  constructor(private uiService: UiService) {}

  toggleMenu(): void {
    this.isOpen = !this.isOpen;
  }

  onSelect(sort: string): void {
    this.selectedSort = sort;
    this.isOpen = false;
    this.uiService.selectSort(sort);
  }
}
