import { OrderItem } from "./order-item";

export class Order {
    id?: number;
    fullName?: string;
    email?: string;
    orderDate?: Date;
    dueDate?: Date;
    tax?: number;
    discount?: number;
    totalPrice?: number;
    totalPriceAfterDiscount?: number;
    totalPriceAfterTax?: number;
    orderItems: OrderItem[]
}
