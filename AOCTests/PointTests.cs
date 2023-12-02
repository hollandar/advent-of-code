using AOCLib.Primitives;

namespace AOCTests
{
    [TestClass]
    public class PointTests
    {
        [TestMethod]
        public void Equality()
        {
            Point a = new Point(1, 10);
            Point b = new Point(1, 10);
            Point c = new Point(10, 1);

            Assert.IsTrue(a == b);
            Assert.IsFalse(a == c);
        }
    }
}