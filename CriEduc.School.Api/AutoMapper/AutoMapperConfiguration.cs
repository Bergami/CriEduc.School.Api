using AutoMapper;
using CriEduc.School.Border.Dtos.Teacher;

namespace CriEduc.School.Api.AutoMapper
{
    public class AutoMapperConfiguration
    {
        public static MapperConfiguration RegisterMappings()
        {
            return new MapperConfiguration(ps =>
            {
                ps.AddProfile(new RequestToResponse());                
            });
        }
    }

    public class RequestToResponse : Profile
    {
        public RequestToResponse()
        {
            CreateMap<CreateTeacherRequest, CreateTeacherResponse>();                    
        }
    }
}
