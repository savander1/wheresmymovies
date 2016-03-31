/// <reference path="../Framework/viewmodel.ts" />

module Models{
    
    export interface ViewModel {
        getHtmlElements() : HTMLElement[];
        isValid() : boolean;
    }
    
    export class MovieViewModel implements ViewModel {
        private _fields: Form.Field<any>[];
        private _id: ViewModel.ObservableProperty<string>;
        private _title: ViewModel.ObservableProperty<string>;
        private _year: ViewModel.ObservableProperty<number[]>; 
        private _rated: ViewModel.ObservableProperty<string>;
        private _released: ViewModel.ObservableProperty<string>;
        private _runtime: ViewModel.ObservableProperty<string>; 
        private _genre: ViewModel.ObservableProperty<string[]>; 
        private _director: ViewModel.ObservableProperty<string[]>;
        private _writer: ViewModel.ObservableProperty<string[]>;
        private _actors: ViewModel.ObservableProperty<string[]>;
        private _plot: ViewModel.ObservableProperty<string>;
        private _language: ViewModel.ObservableProperty<string[]>; 
        private _country: ViewModel.ObservableProperty<string>;
        private _thumbImgUrl: ViewModel.ObservableProperty<string>;
        private _fullImgUrl: ViewModel.ObservableProperty<string>;
        private _location: ViewModel.ObservableProperty<string>;
        
        public constructor (id: ViewModel.ObservableProperty<string>,
                            title: ViewModel.ObservableProperty<string>,
                            year: ViewModel.ObservableProperty<number[]>, 
                            rated: ViewModel.ObservableProperty<string>,
                            released: ViewModel.ObservableProperty<string>,
                            runtime: ViewModel.ObservableProperty<string>, 
                            genre: ViewModel.ObservableProperty<string[]>, 
                            director: ViewModel.ObservableProperty<string[]>,
                            writer: ViewModel.ObservableProperty<string[]>,
                            actors: ViewModel.ObservableProperty<string[]>,
                            plot: ViewModel.ObservableProperty<string>,
                            language: ViewModel.ObservableProperty<string[]>, 
                            country: ViewModel.ObservableProperty<string>,
                            thumbImgUrl: ViewModel.ObservableProperty<string>,
                            fullImgUrl: ViewModel.ObservableProperty<string>,
                            location: ViewModel.ObservableProperty<string>){
            this._id = id;
            this._title = title;
            this._year = year;
            this._rated = rated;
            this._released = released;
            this._runtime = runtime;
            this._genre = genre;
            this._director = director;
            this._writer = writer;
            this._actors = actors;
            this._plot = plot;
            this._language = language;
            this._country = country;
            this._thumbImgUrl = thumbImgUrl;
            this._fullImgUrl = fullImgUrl;
            this._location = location;
        } 
        
        
        public get id(): string {
            return this._id.propertyValue;
        }
        public set id(value: string){
            this._id.propertyValue = value;
        }
       
        public get title(): string {
            return this._title.propertyValue;
        }
        public set title(value: string){
            this._title.propertyValue = value;
        }
        
        public get year(): number[] {
            return this._year.propertyValue;
        }
        public set year(value: number[]){
            this._year.propertyValue = value;
        }
        
        public get rated(): string {
            return this._rated.propertyValue;
        }
        public set rated(value: string){
            this._rated.propertyValue = value;
        }
        
        public get released(): string {
            return this._released.propertyValue;
        }
        public set released(value: string){
            this._released.propertyValue = value;
        }
        
        public get runtime(): string {
            return this._runtime.propertyValue;
        }
        public set runtime(value: string){
            this._runtime.propertyValue = value;
        }
        
        public get genre(): string[] {
            return this._genre.propertyValue;
        }
        public set genre(value: string[]){
            this._genre.propertyValue = value;
        }
        
        public get director(): string[] {
            return this._director.propertyValue;
        }
        public set director(value: string[]){
            this._director.propertyValue = value;
        }
        
        public get writer(): string[] {
            return this._writer.propertyValue;
        }
        public set writer(value: string[]){
            this._writer.propertyValue = value;
        }
        
        public get actors(): string[] {
            return this._actors.propertyValue;
        }
        public set actors(value: string[]){
            this._actors.propertyValue = value;
        }
        
