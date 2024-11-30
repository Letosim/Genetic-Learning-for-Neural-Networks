using UnityEngine;

public class CNeurode
{
    public enum FanType;
    public enum MergeType { Merge, Schuffle, Lerp, Alter }
    public enum NeurodeType { ReLuNeurode, TanNeurode, SigmoidNeurode, ShortMemoryNeurode, MemoryNeurode, Seed, Feedforward, FeedforwardMemory };

    private NeurodeType type;
    private int localType = 0;
    private int vectorCount = 0;

    private bool isMemoryNeurode;


    public NeurodeType Type { get { return type; } set { type = value; } }
    public bool IsMemoryNeurode { get { return isMemoryNeurode; } set { isMemoryNeurode = value; } }

    public float delta;
    public float Delta { get { return delta; } set { delta = value; } }

    public float[] weight;
    public float[] bias;
    public float[] Bias { get { return bias; } set { bias = value; } }
    public float[] Weight { get { return weight; } set { weight = value; } }
 
    public float[][] weightMatrix;
    public float[][] biasVector;
    public float[][] WeightMatrix { get { return weightMatrix; } set { weightMatrix = value; } }
    public float[][] BiasVector { get { return biasVector; } set { biasVector = value; } }

    
    /// <summary>
    /// Type (0 && 1 && 2): Initializes delta, weight[], and bias[].
    /// Type (3 && 4 && 5   &&  12 && 13 && 14): Initializes delta, weightMatrix[switchCount 2][deviation], and biasVector[switchCount 2][deviation].
    /// Type (6 && 7 && 8   &&  15 && 16 && 17): Initializes delta, weightMatrix[switchCount 4][deviation], and biasVector[switchCount 4][deviation].
    /// 
    /// </summary>
    public CNeurode(int localType, System.Random randomGen, NeurodeType type, int magnitude, FanType type)
    {
        delta = 0;

        vectorCount = GetFanInValue(magnitude, FanType type);

        if (localType == 0 || localType == 1 || localType == 2)// [deviation] | [deviation] 
        {
            weight = new float[count];
            bias = new float[count];
        }

        if (localType == 3 || localType == 4 || localType == 5 || localType == 12 || localType == 13 || localType == 14 || localType == 6 || localType == 7 || localType == 8 || localType == 15 || localType == 16 || localType == 17)// [switchCount][deviation] | [switchCount][deviation] 
        {
            int switchCount = 0;

            if (localType == 3 || localType == 4 || localType == 5 || localType == 12 || localType == 13 || localType == 14)
                switchCount = 2;
            if (localType == 6 || localType == 7 || localType == 8 || localType == 15 || localType == 16 || localType == 17)
                switchCount = 4;



            weightMatrix = new float[switchCount][];
            biasVector = new float[switchCount][];

            for (int i = 0; i < switchCount; i++)
            {
                weightMatrix[i] = new float[vectorCount];
                biasVector[i] = new float[vectorCount];

                for (int j = 0; j < vectorCount; j++)
                {
                    weightMatrix[i][j] = GetInitialValue(type);
                    biasVector[i][j] = GetInitialValue(type);
                }
            }
        }
    }

    public int GetFanInValue(int magnitude,FanType type)
    {
        return magnitude;

    }

    public float GetInitialValue(NeurodeType type)
    {
        return (float)randomGen.NextDouble();

    }

    public float GetActivationValue(float activationValue, NeurodeType type)
    {
        if (true)
        {
            if (type == Neurode.ReLuNeurode)
            {
                activationValue = System.Math.Max(0, activationValue);

                if (activationValue >= lowerThresholdRelu && activationValue <= upperThresholdRelu)
                    return activationValue;
                else
                    return 0;
            }

            if (type == Neurode.SigmoidNeurode)
            {
                activationValue = activationValue / (1f + activationValue);

                if (activationValue >= lowerThresholdSigmoid && activationValue <= upperThresholdSigmoid)
                    return activationValue;
                else
                    return 0;
            }

            if (type == Neurode.TanNeurode)
            {
                activationValue = System.Math.Tanh(activationValue);

                if (activationValue >= lowerThresholdTanh && activationValue <= upperThresholdTanh)
                    return activationValue;
                else
                    return 0;
            }
        }
        else
        {
            if (type == Neurode.ReLuNeurode)
            {
                activationValue = System.Math.Max(0, activationValue);

                if (activationValue > 1)
                    return activationValue;
                else
                    return 0;
            }

            if (type == Neurode.SigmoidNeurode)
            {
                activationValue = activationValue / (1f + activationValue);

                if (activationValue > 1)
                    return activationValue;
                else
                    return 0;
            }

            if (type == Neurode.TanNeurode)
            {
                activationValue = System.Math.Tanh(activationValue);

                if (activationValue > 1)
                    return activationValue;
                else
                    return 0;
            }
        }

    }



