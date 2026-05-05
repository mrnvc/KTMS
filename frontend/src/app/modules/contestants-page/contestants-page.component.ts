import { Component, inject, signal, computed, OnInit, OnDestroy } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { MatDialog } from '@angular/material/dialog';
import { forkJoin, Subject } from 'rxjs';
import { takeUntil } from 'rxjs/operators';
import { environment } from '../../../enviroments/enviroment';
import { ContestantsApiService } from '../../api-services/contestants/contestants-api.service';
import { Contestant } from '../../api-services/contestants/contestant-api.model';
import { DialogHelperService } from '../shared/services/dialog-helper.service';
import { DialogButton } from '../shared/models/dialog-config.model';
import { AddContestantFormComponent } from './add-contestant-form/add-contestant-form.component';
import { ToasterService } from '../../core/services/toaster.service';
import { EditContestantFormComponent } from './edit-contestant-form/edit-contestant-form.component';
import jsPDF from 'jspdf';
import autoTable from 'jspdf-autotable';

interface Option {
  id: number;
  name: string;
}

interface AddContestantDialogData {
  genders: Option[];
  cities: Option[];
  belts: Option[];
  clubs: Option[];
  firstNames: string[];
  lastNames: string[];
}

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
  private readonly http = inject(HttpClient);
  private readonly toaster = inject(ToasterService);
  private readonly destroy$ = new Subject<void>();

  private readonly apiUrl = `${environment.apiUrl}/api`;

  readonly contestantsFromApi = signal<Contestant[]>([]);

  firstNameFilter = signal<string>('');
  lastNameFilter = signal<string>('');
  beltFilter = signal<string>('All Belts');
  clubFilter = signal<string>('All Clubs');

  selectedQrValue: string | null = null;
  selectedQrTitle: string | null = null;

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
          this.toaster.error('Failed to load contestants.');
        }
      });
  }

  filteredContestants = computed(() => {
    let filtered = [...this.contestantsFromApi()];

    const firstName = this.firstNameFilter().toLowerCase();
    if (firstName) {
      filtered = filtered.filter(c => c.firstName.toLowerCase().includes(firstName));
    }

    const lastName = this.lastNameFilter().toLowerCase();
    if (lastName) {
      filtered = filtered.filter(c => c.lastName.toLowerCase().includes(lastName));
    }

    if (this.beltFilter() !== 'All Belts') {
      filtered = filtered.filter(c => c.belt === this.beltFilter());
    }

    if (this.clubFilter() !== 'All Clubs') {
      filtered = filtered.filter(c => c.club === this.clubFilter());
    }

    filtered = filtered.filter(c => !!c.firstName);

    return filtered.sort((a, b) => {
      const aName = a.firstName ?? '';
      const bName = b.firstName ?? '';
      return aName.localeCompare(bName);
    });
  });

  getFullName(contestant: Contestant): string {
    return `${contestant.firstName} ${contestant.lastName}`.trim();
  }

  onAddContestant(): void {
    forkJoin({
      cities: this.http.get<any[]>(`${this.apiUrl}/City/GetCities`),
      genders: this.http.get<any[]>(`${this.apiUrl}/Gender/GetGenders`),
      belts: this.http.get<any[]>(`${this.apiUrl}/Belt/GetBelts`),
      clubs: this.http.get<any[]>(`${this.apiUrl}/Club/GetClubs`)
    })
      .pipe(takeUntil(this.destroy$))
      .subscribe({
        next: data => {
          const dialogData: AddContestantDialogData = {
            cities: data.cities.map(city => ({
              id: city.id,
              name: `${city.cityName}, ${city.country}`
            })),
            genders: data.genders.map(gender => ({
              id: gender.id,
              name: gender.name
            })),
            belts: data.belts.map((belt, index) => ({
              id: belt.id ?? belt.rankOrder ?? index + 1,
              name: belt.name
            })),
            clubs: data.clubs.map((club, index) => ({
              id: club.id ?? index + 1,
              name: `${club.name}, ${club.city}, ${club.country}`
            })),
            firstNames: Array.from(
              new Set(
                this.contestantsFromApi()
                  .map(c => c.firstName)
                  .filter(name => !!name)
              )
            ),
            lastNames: Array.from(
              new Set(
                this.contestantsFromApi()
                  .map(c => c.lastName)
                  .filter(name => !!name)
              )
            )
          };

          const dialogRef = this.dialog.open(AddContestantFormComponent, {
            width: '820px',
            maxWidth: '95vw',
            maxHeight: '90vh',
            disableClose: true,
            panelClass: 'contestant-dialog-panel',
            autoFocus: false,
            data: dialogData
          });

          dialogRef.afterClosed().subscribe((wasCreated?: boolean) => {
            if (wasCreated) {
              this.loadContestants();
            }
          });
        },
        error: err => {
          console.error('Error loading form data:', err);
          this.toaster.error('Failed to load form data.');
        }
      });
  }

  onEditContestant(contestant: Contestant): void {
    forkJoin({
      cities: this.http.get<any[]>(`${this.apiUrl}/City/GetCities`),
      genders: this.http.get<any[]>(`${this.apiUrl}/Gender/GetGenders`),
      belts: this.http.get<any[]>(`${this.apiUrl}/Belt/GetBelts`),
      clubs: this.http.get<any[]>(`${this.apiUrl}/Club/GetClubs`),
      contestantDetails: this.contestantsService.getContestant(contestant.id)
    })
      .pipe(takeUntil(this.destroy$))
      .subscribe({
        next: data => {
          const dialogRef = this.dialog.open(EditContestantFormComponent, {
            width: '820px',
            maxWidth: '95vw',
            maxHeight: '90vh',
            disableClose: true,
            panelClass: 'contestant-dialog-panel',
            autoFocus: false,
            data: {
              contestant: data.contestantDetails,

              cities: data.cities.map(city => ({
                id: city.id,
                name: `${city.cityName}, ${city.country}`
              })),

              genders: data.genders.map(gender => ({
                id: gender.id,
                name: gender.name
              })),

              belts: data.belts.map((belt, index) => ({
                id: belt.id ?? belt.rankOrder ?? index + 1,
                name: belt.name
              })),

              clubs: data.clubs.map((club, index) => ({
                id: club.id ?? index + 1,
                name: `${club.name}, ${club.city}, ${club.country}`
              })),

              firstNames: Array.from(
                new Set(
                  this.contestantsFromApi()
                    .map(c => c.firstName)
                    .filter(name => !!name)
                )
              ),

              lastNames: Array.from(
                new Set(
                  this.contestantsFromApi()
                    .map(c => c.lastName)
                    .filter(name => !!name)
                )
              )
            }
          });

          dialogRef.afterClosed().subscribe((wasUpdated?: boolean) => {
            if (wasUpdated) {
              this.loadContestants();
            }
          });
        },
        error: err => {
          console.error('Error loading contestant details:', err);
          this.toaster.error('Failed to load contestant details.');
        }
      });
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
      'Delete Contestant?',
      `Are you sure you want to delete ${this.getFullName(contestant)}?`
    ).subscribe(result => {
      if (result?.button === DialogButton.YES) {
        this.contestantsService.deleteContestant(contestant.id).subscribe({
          next: () => {
            this.loadContestants();
            this.toaster.success('Contestant deleted successfully.');
          },
          error: (err) => {
            console.error('Error deleting contestant:', err);
            this.toaster.error('Failed to delete contestant.');
          }
        });
      }
    });
  }

  showQrCode(contestant: Contestant): void {
    this.selectedQrValue = `${window.location.origin}/admin/contestants?id=${contestant.id}`;
    this.selectedQrTitle = this.getFullName(contestant);
  }

  closeQrCode(): void {
    this.selectedQrValue = null;
    this.selectedQrTitle = null;
  }

  generatePdfReport(): void {
    const contestants = this.filteredContestants();

    if (contestants.length === 0) {
      this.toaster.warning('No contestants available for PDF report.');
      return;
    }

    const doc = new jsPDF();

    const generatedDate = new Date().toLocaleString();

    doc.setFontSize(18);
    doc.text('Contestants Report', 14, 18);

    doc.setFontSize(10);
    doc.text(`Generated: ${generatedDate}`, 14, 26);

    doc.text('Applied Filters:', 14, 36);

    const filterText = [
      `First Name: ${this.firstNameFilter() || 'All'}`,
      `Last Name: ${this.lastNameFilter() || 'All'}`,
      `Belt: ${this.beltFilter()}`,
      `Club: ${this.clubFilter()}`
    ];

    doc.setFontSize(9);
    filterText.forEach((filter, index) => {
      doc.text(filter, 14, 43 + index * 6);
    });

    autoTable(doc, {
      startY: 72,
      head: [['#', 'Name', 'Club', 'Belt']],
      body: contestants.map((contestant, index) => [
        index + 1,
        this.getFullName(contestant),
        this.formatClubWithoutCountry(contestant.club),
        contestant.belt
      ]),
      styles: {
        fontSize: 9,
        cellPadding: 3
      },
      headStyles: {
        fillColor: [37, 99, 235],
        textColor: 255
      }
    });

    doc.save('contestants-report.pdf');
  }

  private formatClubWithoutCountry(club: string): string {
    if (!club) {
      return '';
    }

    const parts = club.split(',').map(part => part.trim());

    if (parts.length < 3) {
      return club;
    }

    return `${parts[0]}, ${parts[1]}`;
  }
}