using UnityEngine;

public class CNeurode : Neurode
{
    private NeurodeType type;
    private int localType = 0;
    private bool isMemoryNeurode;

    public float delta;
    public float[] weight;
    public float[] bias;

    public NeurodeType Type { get { return type; } set { type = value; } }
    public bool IsMemoryNeurode { get { return isMemoryNeurode; } set { isMemoryNeurode = value; } }
    public float Delta { get { return delta; } set { delta = value; } }
    public float[] Bias { get { return bias; } set { bias = value; } }
    public float[] Weight { get { return weight; } set { weight = value; } }
 



    public float[] delta1D;

    public float[][] weight2D;
    public float[][] bias2D;

    public float[][][] weight3D;
    public float[][][] bias3D;

    public float[][][][] weight4D;
    public float[][][][] bias4D;

    public float[][][][][] weight5D;
    public float[][][][][] bias5D;


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
    /// Type 0 || 1: Initializes delta, weight[], and bias[].
    /// </summary>
    public void Initiate(int localType, System.Random randomGen, NeurodeType type, int magnitudeCount)
    {
        if (localType == 0 || localType == 1)
        {
            float delta = 0;
            float[] weight = new float[magnitudeCount];
            float[] bias = new float[magnitudeCount];
        }
        else
            throw new ArgumentException("Invalid localType value. Only '0' is supported.", nameof(localType));
    }

    /// <summary>
    /// Type 2: Initializes delta, weight[2][2][magnitude], and bias[2][2][magnitude].
    /// 
    /// weight = 
    /// [
    ///     [
    ///         [0.12, 0.34, 0.56],
    ///         [0.78, 0.90, 0.21]
    ///     ]
    ///     ,
    ///     [
    ///         [0.43, 0.65, 0.87],
    ///         [0.09, 0.32, 0.54]
    ///     ]
    /// ]
    /// 
    ///Bias array would follow the same structure.
    ///   
    /// </summary>
    public void Initiate(int localType, System.Random randomGen, NeurodeType type, int magnitudeCount)
    {
        if (localType == 2)
        {
            float delta = 0;

            float[][][] weight = new float[2][][];
            float[][][] bias = new float[2][][];

            int count = GetFanInValue(magnitudeCount);

            for (int i = 0; i < 2; i++)
            {
                weight[i] = new float[2][];
                bias[i] = new float[2][];

                for (int j = 0; j < 2; j++)
                {
                    weight[i][j] = new float[count];
                    bias[i][j] = new float[count];

                    for (int k = 0; k < count; k++)
                    {
                        weight[i][j][k] = GetInitialValue(type);
                        bias[i][j][k] = GetInitialValue(type);
                    }
                }
            }
        }
        else
            throw new ArgumentException("Invalid localType value. Only '0' is supported.", nameof(localType));
    }


    /// <summary>
    /// Type 2 || 3: Initializes delta, weight[2][magnitudeCount][magnitudeCount][parentLayerLength], and bias[2][magnitudeCount][magnitudeCount][parentLayerLength].
    /// </summary>
    public void Initiate(int localType, System.Random randomGen, NeurodeType type, int magnitudeCount)
    {
        if (localType == 2)
        {
            float delta = 0;

            float[][][][] weight = new float[2][][][];
            float[][][][] bias = new float[2][][][];

            int count = GetFanInValue(magnitudeCount);

            for (int i = 0; i < 2; i++)
            {
                weight[i] = new float[2][];
                bias[i] = new float[2][];

                for (int j = 0; j < 2; j++)
                {
                    weight[i][j] = new float[count];
                    bias[i][j] = new float[count];

                    for (int k = 0; k < count; k++)
                    {
                        weight[i][j][k] = new float[count];
                        bias[i][j][k] = new float[count];

                        for (int o = 0; o < count; o++)
                        {
                            weight[i][j][k][o] = GetInitialValue(type);
                            bias[i][j][k][o] = GetInitialValue(type);
                        }
                    }
                }
            }
        }
        else
            throw new ArgumentException("Invalid localType value. Only '0' is supported.", nameof(localType));
    }

