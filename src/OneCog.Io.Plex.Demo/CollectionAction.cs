
namespace OneCog.Io.Plex.Demo
{
    public interface ICollectionAction
    {

    }

    public class Clear : ICollectionAction
    {

    }

    public class Add<T> : ICollectionAction
    {
        public Add(T item)
        {
            Item = item;
        }

        public T Item { get; private set; }
    }

    public class Remove<T> : ICollectionAction
    {
        public Remove(T item)
        {
            Item = item;
        }

        public T Item { get; private set; }
    }

    public static class Collection<T>
    {
        public static Clear Clear()
        {
            return new Clear();
        }

        public static Add<T> Add(T item)
        {
            return new Add<T>(item);
        }

        public static Remove<T> Remove(T item)
        {
            return new Remove<T>(item);
        }
    }
}
