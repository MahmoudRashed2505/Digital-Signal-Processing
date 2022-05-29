using DSPAlgorithms.DataStructures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSPAlgorithms.Algorithms
{
    public class DCT: Algorithm
    {
        public Signal InputSignal { get; set; }
        public Signal OutputSignal { get; set; }

        public override void Run()
        {
            var output = new List<float>();

            var size = InputSignal.Samples.Count;

            for (var sampleIndex = 0; sampleIndex < InputSignal.Samples.Count; sampleIndex++)
            {
                var sampleAlpha = sampleIndex == 0 ? Math.Sqrt(
                        1 / (double)size) :
                    Math.Sqrt(
                        2 / (double)size);


                var sampleAccumulator = InputSignal.Samples.Select(
                    (t, x) => 
                        t * 
                        Math.Cos(
                            (2 *
                                x + 1) *
                            sampleIndex * Math.PI / (2 * size))).Sum();


                sampleAccumulator *= sampleAlpha;

                output.Add((float)sampleAccumulator);
            }
            OutputSignal = new Signal(output, false);
        }
    }
}