using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using DAL.Models;
using DMS_Task.ViewModels;

namespace DMS_Task.ViewModels
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<UserForRegistrationDto, ApplicationUser>()
                .ForMember(u => u.UserName, opt => opt.MapFrom(x => x.Email));
            // Unit of Measure
            CreateMap<UnitOfMeasureDto, UnitOfMeasure>().ReverseMap();
            CreateMap<UnitOfMeasureCreateDto, UnitOfMeasure>();

            // Item
            CreateMap<ItemListDto, Item>()
                .ForPath(i => i.UnitOfMeasure.Name, opt => opt.MapFrom(it => it.UnitOfMeasure)).ReverseMap();
            
            CreateMap<ItemCreateDto, Item>()
                .ForMember(i => i.UnitOfMeasureId, opt => opt.MapFrom(it => it.UnitOfMeasureId));
            
            CreateMap<ItemEditDto, Item>()
                            .ForMember(i => i.UnitOfMeasureId, opt => opt.MapFrom(it => it.UnitOfMeasureId))
                            .ForMember(i => i.IsVisible, opt => opt.Ignore())
                            .ForMember(i => i.Id, opt => opt.Ignore())
                            .ReverseMap();

            // Shopping Cart Item
            CreateMap<ShoppingCartItem, ShoppingCartItemDto>()
                .ForMember(s => s.OrderedQuantity, opt => opt.MapFrom(c => c.Quantity))
                .ForMember(s => s.Name, opt => opt.MapFrom(c => c.Item.Name))
                .ForMember(s => s.Description, opt => opt.MapFrom(c => c.Item.Description))
                .ForMember(s => s.AvalibleQuantity, opt => opt.MapFrom(c => c.Item.AvalibleQuantity))
                .ForMember(s => s.Discount, opt => opt.MapFrom(c => c.Item.Discount))
                .ForMember(s => s.UnitPrice, opt => opt.MapFrom(c => c.Item.UnitPrice))
                .ForMember(s => s.LimitPerCustomer, opt => opt.MapFrom(c => c.Item.LimitPerCustomer))
                .ForMember(s => s.UnitOfMeasure, opt => opt.MapFrom(c => c.Item.UnitOfMeasure.Name))
                .ForMember(s => s.Tax, opt => opt.MapFrom(c => c.Item.Tax))
                .ForMember(s => s.ImageUrl, opt => opt.MapFrom(c => c.Item.ImageUrl));

            CreateMap<ShoppingCart, ShoppingCartDto>()
                .ForMember(a => a.CustomerId, opt => opt.MapFrom(s => s.CustomerId))
                .ForMember(a => a.Customer, opt => opt.MapFrom(s => s.Customer.Name));
            //.ForMember(a => a.ShoppingCartItems, opt => opt.MapFrom(s => s.ShoppingCartItems))

            CreateMap<ShoppingCartItemAddDto, ShoppingCartItem>()
                .ForMember(c => c.ItemId, opt => opt.MapFrom(c => c.ItemId))
                .ForMember(c => c.Quantity, opt => opt.MapFrom(c => c.Quantity))
                .ForMember(c => c.Id, opt => opt.Ignore());
        }
    }
}
