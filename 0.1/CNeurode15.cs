using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static CNeurode;

public class CNeurode
{
    public enum FanType { Merge, Schuffle, Lerp, Alter }
    public enum MergeType { Merge, Schuffle, Lerp, Alter }
    public enum NeurodeType { ReLuNeurode, TanNeurode, SigmoidNeurode, ShortMemoryNeurode, MemoryNeurode, Seed, Feedforward, FeedforwardMemory }

    private NeurodeType type;
    private int localType = 0;

    private int vectorCount = 0;
    private int switchCount = 0;

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

    private List<int> neighboors;

    /// <summary>
    /// <(O_O)>
    /// </summary>
    public CNeurode(int localType, System.Random randomGen, NeurodeType type, int magnitude, FanType fanType)
    {
        delta = 0;

        vectorCount = GetFanInValue(magnitude, fanType);

        if (localType == 0 || localType == 1 || localType == 2)
        {
            weight = new float[vectorCount];
            bias = new float[vectorCount];
        }

        if (localType == 3 || localType == 4)
        {
            switchCount = GetSwitchCount(localType);

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
    } //Done
    
    public void Reset(System.Random randomGen)
    {
        delta = 0;

        if (localType == 0 || localType == 1 || localType == 2) 
            for (int v = 0; v < vectorCount; v++)
            {
                bias[v] = GetInitialValue(type);
                weight[v] = GetInitialValue(type);
            }

        if (localType == 3 || localType == 4)
        {
            int switchCount = GetSwitchCount(localType);

            for (int s = 0; s < switchCount; s++)
                for (int v = 0; v < vectorCount; v++)
                {
                    biasVector[s][v] = GetInitialValue(type);
                    weightMatrix[s][v] = GetInitialValue(type);
                }
        }
    } //Done!


    /// <summary>
    /// magnitude = layer size,  neighboor count,    magic number
    /// </summary>
    public int GetFanInValue(int magnitude, int multiplyer,FanType type) //  int multiplyer = 1
    {
        return Math.Sqrt(2 / magnitude) * multiplyer;
    } 

    public float GetInitialValue(int magnitude, NeurodeType type)//layer????
    {
        if (type == Neurode.ReLuNeurode || type == Neurode.SigmoidNeurode)
        {
            double stdDev = Math.Sqrt(2.0 / fanIn);
            return (float)(randomGen.NextDouble() * 2 * stdDev - stdDev);
        }

        if (type == Neurode.TanNeurode)
        {
            double stdDev = Math.Sqrt(1.0 / fanIn);
            return (float)(randomGen.NextDouble() * 2 * stdDev - stdDev);
        }

        return throw new ArgumentException("Invalid Type value. Is not supported.", nameof(type));
    }

    public float GetActivationValue(float activationValue, NeurodeType type, bool useThresholdForActivation)
    {
        if (useThresholdForActivation)
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



    public void RunForward(CNeurode[][] network)
    {
        if (localType == 0)
        {
            if (isMemoryNeurode)
            {
                float activationValue = 0;

                for (int i = 0; i < network.GetLength(0); n++)
                    for (int n = 0; n < network.GetLength(1); n++)
                        for (int v = 0; v < vectorCount; v++)
                            activationValue += network[i][n].delta * weight[v] + bias[v];

                activationValue = GetActivationValue(activationValue, type);

                if (activationValue != 0)
                    delta = activationValue;
            }
            else
            {
                float activationValue = 0;

                for (int i = 0; i < network.GetLength(0); n++)
                    for (int n = 0; n < network.GetLength(1); n++)
                        for (int v = 0; v < vectorCount; v++)
                            activationValue += network[i][n].delta * weight[v] + bias[v];

                delta = GetActivationValue(activationValue, type);
            }
        }

        if (localType == 1)//                                                                                        [>|]
        {
            float activationValue = 0;

            for (int i = 0; i < network.GetLength(0); n++)
                for (int n = 0; n < network.GetLength(1); n++)
                    for (int v = 0; v < vectorCount; v++)
                        activationValue += network[i][n].delta * weight[v] + bias[v];

            delta = GetActivationValue(activationValue, type);
        }

        if (localType == 2)//                                                                                        [..]
        {
            float activationValue = 0;

            for (int i = 0; i < network.GetLength(0); n++)
                for (int n = 0; n < network.GetLength(1); n++)
                    for (int v = 0; v < vectorCount; v++)
                        activationValue += neurodes[n].Delta * weightMatrix[0][v] + biasVector[0][v];

            if (GetActivationValue(activationValue, type) != 0)
            {
                for (int i = 0; i < network.GetLength(0); n++)
                    for (int n = 0; n < network.GetLength(1); n++)
                        for (int v = 0; v < vectorCount; v++)
                            activationValue += neurodes[n].Delta * weightMatrix[1][v] + biasVector[1][v];

                delta = GetActivationValue(activationValue, type);
            }
        }

        if (localType == 3)//                                                                                        [>>]
        {
            float activationValue = 0;

            for (int i = 0; i < network.GetLength(0); n++)
                for (int n = 0; n < network.GetLength(1); n++)
                    for (int v = 0; v < vectorCount; v++)
                        activationValue += neurodes[n].Delta * weightMatrix[0][v] + biasVector[0][v];

            delta = GetActivationValue(activationValue, type);

            if (delta != 0)
            {
                for (int i = 0; i < network.GetLength(0); n++)
                    for (int n = 0; n < network.GetLength(1); n++)
                        for (int v = 0; v < vectorCount; v++)
                            activationValue += neurodes[n].Delta * weightMatrix[1][v] + biasVector[1][v];

                delta = GetActivationValue(activationValue, type);
            }
        }
    }//Done

    public void RunForward(CNeurode[] neurodes)
    {
        if (localType == 0)
        {
            if (isMemoryNeurode)
            {
                float activationValue = 0;

                for (int n = 0; n < neurodes.Length; n++)
                    for (int v = 0; v < vectorCount; v++)
                        activationValue += neurodes[n].delta * weight[v] + bias[v];

                activationValue = GetActivationValue(activationValue, type);

                if (activationValue != 0)
                    delta = activationValue;
            }
            else
            {
                float activationValue = 0;

                for (int n = 0; n < neurodes.Length; n++)
                    for (int v = 0; v < vectorCount; v++)
                        activationValue += neurodes[n].Delta * weight[v] + bias[v];

                delta = GetActivationValue(activationValue, type);
            }
        }

        if (localType == 1)//                                                                                        [>|]
        {
            float activationValue = 0;

            for (int n = 0; n < neurodes.Length; n++)
                for (int v = 0; v < vectorCount; v++)
                    activationValue += neurodes[n].Delta * weight[v] + bias[v];

            delta = GetActivationValue(activationValue, type);
        }

        if (localType == 2)//                                                                                        [..]
        {
            float activationValue = 0;

            for (int n = 0; n < neurodes.Length; n++)
                for (int v = 0; v < vectorCount; v++)
                    activationValue += neurodes[n].Delta * weightMatrix[0][v] + biasVector[0][v];

            if (GetActivationValue(activationValue, type) != 0)
            {
                for (int n = 0; n < neurodes.Length; n++)
                    for (int v = 0; v < vectorCount; v++)
                        activationValue += neurodes[n].Delta * weightMatrix[1][v] + biasVector[1][v];

                delta = GetActivationValue(activationValue, type);
            }
        }

        if (localType == 3)//                                                                                        [>>]
        {
            float activationValue = 0;

            for (int n = 0; n < neurodes.Length; n++)
                for (int v = 0; v < vectorCount; v++)
                    activationValue += neurodes[n].Delta * weightMatrix[0][v] + biasVector[0][v];

            delta = GetActivationValue(activationValue, type);

            if (delta != 0)
            {
                for (int n = 0; n < neurodes.Length; n++)
                    for (int v = 0; v < vectorCount; v++)
                        activationValue += neurodes[n].Delta * weightMatrix[1][v] + biasVector[1][v];

                delta = GetActivationValue(activationValue, type);
            }
        }
    } //Done

    public void RunForward(List<CNeurode> neurodes)
    {
        if (localType == 0)
        {
            if (isMemoryNeurode)
            {
                float activationValue = 0;

                for (int n = 0; n < neurodes.Count; n++)
                    for (int v = 0; v < vectorCount; v++)
                        activationValue += neurodes[n].delta * weight[v] + bias[v];

                activationValue = GetActivationValue(activationValue, type);

                if (activationValue != 0)
                    delta = activationValue;
            }
            else
                {
                    float activationValue = 0;

                    for (int n = 0; n < neurodes.Count; n++)
                        for (int v = 0; v < vectorCount; v++)
                            activationValue += neurodes[n].Delta * weight[v] + bias[v];

                    delta = GetActivationValue(activationValue, type);
                }
        }

        if (localType == 1)//                                                                                        [>|]
        {
            float activationValue = 0;

            for (int n = 0; n < neurodes.Count; n++)
                for (int v = 0; v < vectorCount; v++)
                    activationValue += neurodes[n].Delta * weight[v] + bias[v];

            delta = GetActivationValue(activationValue, type);
        }

        if (localType == 2)//                                                                                        [..]
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

        if (localType == 3)//                                                                                        [>>]
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
    }//Done



    public void MergeInToThis(CNeurode partnerNeurode, MergeType type, System.Random randomGen)
    {
        delta = 0;

        if (localType == 3 || localType == 4)
        {
            if (type == MergeType.Merge)
            {
                for (int s = 0; s < switchCount; s++)
                    for (int v = 0; v < vectorCount; v++)
                    {
                        weightMatrix[s][v] = (weightMatrix[s][v] + partnerNeurode.WeightMatrix[s][v]) / 2f;
                        biasVector[s][v] = (biasVector[s][v] + partnerNeurode.BiasVector[s][v]) / 2f;
                    }
            }

            if (type == MergeType.Schuffle)
            {
                int schuffleOrder = randomGen.Next(0, 1073741823 / 2);

                for (int s = 0; s < switchCount; s++)
                    for (int v = 0; v < vectorCount; v++)
                    {
                        weightMatrix[s][v] = partnerNeurode.WeightMatrix[s][v];
                        biasVector[s][v] = partnerNeurode.BiasVector[s][v];
                    }
            }

            if (type == MergeType.Lerp)
            {
                float t = .75f;

                for (int s = 0; s < switchCount; s++)
                    for (int v = 0; v < vectorCount; v++)
                    {
                        weightMatrix[s][v] = weightMatrix[s][v] + t * (partnerNeurode.WeightMatrix[s][v] - weightMatrix[s][v]);
                        biasVector[s][v] = biasVector[s][v] + t * (partnerNeurode.BiasVector[s][v] - biasVector[s][v]);
                    }
            }
        }
        else
        {
            if (type == MergeType.Merge)
            {
                for (int v = 0; v < vectorCount; v++)
                {
                    weight[v] = (weight[v] + partnerNeurode.Weight[v]) / 2f;
                    bias[v] = (weight[v] + partnerNeurode.Weight[v]) / 2f;
                }
            }

            if (type == MergeType.Schuffle)
            {
                int schuffleOrder = randomGen.Next(0, 1073741823 / 2);

                for (int v = 0; v < vectorCount; v++)
                {
                    weight[v] = partnerNeurode.Weight[v];
                    biasVector[v] = partnerNeurode.BiasVector[v];
                }
            }

            if (type == MergeType.Lerp)
            {
                float t = .75f;

                for (int v = 0; v < vectorCount; v++)
                {
                    weight[v] = weight[v] + t * (partnerNeurode.Weight[v] - weight[v]);
                    biasVector[v] = biasVector[v] + t * (partnerNeurode.BiasVector[v] - biasVector[v]);
                }
            }
        }
        
        //ClampBiases();
    }


      public void AlterNeurode(CNeurode baseNeurode, float alternation, System.Random randomGen
    {
        // Buffer.txt       fitness incerease       |------------------|---------|
        for (int v = 0; v < vectorCount; v++)//Smooth
        {
            weight[v] = weight[v] * (.5f - (float)randomGen.NextDouble()) * alternation;
            bias[v] = bias[v] * (.5f - (float)randomGen.NextDouble()) * alternation;
        }

        for (int v = 0; v < vectorCount; v++)//Scale
        {
            weight[v] = weight[v] * (.5f - (float)randomGen.NextDouble()) * alternation;
            bias[v] = bias[v] + (.5f - (float)randomGen.NextDouble()) * alternation;
        }

        for (int v = 0; v < vectorCount; v++)//Noise
        {
            weight[v] = weight[v] + (.5f - (float)randomGen.NextDouble()) * alternation;
            bias[v] = bias[v] + (.5f - (float)randomGen.NextDouble()) * alternation;
        }
    }

    public void AlterNeurode(CNeurode baseNeurode, Buffer buffer, System.Random randomGen, float fitness, float maxFitness)
    {
        if (fitness > previesFitness) {
            fitness = previesFitness;
        }

        // Buffer.txt       fitness incerease       |------------------|---------|
        for (int v = 0; v < vectorCount; v++)//Smooth
        {
            weight[v] = weight[v] * (.5f - (float)randomGen.NextDouble()) * buffer.Compute(1,  previesFitness / maxFitness);
            bias[v] = bias[v] * (.5f - (float)randomGen.NextDouble()) * alternation;
        }

        for (int v = 0; v < vectorCount; v++)//Scale
        {
            weight[v] = weight[v] * (.5f - (float)randomGen.NextDouble()) * alternation;
            bias[v] = bias[v] + (.5f - (float)randomGen.NextDouble()) * alternation;
        }

        for (int v = 0; v < vectorCount; v++)//Noise
        {
            weight[v] = weight[v] + (.5f - (float)randomGen.NextDouble()) * alternation;
            bias[v] = bias[v] + (.5f - (float)randomGen.NextDouble()) * alternation;
        }
    public void ClampBiases(CNeurode partnerNeurode)
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

    public void MergeInToThis(CNeurode partnerNeurode, MergeType type)
    {
        throw new System.NotImplementedException();
    }

    public CNeurode Merge(CNeurode neurodeA, CNeurode neurodeB, MergeType type)
    {
        throw new System.NotImplementedException();
    }



    private int GetSwitchCount(int localType)
    {
        if (localType == 3 || localType == 4 || localType == 5 || localType == 12 || localType == 13 || localType == 14)
            return 2; 
                else
                    if (localType == 6 || localType == 7 || localType == 8 || localType == 15 || localType == 16 || localType == 17)
                        return 4; 
                            else
                                throw new ArgumentException("Invalid localType value. Is not supported.", nameof(localType));
    }      //Done

    private readonly float lowerThresholdReLu = 0.5f;
    private readonly float upperThresholdReLu = 1.5f;

    private readonly float lowerThresholdSigmoid = 0.25f;
    private readonly float upperThresholdSigmoid = 0.75f;

    private readonly float lowerThresholdTanh = -0.5f;
    private readonly float upperThresholdTanh = 0.5f;
}





  


