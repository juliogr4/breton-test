import { IAddressResponse } from "./IAddressResponse";

export interface ICustomerResponse {
  id: number;
  createdBy: number;
  name: string;
  cpf: string;
  birthDate: Date;
  phone: string;
  address?: IAddressResponse
}
