export class PaginatedViewModel<T> {
    items: T;
    pageSize: number;
    pageNumber: number;
    totalCount: number;
    TotalPages: number;
}