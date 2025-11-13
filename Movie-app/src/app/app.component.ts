import { Component, OnInit } from '@angular/core';
import { ApiService } from './services/api.service';
import { Movie, Genre } from './models/movie.model';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  movies: Movie[] = [];
  genres: Genre[] = [];
  filteredMovies: Movie[] = [];
  activeTab = 'movies';
  searchTerm = '';
  loading = false;

  showMovieModal = false;
  showGenreModal = false;

  editingMovie: Movie | null = null;
  editingGenre: Genre | null = null;

  movieForm: Movie = { title: '', description: '', imdbRate: 0, genreId: 0 };
  genreForm: Genre = { title: '' };

  constructor(private apiService: ApiService) {}

  ngOnInit(): void {
    this.loadMovies();
    this.loadGenres();
  }

  loadMovies(): void {
    this.loading = true;
    this.apiService.getMovies().subscribe({
      next: data => {
        this.movies = data;
        this.filteredMovies = data;
        this.loading = false;
      },
      error: err => { console.error(err); this.loading = false; }
    });
  }

  loadGenres(): void {
    this.apiService.getGenres().subscribe({
      next: data => this.genres = data,
      error: err => console.error(err)
    });
  }

  searchMovies(): void {
    if (!this.searchTerm) { this.filteredMovies = this.movies; return; }
    this.filteredMovies = this.movies.filter(m =>
      m.title.toLowerCase().includes(this.searchTerm.toLowerCase()) ||
      m.description?.toLowerCase().includes(this.searchTerm.toLowerCase())
    );
  }

  openMovieModal(movie?: Movie): void {
    if (movie) { this.editingMovie = movie; this.movieForm = { ...movie }; }
    else { this.editingMovie = null; this.movieForm = { title: '', description: '', imdbRate: 0, genreId: 0 }; }
    this.showMovieModal = true;
  }

  closeMovieModal(): void { this.showMovieModal = false; this.editingMovie = null; }

  saveMovie(): void {
    this.loading = true;
    if (this.editingMovie) {
      this.apiService.updateMovie(this.editingMovie.id!, this.movieForm).subscribe({
        next: () => { this.loadMovies(); this.closeMovieModal(); this.loading = false; },
        error: err => { console.error(err); this.loading = false; }
      });
    } else {
      this.apiService.createMovie(this.movieForm).subscribe({
        next: () => { this.loadMovies(); this.closeMovieModal(); this.loading = false; },
        error: err => { console.error(err); this.loading = false; }
      });
    }
  }

  deleteMovie(id: number): void {
    if (!confirm('آیا از حذف این فیلم مطمئن هستید؟')) return;
    this.apiService.deleteMovie(id).subscribe({
      next: () => this.loadMovies(),
      error: err => console.error(err)
    });
  }

  getGenreName(genreId: number): string {
    const genre = this.genres.find(g => g.id === genreId);
    return genre ? genre.title : 'نامشخص';
  }

  // مشابه Movie: Genres
  openGenreModal(genre?: Genre): void { this.editingGenre = genre || null; this.genreForm = genre ? { ...genre } : { title: '' }; this.showGenreModal = true; }
  closeGenreModal(): void { this.showGenreModal = false; this.editingGenre = null; }
  saveGenre(): void {
    this.loading = true;
    if (this.editingGenre) {
      this.apiService.updateGenre(this.editingGenre.id!, this.genreForm).subscribe({
        next: () => { this.loadGenres(); this.closeGenreModal(); this.loading = false; },
        error: err => { console.error(err); this.loading = false; }
      });
    } else {
      this.apiService.createGenre(this.genreForm).subscribe({
        next: () => { this.loadGenres(); this.closeGenreModal(); this.loading = false; },
        error: err => { console.error(err); this.loading = false; }
      });
    }
  }

  deleteGenre(id: number): void {
    if (!confirm('آیا از حذف این ژانر مطمئن هستید؟')) return;
    this.apiService.deleteGenre(id).subscribe({ next: () => this.loadGenres(), error: err => console.error(err) });
  }
}