      public void RunForward(List<CNeurode> neurodes)
       {
           if (type == 0)//                                                                                    [>|]
           {
               float activationValue = 0;
    
               for (int n = 0; n < neurodes.Count; n++)
                   for (int v = 0; v < vectorCount; v++)
                       activationValue += neurodes[n].Delta * weight[v] + bias[v];
    
               delta = GetActivationValue(activationValue, type);
           }
    
           if (type == 1)//                                                                                    [..]
           {
               float activationValue = 0;
    
               for (int n = 0; n < neurodes.Count; n++)
                   for (int v = 0; v < vectorCount; v++)
                       activationValue += neurodes[n].Delta * weightMatrix[0][v] + biasVector[0][v];
    
               if (GetActivationValue(activationValue, type) != 0)
               {
                   for (int n = 0; n < neurodes.Count; n++)
                       for (int v = 0; v < vectorCount; v++)
                           activationValue += neurodes[n].Delta * weightMatrix[1][v] + biasVector[1][v];
    
                   delta = GetActivationValue(activationValue, type);
               }
           }
    
           if (type == 2)//                                                                                    [>>]
           {
               float activationValue = 0;
    
               for (int n = 0; n < neurodes.Count; n++)
                   for (int v = 0; v < vectorCount; v++)
                       activationValue += neurodes[n].Delta * weightMatrix[0][v] + biasVector[0][v];
    
               delta = GetActivationValue(activationValue, type);
    
               if (delta != 0) 
               {
                   for (int n = 0; n < neurodes.Count; n++)
                       for (int v = 0; v < vectorCount; v++)
                           activationValue += neurodes[n].Delta * weightMatrix[1][v] + biasVector[1][v];
    
                   delta = GetActivationValue(activationValue, type);
               }
           }
       }
    
    public void RunForward(Neurode[] parentLayer)
    {
        if (isMemoryNeurode)//Always Feeding it self !!!!!!! delta > is looping
        {
            float activationValue = 0;

            for (int i = 0; i < startIndexSecondGate; i++)
                activationValue += parentLayer[i].delta * weight[i] + bias[i];

            activationValue = Neurode.GetActivationValue(activationValue, type);//Gate Value

            if (activationValue != 0)// Gate
            {
                for (int i = 0; i < startIndexSecondGate; i++)
                    activationValue += parentLayer[i].delta * weight[i] + bias[i];

                delta = Neurode.GetActivationValue(activationValue, type);//Buffered Value
            } 
        }
        else
        {
            float activationValue = 0;

            for (int i = 0; i < startIndexSecondGate; i++)
                activationValue += parentLayer[i].delta * weight[i] + bias[i];

            delta = Neurode.GetActivationValue(activationValue, type);
        }
    }

