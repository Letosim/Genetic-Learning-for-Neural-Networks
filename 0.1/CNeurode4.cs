﻿using UnityEngine;

public class CNeurode : Neurode
{
  private NeurodeType type;
  private int localType = 0;
  private bool isMemoryNeurode;

  // parallel acces;
  public float delta;
  public float[] weight;
  public float[] bias;


  public override NeurodeType Type { get { return type; } set { type = value; } }
  public override bool IsMemoryNeurode { get { return isMemoryNeurode; } set { isMemoryNeurode = value; } }

  // batch acces;
  public float Delta { get { return delta; } set { delta = value; } }
  public float[] Bias { get { return bias; } set { bias = value; } }
  public float[] Weight { get { return weight; } set { weight = value; } }


  // parallel acces;
  public float[] delta1D;

  public float[][] weight2D;
  public float[][] bias2D;

  public float[][][] weight3D;
  public float[][][] bias3D;

  public float[][][][] weight4D;
  public float[][][][] bias4D;

  public float[][][][][] weight5D;
  public float[][][][][] bias5D;


  // batch acces;
  public float[] Delta1D { get { return delta1D; } set { delta1D = value; } }
  public float[] Delta1D { get { return delta1D; } set { delta1D = value; } }

  public float[][] Weight2D { get { return weight2D; } set { weight2D = value; } }
  public float[][] Bias2D { get { return bias2D; } set { bias2D = value; } }

  public float[][][] Weight3D { get { return weight3D; } set { weight3D = value; } }
  public float[][][] Bias3D { get { return bias3D; } set { bias3D = value; } }

  public float[][][][] Weight4D { get { return weight4D; } set { weight4D = value; } }
  public float[][][][] Bias4D { get { return bias4D; } set { bias4D = value; } }

  public float[][][][][] Weight5D { get { return weight5D; } set { weight5D = value; } }
  public float[][][][][] Bias5D { get { return bias5D; } set { bias5D = value; } }


/// <summary>
/// Type 0: Initializes delta, weight[], and bias[].
/// </summary>
public void Initiate(int localType, System.Random randomGen, NeurodeType type, int magnitudeCount)
{
    if (localType == 0)
    {
        float delta = 0;
        float[] weight = new float[magnitudeCount];
        float[] bias = new float[magnitudeCount];
    }
    else
        throw new ArgumentException("Invalid localType value. Only '0' is supported.", nameof(localType));
}

/// <summary>
/// Type 0: Initializes delta, weight[2][2][magnitudeCount], and bias[2][2][magnitudeCount].
/// 
/// *ChatGPT Implementation ---->
/// 
/// weight = [
/// [
///     [0.12, 0.34, 0.56],
///     [0.78, 0.90, 0.21]
/// ],
/// [
///     [0.43, 0.65, 0.87],
///     [0.09, 0.32, 0.54]
/// ]]
/// 
///Bias array would follow the same structure.
///
/// </summary>
public void Initiate(int localType, System.Random randomGen, NeurodeType type, int magnitudeCount)
{
    if (localType == 0)
    {
        float delta = 0;

        if (is3D)
        {
            float[][][] weight = new float[2][][];
            float[][][] bias = new float[2][][];

            for (int i = 0; i < 2; i++)
            {
                weight[i] = new float[2][];
                bias[i] = new float[2][];

                for (int j = 0; j < 2; j++)
                {
                    weight[i][j] = new float[magnitudeCount];
                    bias[i][j] = new float[magnitudeCount];

                    for (int k = 0; k < magnitudeCount; k++)
                    {
                        weight[i][j][k] = (float)randomGen.NextDouble();
                        bias[i][j][k] = (float)randomGen.NextDouble();
                    }
                }
            }
        }
        else
        {
            float[] weight = new float[magnitudeCount];
            float[] bias = new float[magnitudeCount];

            for (int i = 0; i < magnitudeCount; i++)
            {
                weight[i] = (float)randomGen.NextDouble();
                bias[i] = (float)randomGen.NextDouble();
            }
        }
    }
    else
        throw new ArgumentException("Invalid localType value. Only '0' is supported.", nameof(localType));
}
///<---- ChatGPT Implementation*


