using AutoMapper;
using PetDiverse.Models;
using PetDiverse.Data;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<AnimalViewModel, Animal>();
        CreateMap<Animal, AnimalViewModel>();

        CreateMap<RegistroCirurgia, RegistroCirurgiaViewModel>();
        CreateMap<RegistroCirurgiaViewModel, RegistroCirurgia>();

        CreateMap<RegistroVacina, RegistroVacinaViewModel>();
        CreateMap<RegistroVacinaViewModel, RegistroVacina>();

        CreateMap<TipoVacina, TipoVacinaViewModel>();
        CreateMap<TipoVacinaViewModel, TipoVacina>();

        CreateMap<TipoCirurgia, TipoCirurgiaViewModel>();
        CreateMap<TipoCirurgiaViewModel, TipoCirurgia>();
    }
}
