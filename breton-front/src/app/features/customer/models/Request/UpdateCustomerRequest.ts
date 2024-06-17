import { AddressRequest } from "./AddressRequest"

export type UpdateCustomerRequest = {
  id: number;
  name: string;
  cpf: string;
  birthDate: Date;
  phone: string;
  address: AddressRequest;
}
