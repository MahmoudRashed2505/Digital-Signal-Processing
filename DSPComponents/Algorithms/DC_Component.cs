using DSPAlgorithms.DataStructures;
using System;
using System.Linq;

namespace DSPAlgorithms.Algorithms
{
    public class DC_Component: Algorithm
    {
        public Signal InputSignal { get; set; }
        public Signal OutputSignal { get; set; }

        public override void Run()
        {
            var mean = InputSignal.Samples.Sum()/InputSignal.Samples.Count;
            OutputSignal = new Signal(InputSignal.Samples, false);

            for (var sampleIndex = 0; sampleIndex < OutputSignal.Samples.Count; sampleIndex++)
                OutputSignal.Samples[sampleIndex] = OutputSignal.Samples[sampleIndex] - mean;
        }
    }
}
