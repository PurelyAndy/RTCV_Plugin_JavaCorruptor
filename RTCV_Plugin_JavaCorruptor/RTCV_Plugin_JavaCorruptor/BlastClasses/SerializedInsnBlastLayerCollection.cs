using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web.Script.Serialization;
using System.Xml.Serialization;
using Ceras;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RTCV.CorruptCore;
using SlimDX.Direct2D;

namespace Java_Corruptor.BlastClasses;

// this name is far too long for how often it's used
public class SerializedInsnBlastLayerCollection : IDictionary<string, SerializedInsnBlastLayer>, IList<SerializedInsnBlastUnit>, ICloneable, INote
{
    private Dictionary<string, SerializedInsnBlastLayer> _mappedLayers = new();
    public Dictionary<string, SerializedInsnBlastLayer> MappedLayers {
        get => _mappedLayers;
        set
        {
            _mappedLayers = value;
            _layer = SplitToLayer();
        }
    }
    private SerializedInsnBlastLayer _layer = new();
    public SerializedInsnBlastLayer Layer {
        get => _layer;
        set
        {
            _layer = value;
            _mappedLayers = new();
            foreach (var unit in value.Layer)
            {
                if (!_mappedLayers.ContainsKey(unit.Method))
                    _mappedLayers[unit.Method] = new SerializedInsnBlastLayer();
                _mappedLayers[unit.Method].Layer.Add(unit);
            }
        }
    }

    public ICollection<string> Keys => ((IDictionary<string, SerializedInsnBlastLayer>)_mappedLayers).Keys;

    public ICollection<SerializedInsnBlastLayer> Values => ((IDictionary<string, SerializedInsnBlastLayer>)_mappedLayers).Values;

    public int Count => _layer.Layer.Count;

    public bool IsReadOnly => ((ICollection<KeyValuePair<string, SerializedInsnBlastLayer>>)_mappedLayers).IsReadOnly;

    public SerializedInsnBlastLayer this[string key]
    {
        get => MappedLayers[key];
        set
        {
            if (_mappedLayers.ContainsKey(key))
                _layer.Layer.RemoveAll(unit => _mappedLayers[key].Layer.Contains(unit));

            _layer.Layer.AddRange(value.Layer);

            _mappedLayers[key] = value;
        }
    }

    public SerializedInsnBlastUnit this[int index]
    {
        get => _layer.Layer[index];
        set
        {
            _mappedLayers[_layer.Layer[index].Method].Layer.Remove(_layer.Layer[index]);
            _mappedLayers[_layer.Layer[index].Method].Layer.Add(value);
            _layer.Layer[index] = value;
        }
    }

    public SerializedInsnBlastLayerCollection()
    {
    }

    public SerializedInsnBlastLayerCollection(SerializedInsnBlastUnit bu)
    {
        Add(bu.Method, new(bu));
    }
    
    public SerializedInsnBlastLayerCollection(SerializedInsnBlastLayer bl)
    {
        Layer = bl;
    }

    public SerializedInsnBlastLayerCollection(List<SerializedInsnBlastUnit> layer, bool allCertainlyHaveSameMethod = false)
    {
        if (allCertainlyHaveSameMethod)
            Add(layer.First().Method, new(layer));
        else
            Layer = new(layer);
    }
    
    private SerializedInsnBlastLayer SplitToLayer()
    {
        List<SerializedInsnBlastUnit> units = new();
        
        foreach (var layer in MappedLayers)
            units.AddRange(layer.Value.Layer);
        
        return new(units);
    }
    
    /// <summary>
    /// You MUST load the classes from the appropriate jar file before calling this.
    /// Runs the engines used to create the units in this layer again, replacing the units' corruptions with the result.
    /// </summary>
    public void ReRoll()
    {
        foreach (SerializedInsnBlastUnit bu in Layer.Layer.Where(x => x.IsLocked == false))
        {
            bu.ReRoll();
        }
    }