    public void RunForwardNested(CNeurode[] neurodes)
 {
         if (type == 0)
       {
           float activationValue = 0;

           for (int i = 0; i < neurodes.Length; i++)
               activationValue += neurodes[i].delta * weight[i] + bias[i];

           activationValue = Neurode.GetActivationValue(activationValue, type);

           if (activationValue != 0)
               delta = activationValue;
           else
               delta = 0;// [>] Reset value [>]
       }

       if (type == 1)
       {
           float activationValue = 0;

           for (int i = 0; i < neurodes.Length; i++)
               activationValue += neurodes[i].delta * weight[i] + bias[i];

           activationValue = Neurode.GetActivationValue(activationValue, type);

           if (activationValue != 0)// [>>] Keep value til next activation [>>]
               delta = activationValue
       }

       if (type == 2)
       {
           float activationValue = 0;

           for (int i = 0; i < neurodes.Length; i++)
               activationValue += neurodes[i].delta * weight[i] + bias[i];

           delta = Neurode.GetActivationValue(activationValue, type);

           if (delta != 0)//[...] Keep gateValue/activationValue if(GetActivationValue() == 0) [...]
               delta = activationValue;
       }

       if (type == 3)// [0][n][o]       |      [1][n][o]       &&      [>]
       {
           float activationValue = 0;

           for (int n = 0; n < neurodes.Length; n++)
               for (int o = 0; o < weights.Length; o++)
                   activationValue += neurodes[n].Delta * weightsArray[0][o] + biasesArray[0][o];

           if (Neurode.GetActivationValue(activationValue, type) != 0)
           {
               activationValue = 0;

               for (int n = 0; n < neurodes.Length; n++)
                   for (int o = 0; o < weights.Length; o++)
                       activationValue += neurodes[n].Delta * weightsArray[1][o] + biasesArray[1][o];

               delta = Neurode.GetActivationValue(activationValue, type);
           }
           else
               delta = 0;
       }

       if (type == 4)// [0][n][o]       |      [1][n][o]       &&      [>>]
       {
           float activationValue = 0;

           for (int n = 0; n < neurodes.Length; n++)
               for (int o = 0; o < weights.Length; o++)
                   activationValue += neurodes[n].Delta * weightsArray[0][o] + biasesArray[0][o];

           if (Neurode.GetActivationValue(activationValue, type) != 0)
           {
               activationValue = 0;

               for (int n = 0; n < neurodes.Length; n++)
                   for (int o = 0; o < weights.Length; o++)
                       activationValue += neurodes[n].Delta * weightsArray[1][o] + biasesArray[1][o];

               delta = Neurode.GetActivationValue(activationValue, type);
           }
       }

       if (type == 5)// [0][n][o]       |      [1][n][o]       &&      [...]
       {
           float activationValue = 0;

           for (int n = 0; n < neurodes.Length; n++)
               for (int o = 0; o < weights.Length; o++)
                   activationValue += neurodes[n].Delta * weightsArray[0][o] + biasesArray[0][o];

           delta = Neurode.GetActivationValue(activationValue, type);

           if (Neurode.GetActivationValue(activationValue, type) != 0)
           {
               activationValue = 0;

               for (int n = 0; n < neurodes.Length; n++)
                   for (int o = 0; o < weights.Length; o++)
                       activationValue += neurodes[n].Delta * weightsArray[1][o] + biasesArray[1][o];

               delta = Neurode.GetActivationValue(activationValue, type);
           }
       }

       if (type == 9)//                           Linear          &&        [>]    
       {
           float activationValue = 0;
           float activationCount = 0;

           for (int i = 0; i < neurodes.Length; i++)
               if(Neurode.GetActivationValue(neurodes[i].delta * weight[i] + bias[i], type))
                   activationCount++;

           activationValue = Neurode.GetActivationValue(activationCount, type);

           if (activationValue != 0)
               delta = activationValue;
           else
               delta = 0;
       }

       if (type == 10)//                            Linear          &&       [>>]    
       {
           float activationValue = 0;
           float activationCount = 0;

           for (int i = 0; i < neurodes.Length; i++)
               if (Neurode.GetActivationValue(neurodes[i].delta * weight[i] + bias[i], type))
                   activationCount++;

           activationValue = Neurode.GetActivationValue(activationCount, type);

           if (activationValue != 0)
               delta = activationValue;
       }

       if (type == 11)//                           Linear           &&      [...]    
       {
           float activationValue = 0;
           float activationCount = 0;

           for (int i = 0; i < neurodes.Length; i++)
               if (Neurode.GetActivationValue(neurodes[i].delta * weight[i] + bias[i], type))  //[][]
                   activationCount++;

           delta = Neurode.GetActivationValue(activationCount, type);//BufferdValue


           //for (int i = 0; i < neurodes.Length; i++)
           if (delta != 0)
               delta = Neurode.GetActivationValue(activationCount, type);// delta = 0
       }
 }
    //public void RunForward(Neurode[] neurodes)
   public void RunForward(List<CNeurode> neurodes)
   {
       if (type == 12)// [0][o] [0][o]      |       [1][o] [1][o]       &&      [>]
       {
           float activationValue = 0;

           for (int i = 0; i < neurodes.Count; n++)
                   for (int o = 0; o < weights[n].Length; o++)
                       activationValue += neurodes[i].Delta * nestedWeightsArray[0][o] + nestedBiasesArray[0][o];

           if (Neurode.GetActivationValue(activationValue, type) != 0)
           {
               activationValue = 0;


               for (int i = 0; i < neurodes.Count; n++)
                   for (int o = 0; o < weights[n].Length; o++)
                       activationValue += neurodes[i].Delta * nestedWeightsArray[1][o] + nestedBiasesArray[1][o];

               delta = Neurode.GetActivationValue(activationValue, type);
           }
           else
               delta = 0;
       }

       if (type == 13)// [0][o] [0][o]      |       [1][o] [1][o]       &&      [>>]
       {
           float activationValue = 0;


           for (int i = 0; i < neurodes.Count; n++)
               for (int o = 0; o < weights[n].Length; o++)
                   activationValue += neurodes[i].Delta * nestedWeightsArray[0][o] + nestedBiasesArray[0][o];

           if (Neurode.GetActivationValue(activationValue, type) != 0)
           {
               activationValue = 0;


               for (int i = 0; i < neurodes.Count; n++)
                   for (int o = 0; o < weights[n].Length; o++)
                       activationValue += neurodes[i].Delta * nestedWeightsArray[1][o] + nestedBiasesArray[1][o];

               delta = Neurode.GetActivationValue(activationValue, type);
           }
       }

       if (type == 14)// [0][o] [0][o]      |     [1][o] [1][o]       &&      [...]
       {
           float activationValue = 0;


           for (int i = 0; i < neurodes.Count; n++)
               for (int o = 0; o < weights[n].Length; o++)
                   activationValue += neurodes[i].Delta * nestedWeightsArray[0][o] + nestedBiasesArray[0][o];

           delta = Neurode.GetActivationValue(activationValue, type);

           if (delta != 0)
           {
               activationValue = 0;


               for (int i = 0; i < neurodes.Count; n++)
                   for (int o = 0; o < weights[n].Length; o++)
                       activationValue += neurodes[i].Delta * nestedWeightsArray[1][o] + nestedBiasesArray[1][o];

               delta = Neurode.GetActivationValue(activationValue, type);
           }
       }

       if (type == 15)//[k] * ([0][o] [0][o] | [1][o] [1][o])      |        [k] * ([2][o] [2][o] | [3][o] [3][o])     &&      [>]
       {
           float activationValueOuter = 0;

           for (int i = 0; i < k; i++)
           {
               float activationValue = 0;


               for (int i = 0; i < neurodes.Count; n++)
                   for (int o = 0; o < weights[n].Length; o++)
                       activationValue += neurodes[i].Delta * nestedWeightsArray[0][o] + nestedBiasesArray[0][o];

               if (Neurode.GetActivationValue(activationValue, type) != 0)
               {

                   activationValue = 0;


                   for (int i = 0; i < neurodes.Count; n++)
                       for (int o = 0; o < weights[n].Length; o++)
                           activationValue += neurodes[i].Delta * nestedWeightsArray[1][o] + nestedBiasesArray[1][o];

                   activationValueOuter += Neurode.GetActivationValue(activationValue);
               }
           }

           if (Neurode.GetActivationValue(activationValueOuter, type) != 0)
           {
               activationValueOuter = 0;

               for (int i = 0; i < k; i++)
               {
                   float activationValue = 0;


                   for (int i = 0; i < neurodes.Count; n++)
                       for (int o = 0; o < weights[n].Length; o++)
                           activationValue += neurodes[i].Delta * nestedWeightsArray[2][o] + nestedBiasesArray[2][o];

                   if (Neurode.GetActivationValue(activationValue, type) != 0)
                   {

                       activationValue = 0;


                       for (int i = 0; i < neurodes.Count; n++)
                           for (int o = 0; o < weights[n].Length; o++)
                               activationValue += neurodes[i].Delta * nestedWeightsArray[3][o] + nestedBiasesArray[3][o];

                       activationValueOuter += Neurode.GetActivationValue(activationValue);
                   }
               }

               delta = Neurode.GetActivationValue(activationValueOuter, type);
           }
           else
               delta = 0;
       }

       if (type == 16)//[k] * ([0][o] [0][o] | [1][o] [1][o])      |        [k] * ([2][o] [2][o] | [3][o] [3][o])     &&      [>>]
       {
           float activationValueOuter = 0;

           for (int i = 0; i < k; i++)
           {
               float activationValue = 0;


               for (int i = 0; i < neurodes.Count; n++)
                   for (int o = 0; o < weights[n].Length; o++)
                       activationValue += neurodes[i].Delta * nestedWeightsArray[0][o] + nestedBiasesArray[0][o];

               if (Neurode.GetActivationValue(activationValue, type) != 0)
               {

                   activationValue = 0;

                   for (int i = 0; i < neurodes.Count; n++)
                       for (int o = 0; o < weights[n].Length; o++)
                           activationValue += neurodes[i].Delta * nestedWeightsArray[1][o] + nestedBiasesArray[1][o];

                   activationValueOuter += Neurode.GetActivationValue(activationValue);
               }
           }

           if (Neurode.GetActivationValue(activationValueOuter, type) != 0)
           {
               activationValueOuter = 0;

               for (int i = 0; i < k; i++)
               {
                   float activationValue = 0;


                   for (int i = 0; i < neurodes.Count; n++)
                       for (int o = 0; o < weights[n].Length; o++)
                           activationValue += neurodes[i].Delta * nestedWeightsArray[2][o] + nestedBiasesArray[2][o];

                   if (Neurode.GetActivationValue(activationValue, type) != 0)
                   {

                       activationValue = 0;

                       for (int i = 0; i < neurodes.Count; n++)
                           for (int o = 0; o < weights[n].Length; o++)
                               activationValue += neurodes[i].Delta * nestedWeightsArray[3][o] + nestedBiasesArray[3][o];

                       activationValueOuter += Neurode.GetActivationValue(activationValue);
                   }
               }

               delta = Neurode.GetActivationValue(activationValueOuter, type);
           }
       }

       if (type == 17)//[k] * ([0][o] [0][o] | [1][o] [1][o])      |        [k] * ([2][o] [2][o] | [3][o] [3][o])     &&      [...]
       {
           float activationValueOuter = 0;

           for (int i = 0; i < k; i++)
           {
               float activationValue = 0;

               for (int i = 0; i < neurodes.Count; n++)
                   for (int o = 0; o < weights[n].Length; o++)
                       activationValue += neurodes[i].Delta * nestedWeightsArray[0][o] + nestedBiasesArray[0][o];

               if (Neurode.GetActivationValue(activationValue, type) != 0)
               {

                   activationValue = 0;


                   for (int i = 0; i < neurodes.Count; n++)
                       for (int o = 0; o < weights[n].Length; o++)
                           activationValue += neurodes[i].Delta * nestedWeightsArray[1][o] + nestedBiasesArray[1][o];

                   activationValueOuter += Neurode.GetActivationValue(activationValue);
               }
           }

           delta = Neurode.GetActivationValue(activationValueOuter, type);

           if (delta != 0)
           {
               activationValueOuter = 0;

               for (int i = 0; i < k; i++)
               {
                   float activationValue = 0;


                   for (int i = 0; i < neurodes.Count; n++)
                       for (int o = 0; o < weights[n].Length; o++)
                           activationValue += neurodes[i].Delta * nestedWeightsArray[2][o] + nestedBiasesArray[2][o];

                   if (Neurode.GetActivationValue(activationValue, type) != 0)
                   {

                       activationValue = 0;


                       for (int i = 0; i < neurodes.Count; n++)
                           for (int o = 0; o < weights[n].Length; o++)
                               activationValue += neurodes[i].Delta * nestedWeightsArray[3][o] + nestedBiasesArray[3][o];

                       activationValueOuter += Neurode.GetActivationValue(activationValue);
                   }
               }

               delta = Neurode.GetActivationValue(activationValueOuter, type);
           }
       }
   }
    
