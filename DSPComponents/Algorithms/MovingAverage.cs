using System;
using System.Collections.Generic;
using DSPAlgorithms.DataStructures;

namespace DSPAlgorithms.Algorithms
{
    public class MovingAverage : Algorithm
    {
        public Signal InputSignal { get; set; }
        public int InputWindowSize { get; set; }
        public Signal OutputAverageSignal { get; set; }
 
        public override void Run()
        {
            OutputAverageSignal = new Signal(new List<float>(), new List<int>(), InputSignal.Periodic);
            for (var sampleIneIndex = 0; sampleIneIndex < InputSignal.Samples.Count-2; sampleIneIndex++)
            {
                OutputAverageSignal.Samples.Add((
                    InputSignal.Samples[sampleIneIndex]+
                    InputSignal.Samples[sampleIneIndex+1] + 
                    InputSignal.Samples[sampleIneIndex+2]) /
                    InputWindowSize
                    );

                OutputAverageSignal.SamplesIndices.Add(sampleIneIndex);
            }
        }
    }
}