    public CNeurode(int layer, int parentLayerSize, System.Random randomGen, NeurodeType type)
    {
        this.startIndexSecondGate = parentLayerSize;

        parentLayerSize = parentLayerSize * 2;

        this.layer = layer;
        this.delta = 0;
        this.bias = new float[parentLayerSize];
        this.weight = new float[parentLayerSize];
        this.type = type;

        for (int n = 0; n < parentLayerSize; n++)
        {
            if (type == Neurode.TanNeurode)
            {
                bias[i] = ((float)randomGen.NextDouble() - .5f) * 2f;
                Weight[i] = ((float)randomGen.NextDouble() - .5f) * 2f;
            }
            if (type == Neurode.Sigmoid)
            {
                bias[i] = (float)randomGen.NextDouble() * .5f;
                Weight[i] = (float)randomGen.NextDouble() * .5f;
            }
            if (type == Neurode.Relu)
            {
                bias[i] = (float)randomGen.NextDouble();
                Weight[i] = (float)randomGen.NextDouble();
            }
        }
    }

    public CNeurode(System.Random randomGen, NeurodeType type, int chromosomeCount, bool isMemoryNeurode, int parentLayerSize)
    {
        this.delta = 0;
        this.bias = new float[parentLayerSize];
        this.weight = new float[parentLayerSize];
        this.type = type;
        this.chromosomeCount = chromosomeCount;
        this.isMemoryNeurode = isMemoryNeurode;


        if (isMemoryNeurode)// 
        {

            weights = new float[parentLayerSize][chromosomeCount * 2];
            biases = new float[parentLayerSize][chromosomeCount * 2];
        }
        else
        {

            weights = new float[parentLayerSize][chromosomeCount];
            biases = new float[parentLayerSize][chromosomeCount];

        }

        for (int n = 0; n < parentLayerSize; n++)
            for (int i = 0; i < chromosomeCount; i++)
            {
                if (type == Neurode.TanNeurode)
                    chromosomes[n][i] = ((float)randomGen.NextDouble() - .5f) * 2f;

                if (type == Neurode.Sigmoid)
                    chromosomes[n][i] = (float)randomGen.NextDouble() * .5f;

                if (type == Neurode.Relu)
                    chromosomes[n][i] = (float)randomGen.NextDouble();
            }
    }

    public override void RunForward(Neurode[] parentLayer)
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

