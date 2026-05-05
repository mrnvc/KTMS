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

    // Ukloni razmake i crtice
    let cleaned = value.replace(/[\s-]/g, '');

    // Ako broj počinje sa 0, pretvori ga u +387 format
    // 061111110 -> +38761111110
    if (cleaned.startsWith('0') && cleaned.length === 9) {
      cleaned = '+387' + cleaned.substring(1);
    }

    // Ako broj počinje sa 387 bez plusa, dodaj plus
    // 38761111110 -> +38761111110
    if (cleaned.startsWith('387') && cleaned.length === 11) {
      cleaned = '+' + cleaned;
    }

    // +38761111110 -> +387 61 111 110
    if (cleaned.startsWith('+387') && cleaned.length === 12) {
      return cleaned.replace(/(\+387)(\d{2})(\d{3})(\d{3})/, '$1 $2 $3 $4');
    }

    return value;
  }
}