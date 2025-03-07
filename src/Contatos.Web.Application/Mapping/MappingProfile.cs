using AutoMapper;
using Contatos.Web.Domain.Entities;
using Contatos.Web.Shared.DTO;

namespace Contatos.Web.Application.Mapping;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Contato, ContatoDto>()
            .ForMember(dest =>
                dest.Email, opt =>
                    opt.MapFrom(src => src.Email.Endereco))
            .ReverseMap();
    }
}