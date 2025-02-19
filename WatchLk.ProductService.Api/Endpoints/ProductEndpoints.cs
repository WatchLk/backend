using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using WatchLk.ProductProcessing.Application;
using WatchLk.ProductProcessing.Domains.Dto;
using WatchLk.ProductProcessing.Infrastructure;

namespace WatchLk.ProductProcessing.Api.Endpoints
{
    public static class ProductEndpoints
    {
        const string GetProductEndpointName = "GetProduct";
        public static RouteGroupBuilder MapProductEndpoints(this WebApplication app)
        {


            var group = app.MapGroup("products").WithParameterValidation(); // Need to use minimal api extension package


            // Get all products
            group.MapGet("/", async (
                IProductRepository productRepository,
                [FromQuery(Name = "brandId")] int? brandId,
                [FromQuery(Name = "categoryId")] int? categoryId,
                [FromQuery(Name = "search")] string? search,
                [FromQuery(Name = "sortBy")] string? sortBy,
                [FromQuery(Name = "orderBy")] string? orderBy,
                [FromQuery(Name = "page")] int page=1,
                [FromQuery(Name = "pageSize")] int pageSize=10
                ) =>
            {
                try
                {
                    var response = await productRepository.GetAllProductsAsync(brandId, categoryId, search, sortBy, orderBy, page, pageSize);
                    return Results.Ok(new ResponseDto(200, response, null));

                } catch ( Exception ex) 
                {
                    Console.WriteLine(ex.Message);
                    return Results.BadRequest(new ResponseDto(500, null, "Somthing went wront, please try again"));
                }

            });


            // Get product by ID
            group.MapGet("/{id}", async (int id, IProductRepository productRepository) =>
            {
                try
                {
                    var response = await productRepository.GetProductByIdAsync(id);

                    if (response is null)
                    {
                        return Results.NotFound(new ResponseDto(404, null, "Product not found"));
                    }

                    return Results.Ok(new ResponseDto(200, response, null));
                }
                catch (Exception ex) 
                {
                    Console.WriteLine(ex.Message);
                    return Results.BadRequest(new ResponseDto(500, null, "Somthing went wront, please try again"));
                }

            }).WithName(GetProductEndpointName);


            // Create a new product
            group.MapPost("/", async (CreateProductDto createProductDto, IProductRepository productRepository) =>
            {
                try
                {
                    var response = await productRepository.CreateProductAsync(createProductDto);

                    if (response is null)
                    {
                        return Results.BadRequest(new ResponseDto(500, null, "Internal server error"));
                    }

                    return Results.CreatedAtRoute(GetProductEndpointName, new { id = response.Id}, new ResponseDto(201, response, null));
                }
                catch (Exception ex) 
                {
                    Console.WriteLine(ex.Message);
                    return Results.BadRequest(new ResponseDto(500, null, "Somthing went wront, please try again"));
                }

            });


            // Update a product by ID
            group.MapPut("/{id}", async (int id, UpdateProductDto updatedProductDto, IProductRepository productRepository) =>
            {
                try
                {
                    var response = await productRepository.UpdateProductAsync(id, updatedProductDto);

                    if(response is null)
                    {
                        return Results.NotFound(new ResponseDto(404, null, "Product not found"));
                    }

                    return Results.Ok(new ResponseDto(200, response, null));

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return Results.BadRequest(new ResponseDto(500, null, "Somthing went wront, please try again"));
                }
            });


            // Delete a product by ID
            group.MapDelete("{id}", async (int id, IProductRepository productRepository) =>
            {
                try
                {
                    await productRepository.DeleteProductAsync(id);
                    return Results.Ok(new ResponseDto(200, "Deletion succeeded", null));

                } catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return Results.BadRequest(new ResponseDto(500, null, "Somthing went wront, please try again"));
                }
            });


            return group;
        }
    }
}
