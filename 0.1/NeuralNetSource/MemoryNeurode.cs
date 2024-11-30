using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MemoryNeurode : Neurode
{
    //private int layer;
    //private float delta;
    //private float[] bias;
    //private float[] weight;

    private int startIndexSecondGate;
    private NeurodeType type;

    public override int Layer { get { return layer; } set { layer = value; } }
    public override float Delta { get { return delta; } set { delta = value; } }
    public override float[] Bias { get { return bias; } set { bias = value; } }
    public override float[] Weight { get { return weight; } set { weight = value; } }
    public override NeurodeType Type { get { return type; } set { type = value; } }


    public override float[][] Chromosones { get { return weight; } set { weight = value; } }
    int memoryChromosomeCount = 10;
    int chromosomeCount = 30;


    int memoryPercentage = 10;


    public MemoryNeurode(int layer, float delta, float[][] chromosones)
    {
        Layer = layer;
        Delta = delta;
        Chromosones = chromosones;
    }


    public MemoryNeurode(int layer, float delta, float[] bias, float[] weight)
    {
        Layer = layer;
        Delta = delta;
        Bias = bias;
        Weight = weight;
    }

    public MemoryNeurode(int layer, int parentLayerSize, System.Random randomGen, NeurodeType type)
    {
        this.startIndexSecondGate = parentLayerSize;
        parentLayerSize = parentLayerSize * 2;
        this.layer = layer;
        this.delta = 0;
        this.bias = new float[parentLayerSize];
        this.weight = new float[parentLayerSize];
        this.type = type;

        for (int i = 0; i < parentLayerSize; i++)
        {
            bias[i] = ((float)randomGen.NextDouble() - .5f) * 2f;
            weight[i] = ((float)randomGen.NextDouble() - .5f) *2f;
        }
    }




    public override void RunForward(Neurode[] parentLayer, bool saveGateValue)
    {
        float activationValue = 0;

        for (int i = 0; i < startIndexSecondGate; i++)
                    activationValue += parentLayer[i].Delta * weight[i] * bias[i];

        if (System.Math.Tanh(activationValue) > 0)
        {
            activationValue = 0;

            for (int i = 0; i < startIndexSecondGate; i++)
            {
                int index = i + startIndexSecondGate;
                activationValue += parentLayer[i].Delta * weight[index] * bias[index];

            }
            delta = (float)System.Math.Tanh(activationValue);

        }
        else 
        if(saveGateValue)
            delta = (float)System.Math.Tanh(activationValue);
        else
            delta = 0;


    }


    public override void RunForward(Neurode[] parentLayer)
    {
        float activationValue = 0;

        for (int i = 0; i < startIndexSecondGate; i++)
            activationValue += parentLayer[i].Delta * weight[i] * bias[i];

        if (System.Math.Tanh(activationValue) > 0)
        {
            activationValue = 0;

            for (int i = 0; i < startIndexSecondGate; i++)
            {
                int index = i + startIndexSecondGate;
                activationValue += parentLayer[i].Delta * weight[index] * bias[index];

            }

            delta = (float)System.Math.Tanh(activationValue);

        }

    }

    //>_>

    public override void RunForward(Neurode[] parentLayer)
    {

        int initiationChromosomeCount = 10;
        int memoryChromosomeCount = 10;
        int chromosomeCount = 40;

        float activationValue = 0;
        int index = 0;

        activationValue = 0;

        for (int i = 0; i < initiationChromosomeCount; i++)
        {
            for (int n = 0; n < parentLayer.Length; n++)
                activationValue += parentLayer[i].Delta * Chromosones[n][index];

            index++;
        }


        if (System.Math.Tanh(activationValue) > 0)
        {
            for (int i = 0; i < memoryChromosomeCount; i++)
            {
                for (int n = 0; n < parentLayer.Length; n++)
                    activationValue += parentLayer[i].Delta * Chromosones[n][index];

                index++;
            }
        }

        if (System.Math.Tanh(activationValue) > 0)
        {
            for (int i = 0; i < chromosomeCount; i++)
            {
                for (int n = 0; n < parentLayer.Length; n++)
                    activationValue += parentLayer[i].Delta * Chromosones[n][index];

                index++;
            }

            delta = (float)System.Math.Tanh(activationValue);
        }
        else
            delta = 0;

    }



    public override void RunForward(Neurode[] parentLayer, bool saveGate)// savegate[]??????????????????????
    {
        int initiationChromosomeCount = 10;
        int memoryChromosomeCount = 10;
        int chromosomeCount = 30;

        float activationValue = 0;//[]??????????????????????????
        int index = 0;

        activationValue = 0;

        for (int i = 0; i < initiationChromosomeCount; i++)
        {
            for (int n = 0; n < parentLayer.Length; n++)
                activationValue += parentLayer[i].Delta * Chromosones[n][index];

            index++;
        }

        if (System.Math.Tanh(activationValue) > 0)
        {
            for (int i = 0; i < memoryChromosomeCount; i++)
            {
                for (int n = 0; n < parentLayer.Length; n++)
                    activationValue += parentLayer[i].Delta * Chromosones[n][index];

                index++;
            }
        }

        if (System.Math.Tanh(activationValue) > 0)
        {
            for (int i = 0; i < chromosomeCount; i++)
            {
                for (int n = 0; n < parentLayer.Length; n++)
                    activationValue += parentLayer[i].Delta * Chromosones[n][index];

                index++;
            }

            delta = (float)System.Math.Tanh(activationValue);
        }
        else
            if (saveGate)//[i] == true
                    delta = activationValue;//+=
                else
                    delta = 0;


        //  parentLayer[i].Delta * parentLayer[i].Delta savegate[]??????????????????????
    }

    public override void RunForward(Neurode[] parentLayer, bool[] saveGate)// savegate[]??????????????????????
    {
        int initiationChromosomeCount = 10;
        int memoryChromosomeCount = 10;
        int chromosomeCount = 30;

        float[] activationValue = new float[saveGate.Length];//[]??????????????????????????

        float lesGo;

        for (int s = 0; s < saveGate.Length; s++)
        {
            int index = 0;

            activationValue[s] = 0;

            for (int i = 0; i < initiationChromosomeCount; i++)
            {
                for (int n = 0; n < parentLayer.Length; n++)
                    activationValue[s] += parentLayer[i].Delta * Chromosones[n][index];

                index++;
            }

            if (System.Math.Tanh(activationValue[s]) > 0)
            {
                for (int i = 0; i < memoryChromosomeCount; i++)
                {
                    for (int n = 0; n < parentLayer.Length; n++)
                        activationValue[s] += parentLayer[i].Delta * Chromosones[n][index];

                    index++;
                }
            }

            if (System.Math.Tanh(activationValue[s]) > 0)
            {
                for (int i = 0; i < chromosomeCount; i++)
                {
                    for (int n = 0; n < parentLayer.Length; n++)
                        activationValue[s] += parentLayer[i].Delta * Chromosones[n][index];

                    index++;
                }

                delta = (float)System.Math.Tanh(activationValue[s]);
            }
            else
                if (saveGate[s])
                    delta = activationValue[i];
                else
                    delta = 0;

            lesGo += delta;
        }

        delta = lesGo;
    }


    // for (int i = 0; i<chromosomeCount; i++)       public override void RunForward(Neurode[] parentLayer, bool[] saveGate)// savegate[]??????????????????????
    {
        int initiationChromosomeCount = 10;
        int memoryChromosomeCount = 10;
        int chromosomeCount = 30;

        float[] activationValue = new float[saveGate.Length];//[]??????????????????????????

        float lesGo;

        for (int s = 0; s < saveGate.Length; s++)
        {
            int index = 0;

            activationValue[s] = 0;

            for (int i = 0; i < initiationChromosomeCount; i++)
            {
                for (int n = 0; n < parentLayer.Length; n++)
                    activationValue[s] += parentLayer[i].Delta * Chromosones[n][index];

                index++;
            }

            if (System.Math.Tanh(activationValue[s]) > 0)
            {
                for (int i = 0; i < memoryChromosomeCount; i++)
                {
                    for (int n = 0; n < parentLayer.Length; n++)
                        activationValue[s] += parentLayer[i].Delta * Chromosones[n][index];

                    index++;
                }
            }

            if (System.Math.Tanh(activationValue[s]) > 0)
            {
                for (int i = 0; i < chromosomeCount; i++)
                {
                    for (int n = 0; n < parentLayer.Length; n++)
                        activationValue[s] += parentLayer[i].Delta * Chromosones[n][index];

                    index++;
                }

                delta = (float)System.Math.Tanh(activationValue[s]);
            }
            else
                if (saveGate[s])
                    delta = activationValue[i];
                else
                    delta = 0;

            lesGo += delta;
        }

        delta = lesGo / saveGate.Length; // saveGate.Length
    }
    //{
    //    for (int n = 0; n<parentLayer.Length; n++)
    //        activationValue += parentLayer[i].Delta* Chromosones[n][index];

    //    index++;
    //}


    //float activationValue = 0;

    //for (int i = 0; i < startIndexSecondGate; i++)
    //    activationValue += parentLayer[i].Delta * weight[i] * bias[i];

    //if (System.Math.Tanh(activationValue) > 0)
    //{
    //    activationValue = 0;

    //    for (int i = 0; i < startIndexSecondGate; i++)
    //    {
    //        int index = i + startIndexSecondGate;
    //        activationValue += parentLayer[i].Delta * weight[index] * bias[index];

    //    }

    //    delta = (float)System.Math.Tanh(activationValue);

    //}



    //int memoryChromosomeCount = 10;
    //int chromosomeCount = 30;

    //float activationValue = 0;
    //int index = 0;

    //for (int n = 0; n < parentLayer.Length; n++)
    //{
    //    activationValue = parentLayer[i].Delta;

    //    if (System.Math.Tanh(delta) > 0)
    //    {
    //        for (int i = 0; i < memoryChromosomeCount; i++)
    //        {

    //            for (int n = 0; n < parentLayer.Length; n++)
    //                activationValue *= Chromosones[n][index];
    //            index++;
    //        }
    //    }

    //}



    public override void RunForward(Neurode[] parentLayer, bool saveGateValue)
    {
        int memoryChromosomeCount = 10;
        int chromosomeCount = 30;

        float activationValue = 0;
        int index = 0;





        for (int n = 0; n < parentLayer.Length; n++)
            for (int i = 0; i < memoryChromosomeCount; i++)
                {
                    activationValue += (parentLayer[i].Delta * Chromosones[n][index]);//GateValue
                    index++;
                }



        for (int n = 0; n < parentLayer.Length; n++)
        {
            if (System.Math.Tanh(delta) > 0)
            {
                for (int i = 0; i < memoryChromosomeCount; i++)
                {
                    activationValue += (parentLayer[i].Delta * Chromosones[n][index]);//GateValue
                    index++;
                }
            }
        }



        for (int n = 0; n < parentLayer.Length; n++)
        {
            activationValue = parentLayer[i].Delta;

            if (System.Math.Tanh(activationValue) > 0)
            {
                for (int i = 0; i < chromosomeCount; i++)
                {
                    activationValue += (parentLayer[i].Delta * Chromosones[n][index]);//GateValue
                    index++;


                }

                delta = (float)System.Math.Tanh(activationValue);
            }
        }
        if (saveGateValue)
            delta = activationValue;
        else
            delta = 0;
    }


    public override void RunForward(Neurode[] parentLayer)
    {

        int memoryChromosomeCount = 10;
        int chromosomeCount = 30;

        float activationValue = 0;
        int index = 0;

        for (int n = 0; n < parentLayer.Length; n++)
        {
            activationValue = parentLayer[n].Delta;

            if (System.Math.Tanh(activationValue) > 0)
            {
                for (int i = 0; i < memoryChromosomeCount; i++)
                {
                    activationValue *= Chromosones[n][index];//individual for each neuron!?    chromosomeCount 
                    index++;
                }
            }
        }

        for (int n = 0; n < parentLayer.Length; n++)
        {
            activationValue = parentLayer[n].Delta;





            if (System.Math.Tanh(activationValue) > 0)
            {
                for (int i = 0; i < chromosomeCount; i++)
                {
                    activationValue *= Chromosones[n][index];//individual for each neuron!? chromosomeCount
                    index++;
                }

                delta = (float)System.Math.Tanh(activationValue);
            }
        }
    }






    // >_>

    //public override void RunForward(Neurode[] parentLayer)
    //{

    //    float activationValue = 0;

    //    int index = 0;

    //    for(int n = 0; n < parentLayer.Length; n ++)
    //    {
    //        if (System.Math.Tanh(activationValue) > 0)
    //        {
    //            activationValue = parentLayer[i].Delta;

    //            for (int i = 0; i < memoryChromosomeCount; i++)
    //            {
    //                activationValue *= Chromosones[n][index];
    //                index++;
    //            }

    //            delta = (float)System.Math.Tanh(activationValue);

    //        }
    //    }

    //    for (int n = 0; n < parentLayer.Length; n++)
    //    {
    //        activationValue = parentLayer[i].Delta;


    //        if (System.Math.Tanh(activationValue) > 0)
    //        {
    //            activationValue = parentLayer[i].Delta;

    //            for (int i = 0; i < memoryChromosomeCount; i++)
    //            {
    //                activationValue *= Chromosones[n][index];
    //                index++;
    //            }

    //            delta = (float)System.Math.Tanh(activationValue);

    //        }
    //    }


    //}

    //public override void RunForward(Neurode[] parentLayer)
    //{

    //    float activationValue = 0;

    //    int index = 0;

    //    for (int n = 0; n < parentLayer.Length; n++)
    //    {
    //        activationValue = parentLayer[i].Delta;

    //        for (int i = 0; i < chromosomeCount - memoryChromosomeCount; i++)
    //        {
    //            activationValue *= Chromosones[n][index];
    //            index++;
    //        }


    //        if (System.Math.Tanh(activationValue) > 0)
    //        {
    //            activationValue = parentLayer[i].Delta;

    //            for (int i = 0; i < memoryChromosomeCount; i++)
    //            {
    //                activationValue *= Chromosones[n][index];
    //                index++;
    //            }

    //            delta = (float)System.Math.Tanh(activationValue);

    //        }
    //    }
    //}


    public override void RunForward(Neurode[][] network)
    {
        float activationValue = 0;

        for (int i = 0; i < network[layer - 1].Length; i++)
            activationValue += network[layer - 1][i].Delta * weight[i] * bias[i];

        //delta = (float)System.Math.Tanh((double)activationValue);
        delta = System.Math.Max(0, activationValue);
    }

    public override Neurode Merge(Neurode neurodeA, Neurode neurodeB, MergeType type)
    {
        throw new System.NotImplementedException();
    }

    public override void MergeInToThis(Neurode partnerNeurode, MergeType type)
    {
        delta = 0;

        if(type == MergeType.Merge)
            for (int i = 0; i < partnerNeurode.Weight.Length; i++)
            {
                bias[i] = (partnerNeurode.Bias[i] + bias[i]) / 2f;
                weight[i] = (partnerNeurode.Weight[i] + weight[i]) / 2f;
                if (bias[i] > 1f)
                    bias[i] = 1f;
                else if (bias[i] < -1f)
                    bias[i] = -1f;

                if (weight[i] > 1f)
                    weight[i] = 1f;
                else if (weight[i] < -1f)
                    weight[i] = -1f;
            }

        if (type == MergeType.Schuffle)
            for (int i = 0; i < partnerNeurode.Weight.Length; i++)
                if (i % 2 == 0)                //dont always schuffle the same
                {
                    bias[i] = (partnerNeurode.Bias[i] + bias[i]) / 2f;
                    weight[i] = (partnerNeurode.Weight[i] + weight[i]) / 2f;
                    if (bias[i] > 1f)
                        bias[i] = 1f;
                    else if (bias[i] < -1f)
                        bias[i] = -1f;

                    if (weight[i] > 1f)
                        weight[i] = 1f;
                    else if (weight[i] < -1f)
                        weight[i] = -1f;
                }

        if (type == MergeType.Lerp)
        {
            float t = .75f;
            for (int i = 0; i < partnerNeurode.Weight.Length; i++)
            {

                bias[i] = bias[i] + t * (partnerNeurode.Bias[i] - bias[i]);
                weight[i] = weight[i] + t * (partnerNeurode.Weight[i] - weight[i]);
                if (bias[i] > 1f)
                    bias[i] = 1f;
                else if (bias[i] < -1f)
                    bias[i] = -1f;

                if (weight[i] > 1f)
                    weight[i] = 1f;
                else if (weight[i] < -1f)
                    weight[i] = -1f;
            }
        }



    }

    public override void Respawn(System.Random randomGen)
    {
        for (int i = 0; i < bias.Length; i++)
        {
            bias[i] = (float)randomGen.NextDouble() - .5f;
            weight[i] = (float)randomGen.NextDouble() - .5f;
        }

    }

    public override void AlterNeurode(Neurode baseNeurode, float alternation, System.Random randomGen)
    {
        float magnitude = (float)(.2d + randomGen.NextDouble()) * alternation;
        for(int i = 0; i < bias.Length; i ++)
        {
            if (randomGen.NextDouble() > alternation)
            {
                bias[i] = baseNeurode.Bias[i];
                weight[i] = baseNeurode.Weight[i];
                if (bias[i] > 1f)
                    bias[i] = 1f;
                else if (bias[i] < -1f)
                    bias[i] = -1f;

                if (weight[i] > 1f)
                    weight[i] = 1f;
                else if (weight[i] < -1f)
                    weight[i] = -1f;
                continue;
            }
            bias[i] = baseNeurode.Bias[i] + (.5f - (float)randomGen.NextDouble()) * magnitude;
            weight[i] = baseNeurode.Weight[i] + (.5f - (float)randomGen.NextDouble()) * magnitude;
            if (bias[i] > 1f)
                bias[i] = 1f;
            else if (bias[i] < -1f)
                bias[i] = -1f;

            if (weight[i] > 1f)
                weight[i] = 1f;
            else if (weight[i] < -1f)
                weight[i] = -1f;
        }    
    }
}