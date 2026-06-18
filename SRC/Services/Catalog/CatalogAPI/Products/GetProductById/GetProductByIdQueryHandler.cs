namespace Catalog.API.Products.GetProductById
{
    public record GetProductByIdRequest(Guid Id) : IQuery<GetProductByIdResponse>;
    public record GetProductByIdResponse(Product Product);
    public class GetProductByIdQueryHandler(IDocumentSession session, ILogger<GetProductByIdQueryHandler> logger)
        : IQueryHandler<GetProductByIdRequest, GetProductByIdResponse>
    {
        public async Task<GetProductByIdResponse> Handle(GetProductByIdRequest query, CancellationToken cancellationToken)
        {
            var result = await session.LoadAsync<Product>(query.Id,cancellationToken);
            if (result == null)
            {
                throw new ProductNotFoundException("Product Not Found");
            }
            return new GetProductByIdResponse(result);
        }
    }
}
