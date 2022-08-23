using khiiMapv.Q3Radiant;
using NUnit.Framework;
using SlimDX;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SomeTests {
    public class WindingTest {
        [Test]
        public void Test() {
            var up = new Plane(0, 0, 1, 10);
            var down = new Plane(0, 0, -1, 100);
            var north = new Plane(0, 1, 0, 100);
            var south = new Plane(0, -1, 0, 100);
            var east = new Plane(1, 0, 0, 100);
            var west = new Plane(-1, 0, 0, 100);

            var vertices = WindingUtil.MakeFaceWinding(
                new Plane[] { up, down, north, east, west, south },
                up
            );

            Assert.NotNull(vertices);
            Assert.AreEqual(4, vertices.Count);
            Assert.AreEqual(new Vector3(-100, -100, 10), vertices[0]);
            Assert.AreEqual(new Vector3(-100, 100, 10), vertices[1]);
            Assert.AreEqual(new Vector3(100, 100, 10), vertices[2]);
            Assert.AreEqual(new Vector3(100, -100, 10), vertices[3]);
        }
    }
}
