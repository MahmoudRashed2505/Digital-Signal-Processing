using DSPAlgorithms.DataStructures;
using System;
using System.Collections.Generic;

namespace DSPAlgorithms.Algorithms
{
    public class Derivatives: Algorithm
    {
        public Signal InputSignal { get; set; }
        public Signal FirstDerivative { get; set; }
        public Signal SecondDerivative { get; set; }

        public override void Run()
        {

            FirstDerivative = new Signal(new List<float>(), false);
            SecondDerivative = new Signal(new List<float>(), false);

            for (var sampleIndex = 0; sampleIndex < InputSignal.Samples.Count-1; sampleIndex++)
            {
                if (sampleIndex == 0)
                {
                    FirstDerivative.Samples.Add(
                        InputSignal.Samples[sampleIndex]);

                    SecondDerivative.Samples.Add(
                        InputSignal.Samples[sampleIndex+1]-
                        2*
                        InputSignal.Samples[sampleIndex]);
                }
                else
                {
                    FirstDerivative.Samples.Add(
                        InputSignal.Samples[sampleIndex]-
                        InputSignal.Samples[sampleIndex-1]);

                    SecondDerivative.Samples.Add(
                        InputSignal.Samples[sampleIndex+1] - 
                        2 * 
                        InputSignal.Samples[sampleIndex] +
                        InputSignal.Samples[sampleIndex - 1]);
                }
            }

        }
    }
}
