namespace Gabevlogd.Patterns
{
    /// <summary>
    /// This interface represents the object that is to observe an observable
    /// </summary>
    public interface IObserver
    {
        public abstract void UpdateObserver(string message = null, int value = -1);
    }
}


