export interface IPageResponse<T> {
  data: T[];
  pageSize: number;
  pageNumber: number;
  hasNextPage: boolean;
  hasPreviousPage: boolean;
  total: number;
}
