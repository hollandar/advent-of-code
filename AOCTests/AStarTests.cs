using AOCLib.AStar;
using AOCLib.Primitives;

namespace AOCTests;

[TestClass]
public class AStarTests
{
    [TestMethod]
    public void ShortestPath()
    {

        Node root = new Node(new Point(0, 0));
        Node end = new Node(new Point(5, 10));
        Node a = new Node(new Point(3, 8));
        Node b = new Node(new Point(2, 14));
        Node c = new Node(new Point(9, 27));

        root.ConnectEdge(a, 1);
        root.ConnectEdge(b, 2);
        a.ConnectEdge(end, 1);
        b.ConnectEdge(end, 1);
        c.ConnectEdge(end, 1);

        var path = PathFinder.FindPath(root, end);

        Console.WriteLine(path.ToPointsString());
        Assert.AreEqual(4, path.NodeCount);
        var nodes = path.GetNodes().ToList();
        Assert.AreEqual(new Point(0, 0), nodes[0].Point);
        Assert.AreEqual(new Point(3, 8), nodes[1].Point);
        Assert.AreEqual(new Point(5, 10), nodes[2].Point);
    }
    
    [TestMethod]
    public void IncompletePath()
    {

        Node root = new Node(new Point(0, 0));
        Node end = new Node(new Point(5, 10));
        Node a = new Node(new Point(3, 8));
        Node b = new Node(new Point(2, 14));
        Node c = new Node(new Point(9, 27));

        root.ConnectEdge(a, 1);
        root.ConnectEdge(b, 2);
        a.ConnectEdge(end, 1);
        b.ConnectEdge(end, 1);
        c.ConnectEdge(end, 1);

        var path = PathFinder.FindPath(root, c);

        Console.WriteLine(path.ToPointsString());
        Assert.IsTrue(path.Incomplete);
        Assert.AreEqual(0, path.EdgeCount);
        Assert.AreEqual(2, path.NodeCount);

    }
}
