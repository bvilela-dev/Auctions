export type PagedResult<T> = {
    results: T[];
    pageCount: number;
    totalCount: number;
}

export type Auction = {
    reservePrice: number
    seller: string
    winner?: string
    soldAmount?: number
    currentHighBid?: number
    createdAt: string
    updatedAt: string
    auctionEnd: string
    status: string
    manufacturer: string
    model: string
    year: number
    color: string
    imageUrl: string
    id: string
  }
  