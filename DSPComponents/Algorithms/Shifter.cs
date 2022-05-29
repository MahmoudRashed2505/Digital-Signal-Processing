using System;
using System.Collections.Generic;
using DSPAlgorithms.DataStructures;

namespace DSPAlgorithms.Algorithms
{
    public class Shifter : Algorithm
    {
        public Signal InputSignal { get; set; }
        public int ShiftingValue { get; set; }
        public Signal OutputShiftedSignal { get; set; }

        public override void Run()
        {
            
            OutputShiftedSignal = new Signal(new List<float>(), new List<int>(), InputSignal.Periodic);

            var sampleIndex = InputSignal.Samples.Count - 1;
            for (var indiceisIndex = 0; indiceisIndex < InputSignal.SamplesIndices.Count; indiceisIndex++)
            {
                OutputShiftedSignal.SamplesIndices.Add(InputSignal.SamplesIndices[indiceisIndex] + ShiftingValue);
                OutputShiftedSignal.Samples.Add(InputSignal.Samples[sampleIndex]);
                sampleIndex--;
            }
        }
    }
}
