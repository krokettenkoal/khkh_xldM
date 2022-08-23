using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace khiiMapv.Models.HexView {
    public class MicroLabels {
        public IDictionary<string, int> labelToOffset = new Dictionary<string, int>();

        public void AddLabel(string label, int off) {
            labelToOffset[label] = off;
        }

        public MicroLabels CopyAndShiftAll(int delta) {
            return new MicroLabels {
                labelToOffset = labelToOffset.ToDictionary(pair => pair.Key, pair => pair.Value + delta),
            };
        }
    }
}
