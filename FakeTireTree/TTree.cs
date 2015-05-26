// 项目需要,修改了tire树的一些结构用来存一些东西,所以实现上有点靠项目需求
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TireTree
{
    public class TTree<TKey, TValue> : TElement<TKey, TValue>
    {
        public class StepFinder
        {
            private TElement<TKey, TValue> currentNode;
            private TTree<TKey, TValue> searchTree;
            private List<TKey> searchKeys = new List<TKey>();
            public List<TKey> ValidKeys { get { return searchKeys; } }
            // 未搜索时当前节点为根
            public TElement<TKey, TValue> CurrentElement
            {
                get
                {
                    if (!IsPathExist)
                    {
                        return null;
                    }
                    return currentNode;
                }
            }
            public TTreeNode<TKey, TValue> CurrentNode { get { return CurrentElement as TTreeNode<TKey, TValue>; } }
            public bool IsPathExist { get; private set; }
            public StepFinder(TTree<TKey, TValue> tree)
            {
                searchTree = tree;
                Reset();
            }
            // 返回false表示查找失败(没有路径,但是有路径不一定有值)
            public bool SearchNext(TKey key)
            {
                if (!IsPathExist)
                {
                    return IsPathExist;
                }              
                currentNode = currentNode[key];

                if (currentNode == null)
                {
                    IsPathExist = false; // 搜索失败
                }
                else
                {
                    searchKeys.Add(key);
                }
                return IsPathExist;
            }
            public void Reset()
            {
                currentNode = searchTree;
                IsPathExist = true;
            }
        }



        public TTreeNode<TKey, TValue> CreateNode(IEnumerable<TKey> key)
        {
            if (key == null || key.Count() == 0)
            {
                return null;
            }
            var cursorNode = (TElement<TKey, TValue>)this;
            foreach (var item in key)
            {
                if (!cursorNode.HasChild)
                {
                    cursorNode.ChildDictionary = new Dictionary<TKey, TTreeNode<TKey, TValue>>();
                }
                if (!cursorNode.ChildDictionary.ContainsKey(item))
                {
                    cursorNode.ChildDictionary.Add(item, new TTreeNode<TKey, TValue>()); // 不存储父节点,想回溯Key就直接保存在Value里
                }
                cursorNode = cursorNode[item];

            }
            return (TTreeNode<TKey, TValue>)cursorNode;
        }
        public void Add(IEnumerable<TKey> key, TValue value)
        {
            Add(key, value, null);
        }
        // 回调中前面参数是旧值,后面是要添加的值,返回最终添加的值
        public void Add(IEnumerable<TKey> key, TValue value, Func<TValue, TValue, TValue> handleDuplicateKeyException)
        {
            var node = CreateNode(key);
            if (node == null)
            {
                return;
            }
            if (node.HasValue)
            {
                if (handleDuplicateKeyException != null)
                {
                    node.Value = handleDuplicateKeyException(node.Value, value);
                    return;
                }
                throw new ArgumentException("Value已存在");
            }
            node.Value = value;
        }
        public TTreeNode<TKey, TValue> FindNode(IEnumerable<TKey> key)
        {
            if (key == null || key.Count() == 0)
            {
                return null;
            }
            var node = (TElement<TKey, TValue>)this;
            foreach (var item in key)
            {
                if (!node.HasChild || !node.ChildDictionary.ContainsKey(item))
                {
                    return null;
                }
                node = (TElement<TKey, TValue>)node[item];
            }
            return node as TTreeNode<TKey, TValue>;
        }

        public TTreeNode<TKey, TValue> FindNode(IEnumerable<TKey> key, out List<TKey> endPath)
        {
            endPath = null;
            if (key == null || key.Count() == 0)
            {
                return null;
            }
            endPath = new List<TKey>();
            var node = (TElement<TKey, TValue>)this;
            foreach (var item in key)
            {
                if (!node.HasChild || !node.ChildDictionary.ContainsKey(item))
                {
                    break;
                }
                node = (TElement<TKey, TValue>)node[item];
                endPath.Add(item);
            }
            if (endPath.Count==0)
            {
                endPath = null;
            }
            return node as TTreeNode<TKey, TValue>;
        }

        public TTree<TKey, TValue>.StepFinder CreateFinder()
        {
            return new StepFinder(this);
        }
    }
}
