using Catalog.API.Products.CreateProducts;

namespace Catalog.API.Products.UpdateProducts
{
    public record UpdateProductCommand(Guid id,string Name, List<string> category, string Description, string ImageFile, Decimal Price)
            : ICommand<UpdateProductCommmandResult>;
    public record UpdateProductCommmandResult(bool isSuccess);
    public class UpdateProductsCommandHandler(IDocumentSession session,ILogger<UpdateProductsCommandHandler> logger)
        :ICommandHandler<UpdateProductCommand, UpdateProductCommmandResult>
    {
        public async Task<UpdateProductCommmandResult> Handle(UpdateProductCommand command,CancellationToken cancellationToken)
        {
            var product = await session.LoadAsync<Product>(command.id, cancellationToken);
            if(product == null)
            {
                throw new ProductNotFoundException("Product Not Found");
            }
            product.Name=command.Name;
            product.Category = command.category;
            product.Description = command.Description;
            product.ImageFile = command.ImageFile;
            product.Price = command.Price;

            session.Update(product);
            await session.SaveChangesAsync();
            return new UpdateProductCommmandResult(true);
                    
        }
    }
}
