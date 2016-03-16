interface PropertyObserver {
    updateValue (value:any);
}

export class ObservableProperty<T> { 
    private observers : PropertyObserver [];
    private propertyValue: any;

    constructor(value: T) {
        this.observers = [];
        this.setValue(value);
    }
    
    setValue(value: T){
        this.propertyValue = value;
        this.notifyObservers(value);
    }
    
    getValue(){
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
    
    
    Id(id:string):void{
        this._id.setValue(id);
    }
}