    public override void RunForwardNested(Neurode[] parentLayer)
    {

    //if (type == 5)// [0][0][n][o][i] [0][1][n][o][i]          |         [1][0][n][o][i] [1][1][n][o][i]
    //{
    //    float activationValueOuter = 0;

    //    for (int i = 0; i < deltas.Length; i++)
    //    {
    //        float activationValue = 0;

    //        for (int n = 0; n < parentLayer.Length; n++)
    //            for (int o = 0; o < weights[n].Length; o++)
    //                activationValue += parentLayer[n].Delta * nestedWeightsArray[0][n][o] + nestedBiasesArray[1][n][o];

    //        if (Neurode.GetActivationValue(activationValue, type) != 0)
    //        {
    //            for (int n = 0; n < parentLayer.Length; n++)
    //                for (int o = 0; o < weights[n].Length; o++)
    //                    activationValue += parentLayer[n].Delta * nestedWeightsArray[2][n][o] + nestedBiasesArray[3][n][o];

    //            activationValueOuter += activationValue;
    //        }
    //    }



    
        //Exponentiel  0 - 8  ##############################################################################################################################################################################################################

        if (type == 0)//Feedforward needs to feed it self!!!
        {
            float activationValue = 0;

            for (int i = 0; i < parentLayer.Length; i++)
                activationValue += parentLayer[i].delta * weight[i] + bias[i];

            activationValue = Neurode.GetActivationValue(activationValue, type);

            if (activationValue != 0)
                delta = activationValue;
        }

        if (type == 1)//Feedforward Buffered
        {
            float activationValue = 0;

            for (int i = 0; i < parentLayer.Length; i++)
                activationValue += parentLayer[i].delta * weight[i] + bias[i];

            delta = Neurode.GetActivationValue(activationValue, type); //Buffered Value

            if (activationValue != 0)
                delta = activationValue;
        }

        if (type == 2)// [0][n][o]       |      [1][n][o]
        {
            float activationValue = 0;

            for (int n = 0; n < parentLayer.Length; n++)
                for (int o = 0; o < weights[n].Length; o++)
                    activationValue += parentLayer[n].Delta * weightsArray[0][n][o] + biasesArray[0][n][o];

            delta = Neurode.GetActivationValue(activationValue, type);

            if (delta != 0)
            {
                activationValue = 0;

                for (int n = 0; n < parentLayer.Length; n++)
                    for (int o = 0; o < weights[n].Length; o++)
                        activationValue += parentLayer[n].Delta * weightsArray[1][n][o] + biasesArray[1][n][o];

                delta = Neurode.GetActivationValue(activationValue, type);
            }
        }

        if (type == 3)// [0][n][o][i]       |      [1][n][o][i]
        {
            float activationValueOuter = 0;

            for (int i = 0; i < deltas.Length; i++)
            {
                float activationValue = 0;

                for (int n = 0; n < parentLayer.Length; n++)
                    for (int o = 0; o < weights[n].Length; o++)
                        activationValue += parentLayer[n].Delta * nestedWeightsArray[0][n][o][i] + nestedBiasesArray[0][n][o][i];

                if (Neurode.GetActivationValue(activationValue, type) != 0)
                {
                    for (int n = 0; n < parentLayer.Length; n++)
                        for (int o = 0; o < weights[n].Length; o++)
                            activationValue += parentLayer[n].Delta * nestedWeightsArray[1][n][o][i] + nestedBiasesArray[1][n][o][i];

                    activationValueOuter += activationValue;
                }
            }

            delta = Neurode.GetActivationValue(activationValueOuter, type)
        }

        if (type == 4)// [0][n][o][i]Buffered       |      [1][n][o][i]
        {
            float activationValueOuter = 0;

            for (int i = 0; i < deltas.Length; i++)
            {
                float activationValue = 0;

                for (int n = 0; n < parentLayer.Length; n++)
                    for (int o = 0; o < weights[n].Length; o++)
                        activationValue += parentLayer[n].Delta * nestedWeightsArray[0][n][o][i] + nestedBiasesArray[0][n][o][i];

                delta = Neurode.GetActivationValue(activationValueOuter, type);//Buffer

                if (delta != 0)
                {
                    for (int n = 0; n < parentLayer.Length; n++)
                        for (int o = 0; o < weights[n].Length; o++)
                            activationValue += parentLayer[n].Delta * nestedWeightsArray[1][n][o][i] + nestedBiasesArray[1][n][o][i];

                    activationValueOuter += activationValue;
                }
            }

            delta = Neurode.GetActivationValue(activationValueOuter, type)
        }

        if (type == 5)// [0][0][n][o][i] [0][1][n][o][i]          |         [1][0][n][o][i] [1][1][n][o][i]
        {
            float activationValueOuter = 0;

            for (int i = 0; i < deltas.Length; i++)
            {
                float activationValue = 0;

                for (int n = 0; n < parentLayer.Length; n++)
                    for (int o = 0; o < weights[n].Length; o++)
                        activationValue += parentLayer[n].Delta * nestedWeightsArray[0][0][n][o][i] + nestedBiasesArray[0][0][n][o][i];

                if (Neurode.GetActivationValue(activationValue, type) != 0)
                {
                    for (int n = 0; n < parentLayer.Length; n++)
                        for (int o = 0; o < weights[n].Length; o++)
                            activationValue += parentLayer[n].Delta * nestedWeightsArray[0][1][n][o][i] + nestedBiasesArray[0][1][n][o][i];

                    activationValueOuter += activationValue;
                }
            }

            if (Neurode.GetActivationValue(activationValueOuter, type) != 0)
            {
                activationValueOuter = 0;

                for (int i = 0; i < deltas.Length; i++)
                {
                    float activationValue = 0;

                    for (int n = 0; n < parentLayer.Length; n++)
                        for (int o = 0; o < weights[n].Length; o++)
                            activationValue += parentLayer[n].Delta * nestedWeightsArray[1][0][n][o][i] + nestedBiasesArray[1][0][n][o][i];

                    if (Neurode.GetActivationValue(activationValue, type) != 0)
                    {
                        for (int n = 0; n < parentLayer.Length; n++)
                            for (int o = 0; o < weights[n].Length; o++)
                                activationValue += parentLayer[n].Delta * nestedWeightsArray[1][1][n][o][i] + nestedBiasesArray[1][1][n][o][i];

                        activationValueOuter += activationValue;
                    }
                }

                delta = Neurode.GetActivationValue(activationValueOuter, type);
            }
            else
                delta = 0;
        }

        if(type == 6)// [0][0][n][o][i] [0][1][n][o][i]Buffered          |         [1][0][n][o][i] [1][1][n][o][i]
        {
            float activationValueOuter = 0;

            for (int i = 0; i < deltas.Length; i++)
            {
                float activationValue = 0;

                for (int n = 0; n < parentLayer.Length; n++)
                    for (int o = 0; o < weights[n].Length; o++)
                        activationValue += parentLayer[n].Delta * nestedWeightsArray[0][0][n][o][i] + nestedBiasesArray[0][0][n][o][i];

                if (Neurode.GetActivationValue(activationValue, type) != 0)
                {
                    for (int n = 0; n < parentLayer.Length; n++)
                        for (int o = 0; o < weights[n].Length; o++)
                            activationValue += parentLayer[n].Delta * nestedWeightsArray[0][1][n][o][i] + nestedBiasesArray[0][1][n][o][i];

                    activationValueOuter += activationValue;
                }
            }

            delta = Neurode.GetActivationValue(activationValueOuter, type);//Buffer

            if (Neurode.GetActivationValue(activationValueOuter, type) != 0)
            {
                activationValueOuter = 0;

                for (int i = 0; i < deltas.Length; i++)
                {
                    float activationValue = 0;

                    for (int n = 0; n < parentLayer.Length; n++)
                        for (int o = 0; o < weights[n].Length; o++)
                            activationValue += parentLayer[n].Delta * nestedWeightsArray[1][0][n][o][i] + nestedBiasesArray[1][0][n][o][i];

                    if (Neurode.GetActivationValue(activationValue, type) != 0)
                    {
                        for (int n = 0; n < parentLayer.Length; n++)
                            for (int o = 0; o < weights[n].Length; o++)
                                activationValue += parentLayer[n].Delta * nestedWeightsArray[1][1][n][o][i] + nestedBiasesArray[1][1][n][o][i];

                        activationValueOuter += activationValue;
                    }
                }

                delta = Neurode.GetActivationValue(activationValueOuter, type);
            }
            else
                delta = 0;
        }

        if (type == 7)//[0][n][o][i]       |      [1][n][o][i]   special >_> 
        {
            float activationValue = 0;

            for (int i = 0; i < deltas.Length; i++)
            {
                for (int n = 0; n < parentLayer.Length; n++)
                    for (int o = 0; o < weights[n].Length; o++)
                        activationValue += parentLayer[n].Delta * nestedWeightsArray[0][n][o][i] + nestedBiasesArray[0][n][o][i];

                deltas[i] = Neurode.GetActivationValue(activationValue, type);
            }

            for (int i = 1; i < deltas.Length - 1; i++)
                deltas[0] += deltas[i];

            if (Neurode.GetActivationValue(deltas[0], type) != 0)
            {
                for (int i = 0; i < deltas.Length; i++)
                {
                    for (int n = 0; n < parentLayer.Length; n++)
                        for (int o = 0; o < weights[n].Length; o++)
                            activationValue += parentLayer[n].Delta * nestedWeightsArray[1][n][o][i] + nestedBiasesArray[1][n][o][i];

                    deltas[i] = Neurode.GetActivationValue(activationValue, type);
                }

                for (int i = 1; i < deltas.Length - 1; i++)
                    deltas[0] += deltas[i];

                delta = Neurode.GetActivationValue(deltas[0], type);
            }
        }

        if (type == 8)//[0][n][o][i]Buffered       |      [1][n][o][i]   special >_> 
        {
            float activationValue = 0;

            for (int i = 0; i < deltas.Length; i++)
            {
                for (int n = 0; n < parentLayer.Length; n++)
                    for (int o = 0; o < weights[n].Length; o++)
                        activationValue += parentLayer[n].Delta * nestedWeightsArray[0][n][o][i] + nestedBiasesArray[0][n][o][i];

                deltas[i] = Neurode.GetActivationValue(activationValue, type);
            }

            for (int i = 1; i < deltas.Length - 1; i++)
                deltas[0] += deltas[i];

            delta = Neurode.GetActivationValue(deltas[0], type);//buffer first value.....

            if (delta != 0)
            {
                for (int i = 0; i < deltas.Length; i++)
                {
                    for (int n = 0; n < parentLayer.Length; n++)
                        for (int o = 0; o < weights[n].Length; o++)
                            activationValue += parentLayer[n].Delta * nestedWeightsArray[1][n][o][i] + nestedBiasesArray[1][n][o][i];//all      backwards      forwards       booth

                    deltas[i] = Neurode.GetActivationValue(activationValue, type);
                }

                for (int i = 1; i < deltas.Length - 1; i++)
                    deltas[0] += deltas[i];

                delta = Neurode.GetActivationValue(deltas[0], type);
            }
        }

        //Linear 10 - 18 ###################################################################################################################################################################################################################

        if (type == 10)//Feedforward needs to feed it self!!!
        {
            float activationValue = 0;
            float activationCount = 0;

            for (int i = 0; i < parentLayer.Length; i++)
                if(Neurode.GetActivationValue(parentLayer[i].delta * weight[i] + bias[i], type))
                    activationCount++;

            activationValue = Neurode.GetActivationValue(activationCount, type);

            if (activationValue != 0)
                delta = activationValue;
        }

        if (type == 11)//Feedforward Buffered
        {
            float activationValue = 0;
            float activationCount = 0;

            for (int i = 0; i < parentLayer.Length; i++)
                if (Neurode.GetActivationValue(parentLayer[i].delta * weight[i] + bias[i], type))
                    activationCount++;

            activationValue = Neurode.GetActivationValue(activationCount, type);//BufferdValue
            delta = activationValue;// delta = 0

            if (delta != 0)
                delta = activationValue;
        }

        if (type == 12)
        {
            float activationCounter = 0;

            for (int i = 0; i < deltas.Length; i++)
            {
                float activationValue = 0;

                for (int n = 0; n < parentLayer.Length; n++)
                {
                    for (int o = 0; o < weights[n].Length; o++)
                        activationValue += parentLayer[n].Delta * nestedWeightsArray[0][n][o][i] + nestedBiasesArray[0][n][o][i];

                    if (Neurode.GetActivationValue(activationValue, type) != 0) ;
                        activationCount++;
                }


                if (Neurode.GetActivationValue(activationCount, type) != 0)
                {
                    activationCount = 0;

                    for (int i = 0; i < deltas.Length; i++)
                    {
                        for (int n = 0; n < parentLayer.Length; n++)
                        {
                            for (int o = 0; o < weights[n].Length; o++)
                                activationValue += parentLayer[n].Delta * nestedWeightsArray[1][n][o][i] + nestedBiasesArray[1][n][o][i];

                            if (Neurode.GetActivationValue(activationValue, type) != 0) ;
                                activationCount++;
                        }
                    }

                    delta = Neurode.GetActivationValue(activationCount, type);
                }
                else
                    delta = 0;
            }
        }

        if (type == 13)// linear 2
        {
            float activationValueOuter = 0;

            for (int i = 0; i < deltas.Length; i++)
            {
                float activationValue = 0;

                for (int n = 0; n < parentLayer.Length; n++)
                    for (int o = 0; o < weights[n].Length; o++)
                        activationValue += parentLayer[n].Delta * nestedWeightsArray[0][0][n][o][i] + nestedBiasesArray[0][0][n][o][i];

                if (Neurode.GetActivationValue(activationValue, type) != 0)
                {
                    for (int n = 0; n < parentLayer.Length; n++)
                        for (int o = 0; o < weights[n].Length; o++)
                            activationValue += parentLayer[n].Delta * nestedWeightsArray[0][1][n][o][i] + nestedBiasesArray[0][1][n][o][i];

                    activationValueOuter += activationValue;
                }
            }

            //activationValueOuter = Neurode.GetActivationValue(activationValueOuter, type);

            if (Neurode.GetActivationValue(activationValueOuter, type) != 0)
            {
                activationValueOuter = 0;

                for (int i = 0; i < deltas.Length; i++)
                {
                    float activationValue = 0;

                    for (int n = 0; n < parentLayer.Length; n++)
                        for (int o = 0; o < weights[n].Length; o++)
                            activationValue += parentLayer[n].Delta * nestedWeightsArray[1][0][n][o][i] + nestedBiasesArray[1][0][n][o][i];

                    if (Neurode.GetActivationValue(activationValue, type) != 0)
                    {
                        for (int n = 0; n < parentLayer.Length; n++)
                            for (int o = 0; o < weights[n].Length; o++)
                                activationValue += parentLayer[n].Delta * nestedWeightsArray[1][1][n][o][i] + nestedBiasesArray[1][1][n][o][i];

                        activationValueOuter += activationValue;
                    }
                }

                delta = Neurode.GetActivationValue(activationValueOuter, type);
            }
            else
                delta = 0;
        }

    }


