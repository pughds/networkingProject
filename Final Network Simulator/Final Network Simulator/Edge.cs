using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Final_Network_Simulator
{
    class Edge
    {
        public Vertex from { get; set; }
        public Vertex to { get; set; }
        public int weight { get; set; }

        //Sets Edge field values
        public Edge(Vertex fromInput, Vertex toInput, int weightInput)
        {
            this.from = fromInput;
            this.to = toInput;
            this.weight = weightInput;
        }

        //Returns an integer representing the cost of this edge
        public int getWeight()
        {
            return weight;
        }


    }
}
