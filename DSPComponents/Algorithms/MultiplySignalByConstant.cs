using System.Collections.Generic;
using DSPAlgorithms.DataStructures;

namespace DSPAlgorithms.Algorithms
{
    public class MultiplySignalByConstant : Algorithm
    {
        public Signal InputSignal { get; set; }
        public float InputConstant { get; set; }
        public Signal OutputMultipliedSignal { get; set; }

        public override void Run()
        {

            var result = new List<float>();
            float res = 0;
            foreach (var t in InputSignal.Samples)
            {
                res = InputConstant * t;
                result.Add(res);
                res = 0;
                OutputMultipliedSignal = new Signal(result, false);
            }
        }
    }
}