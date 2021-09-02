import { BaseEntity } from './BaseEntity';
import { Address } from './Address';
import { Vacancy } from './Vacancy';

export class Company extends BaseEntity<number> {
  name: string = '';
  street: string = '';
  city: string = '';
  state: string = '';
  country: string = '';
  zipCode: string = '';
  vacancies: Vacancy[] = [];

  constructor() {
    super();
  }
}