    //for (int i = 0; i<deltas.Length; i++)
    //     {
    //         float activationValue = 0;
    //         for (int n = 0; n<parentLayer.Length; n++)
    //             for (int o = 0; o<weights[n].Length; o++)
    //                 activationValue += parentLayer[n].Delta* nestedWeightsArray[0][n][o][i] + nestedBiasesArray[0][n][o][i];

    public int GetFanInValue(int magnitudeCount)
    {
        return magnitudeCount;

    }

    public float GetInitialValue(NeurodeType type)
    {
        return (float)randomGen.NextDouble();

    }

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



    public override void RunForward(Neurode[][] network)
    {
        if (type == 6)// [0][o][0][o] | [1][o] [1][o]
        {
            float activationValue = 0;

            for (int i = 0; i < network.GetLength(0); n++)
                for (int n = 0; n < network.GetLength(0); n++)
                    for (int o = 0; o < weights[n].Length; o++)
                        activationValue += network[i][n].Delta * nestedWeightsArray[0][o] + nestedBiasesArray[0][o];

                if (Neurode.GetActivationValue(activationValue, type) != 0)
                {
                    activationValue = 0;

                    for (int i = 0; i < network.GetLength(0); n++)
                        for (int n = 0; n < network.GetLength(0); n++)
                            for (int o = 0; o < weights[n].Length; o++)
                                activationValue += network[i][n].Delta * nestedWeightsArray[1][o] + nestedBiasesArray[1][o];

                    delta = Neurode.GetActivationValue(activationValue, type);
                }
                else
                    delta = 0;
        }

        if (type == 7)// [0][o][0][o] | [1][o] [1][o]       &&      [8]
        {
            float activationValue = 0;

            for (int i = 0; i < network.GetLength(0); n++)
                for (int n = 0; n < network.GetLength(0); n++)
                    for (int o = 0; o < weights[n].Length; o++)
                        activationValue += network[i][n].Delta * nestedWeightsArray[0][o] + nestedBiasesArray[0][o];

            delta = Neurode.GetActivationValue(activationValue, type);

            if (delta != 0)
            {
                activationValue = 0;

                for (int i = 0; i < network.GetLength(0); n++)
                    for (int n = 0; n < network.GetLength(0); n++)
                        for (int o = 0; o < weights[n].Length; o++)
                            activationValue += network[i][n].Delta * nestedWeightsArray[1][o] + nestedBiasesArray[1][o];
                    
                delta = Neurode.GetActivationValue(activationValue, type);
            }
        }

            if (type == 8)// [0][o] [0][o] | [1][o] [1][o]      |        [2][o] [2][o] | [3][o] [3][o]      // if looped you loop the whole network abstracted to one float using a activation function * k.
            {
                float activationValue = 0;

                for (int i = 0; i < network.GetLength(0); n++)
                    for (int n = 0; n < network.GetLength(1); n++)
                        for (int o = 0; o < weights[n].Length; o++)
                            for (int k = 0; k < 7; k++)//magic number
                                activationValue += network[i][n].Delta * nestedWeightsArray[0][o][i] + nestedBiasesArray[0][o][i];

                    if (Neurode.GetActivationValue(activationValue, type) != 0)
                    {
                        activationValue = 0;

                        for (int i = 0; i < network.GetLength(0); n++)
                            for (int n = 0; n < network.GetLength(1); n++)
                                for (int o = 0; o < weights[n].Length; o++)
                                    for (int k = 0; k < 7; k++)//magic number
                                activationValue += network[i][n].Delta * nestedWeightsArray[1][o][i] + nestedBiasesArray[1][o][i];

                        delta = Neurode.GetActivationValue(activationValue, type)
                    }
                    else
                     delta = 0;
            }

            if (type == 9)// [0][o] [0][o] | [1][o] [1][o]      |        [2][o] [2][o] | [3][o] [3][o]      &&      [8]
            {
                float activationValue = 0;

                for (int i = 0; i < network.GetLength(0); n++)
                    for (int n = 0; n < network.GetLength(1); n++)
                        for (int o = 0; o < weights[n].Length; o++)
                            for (int k = 0; k < 7; k++)//magic number
                                activationValue += network[i][n].Delta * nestedWeightsArray[0][o][i] + nestedBiasesArray[0][o][i];

                delta = Neurode.GetActivationValue(activationValue, type);

                if (delta != 0)
                {
                    activationValue = 0;

                    for (int i = 0; i < network.GetLength(0); n++)
                        for (int n = 0; n < network.GetLength(1); n++)
                            for (int o = 0; o < weights[n].Length; o++)
                                for (int k = 0; k < 7; k++)//magic number
                                    activationValue += network[i][n].Delta * nestedWeightsArray[1][o][i] + nestedBiasesArray[1][o][i];

                    delta = Neurode.GetActivationValue(activationValue, type)
                }
                
            }

    }

