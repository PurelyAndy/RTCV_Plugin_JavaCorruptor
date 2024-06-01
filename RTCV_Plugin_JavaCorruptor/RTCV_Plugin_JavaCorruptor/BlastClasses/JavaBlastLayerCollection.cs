using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Xml.Serialization;
using Ceras;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RTCV.CorruptCore;
using SlimDX.Direct2D;

namespace Java_Corruptor.BlastClasses;

public class JavaBlastLayerCollection : IDictionary<string, JavaBlastLayer>, IList<JavaBlastUnit>, ICloneable, INote
{
    private Dictionary<string, JavaBlastLayer> _mappedLayers = new();
    public Dictionary<string, JavaBlastLayer> MappedLayers {
        get => _mappedLayers;
        set
        {
            _mappedLayers = value;
            _layer = SplitToLayer();
        }
    }
    private JavaBlastLayer _layer = new();
    public JavaBlastLayer Layer {
        get => _layer;
        set
        {
            _layer = value;
            _mappedLayers = new();
            foreach (var unit in value.Layer)
            {
                if (!_mappedLayers.ContainsKey(unit.Method))
                    _mappedLayers[unit.Method] = new();
                _mappedLayers[unit.Method].Layer.Add(unit);
            }
        }
    }

    public ICollection<string> Keys => ((IDictionary<string, JavaBlastLayer>)_mappedLayers).Keys;

    public ICollection<JavaBlastLayer> Values => ((IDictionary<string, JavaBlastLayer>)_mappedLayers).Values;

    public int Count => _layer.Layer.Count;

    public bool IsReadOnly => ((ICollection<KeyValuePair<string, JavaBlastLayer>>)_mappedLayers).IsReadOnly;

    public JavaBlastLayer this[string key]
    {
        get => MappedLayers[key];
        set
        {
            if (_mappedLayers.TryGetValue(key, out JavaBlastLayer layer))
                _layer.Layer.RemoveAll(unit => layer.Layer.Contains(unit));

            _layer.Layer.AddRange(value.Layer);

            _mappedLayers[key] = value;
        }
    }

    public JavaBlastUnit this[int index]
    {
        get => _layer.Layer[index];
        set
        {
            _mappedLayers[_layer.Layer[index].Method].Layer.Remove(_layer.Layer[index]);
            _mappedLayers[_layer.Layer[index].Method].Layer.Add(value);
            _layer.Layer[index] = value;
        }
    }

    public JavaBlastLayerCollection()
    {
    }

    public JavaBlastLayerCollection(JavaBlastUnit bu)
    {
        Add(bu.Method, new(bu));
    }
    
    public JavaBlastLayerCollection(JavaBlastLayer bl)
    {
        Layer = bl;
    }

    public JavaBlastLayerCollection(List<JavaBlastUnit> layer, bool allCertainlyHaveSameMethod = false)
    {
        if (allCertainlyHaveSameMethod)
            Add(layer.First().Method, new(layer));
        else
            Layer = new(layer);
    }
    
    private JavaBlastLayer SplitToLayer()
    {
        List<JavaBlastUnit> units = new();
        
        foreach (var layer in MappedLayers)
            units.AddRange(layer.Value.Layer);
        
        return new(units);
    }
    
    //TODO: reroll support maybe

    public bool ContainsKey(string key)
    {
        return _mappedLayers.ContainsKey(key);
    }

    public void Add(string key, JavaBlastLayer value)
    {
        _mappedLayers.Add(key, value);
        _layer.Layer.AddRange(value.Layer);
    }

    public bool Remove(string key)
    {
        _layer.Layer.RemoveAll(unit => _mappedLayers[key].Layer.Contains(unit));
        return _mappedLayers.Remove(key);
    }

    public bool TryGetValue(string key, out JavaBlastLayer value)
    {
        return _mappedLayers.TryGetValue(key, out value);
    }

    public void Add(KeyValuePair<string, JavaBlastLayer> item)
    {
        if (!_mappedLayers.ContainsKey(item.Key))
            _mappedLayers.Add(item.Key, item.Value);
        else
            _mappedLayers[item.Key].Layer.AddRange(item.Value.Layer);
        _layer.Layer.AddRange(item.Value.Layer);
    }

    public void Clear()
    {
        _mappedLayers.Clear();
        _layer.Layer.Clear();
    }

    public bool Contains(KeyValuePair<string, JavaBlastLayer> item)
    {
        return _mappedLayers.Contains(item);
    }

    public void CopyTo(KeyValuePair<string, JavaBlastLayer>[] array, int arrayIndex)
    {
        ((IDictionary<string, JavaBlastLayer>)_mappedLayers).CopyTo(array, arrayIndex);
    }

    public bool Remove(KeyValuePair<string, JavaBlastLayer> item)
    {
        _layer.Layer.RemoveAll(item.Value.Layer.Contains);
        return ((IDictionary<string, JavaBlastLayer>)_mappedLayers).Remove(item);
    }

    public IEnumerator<KeyValuePair<string, JavaBlastLayer>> GetEnumerator()
    {
        return _mappedLayers.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return ((IEnumerable)_mappedLayers).GetEnumerator();
    }

    public int IndexOf(JavaBlastUnit item)
    {
        return _layer.Layer.IndexOf(item);
    }

    public void Insert(int index, JavaBlastUnit item)
    {
        _layer.Layer.Insert(index, item);
        _mappedLayers[item.Method].Layer.Add(item);
    }

    public void RemoveAt(int index)
    {
        _mappedLayers[_layer.Layer[index].Method].Layer.Remove(_layer.Layer[index]);
        if (_mappedLayers[_layer.Layer[index].Method].Layer.Count == 0)
            _mappedLayers.Remove(_layer.Layer[index].Method);
        _layer.Layer.RemoveAt(index);
    }

    public void Add(JavaBlastUnit item)
    {
        _layer.Layer.Add(item);
        if (!_mappedLayers.ContainsKey(item.Method))
            _mappedLayers[item.Method] = new();
        _mappedLayers[item.Method].Layer.Add(item);
    }

    public bool Contains(JavaBlastUnit item)
    {
        return _layer.Layer.Contains(item);
    }

    public void CopyTo(JavaBlastUnit[] array, int arrayIndex)
    {
        _layer.Layer.CopyTo(array, arrayIndex);
    }

    public bool Remove(JavaBlastUnit item)
    {
        _layer.Layer.Remove(item);
        bool b = _mappedLayers[item.Method].Layer.Remove(item);
        if (_mappedLayers[item.Method].Layer.Count == 0)
            _mappedLayers.Remove(item.Method);
        return b;
    }

    IEnumerator<JavaBlastUnit> IEnumerable<JavaBlastUnit>.GetEnumerator()
    {
        return _layer.Layer.GetEnumerator();
    }

    public object Clone()
    {
        return ObjectCopierCeras.Clone(this);
    }

    private const string Shared = "[DIFFERENT]";

    [JsonIgnore]
    public string Note
    {
        get
        {
            return Layer.Layer.All(x => x.Note == Layer.Layer.First().Note)
                ? Layer.Layer.FirstOrDefault()?.Note
                : Shared;
        }
        set
        {
            if (value == Shared)
            {
                return;
            }
            foreach (JavaBlastUnit bu in Layer.Layer)
            {
                bu.Note = value;
            }
        }
    }
}