# NewRepo



"just in case"


  private int startIndexSecondGate;
  private NeurodeType type;

  public override int Layer { get { return layer; } set { layer = value; } }
  public override float Delta { get { return delta; } set { delta = value; } }
  public override float[] Bias { get { return bias; } set { bias = value; } }
  public override float[] Weight { get { return weight; } set { weight = value; } }
  public override NeurodeType Type { get { return type; } set { type = value; } }
  public override float[][] Chromosones { get { return weight; } set { weight = value; } }

  int memoryChromosomeCount = 0;
  int chromosomeCount = 0;
  int memoryPercentage = 0;


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

  public override void RunForward(Neurode[] parentLayer)
  {

      float activationValue = 0;
      int index = 0;

      for (int n = 0; n < parentLayer.Length; n++)
      {
          activationValue = parentLayer[i].Delta;

          if (System.Math.Tanh(activationValue) > 0)
          {
              for (int i = 0; i < memoryChromosomeCount; i++)
              {
                  activationValue *= Chromosones[n][index];
                  index++;
              }
          }
      }

      for (int n = 0; n < parentLayer.Length; n++)
      {
          activationValue = parentLayer[i].Delta;

          if (System.Math.Tanh(activationValue) > 0)
          {
              for (int i = 0; i < memoryChromosomeCount; i++)
              {
                  activationValue *= Chromosones[n][index];
                  index++;
              }

              delta = (float)System.Math.Tanh(activationValue);
          }
      }
  }



"also just in case"

  public void RunForward(float[] input)
  {
      for (int n = 0; n < neurodes[0].Length - shortMemoryCount; n++)
      {
          neurodes[0][n].Delta = input[n];// Random.Range(-1,2);
      }

      for (int i = 0; i < shortMemoryCount; i++)
          neurodes[0][shortMemoryEntraceStartIndex + i].Delta = neurodes[neurodes.Length - 1][shortMemoryExitStartIndex + i].Delta;

      for (int i = 1; i < neurodes.Length; i++)
      {
          for (int n = 0; n < neurodes[i].Length; n++)
              neurodes[i][n].RunForward(neurodes[i - 1]);
      }

      if (clampValues)
      {
          float min = 5000;
          float max = -5000;

          for (int n = 0; n < neurodes[neurodes.Length - 1].Length; n++)
          {
              if (neurodes[neurodes.Length - 1][n].Delta < min)
                  min = neurodes[neurodes.Length - 1][n].Delta;
              if (neurodes[neurodes.Length - 1][n].Delta > max)
                  max = neurodes[neurodes.Length - 1][n].Delta;
          }

          if(min < 0)
          {
              if (min > -.45)
                  min -= .2f;
          }
          else
          {
              if (min > .05)
                  min -= .2f;
          }

          for (int n = 0; n < neurodes[neurodes.Length - 1].Length; n++)
              neurodes[neurodes.Length - 1][n].Delta = Mathf.InverseLerp(min, max, neurodes[neurodes.Length - 1][n].Delta);
      }
  }


