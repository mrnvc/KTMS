import { Directive, HostListener, inject } from '@angular/core';
import { NgControl } from '@angular/forms';

@Directive({
  selector: '[appUppercaseInput]',
  standalone: false
})
export class UppercaseInputDirective {
  private readonly ngControl = inject(NgControl, {
    optional: true,
    self: true
  });

  @HostListener('input', ['$event'])
  onInput(event: Event): void {
    const input = event.target as HTMLInputElement;

    if (!input.value) {
      return;
    }

    const uppercaseValue = input.value.toUpperCase();

    if (input.value === uppercaseValue) {
      return;
    }

    input.value = uppercaseValue;

    if (this.ngControl?.control) {
      this.ngControl.control.setValue(uppercaseValue, {
        emitEvent: false
      });
    }
  }
}