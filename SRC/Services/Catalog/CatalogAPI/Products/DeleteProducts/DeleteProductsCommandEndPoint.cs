using Catalog.API.Products.GetProducts;

namespace Catalog.API.Products.DeleteProducts
{
    public record DeleteProductCommandRequest(Guid id);
    public record DeleteProductCommandResponse(bool isSuccess);
    public class DeleteProductsCommandEndPoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapDelete("/Products/Delete/{id}", async (Guid id, ISender sender) =>
            {
                var result = await sender.Send(new DeleteProductCommand(id));
                var response = result.Adapt<DeleteProductCommandResponse>();
                return Results.Ok(response);
            }).WithName("DeleteProducts")
                .Produces<DeleteProductCommandResponse>(StatusCodes.Status200OK)
                .ProducesProblem(StatusCodes.Status400BadRequest)
                .WithSummary("Delete Product")
                .WithDescription("Delete Product");
        }
    }
}