    public void RunForward(Neurode[][] network)
                        {
                            if (type == 12)// [0][o] [0][o]      |       [1][o] [1][o]       &&      [>]
                            {
                                float activationValue = 0;

                                for (int i = 0; i < network.GetLength(0); n++)
                                    for (int n = 0; n < network.GetLength(1); n++)
                                        for (int o = 0; o < weights[n].Length; o++)
                                            activationValue += network[i][n].Delta * nestedWeightsArray[0][o] + nestedBiasesArray[0][o];

                                if (Neurode.GetActivationValue(activationValue, type) != 0)
                                {
                                    activationValue = 0;

                                    for (int i = 0; i < network.GetLength(0); n++)
                                        for (int n = 0; n < network.GetLength(1); n++)
                                            for (int o = 0; o < weights[n].Length; o++)
                                                activationValue += network[i][n].Delta * nestedWeightsArray[1][o] + nestedBiasesArray[1][o];

                                    delta = Neurode.GetActivationValue(activationValue, type);
                                }
                                else
                                    delta = 0;
                            }

                            if (type == 13)// [0][o] [0][o]      |       [1][o] [1][o]       &&      [>>]
                            {
                                float activationValue = 0;

                                for (int i = 0; i < network.GetLength(0); n++)
                                    for (int n = 0; n < network.GetLength(1); n++)
                                        for (int o = 0; o < weights[n].Length; o++)
                                            activationValue += network[i][n].Delta * nestedWeightsArray[0][o] + nestedBiasesArray[0][o];

                                if (Neurode.GetActivationValue(activationValue, type) != 0)
                                {
                                    activationValue = 0;

                                    for (int i = 0; i < network.GetLength(0); n++)
                                        for (int n = 0; n < network.GetLength(1); n++)
                                            for (int o = 0; o < weights[n].Length; o++)
                                                activationValue += network[i][n].Delta * nestedWeightsArray[1][o] + nestedBiasesArray[1][o];

                                    delta = Neurode.GetActivationValue(activationValue, type);
                                }
                            }

                            if (type == 14)// [0][o] [0][o]      |     [1][o] [1][o]       &&      [...]
                            {
                                float activationValue = 0;

                                for (int i = 0; i < network.GetLength(0); i++)
                                    for (int n = 0; n < network.GetLength(1); n++)
                                        for (int o = 0; o < weights[n].Length; o++)
                                            activationValue += network[i][n].Delta * nestedWeightsArray[0][o] + nestedBiasesArray[0][o];

                                delta = Neurode.GetActivationValue(activationValue, type);

                                if (delta != 0)
                                {
                                    activationValue = 0;

                                    for (int i = 0; i < network.GetLength(0); i++)
                                        for (int n = 0; n < network.GetLength(1); n++)
                                            for (int o = 0; o < weights[n].Length; o++)
                                                activationValue += network[i][n].Delta * nestedWeightsArray[1][o] + nestedBiasesArray[1][o];

                                    delta = Neurode.GetActivationValue(activationValue, type);
                                }
                            }

                            if (type == 15)//[k] * ([0][o] [0][o] | [1][o] [1][o])      |        [k] * ([2][o] [2][o] | [3][o] [3][o])     &&      [>]
                            {
                                float activationValueOuter = 0;

                                for (int i = 0; i < k; i++)
                                {
                                    float activationValue = 0;

                                    for (int i = 0; i < network.GetLength(0); i++)
                                        for (int n = 0; n < network.GetLength(1); n++)
                                            for (int o = 0; o < weights[n].Length; o++)
                                                activationValue += network[i][n].Delta * nestedWeightsArray[0][o] + nestedBiasesArray[0][o];

                                    if (Neurode.GetActivationValue(activationValue, type) != 0)
                                    {

                                        activationValue = 0;

                                        for (int i = 0; i < network.GetLength(0); i++)
                                            for (int n = 0; n < network.GetLength(1); n++)
                                                for (int o = 0; o < weights[n].Length; o++)
                                                    activationValue += network[i][n].Delta * nestedWeightsArray[1][o] + nestedBiasesArray[1][o];

                                        activationValueOuter += Neurode.GetActivationValue(activationValue);
                                    }
                                }

                                if (Neurode.GetActivationValue(activationValueOuter, type) != 0)
                                {
                                    activationValueOuter = 0;

                                    for (int i = 0; i < k; i++)
                                    {
                                        float activationValue = 0;

                                        for (int i = 0; i < network.GetLength(0); i++)
                                            for (int n = 0; n < network.GetLength(1); n++)
                                                for (int o = 0; o < weights[n].Length; o++)
                                                    activationValue += network[i][n].Delta * nestedWeightsArray[2][o] + nestedBiasesArray[2][o];

                                        if (Neurode.GetActivationValue(activationValue, type) != 0)
                                        {

                                            activationValue = 0;

                                            for (int i = 0; i < network.GetLength(0); i++)
                                                for (int n = 0; n < network.GetLength(1); n++)
                                                    for (int o = 0; o < weights[n].Length; o++)
                                                        activationValue += network[i][n].Delta * nestedWeightsArray[3][o] + nestedBiasesArray[3][o];

                                            activationValueOuter += Neurode.GetActivationValue(activationValue);
                                        }
                                    }

                                    delta = Neurode.GetActivationValue(activationValueOuter, type);
                                }
                                else
                                    delta = 0;
                            }

                            if (type == 16)//[k] * ([0][o] [0][o] | [1][o] [1][o])      |        [k] * ([2][o] [2][o] | [3][o] [3][o])     &&      [>>]
                            {
                                float activationValueOuter = 0;

                                for (int i = 0; i < k; i++)
                                {
                                    float activationValue = 0;

                                    for (int i = 0; i < network.GetLength(0); i++)
                                        for (int n = 0; n < network.GetLength(1); n++)
                                            for (int o = 0; o < weights[n].Length; o++)
                                                activationValue += network[i][n].Delta * nestedWeightsArray[0][o] + nestedBiasesArray[0][o];

                                    if (Neurode.GetActivationValue(activationValue, type) != 0)
                                    {

                                        activationValue = 0;

                                        for (int i = 0; i < network.GetLength(0); i++)
                                            for (int n = 0; n < network.GetLength(1); n++)
                                                for (int o = 0; o < weights[n].Length; o++)
                                                    activationValue += network[i][n].Delta * nestedWeightsArray[1][o] + nestedBiasesArray[1][o];

                                        activationValueOuter += Neurode.GetActivationValue(activationValue);
                                    }
                                }

                                if (Neurode.GetActivationValue(activationValueOuter, type) != 0)
                                {
                                    activationValueOuter = 0;

                                    for (int i = 0; i < k; i++)
                                    {
                                        float activationValue = 0;

                                        for (int i = 0; i < network.GetLength(0); i++)
                                            for (int n = 0; n < network.GetLength(1); n++)
                                                for (int o = 0; o < weights[n].Length; o++)
                                                    activationValue += network[i][n].Delta * nestedWeightsArray[2][o] + nestedBiasesArray[2][o];

                                        if (Neurode.GetActivationValue(activationValue, type) != 0)
                                        {

                                            activationValue = 0;

                                            for (int i = 0; i < network.GetLength(0); i++)
                                                for (int n = 0; n < network.GetLength(1); n++)
                                                    for (int o = 0; o < weights[n].Length; o++)
                                                        activationValue += network[i][n].Delta * nestedWeightsArray[3][o] + nestedBiasesArray[3][o];

                                            activationValueOuter += Neurode.GetActivationValue(activationValue);
                                        }
                                    }

                                    delta = Neurode.GetActivationValue(activationValueOuter, type);
                                }
                            }

                            if (type == 17)//[k] * ([0][o] [0][o] | [1][o] [1][o])      |        [k] * ([2][o] [2][o] | [3][o] [3][o])     &&      [...]
                            {
                                float activationValueOuter = 0;

                                for (int i = 0; i < k; i++)
                                {
                                    float activationValue = 0;

                                    for (int i = 0; i < network.GetLength(0); i++)
                                        for (int n = 0; n < network.GetLength(1); n++)
                                            for (int o = 0; o < weights[n].Length; o++)
                                                activationValue += network[i][n].Delta * nestedWeightsArray[0][o] + nestedBiasesArray[0][o];

                                    if (Neurode.GetActivationValue(activationValue, type) != 0)
                                    {

                                        activationValue = 0;

                                        for (int i = 0; i < network.GetLength(0); i++)
                                            for (int n = 0; n < network.GetLength(1); n++)
                                                for (int o = 0; o < weights[n].Length; o++)
                                                    activationValue += network[i][n].Delta * nestedWeightsArray[1][o] + nestedBiasesArray[1][o];

                                        activationValueOuter += Neurode.GetActivationValue(activationValue);
                                    }
                                }

                                delta = Neurode.GetActivationValue(activationValueOuter, type);

                                if (delta != 0)
                                {
                                    activationValueOuter = 0;

                                    for (int i = 0; i < k; i++)
                                    {
                                        float activationValue = 0;

                                        for (int i = 0; i < network.GetLength(0); i++)
                                            for (int n = 0; n < network.GetLength(1); n++)
                                                for (int o = 0; o < weights[n].Length; o++)
                                                    activationValue += network[i][n].Delta * nestedWeightsArray[2][o] + nestedBiasesArray[2][o];

                                        if (Neurode.GetActivationValue(activationValue, type) != 0)
                                        {

                                            activationValue = 0;

                                            for (int i = 0; i < network.GetLength(0); i++)
                                                for (int n = 0; n < network.GetLength(1); n++)
                                                    for (int o = 0; o < weights[n].Length; o++)
                                                        activationValue += network[i][n].Delta * nestedWeightsArray[3][o] + nestedBiasesArray[3][o];

                                            activationValueOuter += Neurode.GetActivationValue(activationValue);
                                        }
                                    }

                                    delta = Neurode.GetActivationValue(activationValueOuter, type);
                                }
                            }
                        }   