"sorry"

 public NeuralNetwork(NetworkLayout[] networkLayout,int seed)
 {
     randomGen = new System.Random(seed);

     for (int i = 0; i < networkLayout.Length; i++)
         if (networkLayout[i].type == Neurode.NeurodeType.ShortMemoryNeurode)
             shortMemoryCount += networkLayout[i].count;

     for (int i = 0; i < networkLayout.Length; i++)
         if (networkLayout[i].type == Neurode.NeurodeType.Seed)
             seedCount += networkLayout[i].count;

     if (seedCount != 0)
     {
         if (seedCount <= 3)
             seedCount = 3;

         bin = new int[seedCount];

         bin[0] = 0;
         bin[1] = 1;
         bin[2] = 2;
     }
     for (int i = 3; i < seedCount; i ++)
     {
         bin[i] = bin[i-1] * 2;
     }

     int length = 0;

     for (int i = 0; i < networkLayout.Length; i++)//highest layer count from the sets
     {
         if (length < networkLayout[i].layout.Length)
             length = networkLayout[i].layout.Length;
     }

     neurodes = new Neurode[length][];

     for (int i = 0; i < length; i++)//calulate the length of each layer
     {
         int layerLength = 0;

         for (int n = 0; n < networkLayout.Length; n++)
         {
             if (networkLayout[n].layout.Length <= i)
                 continue;
             layerLength += networkLayout[n].layout[i];

         }

         if (i == 0 || i == length - 1)// loop input and out put layer for memory
         {
             if (i == 0)
                 shortMemoryEntraceStartIndex = layerLength;
             if (i == length - 1)
                 shortMemoryExitStartIndex = layerLength;

             layerLength += shortMemoryCount;

             if (i == 0)
                 seedEntraceStartIndex = layerLength;
             if (i == length - 1)
                 seedExitStartIndex = layerLength;

             layerLength += seedCount;

         }

         neurodes[i] = new Neurode[layerLength];
     }

     for (int i = 0; i < length; i++)//initialise layers
     {
         int d = 0;

         for (int n = 0; n < networkLayout.Length; n++)
         {
             if (networkLayout[n].layout.Length <= i)
                 continue; 
             
             for (int k = 0; k < networkLayout[n].layout[i]; k++)
             { 
                 if (i == 0)
                 {
                     if (networkLayout[n].type == Neurode.NeurodeType.ReLuNeurode)
                         neurodes[0][d] = new ReLuNeurode(d, 0, randomGen, Neurode.NeurodeType.ReLuNeurode);
                     if (networkLayout[n].type == Neurode.NeurodeType.TanNeurode)
                         neurodes[0][d] = new TanNeurode(d, 0, randomGen, Neurode.NeurodeType.TanNeurode);
                     if (networkLayout[n].type == Neurode.NeurodeType.MemoryNeurode)
                         neurodes[0][d] = new MemoryNeurode(d, 0, randomGen, Neurode.NeurodeType.MemoryNeurode);
                     if (networkLayout[n].type == Neurode.NeurodeType.SigmoidNeurode)
                         neurodes[0][d] = new SigmoidNeurode(d, 0, randomGen, Neurode.NeurodeType.SigmoidNeurode);
                 }
                 else
                 {
                     if (networkLayout[n].type == Neurode.NeurodeType.ReLuNeurode)
                         neurodes[i][d] = new ReLuNeurode(d, neurodes[i - 1].Length, randomGen, Neurode.NeurodeType.ReLuNeurode);
                     if (networkLayout[n].type == Neurode.NeurodeType.TanNeurode)
                         neurodes[i][d] = new TanNeurode(d, neurodes[i - 1].Length, randomGen, Neurode.NeurodeType.TanNeurode);
                     if (networkLayout[n].type == Neurode.NeurodeType.MemoryNeurode)
                         neurodes[i][d] = new MemoryNeurode(d, neurodes[i - 1].Length, randomGen, Neurode.NeurodeType.MemoryNeurode);
                     if (networkLayout[n].type == Neurode.NeurodeType.SigmoidNeurode)
                         neurodes[i][d] = new SigmoidNeurode(d, neurodes[i - 1].Length, randomGen, Neurode.NeurodeType.SigmoidNeurode);
                 }
                 d++;
             }
         }
         //-----------------------------------------------------------------------------------------------------------------------------!!!!!!
         if (i == 0 || i == length - 1)
         {
             for (int k = 0; k < shortMemoryCount; k++)
             {
                 if (i == 0)
                     neurodes[i][d] = new TanNeurode(d, 0, randomGen, Neurode.NeurodeType.TanNeurode);
                 else
                     neurodes[i][d] = new TanNeurode(d, neurodes[i - 1].Length, randomGen, Neurode.NeurodeType.TanNeurode);
                 d++;
             }

             for (int k = 0; k < seedCount; k++)
             {
                 if (i == 0)
                     neurodes[i][d] = new TanNeurode(d, 0, randomGen, Neurode.NeurodeType.ReLuNeurode);
                 else
                     neurodes[i][d] = new TanNeurode(d, neurodes[i - 1].Length, randomGen, Neurode.NeurodeType.ReLuNeurode);
                 d++;
             }

         }
         //-------------------------------------------------------------------------------------------------------------------------------------
     }
 }

