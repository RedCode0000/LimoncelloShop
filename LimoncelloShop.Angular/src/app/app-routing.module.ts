import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { AboutComponent } from './about/about.component';
import { LimoncelloOverviewComponent } from './limoncello-overview/limoncello-overview.component';
import { LimoncelloDetailsComponent } from './limoncello-details/limoncello-details.component';
import { ShoppingCartComponent } from './shopping-cart/shopping-cart.component';

const routes: Routes = [
  { path: '', component: HomeComponent },
  { path: 'about', component: AboutComponent },
  { path: 'limoncello', component: LimoncelloOverviewComponent },
  { path: 'limoncello-details/:id', component: LimoncelloDetailsComponent },
  { path: 'shoppingcart', component: ShoppingCartComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
