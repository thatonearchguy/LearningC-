using System;
using System.Collections.Generic;
using System.Linq;

namespace Graphs
{
    public class Graph <T>
    {
        private class AdjacencyMatrix
        {
            internal List<List<int>> matrix {get;}
            internal List<T> header; 
            internal int Count;
            public AdjacencyMatrix(List<Tuple<T,T,int>> values)
            {
                //1st value - First item | 2nd value - Second item | 3rd item - weight
                foreach(var tuple in values) header.Add(tuple.Item1);
                header = header.Distinct().ToList();
                header.Sort();
                Count = header.Count();
                matrix = new List<List<int>>();
                for(int i = 0; i < header.Count; i ++)
                {
                    matrix[i] = new List<int> (new int[header.Count]);
                }
                foreach(var tuple in values)
                {
                    matrix[GetIndex(tuple.Item1)][GetIndex(tuple.Item2)] = tuple.Item3;
                }
            }
            internal int GetIndex(T value)
            {
                return header.FindIndex(a => a.Equals(value)==true);
            }
            internal void AddNode(T newNode, List<Tuple<T, T, int>> adjacencies)
            {
                //Tuple 1st value = start, 2nd value = end, 3rd value = weight
                header.Add(newNode);
                header.Sort();
                Count = header.Count();
                var NodeIndex = GetIndex(newNode);
                matrix.Insert(NodeIndex, new List<int> (new int[header.Count]));
                foreach(var list in matrix)
                {
                    list.Insert(NodeIndex, 0);
                }
                foreach(var link in adjacencies)
                {
                    if(link.Item1.Equals(newNode) == false && link.Item1.Equals(newNode) == false) throw new InvalidOperationException();
                    else
                    {
                        try
                        {
                            matrix[GetIndex(link.Item1)][GetIndex(link.Item2)] = link.Item3;
                        }
                        catch {}
                    }
                }
            }
            internal void RemoveNode(T toRemove)
            {
                var toRemoveIndex = GetIndex(toRemove);
                matrix.RemoveAt(toRemoveIndex);
                foreach(var list in matrix)
                {
                    list.RemoveAt(toRemoveIndex);
                }
                header.RemoveAt(toRemoveIndex);
                Count = header.Count();
            }
            internal List<int> GetVertices(T toFind)
            {
                var adjacencies = matrix[GetIndex(toFind)];
                var output = new List<int>();
                for(int i = 0; i < adjacencies.Count(); i ++)
                {
                    if(adjacencies[i] > 0) output.Add(i);
                }
                return output;
            }
            internal List<int> GetWeights(T toFind)
            {
                var adjacencies = matrix[GetIndex(toFind)];
                var output = new List<int>();
                for(int i = 0; i < adjacencies.Count(); i ++)
                {
                    if(adjacencies[i] > 0) output.Add(adjacencies[i]);
                }
                return output;
            }
        }
        private AdjacencyMatrix GraphMatrix;
        public Graph(List<Tuple<T,T,int>> inputs)
        {
            GraphMatrix = new AdjacencyMatrix(inputs);
        }
        
        
        public List<T> BreadthFirst()
        {
            var output = new List<T>();
            var visited = new List<bool> (new bool[GraphMatrix.Count]);
            var workQueue = new Queue<T>();
            workQueue.Enqueue(GraphMatrix.header[0]);
            while(workQueue.Count != 0)
            {
                var toVisit = workQueue.Dequeue();
                var neighbours = GraphMatrix.GetVertices(toVisit);
                foreach(var neighbour in neighbours)
                {
                    if(visited[neighbour] == false)
                    {
                        workQueue.Enqueue(GraphMatrix.header[neighbour]);
                        visited[neighbour] = true;
                        output.Add(GraphMatrix.header[neighbour]);
                    }
                }
            }
            return output;
        }
        public void DepthFirstHelper(T value, List<bool> visited, List<T> output)
        {
            visited[GraphMatrix.GetIndex(value)] = true;
            var neighbours = GraphMatrix.GetVertices(value);
            foreach(var neighbour in neighbours)
            {
                if(visited[neighbour] == false)
                {
                    DepthFirstHelper(GraphMatrix.header[neighbour], visited, output);
                }
            }
        }
        public List<T> DepthFirst(T value)
        {
            var output = new List<T>();
            var visited = new List<bool> (new bool[GraphMatrix.Count]);
            DepthFirstHelper(value, visited, output);
            return output;
        }
        public List<T> Dijkstra(T startNode)
        {
            var distances = new List<int> (new int[GraphMatrix.Count]);
            var previous = new List<T> (new T[GraphMatrix.Count]);
            for(int i = 0; i < GraphMatrix.Count; i ++)
            {
                distances[i] = int.MaxValue;
                previous[i] = default(T);
            }
            distances[GraphMatrix.GetIndex(startNode)] = 0;
            var nodes = GraphMatrix.header;
            while(nodes.Count() != 0)
            {
                var toVisit = nodes[distances.FindIndex(x=>x==distances.Min())];
                nodes.Remove(toVisit);
                var nodeDistances = GraphMatrix.GetWeights(toVisit);
                var neighbours = GraphMatrix.GetVertices(toVisit);
                for(int i = 0; i < GraphMatrix.Count; i ++)
                {
                    var alt = distances[GraphMatrix.GetIndex(toVisit)] + nodeDistances[i];
                    if(alt < distances[neighbours[i]])
                    {
                        distances[neighbours[i]] = alt;
                        previous[neighbours[i]] = toVisit;
                    }
                }
            }
            return previous;
        }
        public void AddElement(T value, List<Tuple<T,T,int>> adjacencies)
        {
            GraphMatrix.AddNode(value, adjacencies);
        }
        public void RemoveElement(T value)
        {
            GraphMatrix.RemoveNode(value);
        }

    }
}
