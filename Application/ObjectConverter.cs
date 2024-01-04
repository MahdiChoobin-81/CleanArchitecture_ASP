namespace Application;

public static class ObjectConverter<T>
{
    public static T Convert(object obj)
    {
        return (T)obj;
    }
}