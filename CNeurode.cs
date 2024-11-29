using UnityEngine;

public class CNeurode : Neurode
{
    private int layer;
    private float delta;

    private float[] bias;
    private float[] weight;

    private float[][] weights;
    private float[][] biases;



    private bool isMemoryNeurode;

    private int initiationChromosomeCount;
    private int memoryChromosomeCount;


    private int chromosomeCount;

    private int startIndexSecondGate;

    private NeurodeType type;

    public override NeurodeType Type { get { return type; } set { type = value; } }

    public override int Layer { get { return layer; } set { layer = value; } }
    public override float Delta { get { return delta; } set { delta = value; } }

    // public override float Delta[] { get { return delta; } set { delta = value; } }
    // public override float[][][] Weights { get { return weights; } set { weights = value; } }
    // public override float[][][] Biases { get { return biases; } set { biases = value; } }

    // public override float[][]      Delta { get { return delta; } set { delta = value; } }  ?????????????????????????????????????????????
    // public override float[][][] [][][]  Weights { get { return weights; } set { weights = value; } }?????????????????????????????????????????????
    // public override float[][][] [][][]  Biases { get { return biases; } set { biases = value; } } ?????????????????????????????????????????????
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

      public override void RunForwardChromosome(Neurode[] parentLayer)// 4[][]
   {
       if (type == 4)
       {
           float activationValue = 0;
           int index = 0;

           for (int i = 0; i < weights[n].Length / 2; i++)
           {
               for (int n = 0; n < parentLayer.Length; n++)
                   activationValue += parentLayer[n].Delta * weights[n][index] + biases[n][index];//weights checks each node...

               index++;
           }

           delta = Neurode.GetActivationValue(activationValue, type);

           if (delta != 0)
           {
               for (int i = 0; i < weights[n].Length; i++)
               {
                   for (int n = 0; n < parentLayer.Length; n++)
                       activationValue += parentLayer[i].Delta * weights[n][index] + biases[n][index];

                   index++;
               }

               delta = Neurode.GetActivationValue(activationValue, type);
           }
       }
       if (type == 3)
       {
           for (int i = 0; i < deltas.Length; i++)
           {
               activationValue = 0;

               for (int n = 0; n < parentLayer.Length; n++)
                   for (int o = 0; o < weights[n].Length; o++)
                       activationValue += parentLayer[n].Delta * weights[0][n][o][i] + biases[0][n][o][i];

               deltas[i] = Neurode.GetActivationValue(activationValue, type);
           }

           for (int i = 1; i < deltas.Length - 1; i++)
               deltas[0] += deltas[i];

           delta = Neurode.GetActivationValue(deltas[0], type);

           if (delta != 0)
           {
               for (int i = 0; i < deltas.Length; i++)
               {

                   for (int n = 0; n < parentLayer.Length; n++)
                       for (int o = 0; o < weights[n].Length; o++)
                           activationValue += parentLayer[n].Delta * weights[1][n][o][i] + biases[1][n][o][i];

                   deltas[i] = Neurode.GetActivationValue(activationValue, type);

                   index++;
               }

               for (int i = 1; i < deltas.Length - 1; i++)
                   deltas[0] += deltas[i];

               delta = Neurode.GetActivationValue(deltas[0], type);
           }
       }
       if (type == 2)
       {
           float activationValueOuter = 0;

           for (int i = 0; i < deltas.Length; i++)
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
       }
       if (type == 1)
       {
           int activationCount = 0;

           for (int i = 0; i < deltas.Length; i++)
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
       }
       if (type == 0)//Feedforward
       {
           float activationValue = 0;

           for (int i = 0; i < parentLayer.Length; i++)
               activationValue += parentLayer[i].delta * weight[i] + bias[i];

           activationValue = Neurode.GetActivationValue(activationValue, type);

           if (activationValue != 0)
               delta = activationValue;

       }
   }

    public override void RunForward(Neurode[] parentLayer)
    {
        if (isMemoryNeurode)
        {
            float activationValue = 0;

            for (int i = 0; i < startIndexSecondGate; i++)
                activationValue += parentLayer[i].delta * weight[i];

            activationValue = Neurode.GetActivationValue(activationValue, type);

            if (activationValue != 0)
            {
                for (int i = 0; i < startIndexSecondGate; i++)
                    activationValue += parentLayer[i].delta * weight[i] + bias[i];

                delta = Neurode.GetActivationValue(activationValue, type);
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
                                //float activationValueOuter = 0;

                    //for (int i = 0; i < deltas.Length; i++)    //public override float Memory[] { get { return delta; } set { delta = value; } }?????????????????????????????????????????????
                    //{
                    //    activationValue = 0;

                    //    for (int n = 0; n < parentLayer.Length; n++)
                    //        for (int o = 0; o < weights[n].Length; o++)
                    //            activationValue += parentLayer[n].Delta * weights[n][o][i] + biases[n][o][i];

                    //    delta = Neurode.GetActivationValue(activationValue, type);

                    //    if (delta != 0)
                    //    {
                    //        for (int i = 0; i < deltas.Length; i++)
                    //        {

                    //            for (int n = 0; n < parentLayer.Length; n++)
                    //                for (int o = 0; o < weights[n].Length; o++)
                    //                    activationValue += parentLayer[n].Delta * weights[n][o][i] + biases[n][o][i];

                    //            deltas[i] = Neurode.GetActivationValue(activationValue, type);

                    //            index++;
                    //        }

                    //        activationValueOuter += Neurode.GetActivationValue(activationValue, type);
                    //    }
                    //}

                    //delta =  Neurode.GetActivationValue(activationValueOuter, type)


                    //int activationCount = 0;

                    //for (int i = 0; i < deltas.Length; i++)    //public override float Memory[] { get { return delta; } set { delta = value; } }?????????????????????????????????????????????
                    //{
                    //    activationValue = 0;

                    //    for (int n = 0; n < parentLayer.Length; n++)
                    //        for (int o = 0; o < weights[n].Length; o++)
                    //            activationValue += parentLayer[n].Delta * weights[n][o][i] + biases[n][o][i];

                    //    delta = Neurode.GetActivationValue(activationValue, type);

                    //    if (delta != 0)
                    //    {
                    //        for (int i = 0; i < deltas.Length; i++)
                    //        {

                    //            for (int n = 0; n < parentLayer.Length; n++)
                    //                for (int o = 0; o < weights[n].Length; o++)
                    //                    activationValue += parentLayer[n].Delta * weights[n][o][i] + biases[n][o][i];

                    //            deltas[i] = Neurode.GetActivationValue(activationValue, type);

                    //            index++;
                    //        }

                    //        activationCount++;
                    //    }
                    //}
                    //delta = Neurode.GetActivationValue((float)activationCount, type)
