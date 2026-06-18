namespace Catalog.API.Products.CreateProducts
{
    public record CreateProductRequest(string Name, List<string> category, string Description, string ImageFile, Decimal Price);
   
    public record CreateProductResponse(Guid Id);
    public class CreateProductEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder routes)
        {
            routes.MapPost("/Products", async (CreateProductRequest createProduct, ISender sender) =>
            {
                var request = createProduct.Adapt<CreateProductCommand>();
                var result = await sender.Send(request);
                var response = result.Adapt<CreateProductResponse>();
                return Results.Created("/Produnct/{id}", response);
            }).WithDescription("Result Created").WithDisplayName("CreateProduct");

        }

    }
}