        public get plot(): string {
            return this._plot.propertyValue;
        }
        public set plot(value: string){
            this._plot.propertyValue = value;
        }
        
        public get language(): string[] {
            return this._language.propertyValue;
        }
        public set language(value: string[]){
            this._language.propertyValue = value;
        }
        
        public get country(): string {
            return this._country.propertyValue;
        }
        public set country(value: string){
            this._country.propertyValue = value;
        }
        
        public get thumbImgUrl(): string {
            return this._thumbImgUrl.propertyValue;
        }
        public set thumbImgUrl(value: string){
            this._thumbImgUrl.propertyValue = value;
        }
        
        public get fullImgUrl(): string {
            return this._fullImgUrl.propertyValue;
        }
        public set fullImgUrl(value: string){
            this._fullImgUrl.propertyValue = value;
        }
       
        public get location(): string {
            return this._location.propertyValue;
        }
        public set location(value: string){
            this._location.propertyValue = value;
        }       
        
        public addListener(propertyName: string, listener: ViewModel.PropertyObserver<any>){
            
            switch(propertyName){
                case 'id':
                    this._id.registerObserver(listener);
                    break;
                case 'title':
                    this._title.registerObserver(listener);
                    break;
                case 'year':
                    this._year.registerObserver(listener);
                    break;
                case 'rated':
                    this._rated.registerObserver(listener);
                    break;
                case 'released':
                    this._released.registerObserver(listener);
                    break;
                case 'runtime':
                    this._runtime.registerObserver(listener);
                    break;
                case 'genre':
                    this._genre.registerObserver(listener);
                    break;
                case 'director':
                    this._director.registerObserver(listener);
                    break;
                case 'writer':
                    this._writer.registerObserver(listener);
                    break;
                case 'actors':
                    this._actors.registerObserver(listener);
                    break;
                case 'plot':
                    this._plot.registerObserver(listener);
                    break;
                case 'language':
                    this._language.registerObserver(listener);
                    break;
                case 'country':
                    this._country.registerObserver(listener);
                    break;
                case 'thumbImgUrl':
                    this._thumbImgUrl.registerObserver(listener);
                    break;
                case 'fullImgUrl':
                    this._fullImgUrl.registerObserver(listener);
                    break;
                case 'location':
                    this._location.registerObserver(listener);
                    break;
                default:
                    throw new Exceptions.PropertyNotFoundException(propertyName);
            }
           
        }
        
        public removeListener(propertyName: string, listener: ViewModel.PropertyObserver<any>){
            
            switch(propertyName){
                case 'id':
                    this._id.removeObserver(listener);
                    break;
                case 'title':
                    this._title.removeObserver(listener);
                    break;
                case 'year':
                    this._year.removeObserver(listener);
                    break;
                case 'rated':
                    this._rated.removeObserver(listener);
                    break;
                case 'released':
                    this._released.removeObserver(listener);
                    break;
                case 'runtime':
                    this._runtime.removeObserver(listener);
                    break;
                case 'genre':
                    this._genre.removeObserver(listener);
                    break;
                case 'director':
                    this._director.removeObserver(listener);
                    break;
                case 'writer':
                    this._writer.removeObserver(listener);
                    break;
                case 'actors':
                    this._actors.removeObserver(listener);
                    break;
                case 'plot':
                    this._plot.removeObserver(listener);
                    break;
                case 'language':
                    this._language.removeObserver(listener);
                    break;
                case 'country':
                    this._country.removeObserver(listener);
                    break;
                case 'thumbImgUrl':
                    this._thumbImgUrl.removeObserver(listener);
                    break;
                case 'fullImgUrl':
                    this._fullImgUrl.removeObserver(listener);
                    break;
                case 'location':
                    this._location.removeObserver(listener);
                    break;
                default:
                    throw new Exceptions.PropertyNotFoundException(propertyName);
            }
        } 
        
        public getHtmlElements() : Element[]{
            var elements: Element[];
            
            this._fields.forEach(formField => {
                elements.push(formField.render());
            });
            
            return elements;
        }
        
        public isValid() : Boolean{
            return this._fields.every(formField => {
                return formField.isValid();
            });
        }
    }
    
    export class MovieSearchCriteria implements ViewModel{
        id:string;
        name:string;
    }
    
}