public void RunForward(float[] input)
{
    for (int n = 0; n < neurodes[0].Length - shortMemoryCount; n++) //(####) ################
    {
        neurodes[0][n].Delta = input[n];// Random.Range(-1,2);
    }

    for (int i = 0; i < shortMemoryCount; i++)
        neurodes[0][shortMemoryEntraceStartIndex + i].Delta = neurodes[neurodes.Length - 1][shortMemoryExitStartIndex + i].Delta;

    for (int i = 1; i < neurodes.Length; i++)
    {
        for (int n = 0; n < neurodes[i].Length; n++)
            neurodes[i][n].RunForward(neurodes[i - 1]);
    }

    if (clampValues)//.......... for input / output       
    {
        float min = 5000;
        float max = -5000;

        for (int n = 0; n < neurodes[neurodes.Length - 1].Length; n++)
        {
            if (neurodes[neurodes.Length - 1][n].Delta < min)
                min = neurodes[neurodes.Length - 1][n].Delta;
            if (neurodes[neurodes.Length - 1][n].Delta > max)
                max = neurodes[neurodes.Length - 1][n].Delta;
        }

        if(min < 0)
        {
            if (min > -.45)
                min -= .2f;
        }
        else
        {
            if (min > .05)
                min -= .2f;
        }

        for (int n = 0; n < neurodes[neurodes.Length - 1].Length; n++)
            neurodes[neurodes.Length - 1][n].Delta = Mathf.InverseLerp(min, max, neurodes[neurodes.Length - 1][n].Delta);//normalize....
    }
}


  float maxValueLastDrawStage = 1;
  int drawStepCount = 0;
  float distanceX = 5f;
  float distanceZ = 1f;

  public void DrawConnections()
  {
      for (int i = 0; i < neurodes.Length; i++)
          for (int n = 0; n < neurodes[i].Length; n++)
          {
              Vector3 position = new Vector3(i * distanceX, 0, n * distanceZ - (neurodes[i].Length / 2 * distanceZ));
              Vector3 positionUp = new Vector3(position.x, neurodes[i][n].Delta * 3f, position.z);
              Debug.DrawLine(position, positionUp, Color.blue);
              
              if (i == 0)
                  continue;

              for (int k = 0; k < neurodes[i - 1].Length; k++)
              {
                  float c = neurodes[i][n].Weight[k] * neurodes[i - 1][k].Delta;
                  if (c <= 0)
                      c *= -1;
                  if (c > 1)
                      c = 1;
            
                  if (maxValueLastDrawStage < c)
                      maxValueLastDrawStage = c;

                  c /= maxValueLastDrawStage;

                  Vector3 positionB = new Vector3((i - 1) * distanceX, 0, k * distanceZ - (neurodes[i - 1].Length / 2 * distanceZ));
                  Debug.DrawLine(position, positionB,new Color(c,c,c));
              }
          }

      for (int i = 0; i < shortMemoryCount; i++)
      {
          Vector3 position = new Vector3(0 * distanceX, 0, (shortMemoryEntraceStartIndex + i) * distanceZ - (neurodes[0].Length / 2 * distanceZ));
          Vector3 positionB = new Vector3((neurodes.Length - 1) * distanceX, 0, (shortMemoryExitStartIndex + i) * distanceZ - (neurodes[neurodes.Length - 1].Length / 2 * distanceZ));
          Debug.DrawLine(position, positionB, Color.green);
      }

      if (drawStepCount == 300)
      {
          drawStepCount = 0;
          maxValueLastDrawStage = 1;
      }

      drawStepCount++;
  }



using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Neurode
{
    public enum MergeType { Merge, Schuffle, Lerp, Alter}
    public enum NeurodeType { ReLuNeurode, TanNeurode, ShortMemoryNeurode, MemoryNeurode, SigmoidNeurode, Seed };

    public abstract NeurodeType Type { get; set; }
    public abstract int Layer { get; set; }
    public abstract float Delta { get; set; }
    public abstract float[] Bias { get; set; }
    public abstract float[] Weight { get; set; }
    public abstract float[][] Chromosones { get; set; }


    //public abstract NetworkLayout.NeurodeType type;

    public abstract void RunForward(Neurode[] parentLayer);

    public abstract void RunForward(Neurode[][] network);


    public abstract void AlterNeurode(Neurode baseNeurode, float alternation, System.Random randomGen);
 
    
    public abstract Neurode Merge(Neurode neurodeA, Neurode neurodeB, MergeType type);
    public abstract void MergeInToThis(Neurode partnerNeurode, MergeType type);
    public abstract void Respawn(System.Random randomGen);

}

