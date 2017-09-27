using System;
using System.Collections.Generic;

namespace ForeachFileLib.Addon
{
    public class ValueAddedEventArgs : EventArgs
    {
        public string Key { get; private set; }
        public IEnumerable<string> Values { get; private set; }

        public ValueAddedEventArgs(string key, IEnumerable<string> values)
        {
            Key = key;
            Values = values;
        }
    }
    public delegate void ValueAddedEventHandler(object sender, ValueAddedEventArgs e);

    public class KeyRemovedEventArgs : EventArgs
    {
        public IEnumerable<string> Keys { get; private set; }
        public KeyRemovedEventArgs(IEnumerable<string> keys)
        {
            Keys = keys;
        }
    }
    public delegate void KeyRemovedEventHandler(object sender, KeyRemovedEventArgs e);

    public class ValueRemovedEventArgs : EventArgs
    {
        public IEnumerable<string> Values { get; private set; }
        public string Key { get; private set; }
        public ValueRemovedEventArgs(string key, IEnumerable<string> values)
        {
            Key = key;
            Values = values;
        }
    }
    public delegate void ValueRemovedEventHandler(object sender, ValueRemovedEventArgs e);

    public interface IResult
    {
        IReadOnlyDictionary<string, HashSet<string>> Data { get; }

        event KeyRemovedEventHandler KeyRemoved;
        event ValueAddedEventHandler ValueAdded;
        event ValueRemovedEventHandler ValueRemoved;

        void Add(string key, string value);
        void Add(string key, IEnumerable<string> values);
        void Remove(string key);
        void Remove(string key, string value);
        void Remove(string key, IEnumerable<string> item);
        void RemoveKeys(IEnumerable<string> keys);
    }
}