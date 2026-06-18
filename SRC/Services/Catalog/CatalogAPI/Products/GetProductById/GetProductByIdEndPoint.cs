using Catalog.API.Products.GetProducts;

namespace Catalog.API.Products.GetProductById
{
    //public record GetProductByIdQueryRequest(Guid Id);
    public record GetProductByIdResult(Product product);
    public class GetProductByIdEndPoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder routes)
        {
            routes.MapGet("/Products/{id}", async (Guid Id, ISender sender) =>
            {
                var result = await sender.Send(new GetProductByIdRequest(Id));
                var response = result.Adapt<GetProductByIdResult>();
                return Results.Ok(response);
            }).WithName("GetProductsBYId")
                .Produces<GetProductResponse>(StatusCodes.Status200OK)
                .ProducesProblem(StatusCodes.Status400BadRequest)
                .WithSummary("Get Products By Id")
                .WithDescription("Get Products By Id");
        }
    }
}
