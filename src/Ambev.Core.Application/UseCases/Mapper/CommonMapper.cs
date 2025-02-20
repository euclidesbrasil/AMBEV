﻿using Ambev.Application.UseCases.Commands.Cart.CreateCart;
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
using Ambev.Core.Application.UseCases.Commands.Sale.CreateSale;
using Ambev.Core.Application.UseCases.Commands.Sale.CreateBranch;
using Ambev.Core.Application.UseCases.Commands.Branch.UpdateBranch;
using Entities = Ambev.Core.Domain.Entities;
using Ambev.Core.Application.Branch.GetBranchById.GetBranchById;
using Ambev.Core.Application.UseCases.Queries.GetUsersQuery;
using Ambev.Application.UseCases.Commands.Customer.CreateCustomer;
using Ambev.Application.UseCases.Commands.Customer.DeleteCustomer;
using Ambev.Application.UseCases.Commands.Customer.UpdateCustomer;
using Ambev.Core.Application.UseCases.Queries.GetCustomerById;
using Ambev.Core.Application.UseCases.Queries.GetCustomersQuery;
using Microsoft.Extensions.Configuration;
namespace Ambev.Core.Application.UseCases.Mapper
{
    public class CommonMapper : Profile
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
            CreateMap<Rating, RatingDTO>();

            // Carts...
            CreateMap<Cart, GetCartsQueryDataResponse>();
            CreateMap<GetCartsQueryDataResponse, Cart>();
            CreateMap<Cart, CreateCartResponse>();
            CreateMap<Cart, GetCartByIdResponse>();
            CreateMap<Cart, UpdateCartResponse>();
            CreateMap<CartItemBaseDTO, CartItem>();
            CreateMap<CartItem, CartItemBaseDTO>();
            //Product
            CreateMap<CreateProductRequest, Product>();
            CreateMap<Product, CreateProductResponse>();
            CreateMap<Rating, CreateProductRatingResponse>();
            CreateMap<UpdateProductRequest, Product>();
            CreateMap<Product, UpdateProductResponse>();
            CreateMap<Rating, UpdateProductRatingResponse>();
            
            //User
            CreateMap<CreateUserRequest, User>();
            CreateMap<User, CreateUserResponse>();
            CreateMap<DeleteUserRequest, User>();
            CreateMap<User, DeleteUserRequest>();
            CreateMap<UpdateUserRequest, User>();
            CreateMap<User, UpdateUserRequest>();
            CreateMap<User, GetUsersQueryResponse>();
            CreateMap<User, UserDTO>();
            CreateMap<UpdateUserRequest, UpdateUserResponse>();
            //Sale
            CreateMap<CreateSaleRequest, Sale>();
            CreateMap<UpdateSaleRequest, Sale>();
            CreateMap<CreateSaleResponse, Sale>();
            CreateMap<UpdateSaleItemRequest, SaleItem>();
            CreateMap<SaleWithDetaislsDTO, Sale>();
            CreateMap<Sale, SaleWithDetaislsDTO>();
            CreateMap<SaleDTO, Sale>();
            CreateMap<SaleBaseDTO, Sale>();
            CreateMap<SaleItemDTO, SaleItem>();
            CreateMap<SaleItemBaseDTO, SaleItem>();
            CreateMap<SaleBaseDTO, SaleItem>();
            CreateMap<Sale, UpdateSaleResponse>();
            CreateMap<Sale, CreateSaleResponse>();
            CreateMap<Sale, GetSaleByIdResponse>();
            CreateMap<SaleItem, SaleItemDTO>();
            CreateMap<SaleItem, SaleItemBaseDTO>();
            //Branch
            CreateMap<CreateBranchRequest, Entities.Branch >();
            CreateMap<UpdateBranchRequest, Entities.Branch>();

            CreateMap<GetBranchByIdResponse, Entities.Branch>();
            CreateMap<GetBranchByIdResponse, BranchDTO>();
            CreateMap<Entities.Branch, CreateBranchResponse>();
            CreateMap<Entities.Branch, GetBranchByIdResponse>();
            CreateMap<Entities.Branch, UpdateBranchResponse>();
            
            CreateMap<Entities.Branch, BranchDTO>();
            CreateMap<Entities.Branch, BranchBaseDTO>();
            CreateMap<BranchDTO, CreateBranchResponse>();
            CreateMap<BranchBaseDTO, CreateBranchResponse>();
            CreateMap<BranchBaseDTO, UpdateBranchRequest>();
            CreateMap<BranchDTO, CreateBranchResponse>();

            //Customer
            CreateMap<CustomerDTO, Customer>();
            CreateMap<Customer, CustomerDTO>();
            CreateMap<CreateCustomerRequest, Customer>();
            CreateMap<Customer,CreateCustomerResponse>();
            CreateMap<Customer, GetCustomerByIdRequest>();
            CreateMap<Customer, GetCustomersQueryResponse>();
            CreateMap<Customer, GetCustomerByIdResponse>();
            CreateMap<UpdateCustomerResponse, Customer>();
            CreateMap<Customer, UpdateCustomerResponse>();
            CreateMap<DeleteCustomerResponse, Customer>();
        }
    }
}
