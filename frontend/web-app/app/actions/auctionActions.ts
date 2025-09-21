'use server';

import { fetchWrapper } from "@/lib/fetchWrapper";
import { PagedResult, Auction } from "@/types";
import { FieldValues } from "react-hook-form";

export async function getData(query: string): Promise<PagedResult<Auction>> {
    return fetchWrapper.get(`search${query}`);
}


export async function createAuction(data: FieldValues) {
    return fetchWrapper.post('auctions', data);
}

export async function getDetailedViewData(id: string): Promise<Auction> {
    return fetchWrapper.get(`auctions/${id}`);
}

export async function updateAuction(data: FieldValues, id: string) {
    return fetchWrapper.put(`auctions/${id}`, data);
}

export async function deleteAuction(id: string) {
    return fetchWrapper.del(`auctions/${id}`);
}