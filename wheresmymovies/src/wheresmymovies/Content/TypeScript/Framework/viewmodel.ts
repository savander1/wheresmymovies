module ViewModel {
        interface PropertyObserver {
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
        
        
        public get id(): string {
                return this._id.getValue();
        }
        public set id(value: string){
            this._id.setValue(value);
        }
        
        public get title(): string {
                return this._title.getValue();
        }
        public set title(value: string){
            this._title.setValue(value);
        }
        
        public get year(): number[] {
                return this._year.getValue();
        }
        public set year(value: number[]){
            this._year.setValue(value);
        }
        
        public get rated(): string {
                return this._rated.getValue();
        }
        public set rated(value: string){
            this._rated.setValue(value);
        }
        
        public get released(): string {
            return this._released.getValue();
        }
        public set released(value: string){
            this._released.setValue(value);
        }
        
        public get runtime(): string {
            return this._runtime.getValue();
        }
        public set runtime(value: string){
            this._runtime.setValue(value);
        }
        
        public get genre(): string[] {
            return this._genre.getValue();
        }
        public set genre(value: string[]){
            this._genre.setValue(value);
        }
        
        public get director(): string[] {
            return this._director.getValue();
        }
        public set director(value: string[]){
            this._director.setValue(value);
        }
        
        public get writer(): string[] {
            return this._writer.getValue();
        }
        public set writer(value: string[]){
            this._writer.setValue(value);
        }
        
        public get actors(): string[] {
            return this._actors.getValue();
        }
        public set actors(value: string[]){
            this._actors.setValue(value);
        }
        
        public get plot(): string {
            return this._plot.getValue();
        }
        public set plot(value: string){
            this._plot.setValue(value);
        }
        
        public get language(): string[] {
            return this._language.getValue();
        }
        public set language(value: string[]){
            this._language.setValue(value);
        }
        
        public get country(): string {
            return this._country.getValue();
        }
        public set country(value: string){
            this._country.setValue(value);
        }
        
        public get thumbImgUrl(): string {
            return this._thumbImgUrl.getValue();
        }
        public set thumbImgUrl(value: string){
            this._thumbImgUrl.setValue(value);
        }
        
        public get fullImgUrl(): string {
            return this._fullImgUrl.getValue();
        }
        public set fullImgUrl(value: string){
            this._fullImgUrl.setValue(value);
        }
        
        public get location(): string {
            return this._location.getValue();
        }
        public set location(value: string){
            this._location.setValue(value);
        }
    }
}
