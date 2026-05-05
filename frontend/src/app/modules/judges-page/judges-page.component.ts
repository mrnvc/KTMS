import { Component, computed, inject, OnDestroy, OnInit, signal } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { takeUntil } from 'rxjs/operators';
import { JudgesApiService } from '../../api-services/judges/judges-api.service';
import { Judge } from '../../api-services/judges/judge-api.model';
import { DialogHelperService } from '../shared/services/dialog-helper.service';
import { DialogButton } from '../shared/models/dialog-config.model';
import { ToasterService } from '../../core/services/toaster.service';
import { HttpClient } from '@angular/common/http';
import { forkJoin, Subject } from 'rxjs';
import { environment } from '../../../enviroments/enviroment';
import { AddJudgeFormComponent } from './add-judge-form/add-judge-form.component';

@Component({
  selector: 'app-judges-page',
  standalone: false,
  templateUrl: './judges-page.component.html',
  styleUrl: './judges-page.component.scss'
})
export class JudgesPageComponent implements OnInit, OnDestroy {
  private readonly judgesService = inject(JudgesApiService);
  private readonly dialogHelper = inject(DialogHelperService);
  private readonly dialog = inject(MatDialog);
  private readonly toaster = inject(ToasterService);
  private readonly destroy$ = new Subject<void>();
  private readonly http = inject(HttpClient);
private readonly apiUrl = `${environment.apiUrl}/api`;

  readonly judgesFromApi = signal<Judge[]>([]);

  firstNameFilter = signal<string>('');
  lastNameFilter = signal<string>('');
  licenseFilter = signal<string>('');
  rankFilter = signal<string>('All Ranks');

  uniqueRanks = computed(() => {
    const ranks = new Set(
      this.judgesFromApi()
        .map(j => j.rank)
        .filter(rank => !!rank)
    );

    return Array.from(ranks).sort();
  });

  ngOnInit(): void {
    this.loadJudges();
  }

  ngOnDestroy(): void {
    this.destroy$.next();
    this.destroy$.complete();
  }

  private loadJudges(): void {
    this.judgesService.getJudges()
      .pipe(takeUntil(this.destroy$))
      .subscribe({
        next: (data: Judge[]) => {
          this.judgesFromApi.set(data);
        },
        error: err => {
          console.error('Error loading judges:', err);
          this.toaster.error('Failed to load judges.');
        }
      });
  }

  filteredJudges = computed(() => {
    let filtered = [...this.judgesFromApi()];

    const firstName = this.firstNameFilter().toLowerCase();
    if (firstName) {
      filtered = filtered.filter(j => j.name.toLowerCase().includes(firstName));
    }

    const lastName = this.lastNameFilter().toLowerCase();
    if (lastName) {
      filtered = filtered.filter(j => j.surname.toLowerCase().includes(lastName));
    }

    const license = this.licenseFilter().toLowerCase();
    if (license) {
      filtered = filtered.filter(j => j.license.toLowerCase().includes(license));
    }

    if (this.rankFilter() !== 'All Ranks') {
      filtered = filtered.filter(j => j.rank === this.rankFilter());
    }

    return filtered.sort((a, b) => {
      const aName = `${a.name} ${a.surname}`;
      const bName = `${b.name} ${b.surname}`;
      return aName.localeCompare(bName);
    });
  });

  getFullName(judge: Judge): string {
    return `${judge.name} ${judge.surname}`.trim();
  }

  onAddJudge(): void {
  forkJoin({
    cities: this.http.get<any[]>(`${this.apiUrl}/City/GetCities`),
    genders: this.http.get<any[]>(`${this.apiUrl}/Gender/GetGenders`)
  })
    .pipe(takeUntil(this.destroy$))
    .subscribe({
      next: data => {
        const dialogRef = this.dialog.open(AddJudgeFormComponent, {
          width: '820px',
          maxWidth: '95vw',
          maxHeight: '90vh',
          disableClose: true,
          panelClass: 'judge-dialog-panel',
          autoFocus: false,
          data: {
            cities: data.cities.map(city => ({
              id: city.id,
              name: `${city.cityName}, ${city.country}`
            })),
            genders: data.genders.map(gender => ({
              id: gender.id,
              name: gender.name
            }))
          }
        });

        dialogRef.afterClosed().subscribe((wasCreated?: boolean) => {
          if (wasCreated) {
            this.loadJudges();
          }
        });
      },
      error: err => {
        console.error('Error loading judge form data:', err);
        this.toaster.error('Failed to load form data.');
      }
    });
}

  onEditJudge(judge: Judge): void {
    // TODO: Open edit judge dialog
    console.log('Edit judge:', judge);
  }

  onFirstNameChange(value: string): void {
    this.firstNameFilter.set(value);
  }

  onLastNameChange(value: string): void {
    this.lastNameFilter.set(value);
  }

  onLicenseChange(value: string): void {
    this.licenseFilter.set(value);
  }

  onRankChange(value: string): void {
    this.rankFilter.set(value);
  }

  onDeleteJudge(judge: Judge): void {
    this.dialogHelper.confirm(
      'Delete Judge?',
      `Are you sure you want to delete ${this.getFullName(judge)}?`
    ).subscribe(result => {
      if (result?.button === DialogButton.YES) {
        this.judgesService.deleteJudge(judge.id)
          .pipe(takeUntil(this.destroy$))
          .subscribe({
            next: () => {
              this.loadJudges();
              this.toaster.success('Judge deleted successfully.');
            },
            error: err => {
              console.error('Error deleting judge:', err);
              this.toaster.error('Failed to delete judge.');
            }
          });
      }
    });
  }
}