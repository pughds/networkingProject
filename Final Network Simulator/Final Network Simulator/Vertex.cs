using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Final_Network_Simulator
{
    class Vertex : IComparable<Vertex>
    {
        public bool finished;
        public string vertID { get; set; }
        public int minDistance { get; set; }
        public List<Edge> edges = new List<Edge>();
        public Vertex previousVertex { get; set; }

        //Sets the source vertex's minDistance to 0 and all other vertices to a very high value
        public Vertex(string routerID, bool start)
        {
            if (start)
            {
                minDistance = 0;
                previousVertex = null;
            }
            else
            {
                minDistance = Int32.MaxValue;
            }
            finished = false;
            vertID = routerID;
            previousVertex = null;
        }

        //Returns a string indicating which router this vertex represents
        public string getName()
        {
            return vertID;
        }

        //Adds an edge to this vertex's List of edges.
        public void addEdge(Vertex from, Vertex to, int weight)
        {
            edges.Add(new Edge(from, to, weight));
        }

        public void setMinDistance(int value)
        {
            minDistance = value;
        }

        public int getMinDistance()
        {
            return minDistance;
        }

        //This method is used in order to make the vertex List created in the MainWindow part of the program to be able to sort
        //on which vertex has the least minimum distance at the time.
        public int CompareTo(Vertex obj)
        {
            return getMinDistance().CompareTo(obj.getMinDistance());
        }

        //Returns the List containing all the edges originating from this vertex.
        public List<Edge> getEdges()
        {
            return edges;
        }

        //Sets the vertex immediately before this vertex, which one would come from in a least cost path.
        public void setPreviousVertex(Vertex v)
        {
            previousVertex = v;
        }

        //Returns the vertex immediately before this vertex, which one would come from in a least cost path.
        public Vertex getShortestPath()
        {
            return previousVertex;
        }

        //Not used presently
        public void setFinished()
        {
            finished = true;
        }

        //Not used presently
        public int MinDistance
        {
            get { return minDistance; }
            set { minDistance = value; }
        }

        //Returns all non-source vertices to the initial field values
        public void reset()
        {
            minDistance = Int32.MaxValue;
            previousVertex = null;
            edges.Clear();
        }

        //Returns the source vertex to it's initial conditions, which means only clearing it's edge List.
        public void resetSource()
        {
            edges.Clear();
        }

    }
}
