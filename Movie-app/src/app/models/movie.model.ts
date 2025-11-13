export interface Movie {
    id?: number;
    title: string;
    description: string;
    imdbRate: number;
    genreId: number;
    genreTitle?: string;
}

export interface Genre {
    id?: number;
    title: string;
    movies?: Movie[];
}