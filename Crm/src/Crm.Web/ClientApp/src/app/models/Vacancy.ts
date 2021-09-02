import { BaseEntity } from './BaseEntity';

export class Vacancy extends BaseEntity<number> {
  title: string = '';
  description: string = '';
  expired: boolean = false;

  constructor() {
    super();
  }
}
