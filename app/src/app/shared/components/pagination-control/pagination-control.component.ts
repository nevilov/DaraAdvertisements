import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';

@Component({
    selector: 'app-pagination-control',
    templateUrl: './pagination-control.component.html',
    styleUrls: ['./pagination-control.component.scss'],
})
export class PaginationControlComponent implements OnInit {
    @Input() limit: number = 12;
    @Input() offset: number = 0;
    @Input() range: number = 3;
    @Input() total: number = 0;

    @Output() pageChange: EventEmitter<number> = new EventEmitter<number>();

    pages: number[] = [];
    currentPage: number = 0;
    totalPages: number = 0;

    constructor() { }

    ngOnInit(): void {
        this.getPages(this.offset, this.limit, this.total);
    }
    ngOnChanges() {
        this.getPages(this.offset, this.limit, this.total);
    }

    getPages(offset: number, limit: number, total: number) {
        this.currentPage = this.getCurrentPage(offset, limit);

        this.totalPages = this.getTotalPages(limit, total);

        let pages = this.getNumberOfPage(
            this.range,
            this.currentPage,
            this.totalPages
        );

        this.pages = pages;
    }

    selectPage(page: number) {
        if (this.isValidPageNumber(page, this.totalPages)) {
            this.pageChange.emit((page - 1) * this.limit);
            window.scroll(0, 0);
        }
    }

    getCurrentPage(offset: number, limit: number): number {
        return Math.floor(offset / limit) + 1;
    }

    getTotalPages(limit: number, total: number): number {
        return Math.ceil(Math.max(total, 1) / Math.max(limit, 1));
    }

    getNumberOfPage(range: number, currentPage: number, totalPages: number) {
        return Array.from({ length: 7 }, (x, i) => i - range)
            .map((offset) => currentPage + offset)
            .filter((page) => this.isValidPageNumber(page, totalPages));
    }

    isValidPageNumber(page: number, totalPages: number): boolean {
        return page > 0 && page <= totalPages;
    }
}
