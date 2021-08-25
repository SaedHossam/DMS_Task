export class Item {
    constructor(
        public id?: number,
        public name?: string,
        public description?: string,
        public unitOfMeasureId?: number,
        public unitOfMeasure?: string,
        public imageUrl?: string,
        public quantity?:number,
        public avalibleQuantity?: number,
        public unitPrice?: number,
        public discount?: number,
        public tax?:number,
        public limitPerCustomer?: number
    ) { }
}
