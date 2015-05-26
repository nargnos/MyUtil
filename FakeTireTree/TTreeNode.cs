using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TireTree
{
    public class TTreeNode<TKey, TValue> : TElement<TKey, TValue>
    {
        private TValue val;
        private bool hasValue = false;
        public TValue Value 
        { 
            get 
            { 
                return val;
            }
            set 
            {
                hasValue = value != null;
                val = value;
            } 
        }
        public bool HasValue
        {
            get
            {
                return hasValue;
            }
        }
       
        internal TTreeNode()
        {
           
        }

       
    }
}
