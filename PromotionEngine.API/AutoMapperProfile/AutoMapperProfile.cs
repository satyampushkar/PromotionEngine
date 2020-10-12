using AutoMapper;
using PromotionEngine.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PromotionEngine.API.AutoMapperProfile
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<DTO.CartItem, CartItem>()
                .ForPath(dest =>
                dest.Product.SKUId,
                opt => opt.MapFrom(src => src.ProductId));

            CreateMap<DTO.Cart, Cart>()
                .ForMember(dest =>
                dest.CartItem,
                opt => opt.MapFrom(src => src.CartItems));
        }
    }
}
