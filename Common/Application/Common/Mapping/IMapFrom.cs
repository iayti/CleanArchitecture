namespace Application.Common.Mapping
{
    using Mapster;

    public interface IMapFrom<TSource, TDestination>
    {
        //void Mapping(TypeAdapterConfig<TSource, TDestination> config); //=> profile.CreateMap(typeof(T), GetType());
    }
}
