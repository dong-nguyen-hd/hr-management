using API.Resources;
using API.Results;

namespace API.Extensions;

public static class RelatePagination
{
    public static void CreatePaginationResponse<Response, Pagination>(this PaginationResult<Response> result,
        Pagination pagination,
        int totalRecords) where Pagination : QueryResource
    {
            // Assign Query-Resource
            result.Page = pagination.Page;
            result.PageSize = pagination.PageSize;
            // Assign Total-Pages
            var totalPages = ((double)totalRecords / (double)pagination.PageSize);
            int roundedTotalPages = Convert.ToInt32(Math.Ceiling(totalPages));
            // Assign Next-Page
            result.NextPage = (pagination.Page >= 1 && pagination.Page < roundedTotalPages) ? pagination.Page + 1 : null;
            // Assign Previous-Page
            result.PreviousPage = (pagination.Page - 1 >= 1 && pagination.Page <= roundedTotalPages) ? pagination.Page - 1 : null;
            // Assign First-Page
            result.FirstPage = 1;
            // Assign Last-Page
            result.LastPage = roundedTotalPages;
            // Assign Total-Pages
            result.TotalPages = roundedTotalPages;
            // Assign Total-Records
            result.TotalRecords = totalRecords;
        }
}