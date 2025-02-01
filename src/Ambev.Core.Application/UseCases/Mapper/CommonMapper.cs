using Ambev.Application.UseCases.Commands.Cart.CreateCart;
using Ambev.Application.UseCases.Commands.Cart.UpdateCart;
using AutoMapper;
using Ambev.Application.UseCases.Commands.User.UpdateUser;
using Ambev.Core.Application.UseCases.DTOs;
using Ambev.Core.Domain.Entities;
using Ambev.Core.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ambev.Application.UseCases.Commands.Product.CreateProduct;
using Ambev.Application.UseCases.Commands.Product.UpdateProduct;
using Ambev.Application.UseCases.Commands.User.CreateUser;
using Ambev.Application.UseCases.Commands.User.DeleteUser;
using Ambev.Core.Application.UseCases.Queries.GetCartsQuery;
using Ambev.Core.Application.UseCases.Queries.GetCartQueryId;
using Ambev.Core.Application.UseCases.Queries.GetSaleById;
using Ambev.Core.Application.UseCases.Commands.Sale.UpdateSale;
using Ambev.Core.Application.UseCases.Commands.Sale.CancelSale;

namespace Ambev.Core.Application.UseCases.Mapper
{
    public  class CommonMapper : Profile
    {
        public CommonMapper()
        {
            CreateMap<AddressDto, Address>();
            CreateMap<Address, AddressDto>();

            CreateMap<GeolocationDto, Geolocation>()
            .ForMember(dest => dest.Latitude, opt => opt.MapFrom(src => src.Lat))
            .ForMember(dest => dest.Longitude, opt => opt.MapFrom(src => src.Long));

            CreateMap<Geolocation, GeolocationDto>()
            .ForMember(dest => dest.Lat, opt => opt.MapFrom(src => src.Latitude))
            .ForMember(dest => dest.Long, opt => opt.MapFrom(src => src.Longitude));

            CreateMap<RatingDTO, Rating>();

            // Carts...
            CreateMap<Cart, GetCartsQueryDataResponse>();
            CreateMap<GetCartsQueryDataResponse, Cart >();
            CreateMap<Cart, CreateCartResponse>();
            CreateMap<Cart, GetCartByIdResponse>();
            CreateMap<Cart, UpdateCartResponse>();
            CreateMap<CartItemBaseDTO,CartItem>();
            CreateMap<CartItem, CartItemBaseDTO>();
            //Product
            CreateMap<CreateProductRequest, Product>();
            CreateMap<Product, CreateProductResponse>();
            CreateMap<UpdateProductRequest, Product>();
            CreateMap<Product, UpdateProductResponse>();
            //User
            CreateMap<CreateUserRequest, User>();
            CreateMap<User, CreateUserResponse>();
            CreateMap<DeleteUserRequest, User>();
            CreateMap<User, DeleteUserRequest>();
            CreateMap<UpdateUserRequest, User>();
            CreateMap<User, UpdateUserRequest>();
            CreateMap<UpdateUserRequest, UpdateUserResponse>();
            //Sale
            CreateMap<CancelSaleResponse, Sale > ();
            CreateMap<UpdateSaleRequest, Sale > ();
            CreateMap<Sale, UpdateSaleResponse> ();
            CreateMap<Sale, GetSaleByIdResponse>();
        }
    }
}
