import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'clubWithoutCountry',
  standalone: false
})
export class ClubWithoutCountryPipe implements PipeTransform {
  transform(value: string | null | undefined): string {
    if (!value) {
      return '';
    }

    const parts = value.split(',').map(part => part.trim());

    if (parts.length < 3) {
      return value;
    }

    return `${parts[0]}, ${parts[1]}`;
  }
}