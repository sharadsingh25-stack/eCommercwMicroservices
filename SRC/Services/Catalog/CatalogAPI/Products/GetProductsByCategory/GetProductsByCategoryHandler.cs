using JasperFx.Events.Daemon;
using Marten.Linq.QueryHandlers;

namespace Catalog.API.Products.GetProductsByCategory
{
    public record GetProductsByCategoryQuery(string Category) : IQuery<GetProductsByCategoryResult>;
    public record GetProductsByCategoryResult(IEnumerable<Product> Products);

    public class GetProductsByCategoryHandler(IDocumentSession session, ILogger<GetProductsByCategoryResult> logger)
        : IQueryHandler<GetProductsByCategoryQuery, GetProductsByCategoryResult>

    {
        public async Task<GetProductsByCategoryResult> Handle(GetProductsByCategoryQuery request, CancellationToken cancellationToken)
        {
            var products=await session.Query<Product>().ToListAsync(cancellationToken);
            var productByCat=products.Where(p => p.Category.Contains(request.Category)).ToList();
            if(productByCat==null)
            {
                throw new ProductNotFoundException("Product Not found");
            }
            return new GetProductsByCategoryResult(productByCat);
        }
    }
}
