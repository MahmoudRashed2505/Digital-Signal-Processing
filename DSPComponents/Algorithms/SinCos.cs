using System;
using System.Collections.Generic;

namespace DSPAlgorithms.Algorithms
{
    public class SinCos: Algorithm
    {
        public string type { get; set; }
        public float A { get; set; }
        public float PhaseShift { get; set; }
        public float AnalogFrequency { get; set; }
        public float SamplingFrequency { get; set; }
        public List<float> samples { get; set; }
        public override void Run()
        {

            var testSamples = (int)(SamplingFrequency / AnalogFrequency);
            var period = 1 / SamplingFrequency;

            var time = new List<float>(testSamples);

            for (var i = 0; i < testSamples; i++)
                time.Add(i* period);

            var values = new List<float>();

            foreach (var t in time)
                values.Add((float)(A * Math.Sin(2 * Math.PI * AnalogFrequency * t + PhaseShift)));

            samples = new List<float>((int)SamplingFrequency);

            if (type == "sin")
            {
                for (var i = 0; i < SamplingFrequency/2; i++)
                {
                    foreach (var value in values)
                    {
                        samples.Add(value);
                    }
                }
            }
            else
            {
                values.Reverse();
                for (var i = 0; i < SamplingFrequency/2; i++)
                {
                    foreach (var value in values)
                    {
                        samples.Add(value);
                    }
                }
            }
        }
    }
}