    public override void Respawn(System.Random randomGen)
    {
        if (chromosomeCount == 0)
        {
            for (int n = 0; n < bias.Length; n++)
            {
                if (type == Neurode.TanNeurode)
                {
                    bias[i] = ((float)randomGen.NextDouble() - .5f) * 2f;
                    weight[i] = ((float)randomGen.NextDouble() - .5f) * 2f;
                }
                if (type == Neurode.Sigmoid)
                {
                    bias[i] = (float)randomGen.NextDouble() * .5f;
                    weight[i] = (float)randomGen.NextDouble() * .5f;
                }
                if (type == Neurode.Relu)
                {
                    bias[i] = (float)randomGen.NextDouble();
                    weight[i] = (float)randomGen.NextDouble();
                }
            }
        }
        else
            for (int n = 0; n < chromosomes.GetLength(0); n++)
                for (int i = 0; i < chromosomeCount; i++)
                {
                    if (type == Neurode.TanNeurode)
                        chromosomes[n][i] = ((float)randomGen.NextDouble() - .5f) * 2f;

                    if (type == Neurode.Sigmoid)
                        chromosomes[n][i] = (float)randomGen.NextDouble() * .5f;

                    if (type == Neurode.Relu)
                        chromosomes[n][i] = (float)randomGen.NextDouble();
                }
    }