    public void Reset(System.Random randomGen)
    {
        delta = 0;


        if (localType == 0 || localType == 1 || localType == 2)// [deviation] | [deviation] 
            for (int i = 0; i < vectorCount; i++)
            {
                bias[i] = GetInitialValue(type);
                weight[i] = GetInitialValue(type);
            }

        if (localType == 3 || localType == 4 || localType == 5 || localType == 12 || localType == 13 || localType == 14 || localType == 6 || localType == 7 || localType == 8 || localType == 15 || localType == 16 || localType == 17)// [switchCount][deviation] | [switchCount][deviation] 
        {
            int switchCount = 0;

            if (localType == 3 || localType == 4 || localType == 5 || localType == 12 || localType == 13 || localType == 14)
                switchCount = 2;
            if (localType == 6 || localType == 7 || localType == 8 || localType == 15 || localType == 16 || localType == 17)
                switchCount = 4;

            for (int i = 0; i < switchCount; i++)
                for (int n = 0; n < vectorCount; n++)
                {
                    biasVector[i][n] = GetInitialValue(type);
                    weightMatrix[i][n] = GetInitialValue(type);
                }
        }

    }

    public void MergeInToThis(Neurode partnerNeurode, MergeType type, System.Random randomGen)
    {
        delta = 0;

        if (chromosomeCount == 0)
        {
            if (type == MergeType.Merge)
            {
                for (int n = 0; n < partnerNeurode.Chromosomes.GetLength(0); n++)
                    for (int i = 0; i < partnerNeurode.Chromosomes.GetLength(1); i++)
                        chromosomes[n][i] = (chromosomes[n][i] + partnerNeurode.chromosomes[n][i]) / 2f;
            }

            if (type == MergeType.Schuffle)
            {
                int schuffleOrder = randomGen.Next(0, 1073741823 / 2);

                for (int n = 0; n < partnerNeurode.Chromosomes.GetLength(0); n++)
                    for (int i = 0; i < partnerNeurode.Chromosomes.GetLength(1); i++)
                        if ((i + schuffleOrder) % 2 == 0)
                            chromosomes[n][i] = partnerNeurode.chromosomes[n][i];
            }

            if (type == MergeType.Lerp)
            {
                float t = .75f;

                for (int n = 0; n < partnerNeurode.Chromosomes.GetLength(0); n++)
                    for (int i = 0; i < partnerNeurode.Chromosomes.GetLength(1); i++)
                        chromosomes[n][i] = chromosomes[n][i] + t * (partnerNeurode.chromosomes[n][i] - chromosomes[n][i]);
            }
        }
        else
        {
            if (type == MergeType.Merge)
                for (int i = 0; i < partnerNeurode.Weight.Length; i++)
                {
                    bias[i] = (partnerNeurode.Bias[i] + bias[i]) / 2f;
                    weight[i] = (partnerNeurode.Weight[i] + weight[i]) / 2f;
                }

            if (type == MergeType.Schuffle)
            {
                int schuffleOrder = randomGen.Next(0, 1073741823 / 2);

                for (int i = 0; i < partnerNeurode.Weight.Length; i++)
                    if ((i + schuffleOrder) % 2 == 0)
                    {
                        bias[i] = (partnerNeurode.Bias[i] + bias[i]) / 2f;
                        weight[i] = (partnerNeurode.Weight[i] + weight[i]) / 2f;
                    }
            }

            if (type == MergeType.Lerp)
            {
                float t = .75f;
                for (int i = 0; i < partnerNeurode.Weight.Length; i++)
                {
                    bias[i] = bias[i] + t * (partnerNeurode.bias[i] - bias[i]);
                    weight[i] = weight[i] + t * (partnerNeurode.weight[i] - weight[i]);
                }
            }
        }

        ClampBiases();
    }

