export type GetCustomersRequest = {
  pageSize: number;
  pageNumber: number;
  cpf?: string | null;
  name?: string | null;
}
