namespace RatGame;

public class SwapbackList<T>
{
    private T[] _items;
    public int Count = 0;
    private const int defaultCapacity = 16;

    public SwapbackList()
    {
        _items = new T[defaultCapacity];
    }

    public SwapbackList(int capacity = defaultCapacity)
    {
        _items = new T[capacity];
    }

    public void Add(T item)
    {
        if (Count == _items.Length)
            Array.Resize(ref _items, _items.Length * 2);

        _items[Count++] = item;
    }

    public void RemoveAt(int index)
    {
        _items[index] = _items[Count - 1];
        Count--;
    }

    public void Clear()
    {
        _items = new T[defaultCapacity];
        Count = 0;
    }

    public T this[int index] => _items[index];
}
