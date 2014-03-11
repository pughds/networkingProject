/*
 I have abided by the UNCG Academic Integrity Policy on this assignment.
 * Darrick S. Pugh
 */

using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Security.Cryptography;
using System.Threading;

namespace Final_Network_Simulator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public int costR1R2, costR2R3, costR3R4, costR1R7, costR2R5, costR5R7, costR5R6, costR7R8, costR3R6, costR6R8, costR4R8, finishedCount = 0, count = 0;
        Random rnd = new Random();
        Vertex R1 = new Vertex("R1", true);
        Vertex R2 = new Vertex("R2", false);
        Vertex R3 = new Vertex("R3", false);
        Vertex R4 = new Vertex("R4", false);
        Vertex R5 = new Vertex("R5", false);
        Vertex R6 = new Vertex("R6", false);
        Vertex R7 = new Vertex("R7", false);
        Vertex R8 = new Vertex("R8", false);
        List<Vertex> vertList = new List<Vertex>();
        Storyboard animationRed1234, animationRed1784, animationRed25684, animationRed25634, animationRed75684, animationRed75634, animationRed75234, animationRed25784, animationRed23684, animationRed78634;
        Storyboard animationBlue1234, animationBlue1784, animationBlue25684, animationBlue25634, animationBlue75684, animationBlue75634, animationBlue75234, animationBlue25784, animationBlue23684, animationBlue78634;

        //Copied Fields
        int message1LengthRed = 0;
        int message1LengthBlue = 0;
        string messageRed;
        string messageBlue;
        string binaryRepresentation1Red;
        string binaryRepresentation2Red;
        string binaryRepresentation1Blue;
        string binaryRepresentation2Blue;
        string crcStringRed = "";
        string crcStringBlue = "";
        string crcValueRed;
        string crcValueBlue;
        int startX1Red = 0;
        int startX1Blue = 0;
        string h1Address = "6C5D3FBF44BB";
        string r1Address = "648D254B71A4";
        string r2Address = "F8B755ACA8B0";
        CRCCalc crc32Red;
        CRCCalc crc32Blue;

        public MainWindow()
        {
            InitializeComponent();
            animationRed1234 = this.Resources["animationRed1234"] as Storyboard;
            animationRed1784 = this.Resources["animationRed1784"] as Storyboard;
            animationRed25684 = this.Resources["animationRed25684"] as Storyboard;
            animationRed75634 = this.Resources["animationRed75634"] as Storyboard;
            animationRed75684 = this.Resources["animationRed75684"] as Storyboard;
            animationRed75234 = this.Resources["animationRed75234"] as Storyboard;
            animationRed25784 = this.Resources["animationRed25784"] as Storyboard;
            animationRed23684 = this.Resources["animationRed23684"] as Storyboard;
            animationRed78634 = this.Resources["animationRed78634"] as Storyboard;
            animationRed25634 = this.Resources["animationRed25634"] as Storyboard;
            animationBlue1234 = this.Resources["animationBlue1234"] as Storyboard;
            animationBlue1784 = this.Resources["animationBlue1784"] as Storyboard;
            animationBlue25684 = this.Resources["animationBlue25684"] as Storyboard;
            animationBlue75634 = this.Resources["animationBlue75634"] as Storyboard;
            animationBlue75684 = this.Resources["animationBlue75684"] as Storyboard;
            animationBlue75234 = this.Resources["animationBlue75234"] as Storyboard;
            animationBlue25784 = this.Resources["animationBlue25784"] as Storyboard;
            animationBlue23684 = this.Resources["animationBlue23684"] as Storyboard;
            animationBlue78634 = this.Resources["animationBlue78634"] as Storyboard;
            animationBlue25634 = this.Resources["animationBlue25634"] as Storyboard;

        }

        //This is the method called by the Start button
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            testMessages();
        }


        //Resets the graph for the next round
        private void initializeGraphRed()
        {
            R1.resetSource();
            R2.reset();
            R3.reset();
            R4.reset();
            R5.reset();
            R6.reset();
            R7.reset();
            R8.reset();
            vertList.Clear();

            costR1R2 = rnd.Next(1, 7);
            weightR1R2Red.Text = costR1R2.ToString();
            costR2R3 = rnd.Next(1, 7);
            weightR2R3Red.Text = costR2R3.ToString();
            costR3R4 = rnd.Next(1, 7);
            weightR3R4Red.Text = costR3R4.ToString();
            costR1R7 = rnd.Next(1, 7);
            weightR1R7Red.Text = costR1R7.ToString();
            costR2R5 = rnd.Next(1, 7);
            weightR2R5Red.Text = costR2R5.ToString();
            costR5R7 = rnd.Next(1, 7);
            weightR5R7Red.Text = costR5R7.ToString();
            costR5R6 = rnd.Next(1, 7);
            weightR5R6Red.Text = costR5R6.ToString();
            costR7R8 = rnd.Next(1, 7);
            weightR7R8Red.Text = costR7R8.ToString();
            costR3R6 = rnd.Next(1, 7);
            weightR3R6Red.Text = costR3R6.ToString();
            costR6R8 = rnd.Next(1, 7);
            weightR6R8Red.Text = costR6R8.ToString();
            costR4R8 = rnd.Next(1, 7);
            weightR4R8Red.Text = costR4R8.ToString();
            R1.addEdge(R1, R2, costR1R2);
            R1.addEdge(R1, R7, costR1R7);
            R2.addEdge(R2, R1, costR1R2);
            R2.addEdge(R2, R5, costR2R5);
            R2.addEdge(R2, R3, costR2R3);
            R3.addEdge(R3, R2, costR2R3);
            R3.addEdge(R3, R6, costR3R6);
            R3.addEdge(R3, R4, costR3R4);
            R4.addEdge(R4, R3, costR3R4);
            R4.addEdge(R4, R8, costR4R8);
            R5.addEdge(R5, R2, costR2R5);
            R5.addEdge(R5, R7, costR5R7);
            R5.addEdge(R5, R6, costR5R6);
            R6.addEdge(R6, R5, costR5R6);
            R6.addEdge(R6, R3, costR3R6);
            R6.addEdge(R6, R8, costR6R8);
            R7.addEdge(R7, R1, costR1R7);
            R7.addEdge(R7, R5, costR5R7);
            R7.addEdge(R7, R8, costR7R8);
            R8.addEdge(R8, R4, costR4R8);
            R8.addEdge(R8, R6, costR6R8);
            R8.addEdge(R8, R7, costR7R8);
            vertList.Add(R1);
            vertList.Sort();
        }

        //Resets the graph for the next round
        private void initializeGraphBlue()
        {
            R1.resetSource();
            R2.reset();
            R3.reset();
            R4.reset();
            R5.reset();
            R6.reset();
            R7.reset();
            R8.reset();
            vertList.Clear();

            costR1R2 = rnd.Next(1, 7);
            weightR1R2Blue.Text = costR1R2.ToString();
            costR2R3 = rnd.Next(1, 7);
            weightR2R3Blue.Text = costR2R3.ToString();
            costR3R4 = rnd.Next(1, 7);
            weightR3R4Blue.Text = costR3R4.ToString();
            costR1R7 = rnd.Next(1, 7);
            weightR1R7Blue.Text = costR1R7.ToString();
            costR2R5 = rnd.Next(1, 7);
            weightR2R5Blue.Text = costR2R5.ToString();
            costR5R7 = rnd.Next(1, 7);
            weightR5R7Blue.Text = costR5R7.ToString();
            costR5R6 = rnd.Next(1, 7);
            weightR5R6Blue.Text = costR5R6.ToString();
            costR7R8 = rnd.Next(1, 7);
            weightR7R8Blue.Text = costR7R8.ToString();
            costR3R6 = rnd.Next(1, 7);
            weightR3R6Blue.Text = costR3R6.ToString();
            costR6R8 = rnd.Next(1, 7);
            weightR6R8Blue.Text = costR6R8.ToString();
            costR4R8 = rnd.Next(1, 7);
            weightR4R8Blue.Text = costR4R8.ToString();
            R1.addEdge(R1, R2, costR1R2);
            R1.addEdge(R1, R7, costR1R7);
            R2.addEdge(R2, R1, costR1R2);
            R2.addEdge(R2, R5, costR2R5);
            R2.addEdge(R2, R3, costR2R3);
            R3.addEdge(R3, R2, costR2R3);
            R3.addEdge(R3, R6, costR3R6);
            R3.addEdge(R3, R4, costR3R4);
            R4.addEdge(R4, R3, costR3R4);
            R4.addEdge(R4, R8, costR4R8);
            R5.addEdge(R5, R2, costR2R5);
            R5.addEdge(R5, R7, costR5R7);
            R5.addEdge(R5, R6, costR5R6);
            R6.addEdge(R6, R5, costR5R6);
            R6.addEdge(R6, R3, costR3R6);
            R6.addEdge(R6, R8, costR6R8);
            R7.addEdge(R7, R1, costR1R7);
            R7.addEdge(R7, R5, costR5R7);
            R7.addEdge(R7, R8, costR7R8);
            R8.addEdge(R8, R4, costR4R8);
            R8.addEdge(R8, R6, costR6R8);
            R8.addEdge(R8, R7, costR7R8);
            vertList.Add(R1);
            vertList.Sort();
        }

        //Perfroms a Dijkstra's shortest path alrogithmic analysis on the vertex and edge data
        private void djikstra()
        {
            while (vertList.Any())
            {
                Vertex a = vertList[0];
                vertList.Remove(a);
                vertList.Sort();
                a.setFinished();
                foreach (Edge e in a.getEdges())
                {
                    Vertex b = e.to;
                    if (a.MinDistance + e.weight < b.MinDistance)
                    {
                        b.MinDistance = (a.MinDistance + e.weight);
                        b.previousVertex = a;
                        vertList.Add(b);
                        vertList.Sort();
                    }
                }
            }
        }

        /*Compares the times of the two messages to determine who arrived at their destination
         * in the least amount of time.
         */
        private void decideWinner()
        {
            if (Convert.ToInt32(txtDistanceBlue.Text) < Convert.ToInt32(txtDistanceRed.Text))
            {
                txtWinner.Text = "BLUE WINS!";
            }
            else if (Convert.ToInt32(txtDistanceBlue.Text) > Convert.ToInt32(txtDistanceRed.Text))
            {
                txtWinner.Text = "RED WINS!";
            }
            else
            {
                txtWinner.Text = "IT'S A TIE!";
            }
        }

        /*Converts the red message to display it's information on the screen
         */
        private void processRedMessage()
        {
            //Red Message processing
            txtShortestPathRed.Text = "";
            initializeGraphRed();
            djikstra();

            //Builds a string indicating the path taken by the red message and displays it.
            Vertex v = R4;
            String pathRed = R4.vertID;
            while (v.previousVertex != R1)
            {
                pathRed = (v.previousVertex.vertID + "->") + pathRed;
                v = v.previousVertex;
            }
            pathRed = (R1.vertID + "->") + pathRed;
            txtShortestPathRed.Text = pathRed;
            txtDistanceRed.Text = R4.minDistance.ToString();

            //Begins message conversion
            pathCollectionRed.Clear();
            startX1Red = 0;
            messageRed = "";
            crcStringRed = Convert.ToString(Convert.ToInt64(h1Address, 16), 2) + Convert.ToString(Convert.ToInt64(r1Address, 16), 2) + Convert.ToString(Convert.ToInt64("0802", 16), 2) + SupportClasses.binPrint(messageRed);
            Console.WriteLine(crcStringRed); binaryRepresentation1Red = "";
            binaryRepresentation2Red = "";
            messageRed = txtInput1.Text;
            message1LengthRed = messageRed.Length;
            binaryRepresentation1Red += SupportClasses.buildFrame(h1Address, r1Address) + SupportClasses.binPrint(messageRed);
            binaryRepresentation2Red += SupportClasses.buildFrame(r1Address, r2Address) + SupportClasses.binPrint(messageRed);
            byte[] b1Red = System.Text.Encoding.ASCII.GetBytes(crcStringRed);
            byte[] b2Red = System.Text.Encoding.ASCII.GetBytes(crcStringRed);
            binaryRepresentation1Red += Convert.ToString(Convert.ToInt64(calcCRC(b1Red), 10), 2);
            binaryRepresentation2Red += Convert.ToString(Convert.ToInt64(calcCRC(b2Red), 10), 2);
            crc32Red = new CRCCalc();
            crc32Red.ComputeHash(b2Red);
            crcValueRed = crc32Red.CrcValue.ToString("X");
            buildSinePathRed();
            frameTextBlockRed.Text = "Red Ethernet Frame Contents:\nPreamble: AAAAAAAAAAAAAA\nStart Frame Delimeter: AB\n" +
                    "Destination: " + h1Address + "\nSource: " + r1Address + "\nType: 0802\n" + "Data: " + messageRed + "\nCRC: "
                    + crcValueRed;
            
            //Determines which animation to play for the red message
            switch (txtShortestPathRed.Text)
            {
                case "R1->R2->R3->R4":
                    animationRed1234.Begin();
                    break;
                case "R1->R7->R8->R4":
                    animationRed1784.Begin();
                    break;
                case "R1->R2->R5->R6->R8->R4":
                    animationRed25684.Begin();
                    break;
                case "R1->R7->R5->R6->R3->R4":
                    animationRed75634.Begin();
                    break;
                case "R1->R7->R5->R6->R8->R4":
                    animationRed75684.Begin();
                    break;
                case "R1->R7->R5->R2->R3->R4":
                    animationRed75234.Begin();
                    break;
                case "R1->R2->R5->R7->R8->R4":
                    animationRed25784.Begin();
                    break;
                case "R1->R2->R3->R6->R8->R4":
                    animationRed23684.Begin();
                    break;
                case "R1->R7->R8->R6->R3->R4":
                    animationRed78634.Begin();
                    break;
                case "R1->R2->R5->R6->R3->R4":
                    animationRed25634.Begin();
                    break;
                default:
                    break;
            }
        }

        /*Converts the blue message to display it's information on the screen
         */
        private void processBlueMessage()
        {
            //Blue Message Processing
            txtShortestPathBlue.Clear();
            initializeGraphBlue();
            djikstra();

            //Builds a string indicating the path taken by the blue message and displays it.
            Vertex w = R4;
            String pathBlue = R4.vertID;
            while (w.previousVertex != R1)
            {
                pathBlue = (w.previousVertex.vertID + "->") + pathBlue;
                w = w.previousVertex;
            }
            pathBlue = (R1.vertID + "->") + pathBlue;
            txtShortestPathBlue.Text = pathBlue;
            txtDistanceBlue.Text = R4.minDistance.ToString();

            //Begins message conversion
            pathCollectionBlue.Clear();
            startX1Blue = 0;
            messageBlue = "";
            crcStringBlue = Convert.ToString(Convert.ToInt64(h1Address, 16), 2) + Convert.ToString(Convert.ToInt64(r1Address, 16), 2) + Convert.ToString(Convert.ToInt64("0802", 16), 2) + SupportClasses.binPrint(messageBlue);
            Console.WriteLine(crcStringBlue); binaryRepresentation1Blue = "";
            binaryRepresentation2Blue = "";
            messageBlue = txtInput2.Text;
            message1LengthBlue = messageBlue.Length;
            binaryRepresentation1Blue += SupportClasses.buildFrame(h1Address, r1Address) + SupportClasses.binPrint(messageBlue);
            binaryRepresentation2Blue += SupportClasses.buildFrame(r1Address, r2Address) + SupportClasses.binPrint(messageBlue);
            byte[] b1Blue = System.Text.Encoding.ASCII.GetBytes(crcStringBlue);
            byte[] b2Blue = System.Text.Encoding.ASCII.GetBytes(crcStringBlue);
            binaryRepresentation1Blue += Convert.ToString(Convert.ToInt64(calcCRC(b1Blue), 10), 2);
            binaryRepresentation2Blue += Convert.ToString(Convert.ToInt64(calcCRC(b2Blue), 10), 2);
            crc32Blue = new CRCCalc();
            crc32Blue.ComputeHash(b2Blue);
            crcValueBlue = crc32Blue.CrcValue.ToString("X");
            buildSinePathBlue();
            frameTextBlockBlue.Text = " Blue Ethernet Frame Contents:\nPreamble: AAAAAAAAAAAAAA\nStart Frame Delimeter: AB\n" +
                    "Destination: " + h1Address + "\nSource: " + r1Address + "\nType: 0802\n" + "Data: " + messageBlue + "\nCRC: "
                    + crcValueBlue;

            //Determines which animation to play for the red message
            switch (txtShortestPathBlue.Text)
            {
                case "R1->R2->R3->R4":
                    animationBlue1234.Begin();
                    break;
                case "R1->R7->R8->R4":
                    animationBlue1784.Begin();
                    break;
                case "R1->R2->R5->R6->R8->R4":
                    animationBlue25684.Begin();
                    break;
                case "R1->R7->R5->R6->R3->R4":
                    animationBlue75634.Begin();
                    break;
                case "R1->R7->R5->R6->R8->R4":
                    animationBlue75684.Begin();
                    break;
                case "R1->R7->R5->R2->R3->R4":
                    animationBlue75234.Begin();
                    break;
                case "R1->R2->R5->R7->R8->R4":
                    animationBlue25784.Begin();
                    break;
                case "R1->R2->R3->R6->R8->R4":
                    animationBlue23684.Begin();
                    break;
                case "R1->R7->R8->R6->R3->R4":
                    animationBlue78634.Begin();
                    break;
                case "R1->R2->R5->R6->R3->R4":
                    animationBlue25634.Begin();
                    break;
                default:
                    break;
            }
        }

        //Makes sure that the user entered a message in each message input box, and then begins conversion of the messages.
        private void testMessages()
        {
            if (txtInput1.Text == "" || txtInput2.Text == "")
            {
                MessageBox.Show("You must enter an ASCII string into the input box before clicking the Start button.");
            }
            else
            {
                processRedMessage();
                processBlueMessage();
                decideWinner();
            }
        }

        private String calcCRC(byte[] input)
        {
            CRCCalc calc = new CRCCalc();
            calc.ComputeHash(input);
            uint crc = calc.CrcValue;
            return crc.ToString();
        }

        //Creates a FSK sine wave representation of the red message
        public void buildSinePathRed()
        {
            foreach (char ch in binaryRepresentation1Red)
            {
                int value = int.Parse(ch.ToString());
                if (value == 0)     //This builds a non shifted sine wave segment representing a zero in the binary message1
                {
                    BezierSegment segment1Red = new BezierSegment();
                    segment1Red.Point1 = new Point(startX1Red + 10, 0);
                    segment1Red.Point2 = new Point(startX1Red + 20, 100);
                    segment1Red.Point3 = new Point(startX1Red + 30, 50);
                    pathCollectionRed.Add(segment1Red);
                    BezierSegment segment2Red = new BezierSegment();
                    segment2Red.Point1 = new Point(startX1Red + 40, 0);
                    segment2Red.Point2 = new Point(startX1Red + 50, 100);
                    segment2Red.Point3 = new Point(startX1Red + 60, 50);
                    pathCollectionRed.Add(segment2Red);
                    startX1Red += 60;       //moves point of attachment for the next segment to the appropriate space in the Canvas.
                }
                else
                {      //This builds a shifted sine wave segment representing a one in the binary message1.
                    BezierSegment segment1Red = new BezierSegment();
                    segment1Red.Point1 = new Point(startX1Red + 5, 0);
                    segment1Red.Point2 = new Point(startX1Red + 10, 100);
                    segment1Red.Point3 = new Point(startX1Red + 15, 50);
                    pathCollectionRed.Add(segment1Red);
                    BezierSegment segment2Red = new BezierSegment();
                    segment2Red.Point1 = new Point(startX1Red + 20, 0);
                    segment2Red.Point2 = new Point(startX1Red + 25, 100);
                    segment2Red.Point3 = new Point(startX1Red + 30, 50);
                    pathCollectionRed.Add(segment2Red);
                    BezierSegment segment3Red = new BezierSegment();
                    segment3Red.Point1 = new Point(startX1Red + 35, 0);
                    segment3Red.Point2 = new Point(startX1Red + 40, 100);
                    segment3Red.Point3 = new Point(startX1Red + 45, 50);
                    pathCollectionRed.Add(segment3Red);
                    BezierSegment segment4Red = new BezierSegment();
                    segment4Red.Point1 = new Point(startX1Red + 50, 0);
                    segment4Red.Point2 = new Point(startX1Red + 55, 100);
                    segment4Red.Point3 = new Point(startX1Red + 60, 50);
                    pathCollectionRed.Add(segment4Red);
                    startX1Red += 60;       //moves point of attachment for the next segment to the appropriate space in the Canvas.
                }
            }
            sineCanvasRed.Width = startX1Red;
        }

        //Creates a FSK sine wave representation of the blue message
        public void buildSinePathBlue()
        {
            foreach (char ch in binaryRepresentation1Blue)
            {
                int value = int.Parse(ch.ToString());
                if (value == 0)     //This builds a non shifted sine wave segment representing a zero in the binary message1
                {
                    BezierSegment segment1Blue = new BezierSegment();
                    segment1Blue.Point1 = new Point(startX1Blue + 10, 0);
                    segment1Blue.Point2 = new Point(startX1Blue + 20, 100);
                    segment1Blue.Point3 = new Point(startX1Blue + 30, 50);
                    pathCollectionBlue.Add(segment1Blue);
                    BezierSegment segment2Blue = new BezierSegment();
                    segment2Blue.Point1 = new Point(startX1Blue + 40, 0);
                    segment2Blue.Point2 = new Point(startX1Blue + 50, 100);
                    segment2Blue.Point3 = new Point(startX1Blue + 60, 50);
                    pathCollectionBlue.Add(segment2Blue);
                    startX1Blue += 60;       //moves point of attachment for the next segment to the appropriate space in the Canvas.
                }
                else
                {      //This builds a shifted sine wave segment representing a one in the binary message1.
                    BezierSegment segment1Blue = new BezierSegment();
                    segment1Blue.Point1 = new Point(startX1Blue + 5, 0);
                    segment1Blue.Point2 = new Point(startX1Blue + 10, 100);
                    segment1Blue.Point3 = new Point(startX1Blue + 15, 50);
                    pathCollectionBlue.Add(segment1Blue);
                    BezierSegment segment2Blue = new BezierSegment();
                    segment2Blue.Point1 = new Point(startX1Blue + 20, 0);
                    segment2Blue.Point2 = new Point(startX1Blue + 25, 100);
                    segment2Blue.Point3 = new Point(startX1Blue + 30, 50);
                    pathCollectionBlue.Add(segment2Blue);
                    BezierSegment segment3Blue = new BezierSegment();
                    segment3Blue.Point1 = new Point(startX1Blue + 35, 0);
                    segment3Blue.Point2 = new Point(startX1Blue + 40, 100);
                    segment3Blue.Point3 = new Point(startX1Blue + 45, 50);
                    pathCollectionBlue.Add(segment3Blue);
                    BezierSegment segment4Blue = new BezierSegment();
                    segment4Blue.Point1 = new Point(startX1Blue + 50, 0);
                    segment4Blue.Point2 = new Point(startX1Blue + 55, 100);
                    segment4Blue.Point3 = new Point(startX1Blue + 60, 50);
                    pathCollectionBlue.Add(segment4Blue);
                    startX1Blue += 60;       //moves point of attachment for the next segment to the appropriate space in the Canvas.
                }
            }
            sineCanvasBlue.Width = startX1Blue;
        }





    }
}