import { AddressRequest } from "./AddressRequest"

export type CreateCustomerRequest = {
  createdBy: number,
  name: string ,
  CPF: string,
  birthdate: Date,
  phone: string,
  address: AddressRequest
}
