import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'phoneFormat',
  standalone: false
})
export class PhoneFormatPipe implements PipeTransform {
  transform(value: string | null | undefined): string {
    if (!value) {
      return '';
    }

    const cleaned = value.replace(/\s+/g, '');

    if (cleaned.startsWith('+387')) {
      return cleaned.replace(/(\+387)(\d{2})(\d{3})(\d{3})/, '$1 $2 $3 $4');
    }

    if (cleaned.length === 9) {
      return cleaned.replace(/(\d{3})(\d{3})(\d{3})/, '$1 $2 $3');
    }

    return value;
  }
}