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
  sizeId: number;
  size: string;
}

export interface UserCartItems {
  productId: number;
  colorId: number;
  sizeId: number;
  quantity: number;
  productName: string;
  price: number;
}

export interface UserLikesItem{
  userId: number;
  productId: number;
}

export interface ProductImageFile{
  productId: number,
  colorId: number,
  imageFile: File
}

export interface ProductImageDto{
  productId: number,
  colorId: number,
  blobUrl: string
}

