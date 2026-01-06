import { Component, computed, inject, signal } from "@angular/core";
import { toSignal } from "@angular/core/rxjs-interop";
import { TournamentsService } from "../services/tournaments.service";
import { Tournament } from "../models/tournament.model";
import {ActivatedRoute, Router} from '@angular/router';

@Component({
  selector: "app-tournaments-page",
  standalone: false,
  templateUrl: "./tournaments-page.component.html",
  styleUrl: "./tournaments-page.component.scss",
})
export class TournamentsPageComponent {
  private router = inject(Router);
  private route = inject(ActivatedRoute);
  private readonly tournamentsService = inject(TournamentsService);

  // API -> signal (auto subscribe/unsubscribe)
  private readonly tournamentsFromApi = toSignal(
    this.tournamentsService.getTournaments(),
    { initialValue: [] as Tournament[] }
  );

  // Filters
  statusFilter = signal<"all" | string>("All");
  dateFromFilter = signal<string>("");
  dateToFilter = signal<string>("");
  timeFromFilter = signal<string>("");
  timeToFilter = signal<string>("");

  filteredAndSortedTournaments = computed(() => {
    let filtered = this.tournamentsFromApi();

    // status
    if (this.statusFilter() !== "All") {
      filtered = filtered.filter(t => t.status === this.statusFilter());
    }

    // date range
    const from = this.dateFromFilter();
    if (from) {
      const fromDate = new Date(from);
      filtered = filtered.filter(t => new Date(t.date) >= fromDate);
    }

    const to = this.dateToFilter();
    if (to) {
      const toDate = new Date(to);
      filtered = filtered.filter(t => new Date(t.date) <= toDate);
    }

    // time range (string compare radi za "HH:mm")
    const timeFrom = this.timeFromFilter();
    if (timeFrom) {
      filtered = filtered.filter(t => (t.startTime ?? "").slice(0,5) >= timeFrom);
    }

    const timeTo = this.timeToFilter();
    if (timeTo) {
      filtered = filtered.filter(t => (t.startTime ?? "").slice(0,5) <= timeTo);
    }

    // sort by date asc
    return [...filtered].sort(
      (a, b) => new Date(a.date).getTime() - new Date(b.date).getTime()
    );
  });

  // UI helpers
  formatDate(dateString: string): string {
    const d = new Date(dateString);
    return d.toLocaleDateString("en-US", {
      weekday: "short",
      year: "numeric",
      month: "short",
      day: "numeric",
    });
  }

  setStatus(status: "All" | "Active" | "Completed") {
    this.statusFilter.set(status);

    if (status === "All") {
      this.router.navigate(["/tournaments"]);
    } else {
      this.router.navigate(["/tournaments", status]);
    }
  }

  clearFilters() {
    this.statusFilter.set("all");
    this.dateFromFilter.set("");
    this.dateToFilter.set("");
    this.timeFromFilter.set("");
    this.timeToFilter.set("");
  }

  onTournamentClick(t: Tournament) {
    // Za sad ne otvaramo details
    console.log("Tournament clicked:", t);
  }

  protected readonly history = history;
}
