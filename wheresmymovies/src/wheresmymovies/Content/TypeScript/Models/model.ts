module Models{
    export interface IMovie {
        Id: string;
        Title: string;
        Year: number[]; 
        Rated: string;
        Released: string;
        Runtime: string; 
        Genre: string[]; 
        Director: string[];
        Writer: string[];
        Actors: string[];
        Plot: string;
        Language: string[]; 
        Country: string;
        ThumbImgUrl: string;
        FullImgUrl: string;
        Location: string;
    }

    export class Movie implements IMovie{
        public Id: string;
        public Title: string;
        public Year: number[]; 
        public Rated: string;
        public Released: string;
        public Runtime: string; 
        public Genre: string[]; 
        public Director: string[];
        public Writer: string[];
        public Actors: string[];
        public Plot: string;
        public Language: string[]; 
        public Country: string;
        public ThumbImgUrl: string;
        public FullImgUrl: string;
        public Location: string;
    }
    
    export class MovieSearchCriteria{
        id:string;
        name:string;
    }
    
}