using AutoMapper;

namespace Clean.Architecture.Application.Common.Mappings;

public interface IMapFrom<T>
{
  void Mapping(Profile profile) => profile.CreateMap(typeof(T), GetType());
}
