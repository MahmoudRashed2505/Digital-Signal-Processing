using System;
using System.Collections.Generic;
using System.Linq;
using DSPAlgorithms.DataStructures;

namespace DSPAlgorithms.Algorithms
{
    public class DirectCorrelation : Algorithm
    {
        public Signal InputSignal1 { get; set; }
        public Signal InputSignal2 { get; set; }
        public List<float> OutputNonNormalizedCorrelation { get; set; }
        public List<float> OutputNormalizedCorrelation { get; set; }

        public override void Run()
        {

            if (InputSignal2 == null)
            {
                var output = new List<float>();
                var sam = new List<float>(InputSignal1.Samples);
                InputSignal2 = new Signal(sam, InputSignal1.Periodic);
                
                for (var i = 0; i < InputSignal1.Samples.Count; i++)
                {
                    var v = InputSignal1.Samples.Select(
                        (t, j) => 
                            t * InputSignal2.Samples[j]).Sum();

                    output.Add(1.0f / InputSignal1.Samples.Count * v);

                    var val = InputSignal2.Samples[0];

                    InputSignal2.Samples.RemoveAt(0);

                    if (InputSignal2.Periodic)
                        InputSignal2.Samples.Add(val);
                    else
                        InputSignal2.Samples.Add(0);


                }
                OutputNonNormalizedCorrelation = output;

                var outputNorm = output.Select(
                    t => t / output.Max()).ToList();

                OutputNormalizedCorrelation = outputNorm;


            }
            else
            {
                var output = new List<float>();

                var n = (InputSignal2.Samples.Count + InputSignal1.Samples.Count) - 1;

                
                for (var j = InputSignal2.Samples.Count; j < n; j++)
                {

                    InputSignal2.Samples.Add(0);
                    InputSignal2.SamplesIndices.Add(j);

                }
                for (var j = InputSignal1.Samples.Count; j < n; j++)
                {

                    InputSignal1.Samples.Add(0);
                    InputSignal1.SamplesIndices.Add(j);

                }

                float seg1 = 0;
                float seg2 = 0;

                for (var i = 0; i < InputSignal1.Samples.Count; i++)
                {
                    seg1 += InputSignal1.Samples[i] * InputSignal1.Samples[i];
                    seg2 += InputSignal2.Samples[i] * InputSignal2.Samples[i];
                }
                
                var res = (float)Math.Sqrt(seg1 * seg2) * (1.0f / InputSignal1.Samples.Count);
                
                for (var i = 0; i < InputSignal1.Samples.Count; i++)
                {
                    var v = InputSignal1.Samples.Select((t, j) => t * InputSignal2.Samples[j]).Sum();

                    output.Add((1.0f / InputSignal1.Samples.Count) * v);

                    var val = InputSignal2.Samples[0];
                    InputSignal2.Samples.RemoveAt(0);
                    if (InputSignal2.Periodic)
                        InputSignal2.Samples.Add(val);
                    else
                        InputSignal2.Samples.Add(0);


                }

                OutputNonNormalizedCorrelation = output;
                var outputNorm = output.Select(t => t / res).ToList();
                
                
                OutputNormalizedCorrelation = outputNorm;

            



            }
        }
    }
}