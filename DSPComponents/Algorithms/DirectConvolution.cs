using System;
using System.Collections.Generic;
using DSPAlgorithms.DataStructures;

namespace DSPAlgorithms.Algorithms
{
    public class DirectConvolution : Algorithm
    {
        public Signal InputSignal1 { get; set; }
        public Signal InputSignal2 { get; set; }
        public Signal OutputConvolvedSignal { get; set; }

        /// <summary>
        /// Convolved InputSignal1 (considered as X) with InputSignal2 (considered as H)
        /// </summary>
        public override void Run()
        {
            
            var conv = new List<float>();
            var index = new List<int>();
            
            var samplesCount = InputSignal1.Samples.Count + InputSignal2.Samples.Count - 1;
            
            for (var i=0;i<InputSignal1.Samples.Count ; i++)
            {
                for (var j = 0; j < InputSignal2.Samples.Count; j++) {
                    var val = InputSignal1.SamplesIndices[i] + InputSignal2.SamplesIndices[j];
                    if (!index.Contains(val)) index.Add(val);
                }
            }


            for (var i = 0; i < samplesCount; i++)
            {
                float v = 0;
                for (var j = 0; j <= i; j++)
                {
                    var a = !(i - j >= InputSignal1.Samples.Count) ? InputSignal1.Samples[i - j] : 0;

                    var b = !(j >= InputSignal2.Samples.Count) ? InputSignal2.Samples[j] : 0;

                    v += a * b;

                }
                conv.Add(v);

            }
            OutputConvolvedSignal = new Signal(conv,index,false);
        }
    }
}