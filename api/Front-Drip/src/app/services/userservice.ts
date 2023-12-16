import {Injectable} from "@angular/core";
import {User, UserCartItem, UserLikesItem} from "../models";

@Injectable({
  providedIn: 'root'
})
export class Userservice{
  currentUser: User | undefined;
  userLikes: UserLikesItem[] = [];
  userCart: UserCartItem[] = [];

  updateUserLikes(){
    //TODO update list of userLikes from database
  }

  updateUserCart(){
    //TODO update list of userCart from database
  }
}
