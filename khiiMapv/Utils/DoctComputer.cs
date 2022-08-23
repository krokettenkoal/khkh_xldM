using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SlimDX;

namespace khiiMapv.Utils {
    class DoctComputer {
        public HashSet<int> containedTable2Idx = new HashSet<int>();
        public HashSet<int> hitTable1Index = new HashSet<int>();

        public DoctComputer(DoctReader doct, Plane plane) {
            var rest = new Stack<int>();
            rest.Push(0);
            while (rest.Any()) {
                var index = rest.Pop();

                var target = doct.list1[index];

                var pointList = new Vector3[] {
                    new Vector3(target.min.X, target.min.Y, target.min.Z),
                    new Vector3(target.max.X, target.min.Y, target.min.Z),
                    new Vector3(target.min.X, target.max.Y, target.min.Z),
                    new Vector3(target.max.X, target.max.Y, target.min.Z),
                    new Vector3(target.min.X, target.min.Y, target.max.Z),
                    new Vector3(target.max.X, target.min.Y, target.max.Z),
                    new Vector3(target.min.X, target.max.Y, target.max.Z),
                    new Vector3(target.max.X, target.max.Y, target.max.Z),
                };

                if (pointList.Any(point => 0 <= Plane.DotCoordinate(plane, point))) {
                    hitTable1Index.Add(index);
                    var table2Idx = target.link1;
                    var table2LastIdx = target.link2;
                    for (; table2Idx < table2LastIdx; table2Idx++) {
                        containedTable2Idx.Add(table2Idx);
                    }

                    foreach (var subIndex in target.goTo.Where(subIndex => subIndex != -1)) {
                        rest.Push(subIndex);
                    }
                }
            }
        }
    }
}
