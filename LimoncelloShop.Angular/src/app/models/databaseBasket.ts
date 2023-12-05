export default interface DatabaseBasket {

    id: number;
    name: string;
    email?: string;
    totalNumberOfItems: number;
    cookie?: string;
}

export function ToBasket(dbBasket: DatabaseBasket) {
    let basket = {
        id: dbBasket.id,
        name: dbBasket.name,
        email: dbBasket.email,
        totalNumberOfItems: dbBasket.totalNumberOfItems,
        cookie: dbBasket.cookie
    }
    return basket;
}


