using LogToApi.Models;
using AutoMapper;

namespace LogToApi.Mapper
{
    public class AutoMapping : Profile
    {
        public AutoMapping()
        {
            CreateMap<Common.Models.LogRecord, LogRecord>()
                .ForMember(
                    d=>d.ErrorDescription, 
                    s => s.MapFrom(c => c.ErrorText));
        }
    }
}