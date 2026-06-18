using Catalog.API.Products.GetProducts;
using Spectre.Console.Rendering;

namespace Catalog.API.Products.UpdateProducts
{
    public record UpdateProductCommandRequest(Guid id, string Name, List<string> category, string Description, string ImageFile, Decimal Price);
    public record UpdateProductCommandResponse(bool isSuccess);
    public class UpdateProductsCommandEndPoint : ICarterModule
    {
        public async void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPut("/Products", async (UpdateProductCommandRequest command, ISender sender) =>
            {
                var request = command.Adapt<UpdateProductCommand>();
                var result = await sender.Send(request);
                var response = result.Adapt<UpdateProductCommandResponse>();
                return Results.Ok(response);
            }).WithName("UpdateProducts")
                .Produces<UpdateProductCommandResponse>(StatusCodes.Status200OK)
                .ProducesProblem(StatusCodes.Status400BadRequest)
                .WithSummary("Update Products")
                .WithDescription("Update Products");
        }
    }
}
