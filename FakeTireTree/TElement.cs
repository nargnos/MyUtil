using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TireTree
{
    public abstract class TElement<TKey, TValue>
    {
        // 有node不一定有value,不一定有child
        private Dictionary<TKey, TTreeNode<TKey, TValue>> childDictionary;

        internal Dictionary<TKey, TTreeNode<TKey, TValue>> ChildDictionary
        {
            get
            {                
                return childDictionary;
            }
            set 
            {
                childDictionary = value;
            }
        }

        internal bool HasChild
        {
            get
            {
                return childDictionary == null ? false : childDictionary.Count > 0;
            }
        }
      
        public TTreeNode<TKey, TValue> this[TKey key]
        {
            get 
            {
                if (ChildDictionary == null || !ChildDictionary.ContainsKey(key))
                {
                    return null;
                }
                return ChildDictionary[key];
            }
        }
        public Dictionary<TKey, TireTree.TTreeNode<TKey, TValue>>.KeyCollection ChildrenKeys
        {
            get 
            {
                return HasChild? ChildDictionary.Keys:null;
            }
        }

        public Dictionary<TKey, TireTree.TTreeNode<TKey, TValue>>.ValueCollection Children 
        {
            get
            {
                return HasChild ? ChildDictionary.Values : null;
            }
        }

        public void Clear()
        {
            if (ChildDictionary!=null)
            {
                foreach (var item in ChildDictionary)
                {
                    item.Value.Clear();
                }
                ChildDictionary.Clear();
                ChildDictionary = null;
            }
        }
    }
}
