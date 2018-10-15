using System;
using AutoMapper.Configuration;
using MultiTenantDemo.Data.Model;
using MultiTenantDemo.Model;

namespace MultiTenantDemo.Mappings
{
    public class MapsProfile : MapperConfigurationExpression
    {
        public MapsProfile()
        {
            // Device ViewModel To Device
            this.CreateMap<DeviceViewModel, Device>()
                .ForMember(dest => dest.DeviceTitle, opt => opt.MapFrom(src => src.Title))
                .ForMember(dest => dest.DeviceId, opt => opt.MapFrom(src => Guid.NewGuid()))
                ;

            this.CreateMap<UserViewModel, APIUser>()
                .ForMember(dest => dest.Username, opt => opt.MapFrom(src => src.Username))
                .ForMember(dest => dest.Password, opt => opt.MapFrom(src => src.Password))
                ;
        }
    }
}
