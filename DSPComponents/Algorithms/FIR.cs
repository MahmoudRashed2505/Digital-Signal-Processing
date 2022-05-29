using System;
using System.Collections.Generic;
using DSPAlgorithms.DataStructures;

namespace DSPAlgorithms.Algorithms
{
    public class FIR : Algorithm
    {
        public Signal InputTimeDomainSignal { get; set; }
        public FILTER_TYPES InputFilterType { get; set; }
        public float InputFS { get; set; }
        public float? InputCutOffFrequency { get; set; }
        public float? InputF1 { get; set; }
        public float? InputF2 { get; set; }
        public float InputStopBandAttenuation { get; set; }
        public float InputTransitionBand { get; set; }
        public Signal OutputHn { get; set; }
        public Signal OutputYn { get; set; }

        
            public override void Run()
            {
                var N = 0;
                var value = 0.0f;

                var windowName = "";


                OutputHn = new Signal(new List<float>(),new List<int>(),false);

                if (InputStopBandAttenuation <= 21)
                {
                    value = 0.9f;
                    windowName = "rectangle";
                }
                else
                {
                    if (InputStopBandAttenuation > 21 && InputStopBandAttenuation <= 44)
                    {
                        value = 3.1f;
                        windowName = "hanning";
                    }
                    else
                    {
                        if (InputStopBandAttenuation > 44 && InputStopBandAttenuation <= 53)
                        {
                            value = 3.3f;
                            windowName = "hamming";
                        }
                        else
                        {
                            if (InputStopBandAttenuation > 53 && InputStopBandAttenuation <= 74)
                            {
                                value = 5.5f;
                                windowName = "blackman";
                            }
                        }
                    }
                }


                N = (int)Math.Floor((value / (InputTransitionBand / InputFS)) + 1);
                
                var cutOffNormalized = 0.0f;
                var cutOffNormalized1 = 0.0f;
                var cutOffNormalized2 = 0.0f;

                switch (InputFilterType)
                {
                    case FILTER_TYPES.HIGH:
                    case FILTER_TYPES.LOW:
                        cutOffNormalized = (float)(InputCutOffFrequency + (InputTransitionBand / 2));
                        break;
                    case FILTER_TYPES.BAND_STOP:
                    case FILTER_TYPES.BAND_PASS:
                        cutOffNormalized1 = (float)(InputF1 - (InputTransitionBand / 2));
                        cutOffNormalized2 = (float)(InputF2 + (InputTransitionBand / 2));
                        break;
                }

                cutOffNormalized /= InputFS;
                cutOffNormalized1 /= InputFS;
                cutOffNormalized2 /= InputFS;



                for(int i=0 , n=(int) -N/2;i<N;i++,n++)
                {
                    OutputHn.SamplesIndices.Add(n);
                }

                switch (InputFilterType)
                {
                    case FILTER_TYPES.LOW:
                    {
                        for (var i = 0; i < N; i++)
                        {
                            var index = Math.Abs(OutputHn.SamplesIndices[i]);
                            if (OutputHn.SamplesIndices[i] == 0)
                            {
                                var hn = 2 * cutOffNormalized;
                                var wn = window_function(windowName, index, N);
                                OutputHn.Samples.Add(hn * wn);
                            }
                            else
                            {
                                var wc = (float)(2 * Math.PI * cutOffNormalized * index);
                                var hn = (float)(2 * cutOffNormalized * Math.Sin(wc) / wc);
                                var wn = window_function(windowName, index, N);
                                OutputHn.Samples.Add(hn * wn);
                            }
                        }

                        break;
                    }
                    case FILTER_TYPES.HIGH:
                    {
                        for (var i = 0; i < N; i++)
                        {
                            var index = Math.Abs(OutputHn.SamplesIndices[i]);
                            if (OutputHn.SamplesIndices[i] == 0)
                            {
                                var hn = (2 * cutOffNormalized);
                                hn = 1 - hn;
                                var wn = window_function(windowName, index, N);
                                OutputHn.Samples.Add(hn * wn);
                            }
                            else
                            {
                                var wc = (float)(2 * Math.PI * cutOffNormalized * index);
                                var hn = (float)(2 * cutOffNormalized * Math.Sin(wc) / wc);
                                hn = -hn;
                                var wn = window_function(windowName, index, N);
                                OutputHn.Samples.Add(hn * wn);
                            }
                        }

                        break;
                    }
                    case FILTER_TYPES.BAND_PASS:
                    {
                        for (var i = 0; i < N; i++)
                        {
                            var index = Math.Abs(OutputHn.SamplesIndices[i]);
                            if (OutputHn.SamplesIndices[i] == 0)
                            {
                                var hn = 2 * (cutOffNormalized2 - cutOffNormalized1);
                                var wn = window_function(windowName, index, N);
                                OutputHn.Samples.Add(hn * wn);
                            }
                            else
                            {
                                var w2 = (float)(2 * Math.PI * cutOffNormalized2 * index);
                                var w1 = (float)(2 * Math.PI * cutOffNormalized1 * index);
                                var hn = (float)((2 * cutOffNormalized2 * Math.Sin(w2) / w2) -
                                                 (2 * cutOffNormalized1 * Math.Sin(w1) / w1));

                                var wn = (window_function(windowName, index, N));
                                OutputHn.Samples.Add(hn * wn);
                            }
                        }

                        break;
                    }
                    case FILTER_TYPES.BAND_STOP:
                    {
                        for (var i = 0; i < N; i++)
                        {
                            var index = Math.Abs(OutputHn.SamplesIndices[i]);
                            if (OutputHn.SamplesIndices[i] == 0)
                            {
                                var hn = 1 - (2 * (cutOffNormalized2 - cutOffNormalized1));
                                var wn = window_function(windowName, index, N);
                                OutputHn.Samples.Add(hn * wn);
                            }
                            else
                            {
                                var w2 = (float)(2 * Math.PI * cutOffNormalized2 * index);
                                var w1 = (float)(2 * Math.PI * cutOffNormalized1 * index);
                                var hn = (float)((2 * cutOffNormalized1 * Math.Sin(w1) / w1) -
                                                 (2 * cutOffNormalized2 * Math.Sin(w2) / w2));

                                var wn = (window_function(windowName, index, N));
                                OutputHn.Samples.Add(hn * wn);
                            }
                        }

                        break;
                    }
                }


                var c = new DirectConvolution
                {
                    InputSignal1 = InputTimeDomainSignal,
                    InputSignal2 = OutputHn
                };
                c.Run();
                OutputYn = c.OutputConvolvedSignal;



            }
            public float window_function(String windowName , int n , int N)
            {
                if (string.IsNullOrEmpty(windowName))
                    throw new ArgumentException("Value cannot be null or empty.", nameof(windowName));
                
                var res = 0.0f;
                switch (windowName)
                {
                    case "rectangle":
                        res = 1;
                        break;
                    case "hanning":
                        res = (float)0.5 + (float)(0.5 * Math.Cos((2 * Math.PI * n) / N));
                        break;
                    case "hamming":
                        res = (float)0.54 + (float)(0.46 * Math.Cos((2 * Math.PI * n) / N));
                        break;
                    case "blackman":
                    {
                        var term1 = (float)(0.5 * Math.Cos((2 * Math.PI * n) / (N - 1)));
                        var term2 = (float)(0.08 * Math.Cos((4 * Math.PI * n) / (N - 1)));
                        res = (float)(0.42 + term1 + term2);
                        break;
                    }
                }

                return res;
            }
        }
}
