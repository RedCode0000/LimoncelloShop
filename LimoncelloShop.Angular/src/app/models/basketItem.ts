import Basket from "./basket";
import Limoncello from "./limoncello";

export default interface BasketItem {

    id: number;
    limoncello: Limoncello;
    number: number;
    basket: Basket;
}