    public override void MergeInToThis(Neurode partnerNeurode, MergeType type, System.Random randomGen)
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

    public override void AlterNeurode(Neurode baseNeurode, float alternation, System.Random randomGen)
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

    public override Neurode Merge(Neurode neurodeA, Neurode neurodeB, MergeType type)
    {
        throw new System.NotImplementedException();
    }
    public override void MergeInToThis(Neurode partnerNeurode, MergeType type)
    {
        throw new System.NotImplementedException();
    }
    public override void RunForward(Neurode[][] network)
    {
        throw new System.NotImplementedException();
    }
}

//    private int layer;

//public override int Layer { get { return layer; } set { layer = value; } }

//public CNeurode(int layer, float delta, float[][] chromosomes)
//{
//    this.layer = layer;
//    this.delta = delta;
//    this.chromosomes = chromosomes;
//}

//public CNeurode(int layer, float delta, float[] bias, float[] weight)
//{
//    this.layer = layer;
//    this.delta = delta;
//    this.bias = bias;
//    this.weight = weight;
//}


//   activationValue += parentLayer[n].Delta * nestedWeightsArray[n][k][o][i] + nestedBiasesArray[n][k][o][i]; forward???

//   activationValue += parentLayer[n].Delta * nestedWeightsArray[0][0][n][k][o][i] + nestedBiasesArray[0][0][n][k][o][i];gate???
//   activationValue += parentLayer[n].Delta * nestedWeightsArray[1][0][n][k][o][i] + nestedBiasesArray[1][0][n][k][o][i];//Buffer

//   activationValue += parentLayer[n].Delta * nestedWeightsArray[0][0][n][k][o][i] + nestedBiasesArray[0][0][n][k][o][i];gate???
//bufferedGate?
//   activationValue += parentLayer[n].Delta * nestedWeightsArray[1][0][n][k][o][i] + nestedBiasesArray[1][0][n][k][o][i];//Buffer

//        k = networklayers       all   ||   backwards  ||    forwards    ||   booth  || (??  >   logic <3)
//    public override float[][,] DeltasArray { get { return delta; } set { delta = value; } }
