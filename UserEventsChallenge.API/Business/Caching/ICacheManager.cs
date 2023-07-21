namespace UserEventsChallenge.API.Business.Caching;

public interface ICacheManager
{
    T Get<T>(string key);
    object Get(string key);
    void Add(string key, object value);
    bool IsAdd(string key);
    void Remove(string key);
    void RemoveByPattern(string pattern);
}
