using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace Maze_generator.Models
{
    public class Maze
    {
        public Edge[] Edges { get; }
        public Size Size { get; }

        public Maze(Size size)
        {
            var edgesToCheck = Edge.Generate(size);
            var sets = new DisjointSets(size.Height*size.Width);
            var mazeEdges = new List<Edge>();

            var random = new Random();

            while (sets.Count > 1 && edgesToCheck.Count > 0)
            {
                // Pick a random edge
                var edgeIndex = random.Next(edgesToCheck.Count);
                var edge = edgesToCheck[edgeIndex];

                // Find the two sets seperated by the edge
                var set1 = sets.Find(edge.Cell1);
                var set2 = sets.Find(edge.Cell2);

                if (set1 != set2) sets.Union(set1, set2);
                else mazeEdges.Add(edge);

                // Remove edge, so it won't be checked again
                edgesToCheck.RemoveAt(edgeIndex);
            }

            // Set maze properties
            Edges = edgesToCheck.Concat(mazeEdges).ToArray();
            Size = size;
        }
    }
}