using System;

namespace Graphs
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
        }
    }
    public class Graph <T>
    {
        private class AdjacencyMatrix <T>
        {
            private List<List<T>> matrix {get;}
            internal List<T> header; 
            internal int Count;
            public AdjacencyMatrix(List<Tuple<T,T,T>> values)
            {
                //1st value - First item | 2nd value - Second item | 3rd item - weight
                foreach(var tuple in values) header.Add(tuple[0]);
                header = header.Distinct();
                header.Sort();
                Count = header.Length;
                matrix = new List<List<T>>();
                for(int i = 0; i < header.Count; i ++)
                {
                    matrix[i] = new List<T> (new T[header.Count]);
                }
                foreach(var tuple in values)
                {
                    matrix[GetIndex(tuple[0])][GetIndex(tuple[1])] = tuple[2];
                }
            }
            private void GetIndex(T value)
            {
                return header.FindIndex(a => a == value);
            }
            internal void AddNode(T newNode, List<Tuple<T, T, T>> adjacencies)
            {
                //Tuple 1st value = start, 2nd value = end, 3rd value = weight
                header.Add(value);
                header.Sort();
                Count = header.Length;
                var NodeIndex = GetIndex(newNode);
                matrix.Insert(NodeIndex, new List<T> (new T[header.Count]));
                foreach(var list in matrix)
                {
                    list.Insert(NodeIndex, 0);
                }
                foreach(var link in adjacencies)
                {
                    if(link[0] != newNode && link[1] != newNode) throw new InvalidOperationException();
                    else
                    {
                        try
                        {
                            matrix[GetIndex(link[0])][GetIndex(link[1])] = link[2];
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
                Count = header.Length;
            }
        }
        private AdjacencyMatrix GraphMatrix;
        private List<T> GetVertices(T toFind)
        {
            output = matrix[GetIndex(toFind)].Select(x=>x>0).ToList();
            return output;
        }
        public Graph(List<Tuple<T,T,T> inputs)
        {
            GraphMatrix = new AdjacencyMatrix(inputs);
        }
        public List<int> BreadthFirst()
        {
            var visited = new List<bool> (new bool[header.Count()]);
            var workQueue = new Queue<T>();
            while(visited.Count(x=>x==True)!=header.Count());
        }
        public void AddElement(T value)
        {
            GraphMatrix.AddNode(value);
            
        }

    }
}
