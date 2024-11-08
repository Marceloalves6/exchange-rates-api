﻿using AutoMapper;
using ExchangeRates.Core.Commands;
using ExchangeRates.Core.Entities;

namespace ExchangeRates.Core.Mappings;

public class ExchangeRateProfile : Profile
{
    public ExchangeRateProfile()
    {
        CreateMap<AddExchangeRateRequest, ExchangeRate>()
         .ForMember(dest => dest.ExternalId, src => src.MapFrom(i => Guid.NewGuid()))
         .ForMember(dest => dest.Deleted, src => src.MapFrom(i => false))
         .ForMember(dest => dest.CreatedAt, src => src.MapFrom(i => DateTime.UtcNow));

        CreateMap<ExchangeRate, AddExchangeRateResponse>()
            .ForMember(dest => dest.Id, src => src.MapFrom(i => i.ExternalId));

        CreateMap<ExchangeRate, GetExchangeRateResponse>()
            .ForMember(dest => dest.Id, src => src.MapFrom(i => i.ExternalId));

        CreateMap<ExchangeRate, UpdateExchangeRateResponse>()
           .ForMember(dest => dest.Id, src => src.Ignore())
           .ForMember(dest => dest.Id, src => src.MapFrom(i => i.ExternalId));

        CreateMap<AddExchangeRateResponse, GetExchangeRateResponse>();

    }
}
