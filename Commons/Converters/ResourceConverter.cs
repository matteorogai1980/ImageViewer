namespace Commons.Converters;

public interface ResourceConverter<E, R>
{
    R toDTO(E entity);

    E toEntity(R resource);
}