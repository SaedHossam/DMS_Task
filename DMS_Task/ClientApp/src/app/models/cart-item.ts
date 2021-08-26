export class CartItem {
    id?: number;
    shoppingCartId?: number;
    itemId?: number;
    name?: string;
    description?: string;
    orderedQuantity?: number;
    avalibleQuantity?: number;
    limitPerCustomer?: number;
    unitOfMeasure?: string;
    imageUrl?: string;
    unitPrice?: number;
    discount?: number;
    tax?: number;
    finalPrice?: number;
}
