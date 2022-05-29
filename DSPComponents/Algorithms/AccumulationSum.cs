using System;
using System.Collections.Generic;
using DSPAlgorithms.DataStructures;


namespace DSPAlgorithms.Algorithms
{
    public class AccumulationSum : Algorithm
    {
        public Signal InputSignal { get; set; }
        public Signal OutputSignal { get; set; }

        public override void Run()
        {
            OutputSignal = new Signal(new List<float>(), false);

            for (var sampleIndex = 0; sampleIndex < InputSignal.Samples.Count; sampleIndex++)
            {
                if (sampleIndex == 0)
                    OutputSignal.Samples.Add(InputSignal.Samples[sampleIndex]);
                else
                {
                    var sum = 0.0f;
                    for (var i = sampleIndex; i >= 0; i--)
                    {
                        sum+=InputSignal.Samples[i];
                    }
                    OutputSignal.Samples.Add(sum);
                }
            }
        }
    }
}
