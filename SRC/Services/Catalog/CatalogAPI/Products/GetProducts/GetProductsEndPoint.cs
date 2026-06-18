using Microsoft.AspNetCore.Mvc;

namespace Catalog.API.Products.GetProducts
{
    //public record GetProductRequest()
    public record GetProductResponse(IEnumerable<Product> Products);
    public class GetProductsEndPoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder routes)
        {
            routes.MapGet("/products", async (ISender sender) =>
            {
                var products = await sender.Send(new GetProductQueryRequest());
                var result = products.Adapt<GetProductResponse>();
                return Results.Ok(result);
            })
                .WithName("GetProducts")
                .Produces<GetProductResponse>(StatusCodes.Status200OK)
                .ProducesProblem(StatusCodes.Status400BadRequest)
                .WithSummary("Get Products")
                .WithDescription("Get Products");

        }
    }
}
