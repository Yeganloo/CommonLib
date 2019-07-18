﻿using Eve.Collections;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Eve.Collections.Graph
{
    public class SimpleGraph<TNode> : IEnumerable<Node<TNode>>
    {
        private readonly DynamicArray<Node<TNode>> _Nodes;
        private readonly DynamicArray<DynamicArray<int>> Neigbors;
        private int _AverageEdges = 4;
        public bool Directed { get; }


        private int _Count;
        public int Count
        {
            get
            {
                return _Count;
            }
        }

        #region Init

        public SimpleGraph(bool directed)
        {
            _Nodes = new DynamicArray<Node<TNode>>();
            Neigbors = new DynamicArray<DynamicArray<int>>();
            Directed = directed;
        }
        public SimpleGraph(bool directed, int count)
        {
            int bufs = (int)Math.Sqrt(count) + 4;
            _Nodes = new DynamicArray<Node<TNode>>(bufs);
            Neigbors = new DynamicArray<DynamicArray<int>>(bufs);
            Directed = directed;
        }

        #endregion

        public Node<TNode> this[int id]
        {
            get
            {
                return _Nodes[id];
            }
            set
            {
                lock (Neigbors)
                {
                    Neigbors[id] = new DynamicArray<int>(_AverageEdges);
                    _Nodes[id] = value;
                    _Count++;
                }
                _AverageEdges = (int)Math.Sqrt(Count + 16);
            }
        }

        public Node<TNode> Set(int id, TNode value)
        {
            var n = new Node<TNode>(value, id.ToString());
            this[id] = n;
            return n;
        }

        public void AddEdge(int source, int destination)
        {
            if (Directed)
            {
                var src = Neigbors[source];
                lock (src)
                {
                    src.Add(destination);
                }
            }
            else
            {
                var src = Neigbors[source];
                var dst = Neigbors[destination];
                lock (Neigbors)
                {
                    src.Add(destination);
                    dst.Add(source);
                }
            }
        }

        public void Clear()
        {
            lock(Neigbors)
            {
                _Nodes.Clear();
                Neigbors.Clear();
                _Count = 0;
            }
        }

        #region Intefaces

        public IEnumerator<Node<TNode>> GetEnumerator()
        {
            return _Nodes.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _Nodes.GetEnumerator();
        }

        #endregion
    }
}