    public override void RunForwardNested(Neurode[] parentLayer)
    {
        if (type == 0)//Needs to feed it self!!!
        {
            float activationValue = 0;

            for (int i = 0; i < parentLayer.Length; i++)
                activationValue += parentLayer[i].delta * weight[i] + bias[i];

            activationValue = Neurode.GetActivationValue(activationValue, type);

            if (activationValue != 0)
                delta = activationValue;
        }

        if (type == 1)//Buffered
        {
            float activationValue = 0;

            for (int i = 0; i < parentLayer.Length; i++)
                activationValue += parentLayer[i].delta * weight[i] + bias[i];

            delta = Neurode.GetActivationValue(activationValue, type); //Buffered value

            if (activationValue != 0)
                delta = activationValue;
        }

            if (type == 2)// [0][n][o]       |      [1][n][o]
            {
                float activationValue = 0;

                for (int n = 0; n < parentLayer.Length; n++)
                    for (int o = 0; o < weights.Length; o++)
                        activationValue += parentLayer[n].Delta * weightsArray[0][o] + biasesArray[0][o];


                if (Neurode.GetActivationValue(activationValue, type) != 0)
                {
                    activationValue = 0;

                    for (int n = 0; n < parentLayer.Length; n++)
                        for (int o = 0; o < weights.Length; o++)
                            activationValue += parentLayer[n].Delta * weightsArray[1][o] + biasesArray[1][o];


                    delta = Neurode.GetActivationValue(activationValue, type);
                }
            }

            if (type == 3)// ([0][n][o]       |      [1][n][o])     &&      [8]
            {
                float activationValue = 0;

                for (int n = 0; n < parentLayer.Length; n++)
                    for (int o = 0; o < weights.Length; o++)
                        activationValue += parentLayer[n].Delta * weightsArray[0][o] + biasesArray[0][o];

                delta = Neurode.GetActivationValue(activationValue, type);

                if (Neurode.GetActivationValue(activationValue, type) != 0)
                {
                    activationValue = 0;

                    for (int n = 0; n < parentLayer.Length; n++)
                        for (int o = 0; o < weights.Length; o++)
                            activationValue += parentLayer[n].Delta * weightsArray[1][o] + biasesArray[1][o];


                    delta = Neurode.GetActivationValue(activationValue, type);
                }
            }

                if (type == 4)// [0][o] [0][o] | [1][o] [1][o]      |        [2][o] [2][o] | [3][o] [3][o]    
                {
                    float activationValueOuter = 0;

                    for (int i = 0; i < parentLayer.Length; i++)
                    {
                        float activationValue = 0;

                        for (int n = 0; n < parentLayer.Length; n++)
                            for (int o = 0; o < weights[n].Length; o++)
                                activationValue += parentLayer[n].Delta * nestedWeightsArray[0][o] + nestedBiasesArray[0][o];

                        if (Neurode.GetActivationValue(activationValue, type) != 0)
                        {
                            for (int n = 0; n < parentLayer.Length; n++)
                                for (int o = 0; o < weights[n].Length; o++)
                                    activationValue += parentLayer[n].Delta * nestedWeightsArray[1][o] + nestedBiasesArray[1][o];

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
                                    activationValue += parentLayer[n].Delta * nestedWeightsArray[2][o] + nestedBiasesArray[2][o];

                            if (Neurode.GetActivationValue(activationValue, type) != 0)
                            {
                                for (int n = 0; n < parentLayer.Length; n++)
                                    for (int o = 0; o < weights[n].Length; o++)
                                        activationValue += parentLayer[n].Delta * nestedWeightsArray[3][o] + nestedBiasesArray[3][o];

                                activationValueOuter += activationValue;
                            }
                        }

                        delta = Neurode.GetActivationValue(activationValueOuter, type);
                    }
                    else
                        delta = 0;
                }

                if (type == 5)// [0][o] [0][o] | [1][o] [1][o]      |        [2][o] [2][o] | [3][o] [3][o]      &&      [8]
                {
                    float activationValueOuter = 0;

                    for (int i = 0; i < deltas.Length; i++)
                    {
                        float activationValue = 0;

                        for (int n = 0; n < parentLayer.Length; n++)
                            for (int o = 0; o < weights[n].Length; o++)
                                activationValue += parentLayer[n].Delta * nestedWeightsArray[0][o] + nestedBiasesArray[0][o];

                        if (Neurode.GetActivationValue(activationValue, type) != 0)
                        {
                            for (int n = 0; n < parentLayer.Length; n++)
                                for (int o = 0; o < weights[n].Length; o++)
                                    activationValue += parentLayer[n].Delta * nestedWeightsArray[1][o] + nestedBiasesArray[1][o];

                            activationValueOuter += activationValue;
                        }
                    }

                    delta = Neurode.GetActivationValue(activationValueOuter, type);

                    if (delta != 0)
                    {
                        activationValueOuter = 0;

                        for (int i = 0; i < deltas.Length; i++)
                        {
                            float activationValue = 0;

                            for (int n = 0; n < parentLayer.Length; n++)
                                for (int o = 0; o < weights[n].Length; o++)
                                    activationValue += parentLayer[n].Delta * nestedWeightsArray[2][o] + nestedBiasesArray[2][o];

                            if (Neurode.GetActivationValue(activationValue, type) != 0)
                            {
                                for (int n = 0; n < parentLayer.Length; n++)
                                    for (int o = 0; o < weights[n].Length; o++)
                                        activationValue += parentLayer[n].Delta * nestedWeightsArray[3][o] + nestedBiasesArray[3][o];

                                activationValueOuter += activationValue;
                            }
                        }

                        delta = Neurode.GetActivationValue(activationValueOuter, type);
                    }
                }

                  



        //Linear 10 - 17 ###################################################################################################################################################################################################################

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




//Important Considerations

//    Dynamic Weight Scaling: As you've mentioned, the upper bound for weights can grow with the layer depth or network size.
//    If you want the weights to grow larger as the layers increase, you can !!! dynamically scale !!! the weights based on the number of layers or neurons in the previous layer (fan-in).
//    This scaling would ensure that as you move deeper into the network, the weights can grow larger, which could be helpful in cases where you're dealing with deeper architectures.

//    Fan-In / Fan-Out: If you are multiplying weights by layers,
//    you may want to factor in the fan-in (the number of neurons in the previous layer) or fan-out (the number of neurons in the current layer) to control the magnitude of the weights.
//    !!! He initialization or Xavier !!! initialization can be adapted here, but since you’re allowing the weights to range freely (up to open-ended values), you can adjust the scaling factor to match your needs.

//    Activation Functions: If you're using ReLU or other activation functions like sigmoid or tanh,
//    the weight initialization should be scaled to ensure proper gradient flow.
//    !!! He initialization !!! is often used for ReLU because it accounts for the number of neurons in the previous layer.
//    You can adjust the standard deviation of the weights based on the fan-in (previous layer size).




//   activationValue += parentLayer[n].Delta * nestedWeightsArray[n][k][o][i] + nestedBiasesArray[n][k][o][i]; forward???

//   activationValue += parentLayer[n].Delta * nestedWeightsArray[0][0][n][k][o][i] + nestedBiasesArray[0][0][n][k][o][i];gate???
//   activationValue += parentLayer[n].Delta * nestedWeightsArray[1][0][n][k][o][i] + nestedBiasesArray[1][0][n][k][o][i];//Buffer

//   activationValue += parentLayer[n].Delta * nestedWeightsArray[0][0][n][k][o][i] + nestedBiasesArray[0][0][n][k][o][i];gate???
//bufferedGate?
//   activationValue += parentLayer[n].Delta * nestedWeightsArray[1][0][n][k][o][i] + nestedBiasesArray[1][0][n][k][o][i];//Buffer

//        k = networklayers       all   ||   backwards  ||    forwards    ||   booth  || (??  >   logic <3)
//    public override float[][,] DeltasArray { get { return delta; } set { delta = value; } }
