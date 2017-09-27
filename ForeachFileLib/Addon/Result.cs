using System.Collections.Generic;
using System.Linq;

namespace ForeachFileLib.Addon
{
    public class Result : IResult
    {
        private Dictionary<string, HashSet<string>> data_;

        public IReadOnlyDictionary<string, HashSet<string>> Data { get { return data_; } }
        public Result(Dictionary<string, HashSet<string>> data)
        {
            data_ = data;
        }
        public Result()
        {
            data_ = new Dictionary<string, HashSet<string>>();
        }
        public void Remove(string key)
        {
            if (RemoveKey(key))
            {
                OnKeyRemoved(key);
            }
        }
        public void Remove(string key, IEnumerable<string> item)
        {
            HashSet<string> set = null;
            if (item == null || !data_.TryGetValue(key, out set))
            {
                return;
            }
            set.ExceptWith(item);
            if (set.Any())
            {
                OnValueRemoved(key, item);
            }
            else
            {
                RemoveKey(key);
                OnKeyRemoved(key);
            }

        }
        public void Remove(string key, string value)
        {
            Remove(key, new string[] { value });
        }
        public void RemoveKeys(IEnumerable<string> keys)
        {
            if (keys == null)
            {
                return;
            }
            bool flag = false;
            foreach (var item in keys)
            {
                if (RemoveKey(item))
                {
                    flag = true;
                }
            }
            if (flag)
            {
                OnKeyRemoved(keys);
            }
        }

        public void Add(string key, string value)
        {
            Add(key, new string[] { value });
        }
        public void Add(string key, IEnumerable<string> values)
        {
            if (values == null)
            {
                return;
            }
            if (!data_.ContainsKey(key))
            {
                data_.Add(key, new HashSet<string>());
            }
            bool flag = false;
            var set = data_[key];
            foreach (var item in values)
            {
                if (set.Add(item))
                {
                    flag = true;
                }
            }
            if (flag)
            {
                OnValueAdded(key, values);
            }
        }

        private void OnKeyRemoved(string key)
        {
            OnKeyRemoved(new string[] { key });
        }

        public event ValueAddedEventHandler ValueAdded;
        private void OnValueAdded(string key, IEnumerable<string> values)
        {
            ValueAdded?.Invoke(this, new ValueAddedEventArgs(key, values));
        }
        public event KeyRemovedEventHandler KeyRemoved;
        private void OnKeyRemoved(IEnumerable<string> keys)
        {
            KeyRemoved?.Invoke(this, new KeyRemovedEventArgs(keys));
        }
        public event ValueRemovedEventHandler ValueRemoved;
        private void OnValueRemoved(string key, IEnumerable<string> values)
        {
            ValueRemoved?.Invoke(this, new ValueRemovedEventArgs(key, values));
        }

        private bool RemoveKey(string key)
        {
            return data_.Remove(key);
        }
    }
}
