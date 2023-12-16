export interface User{
  userId: number;
  userEmail: string;
  isAdmin: boolean;
}

export interface Product{
  productId: number;
  productName: string;
  typeId: number;
  price: number;
  gender: string;
  description: string;
}

export interface ProductColor{
  productId: number;
  colorId: number;
}


export interface ProductSize{
  productId: number;
  sizeId: number;
}

export interface UserCartItem{
  userId: number;
  productId: number;
  sizeId: number;
  colorId: number;
  quantity: number;
  productName: string;
  price: number;
}

export interface UserLikesItem{
  userId: number;
  productId: number;
}
