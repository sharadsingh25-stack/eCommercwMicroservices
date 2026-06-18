using FluentValidation;
using System.Data;

namespace Catalog.API.Products.CreateProducts
{
    public record CreateProductCommand(string Name, List<string> category, string Description, string ImageFile, Decimal Price)
            : ICommand<CreateProductResult>;
   
    public record CreateProductResult(Guid Id);

    public class CreateProductValidator:AbstractValidator<CreateProductCommand>
    {
        public CreateProductValidator()
        {
            RuleFor(X => X.category).NotEmpty().WithMessage("Category is reqired");
            RuleFor(X => X.Name).NotEmpty().WithMessage("Name is reqired");
            RuleFor(X => X.ImageFile).NotEmpty().WithMessage("Image File is reqired");
            RuleFor(X => X.Description).NotEmpty().WithMessage("Desccription is reqired");
            RuleFor(X => X.Price).NotEmpty().GreaterThan(0).WithMessage("Price must be non Empty and greater that 0");
        }
    }
    public class CreateProductHandler(IDocumentSession session,IValidator<CreateProductCommand> validator)
        :ICommandHandler<CreateProductCommand, CreateProductResult>

    {
        public async Task<CreateProductResult> Handle(CreateProductCommand command, CancellationToken cancellationToken)
        {
            var result = await validator.ValidateAsync(command,cancellationToken);
            var errors=result.Errors.Select(e => e.ErrorMessage);
            if(errors.Any())
            {
                throw new ValidationException(errors.FirstOrDefault());
            }
            var product = new Product
            {
                Name = command.Name,
                Category = command.category,
                Description = command.Description,
                ImageFile = command.ImageFile,
                Price = command.Price
            };
            session.Store(product);
            await session.SaveChangesAsync(cancellationToken);
            return new CreateProductResult(product.Id);
        }
    }
}
