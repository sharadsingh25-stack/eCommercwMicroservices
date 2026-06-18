namespace Catalog.API.Products.DeleteProducts
{
    public record DeleteProductCommand(Guid id): ICommand<DeleteProductCommandResult>;
    public record DeleteProductCommandResult(bool isSuccess);

    public class DeleteProductsCommandHandler(IDocumentSession session, ILogger<DeleteProductsCommandHandler> logger)
        :ICommandHandler<DeleteProductCommand, DeleteProductCommandResult>
    {
        public async Task<DeleteProductCommandResult> Handle(DeleteProductCommand command, CancellationToken cancellationToken)
        {
            session.Delete<Product>(command.id);
            await session.SaveChangesAsync(cancellationToken);
            return new DeleteProductCommandResult(true);
        }
    }
}
