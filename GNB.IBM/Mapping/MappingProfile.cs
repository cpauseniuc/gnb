using AutoMapper;
using GNB.IBM.Api.ViewModels;
using GNB.IBM.HerokuApp.Proxy.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GNB.IBM.Api.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<RateRecord, RateViewModel>()
                .ForMember(d => d.From, opt => opt.MapFrom(s => s.From))
                .ForMember(d => d.To, opt => opt.MapFrom(s => s.To))
                .ForMember(d => d.Rate, opt => opt.MapFrom(s => s.Rate));
            CreateMap<Transaction, TransactionViewModel>()
                .ForMember(d => d.Amount, opt => opt.MapFrom(s => s.Amount))
                .ForMember(d => d.Currency, opt => opt.MapFrom(s => s.Currency))
                .ForMember(d => d.SKU, opt => opt.MapFrom(s => s.SKU));
        }
    }
    
}
