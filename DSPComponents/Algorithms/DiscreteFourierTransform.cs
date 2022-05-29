using System;
using System.Collections.Generic;
using System.IO;
using DSPAlgorithms.DataStructures;

namespace DSPAlgorithms.Algorithms
{
    public class DiscreteFourierTransform : Algorithm
    {
        public Signal InputTimeDomainSignal { get; set; }
        public float InputSamplingFrequency { get; set; }
        public Signal OutputFreqDomainSignal { get; set; }

        public override void Run()
        {
            

            var samplesCount = InputTimeDomainSignal.Samples.Count;
            
            OutputFreqDomainSignal = new Signal
                (new List<float>(),
                    false, new List<float>(),
                    new List<float>(),
                    new List<float>());

            var omega = (int)(2 * Math.PI) / (samplesCount * (1 / InputSamplingFrequency));

            for (var harmonicIndex = 0; harmonicIndex < samplesCount; harmonicIndex++)
            {
                var realPart = 0.0f;
                var imaginaryPart = 0.0f;
                
                for (var sampleIndex = 0; sampleIndex < samplesCount; sampleIndex++)
                {
                    realPart += 
                        InputTimeDomainSignal.Samples[sampleIndex] * 
                        // ReSharper disable once PossibleLossOfFraction
                        (float)Math.Cos(
                            harmonicIndex *
                            2 * 
                            180 * 
                            sampleIndex /
                            samplesCount * 
                            (Math.PI / 180));
                    
                    imaginaryPart += -1 * 
                                     InputTimeDomainSignal.Samples[sampleIndex] * 
                                     // ReSharper disable once PossibleLossOfFraction
                                     (float)Math.Sin(harmonicIndex *
                                                     2 *
                                                     180 *
                                                     sampleIndex /
                                                     samplesCount *
                                                     (Math.PI / 180));

                }
                
                OutputFreqDomainSignal.FrequenciesAmplitudes.Add(
                    (float)
                    Math.Sqrt(
                        Math.Pow(realPart, 2) +
                        Math.Pow(imaginaryPart, 2)));

                OutputFreqDomainSignal.FrequenciesPhaseShifts.Add(
                    (float)
                    Math.Atan2(imaginaryPart, realPart));
                
                OutputFreqDomainSignal.Frequencies.Add(omega * 
                                                       (harmonicIndex + 1));


            }


            StreamWriter sw = new StreamWriter("text.txt");
            for (var itr = 0; itr < samplesCount; itr++)
            {
                sw.WriteLine(OutputFreqDomainSignal.FrequenciesAmplitudes[itr] + 
                             "," + 
                             OutputFreqDomainSignal.FrequenciesPhaseShifts[itr]);
            }
            sw.Close();


        }
    }
}
