namespace Catalog.API.Products.CreateProduct;

public record CreateProductResquest(
    string Name,
    List<string> Category,
    string Description,
    decimal Price,
    string ImageFile);

public record CreateProductResponse(Guid Id);

public class CreateProductEndpoint: ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/products", async (CreateProductResquest request, ISender sender) =>
        {
            var command = request.Adapt<CreateProductCommand>();
            var result = sender.Send(command);
            Console.WriteLine(result);
            var response = result.Result.Adapt<CreateProductResponse>();
            
            return Results.Created($"/products/{response.Id}", response);
        })
            .WithName("CreateProduct")
            .WithSummary("Creates a product")
            .WithDescription("Creates a product")
            .Produces<CreateProductResponse>(StatusCodes.Status201Created)
            .ProducesProblem(StatusCodes.Status400BadRequest);
    }
}