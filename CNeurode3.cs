using UnityEngine;

public class CNeurode : Neurode
{
    private int layer;

    private float delta;
    private float[] deltas;
    private float[][] deltasArray;

    private float[] weight;
    private float[] bias;

    private float[][] weights;
    private float[][] biases;

    private float[][][] weightsArray;
    private float[][][] biasesArray;
    private float[][][][] nestedWeightsArray;
    private float[][][][] nestedBiasesArray;

    public override float[][] DeltasArray { get { return delta; } set { delta = value; } }
    public override float[][][] WeightsArray { get { return weights; } set { weights = value; } }
    public override float[][][] BiasesArray { get { return biases; } set { biases = value; } }
    public override float[][][][] NestedWeightsArray { get { return weights; } set { weights = value; } }
    public override float[][][][] NestedBiasesArray { get { return biases; } set { biases = value; } }

    private bool isMemoryNeurode;

    private int initiationChromosomeCount;
    private int memoryChromosomeCount;


    private int chromosomeCount;

    private int startIndexSecondGate;

    private NeurodeType type;

    public override NeurodeType Type { get { return type; } set { type = value; } }

    public override int Layer { get { return layer; } set { layer = value; } }
    public override float Delta { get { return delta; } set { delta = value; } }



    public override float[] Bias { get { return bias; } set { bias = value; } }
    public override float[] Weight { get { return weight; } set { weight = value; } }


    public override float[][] Chromosomes { get { return chromosomes; } set { chromosomes = value; } }


    public override float[][] Weights { get { return weights; } set { weights = value; } }
    public override float[][] Biases { get { return biases; } set { biases = value; } }


    public override bool IsMemoryNeurode { get { return isMemoryNeurode; } set { isMemoryNeurode = value; } }    

    public override int InitiationChromosomeCount { get { return initiationChromosomeCount; } set { initiationChromosomeCount = value; } }
    public override int MemoryChromosomeCount { get { return memoryChromosomeCount; } set { memoryChromosomeCount = value; } }
    public override int ChromosomeCount { get { return chromosomeCount; } set { chromosomeCount = value; } }


    public CNeurode(int layer, float delta, float[][] chromosomes)
    {
        this.layer = layer;
        this.delta = delta;
        this.chromosomes = chromosomes;
    }

    public CNeurode(int layer, float delta, float[] bias, float[] weight)
    {
        this.layer = layer;
        this.delta = delta;
        this.bias = bias;
        this.weight = weight;
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

        //Feedforward, FeedforwardMemory             if (weights[n].Length != 4)        8!!!!!!


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
        //else
        //initiationChromosomeCount;
        //private int memoryChromosomeCount;

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
        //Exponentiel ##############################################################################################################################################################################################################

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
                            activationValue += parentLayer[n].Delta * nestedWeightsArray[1][n][o][i] + nestedBiasesArray[1][n][o][i];

                    deltas[i] = Neurode.GetActivationValue(activationValue, type);
                }

                for (int i = 1; i < deltas.Length - 1; i++)
                    deltas[0] += deltas[i];

                delta = Neurode.GetActivationValue(deltas[0], type);
            }
        }

        //Linear ###################################################################################################################################################################################################################


        if (type == 9)//Feedforward needs to feed it self???
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

        if (type == 10)//Feedforward Buffered
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

        if (type == 11)
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

        if (type == 12)// linear 2
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






// Could be useless "¯\_(ツ)_/¯ "
//???????????????????????????????????????????????????????????????????????????????????????????????????????????????????????????? 
return;

float activationValueOuter = 0;

for (int i = 0; i < deltas.Length; i++)    //public override float Memory[] { get { return delta; } set { delta = value; } }?????????????????????????????????????????????
{
    activationValue = 0;

    for (int n = 0; n < parentLayer.Length; n++)
        for (int o = 0; o < weights[n].Length; o++)
            activationValue += parentLayer[n].Delta * weights[n][o][i] + biases[n][o][i];

    delta = Neurode.GetActivationValue(activationValue, type);

    if (delta != 0)
    {
        for (int i = 0; i < deltas.Length; i++)
        {

            for (int n = 0; n < parentLayer.Length; n++)
                for (int o = 0; o < weights[n].Length; o++)
                    activationValue += parentLayer[n].Delta * weights[n][o][i] + biases[n][o][i];

            deltas[i] = Neurode.GetActivationValue(activationValue, type);

            index++;
        }

        activationValueOuter += Neurode.GetActivationValue(activationValue, type);
    }
}

delta = Neurode.GetActivationValue(activationValueOuter, type)

                    return;

int activationCount = 0;

for (int i = 0; i < deltas.Length; i++)    //public override float Memory[] { get { return delta; } set { delta = value; } }?????????????????????????????????????????????
{
    activationValue = 0;

    for (int n = 0; n < parentLayer.Length; n++)
        for (int o = 0; o < weights[n].Length; o++)
            activationValue += parentLayer[n].Delta * weights[n][o][i] + biases[n][o][i];

    delta = Neurode.GetActivationValue(activationValue, type);

    if (delta != 0)
    {
        for (int i = 0; i < deltas.Length; i++)
        {

            for (int n = 0; n < parentLayer.Length; n++)
                for (int o = 0; o < weights[n].Length; o++)
                    activationValue += parentLayer[n].Delta * weights[n][o][i] + biases[n][o][i];

            deltas[i] = Neurode.GetActivationValue(activationValue, type);

            index++;
        }

        activationCount++;
    }
}
delta = Neurode.GetActivationValue((float)activationCount, type)
