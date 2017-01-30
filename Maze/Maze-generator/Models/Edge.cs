using System;
using System.Collections.Generic;
using System.Drawing;

namespace Maze_generator.Models
{
    /// <summary>
    ///     A wall between two cells
    /// </summary>
    public class Edge : IEquatable<Edge>
    {
        public int Cell1 { get; }
        public int Cell2 { get; }

        public Edge(int cell1, int cell2)
        {
            Cell1 = cell1;
            Cell2 = cell2;
        }

        public static List<Edge> Generate(Size size)
        {
            var edges = new List<Edge>();

            var cell = 0;
            for (var row = 0; row < size.Height; row++)
            {
                for (var column = 0; column < size.Width; column++)
                {
                    // Create edge on the right
                    if (column != size.Width - 1)
                    {
                        edges.Add(new Edge(cell, cell + 1));
                    }

                    // Create edge on the bottom
                    if (row != size.Height - 1)
                    {
                        edges.Add(new Edge(cell, cell + size.Width));
                    }

                    cell++;
                }
            }

            return edges;
        }

        #region Equality comparison

        public bool Equals(Edge other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Cell1 == other.Cell1 && Cell2 == other.Cell2;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj.GetType() == GetType() && Equals((Edge) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (Cell1*397) ^ Cell2;
            }
        }

        public static bool operator ==(Edge left, Edge right) => Equals(left, right);
        public static bool operator !=(Edge left, Edge right) => !Equals(left, right);

        #endregion
    }
}