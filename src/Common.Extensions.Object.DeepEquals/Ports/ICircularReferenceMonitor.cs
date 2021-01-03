namespace Common.Extensions.Object.DeepEquals.Ports
{
    // Primary Port: Implemented Internally
    public interface ICircularReferenceMonitor
    {
        bool AddReference(object parent, object child);
    }
}
