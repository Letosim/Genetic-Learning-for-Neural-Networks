using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    private bool[] wasActive;
    public bool[] WasActive { get { return wasActive; } set { wasActive = value; } }


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
    private List<CNeurode> depthNeurodes;

    private readonly float lowerThresholdReLu = 0.5f;
    private readonly float upperThresholdReLu = 1.5f;

    private readonly float lowerThresholdSigmoid = 0.25f;
    private readonly float upperThresholdSigmoid = 0.75f;

    private readonly float lowerThresholdTanh = -0.5f;
    private readonly float upperThresholdTanh = 0.5f;

    private LayerCube cube { get; set; }  = null;


    /// <summary>
    /// <(O_O)>
    /// </summary>
    public CNeurode(int localType, System.Random randomGen, NeurodeType type, int magnitude, FanType fanType)
    {
        delta = 0;

        vectorCount = GetFanInValue(magnitude, 1, fanType, randomGen);

        if (localType == 0 || localType == 1 || localType == 2)
        {
            weight = new float[vectorCount];
            bias = new float[vectorCount];
        }

        if (localType == 3 || localType == 4)
        {
            switchCount = GetSwitchCount(localType);

            if (switchCount == 2)
                wasActive = new bool[switchCount];

            weightMatrix = new float[switchCount][];
            biasVector = new float[switchCount][];

            for (int i = 0; i < switchCount; i++)
            {
                weightMatrix[i] = new float[vectorCount];
                biasVector[i] = new float[vectorCount];

                for (int j = 0; j < vectorCount; j++)
                {
                    weightMatrix[i][j] = GetInitialValue(magnitude, type, randomGen);
                    biasVector[i][j] = GetInitialValue(magnitude, type, randomGen);
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
                bias[v] = GetInitialValue(1, type, randomGen);
                weight[v] = GetInitialValue(1, type, randomGen);
            }

        if (localType == 3 || localType == 4)
        {
            int switchCount = GetSwitchCount(localType);

            for (int s = 0; s < switchCount; s++)
                for (int v = 0; v < vectorCount; v++)
                {
                    biasVector[s][v] = GetInitialValue(1, type, randomGen);
                    weightMatrix[s][v] = GetInitialValue(1, type, randomGen);
                }
        }
    } //Done!

    public int GetFanInValue(int magnitude, int multiplyer, FanType type, System.Random randomGen)
    {
        return (int)System.Math.Sqrt(2 / magnitude) * multiplyer;
    }

    public float GetInitialValue(int magnitude, NeurodeType type, System.Random randomGen)//layer????
    {
        if (type == NeurodeType.ReLuNeurode || type == NeurodeType.SigmoidNeurode)
        {
            double stdDev = System.Math.Sqrt(2.0 / magnitude);
            return (float)(randomGen.NextDouble() * 2 * stdDev - stdDev);
        }

        if (type == NeurodeType.TanNeurode)
        {
            double stdDev = System.Math.Sqrt(1.0 / magnitude);
            return (float)(randomGen.NextDouble() * 2 * stdDev - stdDev);
        }

        return -1f;
    }

    public float GetActivationValue(float activationValue, NeurodeType type, bool useThresholdForActivation)
    {
        if (useThresholdForActivation)
        {
            if (type == NeurodeType.ReLuNeurode)
            {
                activationValue = System.Math.Max(0, activationValue);

                if (activationValue >= lowerThresholdReLu && activationValue <= upperThresholdReLu)
                    return activationValue;
                else
                    return 0;
            }

            if (type == NeurodeType.SigmoidNeurode)
            {
                activationValue = activationValue / (1f + activationValue);

                if (activationValue >= lowerThresholdSigmoid && activationValue <= upperThresholdSigmoid)
                    return activationValue;
                else
                    return 0;
            }

            if (type == NeurodeType.TanNeurode)
            {
                activationValue = (float)System.Math.Tanh(activationValue);

                if (activationValue >= lowerThresholdTanh && activationValue <= upperThresholdTanh)
                    return activationValue;
                else
                    return 0;
            }
        }
        else
        {
            if (type == NeurodeType.ReLuNeurode)
            {
                activationValue = (float)System.Math.Max(0, activationValue);

                if (activationValue > 1)
                    return activationValue;
                else
                    return 0;
            }

            if (type == NeurodeType.SigmoidNeurode)
            {
                activationValue = activationValue / (1f + activationValue);

                if (activationValue > 1)
                    return activationValue;
                else
                    return 0;
            }

            if (type == NeurodeType.TanNeurode)
            {
                activationValue = (float)System.Math.Tanh(activationValue);

                if (activationValue > 1)
                    return activationValue;
                else
                    return 0;
            }
        }

        return -1;
    }

    public void RunForward(CNeurode[][] network, bool useThershold, NeurodeType type)
    {
        if (localType == 0)
        {
            if (isMemoryNeurode)
            {
                float activationValue = 0;

                for (int i = 0; i < network.GetLength(0); i++)
                    for (int n = 0; n < network.GetLength(1); n++)
                        for (int v = 0; v < vectorCount; v++)
                            activationValue += network[i][n].delta * weight[v] + bias[v];

                activationValue = GetActivationValue(activationValue, type, useThershold);

                if (activationValue != 0)
                    delta = activationValue;
            }
            else
            {
                float activationValue = 0;

                for (int i = 0; i < network.GetLength(0); i++)
                    for (int n = 0; n < network.GetLength(1); n++)
                        for (int v = 0; v < vectorCount; v++)
                            activationValue += network[i][n].delta * weight[v] + bias[v];

                delta = GetActivationValue(activationValue, type, useThershold);
            }
        }

        if (localType == 1)//                                                                                        [>|]
        {
            float activationValue = 0;

            for (int i = 0; i < network.GetLength(0); i++)
                for (int n = 0; n < network.GetLength(1); n++)
                    for (int v = 0; v < vectorCount; v++)
                        activationValue += network[i][n].delta * weight[v] + bias[v];

            delta = GetActivationValue(activationValue, type, useThershold);
        }

        if (localType == 2)//                                                                                        [..]
        {
            float activationValue = 0;

            for (int i = 0; i < network.GetLength(0); i++)
                for (int n = 0; n < network.GetLength(1); n++)
                    for (int v = 0; v < vectorCount; v++)
                        activationValue += network[i][n].Delta * weightMatrix[0][v] + biasVector[0][v];

            if (GetActivationValue(activationValue, type, useThershold) != 0)
            {
                for (int i = 0; i < network.GetLength(0); i++)
                    for (int n = 0; n < network.GetLength(1); n++)
                        for (int v = 0; v < vectorCount; v++)
                            activationValue += network[i][n].Delta * weightMatrix[1][v] + biasVector[1][v];

                delta = GetActivationValue(activationValue, type, useThershold);
            }
        }

        if (localType == 3)//                                                                                        [>>  ||  |>]
        {
            float activationValue = 0;

            for (int i = 0; i < network.GetLength(0); i++)
                for (int n = 0; n < network.GetLength(1); n++)
                    for (int v = 0; v < vectorCount; v++)
                        activationValue += network[i][n].Delta * weightMatrix[0][v] + biasVector[0][v];

            delta = GetActivationValue(activationValue, type, useThershold);

            if (delta != 0)
            {

                activationValue = 0;

                for (int i = 0; i < network.GetLength(0); i++)
                    for (int n = 0; n < network.GetLength(1); n++)
                        for (int v = 0; v < vectorCount; v++)
                            activationValue += network[i][n].Delta * weightMatrix[1][v] + biasVector[1][v];

                delta = GetActivationValue(activationValue, type, useThershold);

            }
        }

        if (localType == 4)//                                                                                         [>>  ||  |>]
        {
            float activationValue = 0;

            for (int i = 0; i < network.GetLength(0); i++)
                for (int n = 0; n < network.GetLength(1); n++)
                    for (int v = 0; v < vectorCount; v++)
                        activationValue += network[i][n].Delta * weightMatrix[0][v] + biasVector[0][v];

            delta = GetActivationValue(activationValue, type, useThershold);

            if (delta != 0)
            {
                if (cube.IsRunning)
                    delta = GetActivationValue(cube.Vector, type, useThershold);
                else
                    cube.Reset(delta);
            }

            if (cube.IsRunning)
                cube.Update();

        }
    }//Done

    public void RunForward(CNeurode[][] network, bool useThershold, NeurodeType type)
    {
        if (localType == 5)
        {
            if (isMemoryNeurode)//outputlayer out > in
            {
                float activationValue = 0;
                wasActive = false;

                for (int i = 0; i < network.GetLength(0); i++)
                    for (int n = 0; n < network.GetLength(1); n++)
                        if (network[i][n].WasActive[0])
                            activationValue += 1;

                activationValue = GetActivationValue(activationValue, type, useThershold);

                if (activationValue != 0)
                    delta = activationValue;
            }
            else
            {
                float activationValue = 0;

                for (int i = 0; i < network.GetLength(0); i++)
                    for (int n = 0; n < network.GetLength(1); n++)
                        if (network[i][n].WasActive[0])
                            activationValue += 1;

                delta = GetActivationValue(activationValue, type, useThershold);
            }
        }

        if (localType == 1)//                                                                                        [>|]
        {
            float activationValue = 0;
            wasActive = false;

            for (int i = 0; i < network.GetLength(0); i++)
                for (int n = 0; n < network.GetLength(1); n++)
                        if (network[i][n].WasActive[0])
                            activationValue += 1;

            delta = GetActivationValue(activationValue, type, useThershold);
        }

        if (localType == 2)//                                                                                        [..]
        {
            float activationValue = 0;
            wasActive = false;

            for (int i = 0; i < network.GetLength(0); i++)
                for (int n = 0; n < network.GetLength(1); n++)
                    if (network[i][n].WasActive[0])
                        activationValue += 1;
            
            if (GetActivationValue(activationValue, type, useThershold) != 0)
            {
                for (int i = 0; i < network.GetLength(0); i++)
                    for (int n = 0; n < network.GetLength(1); n++)
                        if (network[i][n].WasActive[1])
                            activationValue += 1;

                delta = GetActivationValue(activationValue, type, useThershold);
            }
        }

        if (localType == 3)//                                                                                        [>>  ||  |>]
        {
            float activationValue = 0;
            wasActive = false;

            for (int i = 0; i < network.GetLength(0); i++)
                for (int n = 0; n < network.GetLength(1); n++)
                    if (network[i][n].WasActive[0])
                        activationValue += 1;

            delta = GetActivationValue(activationValue, type, useThershold);

            if (delta != 0)
            {
                activationValue = 0;
                wasActive = true;

                for (int i = 0; i < network.GetLength(0); i++)
                    for (int n = 0; n < network.GetLength(1); n++)
                        if (network[i][n].WasActive[1])
                            activationValue += 1;

                delta = GetActivationValue(activationValue, type, useThershold);

            }
        }
        
        if (localType == 4)//                                                                                         [>>  ||  |>]
        {
            float activationValue = 0;
            wasActive = false;

            for (int i = 0; i < network.GetLength(0); i++)
                for (int n = 0; n < network.GetLength(1); n++)
                        if (network[i][n].WasActive[0])
                            activationValue += 1;

            delta = GetActivationValue(activationValue, type, useThershold);

            if (delta != 0)
            {
                wasActive = true;

                if (cube.IsRunning)
                    delta = GetActivationValue(cube.Vector, type, useThershold);
                else
                    cube.Reset(delta);
            }

            if (cube.IsRunning)
                cube.Update();

        }
    }

 public void RunForward( bool useThershold, NeurodeType type)
    {
        if (localType == 5)
        {
            if (isMemoryNeurode)//outputlayer out > in
            {
                float activationValue = 0;
                wasActive = false;

                for (int i = 0; i < network.GetLength(0); i++)
                    for (int n = 0; n < network.GetLength(1); n++)
                        if (network[i][n].WasActive[0])
                            activationValue += 1;

                activationValue = GetActivationValue(activationValue, type, useThershold);

                if (activationValue != 0)
                    delta = activationValue;
            }
            else
            {
                float activationValue = 0;

                for (int i = 0; i < network.GetLength(0); i++)
                    for (int n = 0; n < network.GetLength(1); n++)
                        if (network[i][n].WasActive[0])
                            activationValue += 1;
              
//                 *
//                 |          
//Neighbors.Add()-----*
//                 |    
//                 *


// input|> this.run.neighbours.run... |> out
{  
float activationVue = 0;

 for(int i = 0; i < neighbours.Count; i++)
      for(int v = 0; v < vectorCount; v++)
{ 
     activationVue += neighbours_.[i].delta * weightMatrix[0][v] + weightMatrix[0][v];
     neighbours_.CalculateDelta();//[][] + List vector2 hmm |vector2 + Vector3 VR?
}
delta = Getactivationvalue(delta, type, useThershold);

if(delta != 0)
{
float activationVue = 0;

for(int i = 0; i < neighbours.Count; i++)
      for(int v = 0; v < vectorCount; v++)
     activationVue += neighbours_.[i].delta * weightMatrix[1][v] + weightMatrix[1][v];
       neighbours_.CalculateDelta();

delta = Getactivationvalue(delta, type, useThershold);
}
}
}



             
//                 
//                 |          
//Neighbors.Add()-----<
//                 |    
//                 ^


// input|> this.run.neighbours.run... |> out
{  
float activationVue = 0;

 for(int i = 0; i < neighbours.Count; i++)
      for(int v = 0; v < vectorCount; v++) 
     activationVue += neighbours_.[i].delta * weightMatrix[0][v] + weightMatrix[0][v];
       neighbours_.CalculateDelta();

delta = Getactivationvalue(delta, type, useThershold);

if(delta != 0)
{
float activationVue = 0;

for(int i = 0; i < neighbours.Count; i++)
      for(int v = 0; v < vectorCount; v++)
     activationVue += neighbours_.[i].delta * weightMatrix[1][v] + weightMatrix[1][v];
       neighbours_.CalculateDelta();

delta = Getactivationvalue(delta, type, useThershold);
}
}
}









                delta = GetActivationValue(activationValue, type, useThershold);
            }
        }

        if (localType == 1)//                                                                                        [>|]
        {
            float activationValue = 0;
            wasActive = false;

            for (int i = 0; i < network.GetLength(0); i++)
                for (int n = 0; n < network.GetLength(1); n++)
                        if (network[indices[i]][indices[n]].WasActive[0])
                            activationValue += 1;

            delta = GetActivationValue(activationValue, type, useThershold);
        }

        if (localType == 2)//                                                                                        [..]
        {
            float activationValue = 0;
            wasActive = false;

            for (int i = 0; i < network.GetLength(0); i++)
                for (int n = 0; n < network.GetLength(1); n++)
                    if (network[i][n].WasActive[0])
                        activationValue += 1;
            
            if (GetActivationValue(activationValue, type, useThershold) != 0)
            {
                for (int i = 0; i < network.GetLength(0); i++)
                    for (int n = 0; n < network.GetLength(1); n++)
                        if (network[i][n].WasActive[1])
                            activationValue += 1;

                delta = GetActivationValue(activationValue, type, useThershold);
            }
        }

        if (localType == 3)//                                                                                        [>>  ||  |>]
        {
            float activationValue = 0;
            wasActive = false;

            for (int i = 0; i < network.GetLength(0); i++)
                for (int n = 0; n < network.GetLength(1); n++)
                    if (network[i][n].WasActive[0])
                        activationValue += 1;

            delta = GetActivationValue(activationValue, type, useThershold);

            if (delta != 0)
            {
                activationValue = 0;
                wasActive = true;

                for (int i = 0; i < network.GetLength(0); i++)
                    for (int n = 0; n < network.GetLength(1); n++)
                        if (network[i][n].WasActive[1])
                            activationValue += 1;

                delta = GetActivationValue(activationValue, type, useThershold);

            }
        }
        
        if (localType == 4)//                                                                                         [>>  ||  |>]
        {
            float activationValue = 0;
            wasActive = false;

            for (int i = 0; i < network.GetLength(0); i++)
                for (int n = 0; n < network.GetLength(1); n++)
                        if (network[i][n].WasActive[0])
                            activationValue += 1;

            delta = GetActivationValue(activationValue, type, useThershold);

            if (delta != 0)
            {
                wasActive = true;

                if (cube.IsRunning)
                    delta = GetActivationValue(cube.Vector, type, useThershold);
                else
                    cube.Reset(delta);
            }

            if (cube.IsRunning)
                cube.Update();

        }
    }



    public void RunForward(List<CNeurode> neurodes, bool useThershold)
    {
        if (localType == 0)
        {
            if (isMemoryNeurode)
            {
                float activationValue = 0;

                for (int n = 0; n < neurodes.Count; n++)
                    for (int v = 0; v < vectorCount; v++)
                        activationValue += neurodes[n].delta * weight[v] + bias[v];

                activationValue = GetActivationValue(activationValue, type, useThershold);

                if (activationValue != 0)
                    delta = activationValue;
            }
            else
            {
                float activationValue = 0;

                for (int n = 0; n < neurodes.Count; n++)
                    for (int v = 0; v < vectorCount; v++)
                        activationValue += neurodes[n].Delta * weight[v] + bias[v];

                delta = GetActivationValue(activationValue, type, useThershold);
            }
        }

        if (localType == 1)//                                                                                        [>|]
        {
            float activationValue = 0;

            for (int n = 0; n < neurodes.Count; n++)
                for (int v = 0; v < vectorCount; v++)
                    activationValue += neurodes[n].Delta * weight[v] + bias[v];

            delta = GetActivationValue(activationValue, type, useThershold);
        }

        if (localType == 2)//                                                                                        [..]
        {
            float activationValue = 0;

            for (int n = 0; n < neurodes.Count; n++)
                for (int v = 0; v < vectorCount; v++)
                    activationValue += neurodes[n].Delta * weightMatrix[0][v] + biasVector[0][v];

            if (GetActivationValue(activationValue, type, useThershold) != 0)
            {
                for (int n = 0; n < neurodes.Count; n++)
                    for (int v = 0; v < vectorCount; v++)
                        activationValue += neurodes[n].Delta * weightMatrix[1][v] + biasVector[1][v];

                delta = GetActivationValue(activationValue, type, useThershold);
            }
        }

        if (localType == 3)////                                                                                        [>>  ||  |>]
        {
            float activationValue = 0;

            for (int n = 0; n < neurodes.Count; n++)
                for (int v = 0; v < vectorCount; v++)
                    activationValue += neurodes[n].Delta * weightMatrix[0][v] + biasVector[0][v];

            delta = GetActivationValue(activationValue, type, useThershold);

            if (delta != 0)
            {
                for (int n = 0; n < neurodes.Count; n++)
                    for (int v = 0; v < vectorCount; v++)
                        activationValue += neurodes[n].Delta * weightMatrix[1][v] + biasVector[1][v];

                delta = GetActivationValue(activationValue, type, useThershold);
            }
        }

        if (localType == 4)//                                                                                        [>>  ||  |>]
        {
            float activationValue = 0;

            for (int n = 0; n < neurodes.Count; n++)
                for (int v = 0; v < vectorCount; v++)
                    activationValue += neurodes[n].Delta * weightMatrix[0][v] + biasVector[0][v];

            delta = GetActivationValue(activationValue, type, useThershold);

            if (delta != 0)
            {
                if (cube.IsRunning)
                    delta = GetActivationValue(cube.Vector, type, useThershold);
                else
                    cube.Reset(delta);
            }

            if (cube.IsRunning)
                cube.Update();
        }
    }//Done

    float previesFitness;

    public void AlterNeurode(CNeurode baseNeurode, float alternation, System.Random randomGen)
    {
        //Buffer.txt                    fitness incerease                   |------------------|---------|
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

    public void AlterNeurode(CNeurode baseNeurode, Buffer buffer, System.Random randomGen, float magnitude, float maxFitness) 
    {
        if (magnitude > previesFitness)
        {
            previesFitness = magnitude;
        }

        //Buffer.txt                    fitness incerease                   |------------------|---------|
        for (int v = 0; v < vectorCount; v++)//Smooth
        {
            weight[v] = weight[v] * (.5f - (float)randomGen.NextDouble()) * buffer.Compute(1, previesFitness / maxFitness);
            bias[v] = bias[v] * (.5f - (float)randomGen.NextDouble()) * buffer.Compute(1, previesFitness / maxFitness);
        }

        for (int v = 0; v < vectorCount; v++)//Scale
        {
            weight[v] = weight[v] * (.5f - (float)randomGen.NextDouble()) * magnitude;
            bias[v] = bias[v] + (.5f - (float)randomGen.NextDouble()) * magnitude;
        }

        for (int v = 0; v < vectorCount; v++)//Noise
        {
            weight[v] = weight[v] + (.5f - (float)randomGen.NextDouble()) * magnitude;
            bias[v] = bias[v] + (.5f - (float)randomGen.NextDouble()) * magnitude;
        }
    }

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
                    bias[v] = bias[v] + t * (partnerNeurode.Bias[v] - bias[v]);
                }
            }
        }


        //      A(weightA weight) *> weight |   //weightA = A * stuff >_>
        //ClampBiases();
    }

    public void ClampBiases(CNeurode partnerNeurode)
    {
        if (localType == 3 || localType == 4)
        {
            for (int v = 0; v < vectorCount; v++)
                for (int s = 0; s < switchCount; s++)
                {
                    if (biasVector[s][v] > 1f)
                        biasVector[s][v] = 1f;
                    else
                        if (biasVector[s][v] < -1f)
                        biasVector[s][v] = -1f;

                    if (weightMatrix[s][v] > 1f)
                        weightMatrix[s][v] = 1f;
                    else
                        if (weightMatrix[s][v] < -1f)
                        weightMatrix[s][v] = -1f;
                }
        }
        else
        {
            for (int v = 0; v < vectorCount; v++)
            {
                if (bias[v] > 1f)
                    bias[v] = 1f;
                else
                    if (bias[v] < -1f)
                    bias[v] = -1f;

                if (weight[v] > 1f)
                    weight[v] = 1f;
                else
                    if (weight[v] < -1f)
                    weight[v] = -1f;
            }
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
            return -1;
    }      //Done
}
