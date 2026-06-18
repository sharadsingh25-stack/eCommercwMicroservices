namespace Catalog.API.Products.GetProducts
{
    public record GetProductQueryRequest() : IQuery<GetProductQueryResult>;
    public record GetProductQueryResult(IEnumerable<Product> Products);
    internal class GetProductsQueryHandler(IDocumentSession session, ILogger<GetProductsQueryHandler> logger):
        IQueryHandler<GetProductQueryRequest,GetProductQueryResult>
    {
        public async Task<GetProductQueryResult> Handle(GetProductQueryRequest query, CancellationToken cancellationToken)
        {
            logger.LogInformation("We have sucessfully called the query handler for get products with {@Query}",query);
            var products = await session.Query<Product>().ToListAsync(cancellationToken);
            return new GetProductQueryResult(products);
        }
    }
}
