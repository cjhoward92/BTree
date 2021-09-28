using System;
using System.Collections.Generic;
using System.Text;

namespace BTree
{
    // TODO: Calculate page size based on TKey + TData
    // https://en.wikipedia.org/wiki/B%2B_tree
    public class Tree<TKey, TData>
        where TKey : IComparable<TKey>
        where TData : class
    {
        private Node root;
        private ushort maxNodeSize;

        public Tree() : this(100) { }

        public Tree(ushort maxNodeSize)
        {
            this.maxNodeSize = maxNodeSize;
            this.root = new Node(this.maxNodeSize, true);
        }

        public TData Search(TKey key)
        {
            var node = this.SearchInternal(key, this.root);
            if (node is null)
            {
                return null;
            }
            // TODO
            return default(TData);
        }

        private Node SearchInternal(TKey key, Node root)
        {
            // TODO
            return null;
        }

        public void Insert(TKey key, TData data)
        {
            var storageNode = this.root.FindStorageNode(key);
            if (storageNode is null)
            {
                throw new Exception("Something went wrong");
            }

            storageNode.Insert(key, data);
        }

        // TODO: Refactor to handle binary data like it would be found in a page
        // TODO: Refactor to page/leaf inheritance?
        private class Node
        {
            private ushort nodeSize;
            private bool isLeaf;
            private TKey[] keys;
            private object[] pointers;

            public Node(ushort nodeSize, bool isLeaf)
            {
                this.nodeSize = nodeSize;
                this.isLeaf = isLeaf;
                this.keys = new TKey[nodeSize];
                this.pointers = new object[nodeSize];
            }

            public Node FindStorageNode(TKey key)
            {
                if (this.isLeaf)
                {
                    return this;
                }

                for (var i = 0; i < this.keys.Length - 1; i += 2)
                {
                    var startKey = this.keys[i];
                    var endKey = this.keys[i + 1];

                    Node n = null;
                    if (key.CompareTo(startKey) <= 0)
                    {
                        n = this.pointers[i] as Node;
                    }
                    else if (key.CompareTo(startKey) > 0 && key.CompareTo(endKey) <= 0)
                    {
                        n = this.pointers[i + 1] as Node;
                    }

                    if (!(n is null))
                    {
                        return n.FindStorageNode(key);
                    }
                }

                return (this.pointers[this.pointers.Length - 1] as Node).FindStorageNode(key);
            }

            public void Insert(TKey key, TData data)
            {
                // TODO;
            }
        }
    }
}
