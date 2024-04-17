using System.Collections;
using System.Collections.Generic;

namespace Java_Corruptor;

public class BidirectionalDictionary <TKey, TValue> : IDictionary<TKey, TValue>
{
    private readonly Dictionary<TKey, TValue> Delegate;
    private readonly Dictionary<TValue, TKey> Inverse;

    public TValue this[TKey key]
    {
        get => Delegate[key];
        set
        {
            Delegate[key] = value;
            Inverse[value] = key;
        }
    }
        
    public TKey this[TValue key]
    {
        get => Inverse[key];
        set
        {
            Inverse[key] = value;
            Delegate[value] = key;
        }
    }
        
    public BidirectionalDictionary()
    {
        Delegate = new();
        Inverse = new();
    }
        
    public BidirectionalDictionary(int capacity)
    {
        Delegate = new(capacity);
        Inverse = new(capacity);
    }
        
    public BidirectionalDictionary(IDictionary<TKey, TValue> dictionary)
    {
        Delegate = new(dictionary);
        Inverse = new();
        foreach (KeyValuePair<TKey, TValue> pair in dictionary)
        {
            Inverse.Add(pair.Value, pair.Key);
        }
    }
        
    public ICollection<TKey> Keys => Delegate.Keys;
    public ICollection<TValue> Values => Delegate.Values;
    public int Count => Delegate.Count;
    public bool IsReadOnly => false;
        
    public void Add(TKey key, TValue value)
    {
        Delegate.Add(key, value);
        Inverse.Add(value, key);
    }
        
    public void Add(KeyValuePair<TKey, TValue> item)
    {
        Delegate.Add(item.Key, item.Value);
        Inverse.Add(item.Value, item.Key);
    }
        
    public void Clear()
    {
        Delegate.Clear();
        Inverse.Clear();
    }

    public bool ContainsKey(TKey key)
    {
        return Delegate.ContainsKey(key);
    }
        
    public bool Contains(KeyValuePair<TKey, TValue> item)
    {
        return Delegate.ContainsKey(item.Key) && Delegate[item.Key].Equals(item.Value);
    }
        
    public bool Remove(TKey key)
    {
        Inverse.Remove(Delegate[key]);
        return Delegate.Remove(key);
    }
        
    public bool Remove(KeyValuePair<TKey, TValue> item)
    {
        Inverse.Remove(item.Value);
        return Delegate.Remove(item.Key);
    }
        
    public bool TryGetValue(TKey key, out TValue value)
    {
        return Delegate.TryGetValue(key, out value);
    }
        
    public bool TryGetValue(TValue key, out TKey value)
    {
        return Inverse.TryGetValue(key, out value);
    }
        
    public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
    {
        foreach (KeyValuePair<TKey, TValue> pair in Delegate)
        {
            array[arrayIndex++] = pair;
        }
    }
        
    public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
    {
        return Delegate.GetEnumerator();
    }
        
    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
        
    public bool ContainsValue(TValue value)
    {
        return Inverse.ContainsKey(value);
    }
}