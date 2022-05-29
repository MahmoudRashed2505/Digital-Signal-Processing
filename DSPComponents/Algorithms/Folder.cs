using System;
using DSPAlgorithms.DataStructures;

namespace DSPAlgorithms.Algorithms
{
    public class Folder : Algorithm
    {
        public Signal InputSignal { get; set; }
        public Signal OutputFoldedSignal { get; set; }

        public override void Run()
        {
            var reversedSamples = InputSignal.Samples;
            reversedSamples.Reverse();
            OutputFoldedSignal = new Signal(reversedSamples, InputSignal.SamplesIndices, InputSignal.Periodic);

        }
    }
}
