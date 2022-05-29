using System.Collections.Generic;
using System.Linq;
using DSPAlgorithms.DataStructures;

namespace DSPAlgorithms.Algorithms
{
    public class Adder : Algorithm
    {
        public List<Signal> InputSignals { get; set; }
        public Signal OutputSignal { get; set; }

        public override void Run()
        {
            //throw new NotImplementedException();
            var result = new List<float>();
            float res = 0;
            var maxSampleCount = InputSignals.Max(signal => signal.Samples.Count);
            
            
            foreach (var signal in InputSignals)
            {
                while (signal.Samples.Count<maxSampleCount)
                {
                    signal.Samples.Add(0);
                }
            }

            for (var j = 0; j < InputSignals[0].Samples.Count; j++)
            {
                res += InputSignals.Sum(t => t.Samples[j]);
                result.Add(res);
                res = 0;
            }

            OutputSignal = new Signal(result,false);
        }
    }
}