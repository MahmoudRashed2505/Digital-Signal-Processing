using System.Collections.Generic;
using System.Linq;
using DSPAlgorithms.DataStructures;

namespace DSPAlgorithms.Algorithms
{
    public class Normalizer : Algorithm
    {
        public Signal InputSignal { get; set; }
        public float InputMinRange { get; set; }
        public float InputMaxRange { get; set; }
        public Signal OutputNormalizedSignal { get; set; }

        public override void Run()
        {
            //throw new NotImplementedException();
            var Result = new List<float>();
            float res = 0;
            var max = InputSignal.Samples.Max();
            var min = InputSignal.Samples.Min();
            foreach (var t in InputSignal.Samples)
            {
                res = (InputMaxRange-InputMinRange)*((t - min) / (max - min))+InputMinRange;
                Result.Add(res);
                res = 0;
            }
            OutputNormalizedSignal = new Signal(Result, false);
            
            
                
        }
    }
}