namespace Utils;

public interface ICacheProvider<Key, Value>
{
    bool TryGetValue(Key key, out Value val);
    void Set(Key key, Value value, TimeSpan duration);
    void Set(Dictionary<Key, Value> di, TimeSpan duration);
}