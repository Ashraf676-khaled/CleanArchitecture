// Api/Endpoints/IEndpoint.cs
namespace Clean.Architecture.Api.Endpoints;

public interface IEndpoint
{
  void MapEndpoint(IEndpointRouteBuilder app);
}
