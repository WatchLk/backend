using Microsoft.AspNetCore.Mvc;
using WatchLk.CartService.Application;
using WatchLk.CartService.Domains.Dto;

namespace WatchLk.CartService.Api.Endpoints
{
    public static class CartEndpoints
    {
        public static RouteGroupBuilder MapCartEndpoints(this WebApplication app)
        {
            var group = app.MapGroup("/api/Cart").WithParameterValidation().RequireAuthorization("client_only");

            //Get all cart items of a user
            group.MapGet("/{userId}", async (string userId, ICartRepository cartRepository, ITokenRepository tokenRepository,HttpContext context) =>
            {
                var authheader = context.Request.Headers.Authorization;
                var token = authheader.ToString().Replace("Bearer ", "");
                var isAuthorized = await tokenRepository.IsUserAuthorized(token, userId);
                if (!isAuthorized)
                {
                    return Results.Unauthorized();
                }
                var response = await cartRepository.GetCartByUserIdAsync(userId);

                if (response is null)
                {
                    return Results.BadRequest("Something went wrong, please try again");
                } else if (response.Succeeded)
                {
                    return Results.Ok(response);
                }
                return Results.BadRequest(response);
            });

            // Add an item to cart
            group.MapPost("/", async ([FromBody] CreateCartItemDto createCartItemDto, ICartRepository cartRepository, ITokenRepository tokenRepository, HttpContext context ) =>
            {
                var authHeader = context.Request.Headers.Authorization;
                var token = authHeader.ToString().Replace("Bearer ", "");
                var isAuthorized = await tokenRepository.IsUserAuthorized(token, createCartItemDto.UserId);
                if (!isAuthorized)
                {
                    return Results.Unauthorized();
                }

                var response = await cartRepository.AddToCartAsync(createCartItemDto);

                if (response is null)
                {
                    return Results.BadRequest("Something went wrong, please try again");
                }
                else if (response.Succeeded)
                {
                    return Results.Ok(response);
                }
                return Results.BadRequest(response);
            });

            //Update an item in cart
            group.MapPut("/{cartItemId}", async (int cartItemId, UpdateCartItemDto updateCartItem, ICartRepository cartRepository, ITokenRepository tokenRepository, HttpContext context) =>
            {
                var authHeader = context.Request.Headers.Authorization;
                var token = authHeader.ToString().Replace("Bearer ", "");
                var isAuthorized = await tokenRepository.IsUserAuthorized(token, updateCartItem.UserId);
                if (!isAuthorized)
                {
                    return Results.Unauthorized();
                }

                var response = await cartRepository.UpdateCartItemAsync(cartItemId, updateCartItem);

                if (response is null)
                {
                    return Results.BadRequest("Something went wrong, please try again");
                }
                else if (response.Succeeded)
                {
                    return Results.Ok(response);
                }
                return Results.BadRequest(response);
            });

            //Delete an item from cart
            group.MapDelete("/{userId}/{cartItemId}", async (string userId, int cartItemId, ICartRepository cartRepository, ITokenRepository tokenRepository, HttpContext context) =>
            {
                var authHeader = context.Request.Headers.Authorization;
                var token = authHeader.ToString().Replace("Bearer ", "");
                var isAuthorized = await tokenRepository.IsUserAuthorized(token, userId);
                if (!isAuthorized)
                {
                    return Results.Unauthorized();
                }

                var response = await cartRepository.RemoveFromCartAsync(cartItemId);

                if (response is null)
                {
                    return Results.BadRequest("Something went wrong, please try again");
                }
                else if (response.Succeeded)
                {
                    return Results.Ok(response);
                }
                return Results.BadRequest(response);
            });

            //Clear an user's cart
            group.MapDelete("/ClearUserCart/{userId}", async (string userId, ICartRepository cartRepository) =>
            {
                var response = await cartRepository.ClearCartAsync(userId);

                if (response is null)
                {
                    return Results.BadRequest("Something went wrong, please try again");
                }
                else if (response.Succeeded)
                {
                    return Results.Ok(response);
                }
                return Results.BadRequest(response);
            });

            return group;
        }
    }
}
