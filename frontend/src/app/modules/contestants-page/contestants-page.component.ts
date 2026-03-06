import { Component, inject, signal, computed, OnInit, OnDestroy } from '@angular/core';
import { ContestantsApiService } from '../../api-services/contestants/contestants-api.service';
import { Contestant } from '../../api-services/contestants/contestant-api.model';
import { DialogHelperService } from '../shared/services/dialog-helper.service';
import { DialogButton } from '../shared/models/dialog-config.model';
import { MatDialog } from '@angular/material/dialog';
import { Subject } from 'rxjs';
import { takeUntil } from 'rxjs/operators';

@Component({
  selector: 'app-contestants-page',
  standalone: false,
  templateUrl: './contestants-page.component.html',
  styleUrl: './contestants-page.component.scss',
})
export class ContestantsPageComponent implements OnInit, OnDestroy {
  private readonly contestantsService = inject(ContestantsApiService);
  private readonly dialogHelper = inject(DialogHelperService);
  private readonly dialog = inject(MatDialog);
  private readonly destroy$ = new Subject<void>();

  // API -> signal
  readonly contestantsFromApi = signal<Contestant[]>([]);

  // Filters
  firstNameFilter = signal<string>("");
  lastNameFilter = signal<string>("");
  beltFilter = signal<string>("All Belts");
  clubFilter = signal<string>("All Clubs");

  // Get unique values for dropdowns
  uniqueBelts = computed(() => {
    const belts = new Set(this.contestantsFromApi().map(c => c.belt));
    return Array.from(belts).sort();
  });

  uniqueClubs = computed(() => {
    const clubs = new Set(this.contestantsFromApi().map(c => c.club));
    return Array.from(clubs).sort();
  });

  ngOnInit(): void {
    this.loadContestants();
  }

  ngOnDestroy(): void {
    this.destroy$.next();
    this.destroy$.complete();
  }

  private loadContestants(): void {
    this.contestantsService.getContestants()
      .pipe(takeUntil(this.destroy$))
      .subscribe({
        next: (data: Contestant[]) => {
          this.contestantsFromApi.set(data);
        },
        error: (err) => {
          console.error('Error loading contestants:', err);
        }
      });
  }

  // Filtered contestants
  filteredContestants = computed(() => {
    let filtered = this.contestantsFromApi();

    // First name filter
    const firstName = this.firstNameFilter().toLowerCase();
    if (firstName) {
      filtered = filtered.filter(c => c.firstName.toLowerCase().includes(firstName));
    }

    // Last name filter
    const lastName = this.lastNameFilter().toLowerCase();
    if (lastName) {
      filtered = filtered.filter(c => c.lastName.toLowerCase().includes(lastName));
    }

    // Belt filter
    if (this.beltFilter() !== "All Belts") {
      filtered = filtered.filter(c => c.belt === this.beltFilter());
    }

    // Club filter
    if (this.clubFilter() !== "All Clubs") {
      filtered = filtered.filter(c => c.club === this.clubFilter());
    }


    // Remove any contestants lacking a first name to avoid sort errors
    filtered = filtered.filter(c => !!c.firstName);

    // Guard against missing names so we don't try to call localeCompare on undefined
    return filtered.sort((a, b) => {
      const aName = a.firstName ?? '';
      const bName = b.firstName ?? '';
      return aName.localeCompare(bName);
    });
  });

  getFullName(contestant: Contestant): string {
    return `${contestant.firstName} ${contestant.lastName}`;
  }

  onAddContestant(): void {
    // TODO: Open add contestant dialog
    console.log('Add contestant clicked');
  }

  onEditContestant(contestant: Contestant): void {
    // TODO: Open edit contestant dialog
    console.log('Edit contestant:', contestant);
  }

  onFirstNameChange(value: string): void {
    this.firstNameFilter.set(value);
  }

  onLastNameChange(value: string): void {
    this.lastNameFilter.set(value);
  }

  onBeltChange(value: string): void {
    this.beltFilter.set(value);
  }

  onClubChange(value: string): void {
    this.clubFilter.set(value);
  }


  onDeleteContestant(contestant: Contestant): void {
    this.dialogHelper.confirm(
      `Delete Contestant?`,
      `Are you sure you want to delete ${this.getFullName(contestant)}?`
    ).subscribe(result => {
      if (result?.button === DialogButton.YES) {
        this.contestantsService.deleteContestant(contestant.id).subscribe({
          next: () => {
            // Refresh the list
            window.location.reload();
          },
          error: (err) => {
            console.error('Error deleting contestant:', err);
          }
        });
      }
    });
  }
}