    public bool ContainsKey(string key)
    {
        return _mappedLayers.ContainsKey(key);
    }

    public void Add(string key, SerializedInsnBlastLayer value)
    {
        _mappedLayers.Add(key, value);
        _layer.Layer.AddRange(value.Layer);
    }

    public bool Remove(string key)
    {
        _layer.Layer.RemoveAll(unit => _mappedLayers[key].Layer.Contains(unit));
        return _mappedLayers.Remove(key);
    }

    public bool TryGetValue(string key, out SerializedInsnBlastLayer value)
    {
        return _mappedLayers.TryGetValue(key, out value);
    }

    public void Add(KeyValuePair<string, SerializedInsnBlastLayer> item)
    {
        ((IDictionary<string, SerializedInsnBlastLayer>)_mappedLayers).Add(item);
        _layer.Layer.AddRange(item.Value.Layer);
    }

    public void Clear()
    {
        _mappedLayers.Clear();
        _layer.Layer.Clear();
    }

    public bool Contains(KeyValuePair<string, SerializedInsnBlastLayer> item)
    {
        return _mappedLayers.Contains(item);
    }

    public void CopyTo(KeyValuePair<string, SerializedInsnBlastLayer>[] array, int arrayIndex)
    {
        ((IDictionary<string, SerializedInsnBlastLayer>)_mappedLayers).CopyTo(array, arrayIndex);
    }

    public bool Remove(KeyValuePair<string, SerializedInsnBlastLayer> item)
    {
        _layer.Layer.RemoveAll(item.Value.Layer.Contains);
        return ((IDictionary<string, SerializedInsnBlastLayer>)_mappedLayers).Remove(item);
    }

    public IEnumerator<KeyValuePair<string, SerializedInsnBlastLayer>> GetEnumerator()
    {
        return _mappedLayers.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return ((IEnumerable)_mappedLayers).GetEnumerator();
    }

    public int IndexOf(SerializedInsnBlastUnit item)
    {
        return _layer.Layer.IndexOf(item);
    }

    public void Insert(int index, SerializedInsnBlastUnit item)
    {
        _layer.Layer.Insert(index, item);
        if (!_mappedLayers.ContainsKey(item.Method))
            _mappedLayers[item.Method] = new();
        _mappedLayers[item.Method].Layer.Add(item);
    }

    public void RemoveAt(int index)
    {
        _mappedLayers[_layer.Layer[index].Method].Layer.Remove(_layer.Layer[index]);
        _layer.Layer.RemoveAt(index);
    }

    public void Add(SerializedInsnBlastUnit item)
    {
        _layer.Layer.Add(item);
        if (!_mappedLayers.ContainsKey(item.Method))
            _mappedLayers[item.Method] = new();
        _mappedLayers[item.Method].Layer.Add(item);
    }

    public bool Contains(SerializedInsnBlastUnit item)
    {
        return _layer.Layer.Contains(item);
    }

    public void CopyTo(SerializedInsnBlastUnit[] array, int arrayIndex)
    {
        _layer.Layer.CopyTo(array, arrayIndex);
    }

    public bool Remove(SerializedInsnBlastUnit item)
    {
        _layer.Layer.Remove(item);
        return _mappedLayers[item.Method].Layer.Remove(item);
    }

    IEnumerator<SerializedInsnBlastUnit> IEnumerable<SerializedInsnBlastUnit>.GetEnumerator()
    {
        return _layer.Layer.GetEnumerator();
    }

    public object Clone()
    {
        //return ObjectCopierCeras.Clone(this);
        SerializedInsnBlastLayerCollection clone = new();
        
        foreach (var layer in MappedLayers)
            clone.Add(layer.Key, (SerializedInsnBlastLayer)layer.Value.Clone());
        clone.Note = Note;
        
        return clone;
        
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
            foreach (SerializedInsnBlastUnit bu in Layer.Layer)
            {
                bu.Note = value;
            }
        }
    }
}