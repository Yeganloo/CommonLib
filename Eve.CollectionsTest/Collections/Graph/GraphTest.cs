﻿using CommonLib.Collections.Graph;
using NUnit.Framework;
using System;

namespace Eve.CollectionsTest.Graph
{
    [TestFixture()]
    public class GraphTest
    {
        private const int Round = 300000;

        [Test()]
        public void _()
        {

        }

        [Test()]
        public void _Sequenc_ReadWrite()
        {
            var graph = new Graph<object, int>(false, Round);
            var random = new Random(DateTime.Now.Millisecond);
            for (int i = 0; i < Round; i++)
            {
                graph.Set(i, i);
            }
            for (int i = 0; i < 30000000; i++)
            {
                graph.AddEdge(random.Next(0, Round - 1), random.Next(0, Round - 1), i);
            }
            for (int i = 0; i < Round; i++)
            {
                Assert.IsTrue(i == (int)graph[i].Value);
            }
        }

    }
}