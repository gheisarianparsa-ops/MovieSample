import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Movie } from '../models/movie.model';
import { Genre } from '../models/movie.model';

@Injectable({
  providedIn: 'root'
})
export class ApiService {
  private baseUrl = 'https://localhost:7048/api'; // حتماً URL درست باشه

  constructor(private http: HttpClient) { }

  // Movies
  getMovies(): Observable<Movie[]> {
    return this.http.get<Movie[]>(`${this.baseUrl}/Movie`);
  }

  getMovie(id: number): Observable<Movie> {
    return this.http.get<Movie>(`${this.baseUrl}/Movie/${id}`);
  }

  createMovie(movie: Movie): Observable<Movie> {
    return this.http.post<Movie>(`${this.baseUrl}/Movie`, movie);
  }

  updateMovie(id: number, movie: Movie): Observable<Movie> {
    return this.http.put<Movie>(`${this.baseUrl}/Movie/${id}`, movie);
  }

  deleteMovie(id: number): Observable<any> {
    return this.http.delete(`${this.baseUrl}/Movie/${id}`);
  }

  // Genres
  getGenres(): Observable<Genre[]> {
    return this.http.get<Genre[]>(`${this.baseUrl}/Genre`);
  }

  createGenre(genre: Genre): Observable<Genre> {
    return this.http.post<Genre>(`${this.baseUrl}/Genre`, genre);
  }

  updateGenre(id: number, genre: Genre): Observable<Genre> {
    return this.http.put<Genre>(`${this.baseUrl}/Genre/${id}`, genre);
  }

  deleteGenre(id: number): Observable<any> {
    return this.http.delete(`${this.baseUrl}/Genre/${id}`);
  }
}
