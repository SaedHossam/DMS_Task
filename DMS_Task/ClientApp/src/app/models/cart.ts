import { CartItem } from "./cart-item";

export class Cart {
    id?: number;
    customerId?: number;
    customer: string;
    orderedQuantity?: number;
    totalPrice?: number;
    totalDiscount?: number;
    totalTax?: number;
    finalPrice?: number;
    shoppingCartItems: CartItem[];
}