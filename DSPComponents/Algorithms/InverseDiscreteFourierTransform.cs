using System;
using System.Collections.Generic;
using System.IO;
using DSPAlgorithms.DataStructures;

namespace DSPAlgorithms.Algorithms
{
    public class InverseDiscreteFourierTransform : Algorithm
    {
        public Signal InputFreqDomainSignal { get; set; }
        public Signal OutputTimeDomainSignal { get; set; }

        public override void Run()
        {
            var sr = new StreamReader("text.txt");

            var line = sr.ReadLine();
           
            var sampleIndex = 0;
            
            while (line != null)
            {
                var sample = line.Split(',');

                var sampleAmplitude = float.Parse(sample[0]);
                
                var phaseShift = float.Parse(sample[1]);
                
                InputFreqDomainSignal.FrequenciesAmplitudes[sampleIndex] = sampleAmplitude;
                
                InputFreqDomainSignal.FrequenciesPhaseShifts[sampleIndex] = phaseShift;
                
                sampleIndex += 1;
                
                line = sr.ReadLine();

            }

            sr.Close();

            OutputTimeDomainSignal = new Signal(
                new List<float>(),
                new List<int>(), 
                false, 
                new List<float>(),
                InputFreqDomainSignal.FrequenciesAmplitudes,
                InputFreqDomainSignal.FrequenciesPhaseShifts
                );


            var samplesCount = InputFreqDomainSignal.FrequenciesAmplitudes.Count;

            for (var harmonicIndex =0;harmonicIndex< samplesCount; harmonicIndex++) {
                
                var realPart = 0.0f;
                var imaginaryPart = 0.0f;

                for (sampleIndex = 0; sampleIndex < samplesCount; sampleIndex++) {

                    var a = InputFreqDomainSignal.FrequenciesAmplitudes[sampleIndex] *
                            (float)
                            Math.Cos(InputFreqDomainSignal.FrequenciesPhaseShifts[sampleIndex]);
                    var b = InputFreqDomainSignal.FrequenciesAmplitudes[sampleIndex] *
                            (float)
                            Math.Sin(InputFreqDomainSignal.FrequenciesPhaseShifts[sampleIndex]);


                    // ReSharper disable once PossibleLossOfFraction
                    realPart += a* 
                                (float)
                        Math.Cos(
                            harmonicIndex * 
                            2 * 
                            180 *
                            sampleIndex /
                            samplesCount *
                            Math.PI /180);

                    // ReSharper disable once PossibleLossOfFraction
                    imaginaryPart += -1 * 
                                     b * 
                                     (float)
                                     Math.Sin(
                                         harmonicIndex *
                                         2 *
                                         180 *
                                         sampleIndex / 
                                         samplesCount *
                                         Math.PI /180);

                }

                OutputTimeDomainSignal.Samples.Add((realPart+imaginaryPart)/ samplesCount);
                
                OutputTimeDomainSignal.SamplesIndices.Add(harmonicIndex);
            }
            
        }
        }
    }

