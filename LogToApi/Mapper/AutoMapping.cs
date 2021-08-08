using LogToApi.Models;
using AutoMapper;

namespace LogToApi.Mapper
{
    public class AutoMapping : Profile
    {
        public AutoMapping()
        {
            CreateMap<Common.Models.LogRecord, LogRecord>()
                .ForMember(d=>d.ErrorDescription, 
                    s => s.MapFrom(c => c.ErrorText))
                .ForMember(d=>d.DateTime, 
                    s=>s.MapFrom(c=>c.DateTime.ToString("yyyy-MM-dd HH:mm:ss")));
            
        }
    }
}