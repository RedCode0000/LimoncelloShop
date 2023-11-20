import Basket from "./basket";
import Limoncello from "./limoncello";

export default interface DatabaseBasketItem {

    id: number;
    limoncello: Limoncello;
    number: number;
    basket: Basket;
}

export function ToBasketItem(dbBasketItem: DatabaseBasketItem) {
    let basketItem = {
        id: dbBasketItem.id,
        limoncello: dbBasketItem.limoncello,
        number: dbBasketItem.number,
        basket: dbBasketItem.basket

    }
    return basketItem;
}

