using khiiMapv.Models.Coct;
using SlimDX;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace khiiMapv.Utils {
    class CoctComputer {
        public List<Vector3> hitPointList = new List<Vector3>();
        public List<Co2> co2List = new List<Co2>();
        public List<Co7> co7List = new List<Co7>();

        public CoctComputer(CollisionReader coct, Ray ray) {
            var rest = new Stack<int>();
            rest.Push(0);
            while (rest.Any()) {
                var index = rest.Pop();

                var ent1 = coct.alCo1[index];

                for (var x = ent1.Co2First; x < ent1.Co2Last; x++) {
                    var ent2 = coct.alCo2[x];
                    for (var y = ent2.Co3frm; y < ent2.Co3to; y++) {
                        var ent3 = coct.alCo3[y];
                        var plane = coct.alCo5[ent3.PlaneCo5];
                        if (Ray.Intersects(ray, plane, out float distance)) {
                            var at = ray.Position + ray.Direction * distance;

                            var bbox = ent2.BBox;
                            if (ent3.Co6 != -1) {
                                bbox = coct.alCo6[ent3.Co6].BBox;
                            }

                            if (BoundingBox.Contains(bbox, at) != ContainmentType.Disjoint) {
                                hitPointList.Add(at);

                                co2List.Add(ent2);
                                co7List.Add(coct.alCo7[ent3.Co7]);
                            }
                        }
                    }
                }

                foreach (var subIndex in ent1.GetChildren().Where(subIndex => subIndex != -1)) {
                    rest.Push(subIndex);
                }
            }
        }
    }
}
