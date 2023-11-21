Why I am making this project

My brother worked as a waiter at Very Italian Pizza and Impero Romano in The Hague, so he has a passion for the Italian kitchen. He founded his not-official company Lemonbro's to sell his own made limoncello to our friends and acquintances. He goes to Italy frequently with his wife to enjoy the culture and food of the Italian atmosphere. Since I tasted the drink and enjoyed it, I helped him with preparing the limoncello and labeling the bottles.

Because of our love for limoncello, I started making this website (it's in dutch). At this moment the website does not look professional but it is partially functional. 



How it works


API

The backend uses ASP.NET Core so the website can communicate with it.


Authentication

The backend makes use of Microsoft Identity to register and login users. The two roles being used are Admin and User. Both roles can get, create, edit and delete objects Basket and BasketItem and get Limoncello. Only the Admin has the right to create, edit and delete the Limoncello object, because the Admin has to make sure which limoncello drinks are available.


Creation of cookies

For the backend cookies, the CookieOptions and the HttpContextAccessor classes are used. These cookies are stored in the baskets of the relevant persons when they are created, so that it is known which shopping cart belongs to which person. Sgx cookies are used in the frontend. This is to make sure when a person returns to the website the (filled) basket still exists. The reason for these systems is that they are easy to implement. (kijk nog naar backend cookies)


Entity Relationship Diagram

The relations of the objects being used are like this: The objects Limoncello (item) and Basket have an one-to-many relation with the BasketItem, while the optional User object has an one-on-one relation with the Basket object when the User is known so a cookie is not necessary. The records will be saved in an EF Core database.

The AddbasketItem method was the hardest part to implement because of the complexity of the relationships of the BasketItem and checks at certain points of the code. For example: Firstly, there have to be checks whether the parent objects in de BasketItemDTO exist in the database; Limoncello by name, else Basket by name or else its cookie. Secondly, the Number (amount) of the BasketItem should be substracted from the Stock property in de Limoncello object. Thirdly, the Number has to be assigned to the TotalNumbersOfItems property of the Basket object. If the basket doesn't exist, a normal assignment should be made, because its the first assignment for the corresponding basket. If it does exist, an addition or substract assignment should be done, because there are assignment(s) done on that object.

