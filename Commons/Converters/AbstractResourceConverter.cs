namespace Commons.Converters;

public abstract class AbstractResourceConverter<E, R> : ResourceConverter<E, R>
{
    public abstract R toDTO(E entity);
    public abstract E toEntity(R resource);

    public List<R> toDTOs(List<E> entities)
    {
        List<R> dtos = new List<R>(entities.Count);
        foreach (E e in entities) dtos.Add(toDTO(e));
        return dtos;
    }

    public List<E> toEntities(List<R> resources)
    {
        List<E> entities = new List<E>(resources.Count());
        foreach (R r in resources) entities.Add(toEntity(r));
        return entities;
    }
}