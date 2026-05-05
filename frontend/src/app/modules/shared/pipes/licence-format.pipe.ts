import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'judgeLicenseFormat',
  standalone: false
})
export class JudgeLicenseFormatPipe implements PipeTransform {
  transform(value: string | null | undefined): string {
    if (!value) {
      return '';
    }

    let cleaned = value.trim().toUpperCase();

    // Ako već ima pravi format, samo vrati
    if (cleaned.startsWith('JUDGE-LIC-')) {
      return cleaned;
    }

    // Ako je format JUDGE-001, uzmi samo broj 001
    if (cleaned.startsWith('JUDGE-')) {
      cleaned = cleaned.replace('JUDGE-', '');
    }

    // Ako korisnik unese samo 001, dodaj prefix
    return `JUDGE-LIC-${cleaned}`;
  }
}