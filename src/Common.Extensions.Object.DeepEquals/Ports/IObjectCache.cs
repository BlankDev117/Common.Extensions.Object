namespace Common.Extensions.Object.DeepEquals.Ports
{
    // Primary Port: Implemented Internally
    public interface IObjectCache
    {
        void Add(object key, object value);

        bool TryGet(object key, out object value);
    }
}
