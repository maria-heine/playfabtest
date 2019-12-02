using System;
using System.Collections.Generic;

public class GenericDataStore<TKey, UValue>
{
    protected Dictionary<TKey, UValue> _store;

    public GenericDataStore()
    {
        _store = new Dictionary<TKey, UValue>();
    }

    public void SetItem(TKey key, UValue item)
    {
        //_store.Add(key, item);
        _store[key] = item;
    }

    public bool ContainsKey(TKey key)
    {
        return _store.ContainsKey(key);
    }

    public UValue GetItem(TKey key)
    {
        return _store[key];
    }

    public void RemoveItem(TKey key)
    {
        _store.Remove(key);
    }

    public void QueryItems(Action<UValue> query)
    {
        foreach (var keyValuePair in _store)
        {
            query.Invoke(keyValuePair.Value);
        }
    }
}

//public class DictionaryDataStore<TKey, UValue> : GenericDataStore<TKey, UValue>
//{
//    public DictionaryDataStore()
//    {
//        _store = new Dictionary<TKey, UValue>();
//    }
//}

//public class LeaderboardDataStore : GenericDataStore<int, LeaderboardResult>
//{
//    public LeaderboardDataStore()
//    {
//        _store = new SortedDictionary<int, LeaderboardResult>();
//    }

//    public LeaderboardDataStore()
//    {
//        _store = new SortedDictionary<int, LeaderboardResult>();
//    }
//    public void QueryLeaderboardInScoreOrder(Action<LeaderboardResult> query)
//    {
//        foreach (var keyValuePair in _store)
//        {
//            query.Invoke(keyValuePair.Value);
//        }

//        IEnumerable<LeaderboardResult> sortedLeaderboards =
//            from score in _store.Values
//            orderby score. descending

//    }
//}
