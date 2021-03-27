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
            public AdjacencyMatrix(List<Tuple<T,T,T>> values)
            {
                //1st value - First item | 2nd value - Second item | 3rd item - weight
                foreach(var tuple in values) header.Add(tuple[0]);
                header = header.Distinct();
                foreach(var tuple in values)
                {
                    matrix[GetIndex(tuple[0])][GetIndex(tuple[1])] = tuple[2];
                }
            }
            private void GetIndex(T value)
            {
                return header.FindIndex(a => a == value);
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
        public List<int> DepthFirst()
        {
            List<bool> visited = new List<bool> (new bool[header.Count()]);
            


        }

    }
}
