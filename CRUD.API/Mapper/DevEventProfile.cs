using AutoMapper;
using CRUD.API.Models;
using CRUD.API.Entities;

namespace CRUD.API.Mapper
{
    public class DevEventProfile : Profile
    {
        public DevEventProfile() 
        {
            CreateMap<DevEvent, DevEventViewModel>();
            CreateMap<DevEventSpeaker, DevEventSpeakerViewModel>();

            CreateMap<DevEventInputModel, DevEvent>();
            CreateMap<DevEventSpeakerInputModel, DevEventSpeaker>();
        }
    }
}
