using AutoMapper;
using BancoBari_Domain.Dto.Mensagem;
using BancoBari_Domain.Entities;

namespace BancoBari_Application.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Mensagem, MensagemDto>().ReverseMap();
        }
    }
}