    public void AlterNeurode(Neurode baseNeurode, float alternation, System.Random randomGen)
    {
        float magnitude = (float)(.2d + randomGen.NextDouble()) * alternation;

        if (chromosomeCount == 0)
        {
            for (int n = 0; n < baseNeurode.Chromosomes.GetLength(0); n++)
                for (int i = 0; i < baseNeurode.Chromosomes.GetLength(1); i++)
                    chromosomes[n][i] = baseNeurode.Chromosomes[n][i] + (.5f - (float)randomGen.NextDouble()) * magnitude;
        }
        else
            {
                for (int i = 0; i < bias.Length; i++)
                {
                    if (randomGen.NextDouble() > alternation)
                    {
                        bias[i] = baseNeurode.bias[i];
                        weight[i] = baseNeurode.weight[i];
                        continue;
                    }

                    bias[i] = baseNeurode.Bias[i] + (.5f - (float)randomGen.NextDouble()) * magnitude;
                    weight[i] = baseNeurode.Weight[i] + (.5f - (float)randomGen.NextDouble()) * magnitude;
                }
            }

        ClampBiases();
    }

    public void ClampBiases(Neurode partnerNeurode)
    {
        if (ChromosomeCount == 0)
        {
            for (int n = 0; n < chromosomes.GetLength(0); n++)
                for (int i = 0; i < chromosomes.GetLength(1); i++)
                {
                    if (chromosomes[n][i] > 1f)
                        chromosomes[n][i] = 1f;
                        else
                    if (chromosomes[n][i] < -1f)
                        chromosomes[n][i] = -1f;

                    if (chromosomes[n][i] > 1f)
                        chromosomes[n][i] = 1f;
                        else
                    if (chromosomes[n][i] < -1f)
                        chromosomes[n][i] = -1f;
                }
        }
        else
            for (int i = 0; i < partnerNeurode.Weight.Length; i++)
            {
                if (bias[i] > 1f)
                    bias[i] = 1f;
                    else 
                if (bias[i] < -1f)
                    bias[i] = -1f;

                if (weight[i] > 1f)
                    weight[i] = 1f;
                    else 
                if (weight[i] < -1f)
                    weight[i] = -1f;
            }
    }

    public CNeurode Merge(Neurode neurodeA, Neurode neurodeB, MergeType type)
    {
        throw new System.NotImplementedException();
    }
    public void MergeInToThis(Neurode partnerNeurode, MergeType type)
    {
        throw new System.NotImplementedException();
    }
}
