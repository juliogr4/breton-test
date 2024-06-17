export class Pagination {
  totalItems: number;
  totalIPages: number;
  pageSize: number;
  pageNumber: number;

  constructor(totalItems: number, totalIPages: number, pageSize: number, pageNumber: number) {
    this.totalItems = totalItems;
    this.totalIPages = totalIPages;
    this.pageSize = pageSize;
    this.pageNumber = pageNumber;
  }
};
