using AutoMapper;
using PetDiverse.Models;
using PetDiverse.Data;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<AnimalViewModel, Animal>();
        CreateMap<Animal, AnimalViewModel>();
    }
}
