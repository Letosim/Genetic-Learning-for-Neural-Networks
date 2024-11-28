using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Neurode
{
    public enum MergeType { Merge, Schuffle, Lerp, Alter}
    public enum NeurodeType { ReLuNeurode, TanNeurode, SigmoidNeurode, ShortMemoryNeurode, MemoryNeurode,  Seed };
    public abstract bool IsMemoryNeurode { get; set; }

    public abstract NeurodeType Type { get; set; }
    public abstract int Layer { get; set; }
    public abstract float Delta { get; set; }

    public abstract float[] Bias { get; set; }
    public abstract float[] Weight { get; set; }
  
    public abstract float[][] Chromosomes { get; set; }
    public abstract int InitiationChromosomeCount { get; set; }
    public abstract int MemoryChromosomeCount { get; set; }
    public abstract int ChromosomeCount { get; set; }

    public abstract void RunForwardChromosome(Neurode[] parentLayer);
    public abstract void RunForward(Neurode[] parentLayer);
    public abstract void RunForward(Neurode[][] network);

    public abstract void AlterNeurode(Neurode baseNeurode, float alternation, System.Random randomGen);
    public abstract Neurode Merge(Neurode neurodeA, Neurode neurodeB, MergeType type);
    public abstract void MergeInToThis(Neurode partnerNeurode, MergeType type);
    public abstract void MergeInToThis(Neurode partnerNeurode, MergeType type, System.Random randomGen);

    public abstract void Respawn(System.Random randomGen);

    public static float GetActivationValue(float activationValue, NeurodeType type)
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

    private static readonly float lowerThresholdReLu = 0.5f;
    private static readonly float upperThresholdReLu = 1.5f;

    private static readonly float lowerThresholdSigmoid = 0.25f;
    private static readonly float upperThresholdSigmoid = 0.75f;

    private static readonly float lowerThresholdTanh = -0.5f;
    private static readonly float upperThresholdTanh = 0.5f;
}


//public static float GetActivationValue(Neurode[] parentLayer, float[][] Chromosomes, int index, int chromosomneCount, NeurodeType type)
//{
//    for (int n = 0; n < parentLayer.Length; n++)
//        for (int i = 0; i < chromosomneCount; i++)
//        {

//            activationValue += parentLayer[i].Delta * Chromosomes[n][index];
//            index++;
//        }

//    if (type == Neurode.ReLuNeurode)
//    {
//        activationValue = System.Math.Max(0, activationValue);

//        if (activationValue >= lowerThresholdRelu && activationValue <= upperThresholdRelu)
//            return activationValue;
//        else
//            return 0;
//    }

//    if (type == Neurode.SigmoidNeurode)
//    {
//        activationValue = activationValue / (1f + activationValue);

//        if (activationValue >= lowerThresholdSigmoid && activationValue <= upperThresholdSigmoid)
//            return activationValue;
//        else
//            return 0;
//    }

//    if (type == Neurode.TanNeurode)
//    {
//        activationValue = System.Math.Tanh(activationValue);

//        if (activationValue >= lowerThresholdTanh && activationValue <= upperThresholdTanh)
//            return activationValue;
//        else
//            return 0;
//    }
//}

//public abstract void RunForwardChromosome(Neurode[] parentLayer, bool[] saveGate);
//public abstract void RunForwardChromosome(Neurode[] parentLayer, bool saveGate);
//public abstract void RunForward(Neurode[] parentLayer, bool[] saveGate);
//public abstract void RunForward(Neurode[] parentLayer, bool saveGate);
//public abstract void RunForwardChromosomeSaveGate(Neurode[] parentLayer);
//public abstract void RunForwardChromosomeSaveGateArray(Neurode[] parentLayer);
//public abstract NetworkLayout.NeurodeType type;

