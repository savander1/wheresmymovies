module ViewModel {
    export interface PropertyObserver {
        updateValue (value:any);
    }

    export class ObservableProperty<T> { 
        private observers : PropertyObserver [];
        private propertyValue: T;

        constructor(value: T) {
            this.observers = [];
            this.setValue(value);
        }
        
        setValue(value: T){
            this.propertyValue = value;
            this.notifyObservers(value);
        }
        
        getValue(): T{
            return this.propertyValue;
        }

        registerObserver (observer : PropertyObserver) : void {
            this.observers.push(observer);
        }

        removeObserver (observer : PropertyObserver) : void {
            this.observers.splice(this.observers.indexOf(observer), 1);
        }

        notifyObservers (arg : any) : void {

            this.observers.forEach((observer : PropertyObserver)=> {
                observer.updateValue(arg);
            });
        }
    }
    
    export class MovieViewModel {
        private _id: ObservableProperty<string>;
        private _title: ObservableProperty<string>;
        private _year: ObservableProperty<number[]>; 
        private _rated: ObservableProperty<string>;
        private _released: ObservableProperty<string>;
        private _runtime: ObservableProperty<string>; 
        private _genre: ObservableProperty<string[]>; 
        private _director: ObservableProperty<string[]>;
        private _writer: ObservableProperty<string[]>;
        private _actors: ObservableProperty<string[]>;
        private _plot: ObservableProperty<string>;
        private _language: ObservableProperty<string[]>; 
        private _country: ObservableProperty<string>;
        private _thumbImgUrl: ObservableProperty<string>;
        private _fullImgUrl: ObservableProperty<string>;
        private _location: ObservableProperty<string>;
        
        public constructor (id: ObservableProperty<string>,
                            title: ObservableProperty<string>,
                            year: ObservableProperty<number[]>, 
                            rated: ObservableProperty<string>,
                            released: ObservableProperty<string>,
                            runtime: ObservableProperty<string>, 
                            genre: ObservableProperty<string[]>, 
                            director: ObservableProperty<string[]>,
                            writer: ObservableProperty<string[]>,
                            actors: ObservableProperty<string[]>,
                            plot: ObservableProperty<string>,
                            language: ObservableProperty<string[]>, 
                            country: ObservableProperty<string>,
                            thumbImgUrl: ObservableProperty<string>,
                            fullImgUrl: ObservableProperty<string>,
                            location: ObservableProperty<string>){
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
            return this._id.getValue();
        }
        public set id(value: string){
            this._id.setValue(value);
        }
        public addIdListener(listener: PropertyObserver): void{
            this._id.registerObserver(listener);
        }
        public removeIdListener(listener: PropertyObserver): void{
            this._id.removeObserver(listener);
        }
        
        
        public get title(): string {
            return this._title.getValue();
        }
        public set title(value: string){
            this._title.setValue(value);
        }
        public addTitleListener(listener: PropertyObserver): void{
            this._title.registerObserver(listener);
        }
        public removeTitleListener(listener: PropertyObserver): void{
            this._title.removeObserver(listener);
        }
        
        public get year(): number[] {
            return this._year.getValue();
        }
        public set year(value: number[]){
            this._year.setValue(value);
        }
        public addYearListener(listener: PropertyObserver): void{
            this._year.registerObserver(listener);
        }
        public removeYearListener(listener: PropertyObserver): void{
            this._year.removeObserver(listener);
        }
        
        public get rated(): string {
            return this._rated.getValue();
        }
        public set rated(value: string){
            this._rated.setValue(value);
        }
        public addRatedListener(listener: PropertyObserver): void{
            this._rated.registerObserver(listener);
        }
        public removeRatedListener(listener: PropertyObserver): void{
            this._rated.removeObserver(listener);
        }
        
        public get released(): string {
            return this._released.getValue();
        }
        public set released(value: string){
            this._released.setValue(value);
        }
        public addReleasedListener(listener: PropertyObserver): void{
            this._released.registerObserver(listener);
        }
        public removeReleasedListener(listener: PropertyObserver): void{
            this._released.removeObserver(listener);
        }
        
        public get runtime(): string {
            return this._runtime.getValue();
        }
        public set runtime(value: string){
            this._runtime.setValue(value);
        }
        public addRuntimeListener(listener: PropertyObserver): void{
            this._runtime.registerObserver(listener);
        }
        public removeRuntimeListener(listener: PropertyObserver): void{
            this._runtime.removeObserver(listener);
        }
        
        public get genre(): string[] {
            return this._genre.getValue();
        }
        public set genre(value: string[]){
            this._genre.setValue(value);
        }
        public addGenreListener(listener: PropertyObserver): void{
            this._genre.registerObserver(listener);
        }
        public removeGenreListener(listener: PropertyObserver): void{
            this._genre.removeObserver(listener);
        }
        
        public get director(): string[] {
            return this._director.getValue();
        }
        public set director(value: string[]){
            this._director.setValue(value);
        }
        public addDirectorListener(listener: PropertyObserver): void{
            this._director.registerObserver(listener);
        }
        public removeDirectorListener(listener: PropertyObserver): void{
            this._director.removeObserver(listener);
        }
        
        public get writer(): string[] {
            return this._writer.getValue();
        }
        public set writer(value: string[]){
            this._writer.setValue(value);
        }
        public addWriterListener(listener: PropertyObserver): void{
            this._writer.registerObserver(listener);
        }
        public removeWriterListener(listener: PropertyObserver): void{
            this._writer.removeObserver(listener);
        }
        
        public get actors(): string[] {
            return this._actors.getValue();
        }
        public set actors(value: string[]){
            this._actors.setValue(value);
        }
        public addActorsListener(listener: PropertyObserver): void{
            this._actors.registerObserver(listener);
        }
        public removeActorsListener(listener: PropertyObserver): void{
            this._actors.removeObserver(listener);
        }
        
        public get plot(): string {
            return this._plot.getValue();
        }
        public set plot(value: string){
            this._plot.setValue(value);
        }
        public addPlotListener(listener: PropertyObserver): void{
            this._plot.registerObserver(listener);
        }
        public removePlotListener(listener: PropertyObserver): void{
            this._plot.removeObserver(listener);
        }
        
        public get language(): string[] {
            return this._language.getValue();
        }
        public set language(value: string[]){
            this._language.setValue(value);
        }
        public addLanguageListener(listener: PropertyObserver): void{
            this._language.registerObserver(listener);
        }
        public removeLanguageListener(listener: PropertyObserver): void{
            this._language.removeObserver(listener);
        }
        
        public get country(): string {
            return this._country.getValue();
        }
        public set country(value: string){
            this._country.setValue(value);
        }
        public addCountryListener(listener: PropertyObserver): void{
            this._country.registerObserver(listener);
        }
        public removeCountryListener(listener: PropertyObserver): void{
            this._country.removeObserver(listener);
        }
        
        public get thumbImgUrl(): string {
            return this._thumbImgUrl.getValue();
        }
        public set thumbImgUrl(value: string){
            this._thumbImgUrl.setValue(value);
        }
        public addThumbImgUrlListener(listener: PropertyObserver): void{
            this._thumbImgUrl.registerObserver(listener);
        }
        public removeThumbImgUrlListener(listener: PropertyObserver): void{
            this._thumbImgUrl.removeObserver(listener);
        }
        
        public get fullImgUrl(): string {
            return this._fullImgUrl.getValue();
        }
        public set fullImgUrl(value: string){
            this._fullImgUrl.setValue(value);
        }
        public addFullImgUrlListener(listener: PropertyObserver): void{
            this._fullImgUrl.registerObserver(listener);
        }
        public removeFullImgUrlListener(listener: PropertyObserver): void{
            this._fullImgUrl.removeObserver(listener);
        }
        
        public get location(): string {
            return this._location.getValue();
        }
        public set location(value: string){
            this._location.setValue(value);
        }       
        public addLocationListener(listener: PropertyObserver): void{
            this._location.registerObserver(listener);
        }
        public removeLocationListener(listener: PropertyObserver): void{
            this._location.removeObserver(listener);
        }
        
    }
}
