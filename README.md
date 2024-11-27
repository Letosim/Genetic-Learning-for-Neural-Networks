# NewRepo

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UnitNN
{
    public abstract int SensorValueCount { get; set; }

    public abstract float Fitness { get; set; }
    public abstract float[] SensorValues { get; set; }
    public abstract float[] NetworkValues { get; set; }
    public abstract Color DrawColor { get; set; }


    public abstract void UpdateSensors();
    public abstract void UpdateSensors(WorldDataNN worldData);

    public abstract void Update();
    public abstract void Update(WorldDataNN worldData);
    //public abstract void Update(float fitness);

    public abstract void Reset();
    public abstract void Reset(WorldDataNN worldData);

    public abstract void Draw(float layer);
    public abstract void DrawAdvanced(float layer);

}

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

    public override void RunForward(Neurode[] parentLayer)
    {

        int memoryChromosomeCount = 10;
        int chromosomeCount = 30;

        float activationValue = 0;
        int index = 0;

        for (int n = 0; n < parentLayer.Length; n++)
        {
            activationValue = parentLayer[i].Delta;

            if (System.Math.Tanh(activationValue) > 0)
            {
                for (int i = 0; i < memoryChromosomeCount; i++)
                {
                    activationValue *= Chromosones[n][index];//individual for each neuron!?
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
                    activationValue *= Chromosones[n][index];//individual for each neuron!?
                    index++;
                }

                delta = (float)System.Math.Tanh(activationValue);
            }
        }
    }

    public override void RunForward(Neurode[] parentLayer, bool saveGateValue)
    {
        int memoryChromosomeCount = 10;
        int chromosomeCount = 30;

        float activationValue = 0;
        int index = 0;

        for (int n = 0; n < parentLayer.Length; n++)
        {
            activationValue = parentLayer[i].Delta;

            if (System.Math.Tanh(activationValue) > 0)
            {
                for (int i = 0; i < memoryChromosomeCount; i++)
                {
                    activationValue *= Chromosones[n][index];//GateValue
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
                    activationValue *= Chromosones[n][index];
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
                  activationValue *= Chromosones[n][i];
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
                  activationValue *= Chromosones[n][i];
              }

              delta = (float)System.Math.Tanh(activationValue);
          }
      }
  }

   public override void RunForward(Neurode[] parentLayer, bool saveGateValue)
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
                   activationValue *= Chromosones[n][i];//GateValue
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
                   activationValue *= Chromosones[n][i];
               }

               delta = (float)System.Math.Tanh(activationValue);
           }
       }
       if (saveGateValue)
           delta = activationValue;
       else
           delta = 0;
   }


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


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;
using System.Linq;

public class NetworkManager
{
    public enum AgentType { Racer2D, Flyer2D , Flyer3D};

    private NeuralNetwork[][] networks;
    private UnitNN[][] agents;
    private int totalNetworkCount = 0;
    private int[] freeNetworkIndexes;
    private WorldDataNN[] worldData;
    private System.Random randomGen;
    private bool[] instanceIsRunning;
    private int[] currentStep;
    private int generationsCurrentWorld = 0; 

    public int maxSteps;
    public int generationsBeforeNewWorld = 25;
    
    public AgentType currentAgentType;

    public float[][][] outPut;
    public int instanceCount;
    public int networkCount;

    private int networksToLoad = 10;
    private int topNetworksCount = 30;
    public Vector2Int[] topNetworks;

    private bool trainingIsRunning = false;

    public bool autostartTraining = false;
    public bool startTraining = false;
    public bool stopTraining = false;
    public bool useThreading = false;

    public NetworkManager(int instanceCount,int networkCount, NetworkLayout[] layout, AgentType type)
    {
        randomGen = new System.Random(System.DateTime.Now.Millisecond);
        currentAgentType = type;
        this.instanceCount = instanceCount;
        this.networkCount = networkCount;
        this.totalNetworkCount = instanceCount * networkCount;

        networks = new NeuralNetwork[instanceCount][];
        agents = new UnitNN[instanceCount][];
        topNetworks = new Vector2Int[topNetworksCount];
        outPut = new float[instanceCount][][];
        instanceIsRunning = new bool[instanceCount];
        currentStep = new int[instanceCount];
        freeNetworkIndexes = new int[instanceCount];
        worldData = new WorldDataNN[instanceCount];

        WorldDataNN sharedWorld = null;

        if (currentAgentType == AgentType.Racer2D)
         sharedWorld = new NN2DCarWorldData(90, 100, 110);




        float x = 0;
        float y = 0;

        //List .... 

        List<UnitNN> agentList = new List<UnitNN>();

        for (int i = 0; i < instanceCount; i++)     //world network
        {
            networks[i] = new NeuralNetwork[networkCount];
            
            agents[i] = new UnitNN[networkCount];
            outPut[i] = new float[networkCount][];

            if (currentAgentType == AgentType.Racer2D)
                worldData[i] = sharedWorld;


            if (currentAgentType == AgentType.Flyer2D)
            {
                float radius = 350;

                worldData[i] = new NN2dFlyerWorldData(agents[i], new Vector3(x * radius,0, y * radius) * 2.1f, 250, radius, i); //Vector3.up * (15f * (float)i), 250, radius, i); 
                worldData[i].GenerateNewWorld();
                x++;

                if (x > 2)
                {
                    x = 0;
                    y++;
                }


            }
            for (int n = 0; n < networkCount; n++)  //networks
            {
                if (currentAgentType == AgentType.Racer2D)
                    agents[i][n] = new NN2dCarAgent(sharedWorld,randomGen);

                if (currentAgentType == AgentType.Flyer2D)
                    agents[i][n] = new NN2DFlyerAgent(randomGen, n, worldData[i]);

                if (currentAgentType == AgentType.Flyer3D)
                {
                    agents[i][n] = new NN3DFlyerAgent(n);
                    agentList.Add(agents[i][n]);
                }
                //Concate

                networks[i][n] = new NeuralNetwork(layout,randomGen.Next(0, 2094967296));
                //networks[i][n].clampValues = true;
                outPut[i][n] = new float[networks[i][n].neurodes[networks[i][n].neurodes.Length - 1].Length];
            }
        }


        if (currentAgentType == AgentType.Flyer3D)
        {
            //sharedWorld = new NN3dFlyerWorldData(instanceCount * networkCount, (List<NN3DFlyerAgent>)agentList); // add
            List<NN3DFlyerAgent> castedList = agentList.Select(item => (NN3DFlyerAgent)item).ToList();
            sharedWorld = new NN3dFlyerWorldData(instanceCount * networkCount, castedList);

            for (int i = 0; i < instanceCount; i++)     //world network
                worldData[i] = sharedWorld;

        }

        for (int i = 0; i < instanceCount; i++)     //world network
            for (int n = 0; n < networkCount; n++)  //networks
                agents[i][n].Reset(sharedWorld);
    }

    public void Update(Neurode.MergeType mergeType)//use unity
    {

        if (stopTraining)
        {
            if (IsAnyThreadRunning())
            {
                for (int i = 0; i < instanceCount; i++)
                    currentStep[i] = maxSteps;

                return;
            }

            trainingIsRunning = false;
            startTraining = false;
            stopTraining = false;

            return;
        }

        if (startTraining)
        {
            StartTraining();
            startTraining = false;

            return;
        }

        if (!useThreading && trainingIsRunning)
        {
            for (int i = 0; i < instanceCount; i++)
                NetworkTrainingSingleStep(i);

            if(currentStep[0] > maxSteps)
                EndTraining(mergeType);

            return;
        }

        if (trainingIsRunning && !IsAnyThreadRunning() && useThreading)
        {
            EndTraining(mergeType);

            return;
        }

        if (!trainingIsRunning && autostartTraining)
            startTraining = true;
    }

    public void EndTraining(Neurode.MergeType mergeType)
    {
        ApplyUnitFitnessToNetworks();
        CalulateTopNetworks();
        MergeNetworks(mergeType);
        trainingIsRunning = false;
        Debug.Log("Reptoloid's-Net");
    }

    public void MergeNetworks(Neurode.MergeType mergeType)
    {
        int mergedNetworks = 0;
        int alteredNetworks = 0;

        int networksToMerge = (int)((float)totalNetworkCount * .3f);
        int networksToAlter = (int)((float)totalNetworkCount * .3f);
        int total = 0;
        int currentIndexTopNetwork = 0;
        int layerIndex = randomGen.Next(0, instanceCount);

        for (int i = 0; i < instanceCount; i++)
        {
            int networkIndex = randomGen.Next(0, networkCount);

            for (int n = 0; n < networkCount; n++)
            {
                bool isTopNetwork = false;

                for (int o = 0; o < topNetworksCount; o++)
                    if (layerIndex == topNetworks[o].x && networkIndex == topNetworks[o].y)
                    {
                        isTopNetwork = true;
                        break;
                    }

                if (isTopNetwork)
                {
                    networkIndex--;
                    if (networkIndex == -1)
                        networkIndex = networkCount - 1;
                    continue;

                }
                if (mergedNetworks == networksToMerge && alteredNetworks == networksToAlter)
                {
                    total++;

                    networks[layerIndex][networkIndex].Respawn();
                    agents[layerIndex][networkIndex].DrawColor = Color.green;
                    networkIndex--;
                    if (networkIndex == -1)
                        networkIndex = networkCount - 1;
                    continue;
                }

                if (mergedNetworks < networksToMerge)
                {
                    networks[layerIndex][networkIndex].MergeInToThis(networks[topNetworks[currentIndexTopNetwork].x][topNetworks[currentIndexTopNetwork].y], mergeType);
                    agents[layerIndex][networkIndex].DrawColor = Color.black;
                    mergedNetworks++;
                }
                else if (alteredNetworks < networksToAlter)
                {
                    networks[layerIndex][networkIndex].AlterInToThis(networks[topNetworks[currentIndexTopNetwork].x][topNetworks[currentIndexTopNetwork].y]);
                    agents[layerIndex][networkIndex].DrawColor = Color.white;
                    alteredNetworks++;
                }

                currentIndexTopNetwork++;
                if (currentIndexTopNetwork == topNetworksCount)
                    currentIndexTopNetwork = 0;

                networkIndex--;
                if (networkIndex == -1)
                    networkIndex = networkCount - 1;
            }

            layerIndex--;
            if (layerIndex == -1)
                layerIndex = instanceCount - 1;
        }
        Debug.Log(alteredNetworks);
        Debug.Log(mergedNetworks);
        Debug.Log(total);


    }

    public void StartTraining()
    {
        trainingIsRunning = true;

        Debug.Log("Start Training");
        for (int i = 0; i < instanceCount; i++)
        {
            instanceIsRunning[i] = false;
            currentStep[i] = 0;

            if (generationsCurrentWorld > generationsBeforeNewWorld)
            {
                if (currentAgentType == AgentType.Racer2D || currentAgentType == AgentType.Flyer3D)
                {
                    if (i == 0)
                        worldData[i].GenerateNewWorld();
                    else
                        worldData[i] = worldData[0];
                }
                generationsCurrentWorld = 0;
            }

            if (currentAgentType == AgentType.Flyer2D)
                worldData[i].RegenerateWorld();


            for (int n = 0; n < networkCount; n++)
            {
                networks[i][n].ResetDelta();
                agents[i][n].Reset(worldData[i]);
            }
        }
        if (useThreading)
        {
            for (int i = 0; i < instanceCount; i++)
                freeNetworkIndexes[i] = i;

            for (int i = 0; i < instanceCount; i++)
            {
                instanceIsRunning[i] = true;
                Thread newTrainingThread = new Thread(new ThreadStart(NetworkTrainingThread));

                //Thread newTrainingThread = new Thread(() => NetworkTrainingThread());
                newTrainingThread.Start();
                Thread.Sleep(50);
            }
        }
        generationsCurrentWorld++;
    }


    public void NetworkTrainingThread()
    {
        int index = -1;
        for (int i = 0; i < instanceCount; i++)
            if (freeNetworkIndexes[i] != -1)
            {
                index = freeNetworkIndexes[i];
                freeNetworkIndexes[i] = -1;
                break;
            }    


        while (currentStep[index] < maxSteps)
        {
            for (int n = 0; n < networkCount; n++)
                RunForward(index,n);

            currentStep[index]++;
        }
        instanceIsRunning[index] = false;
    }

    public void NetworkTrainingSingleStep(int index)
    {
        for (int n = 0; n < networkCount; n++)
            RunForward(index, n);

        currentStep[index]++;

    }

    public void RunForward(int instanceIndex, int networkIndex)
    {
        agents[instanceIndex][networkIndex].UpdateSensors(worldData[instanceIndex]);
        networks[instanceIndex][networkIndex].RunForward(agents[instanceIndex][networkIndex].SensorValues);

        for (int o = 0; o < agents[instanceIndex][networkIndex].NetworkValues.Length; o++)
            agents[instanceIndex][networkIndex].NetworkValues[o] = networks[instanceIndex][networkIndex].neurodes[networks[instanceIndex][networkIndex].neurodes.Length - 1][o].Delta;

        agents[instanceIndex][networkIndex].Update();

    }

    public void Draw(int maxDrawSteps,int maxDrawAdvancedSteps)
    {
        if (currentAgentType == AgentType.Racer2D || currentAgentType == AgentType.Flyer3D)
            worldData[0].Draw();

        int drawCount = 0;
        int drawAdavancedCount = 0;

        for (int i = 0; i < instanceCount; i++)
        {
            if (currentAgentType == AgentType.Flyer2D)
                worldData[i].Draw();

            for (int n = 0; n < networkCount; n++)
            {
                agents[i][n].Draw(n + drawCount);
                drawCount++;

                if (drawAdavancedCount < maxDrawAdvancedSteps)
                {
                    agents[i][n].DrawAdvanced(n + drawCount);
                    drawAdavancedCount++;
                }

                if (drawCount > maxDrawSteps)
                {
                    i = instanceCount;
                    n = networkCount;
                }
            }
        }
        for (int i = 0; i < topNetworks.Length; i++)
        {
            if (i == 0)
                networks[topNetworks[i].x][topNetworks[i].y].DrawConnections();
            agents[topNetworks[i].x][topNetworks[i].y].Draw(i);
            agents[topNetworks[i].x][topNetworks[i].y].DrawAdvanced(i);
        }

    }

    public void RunAllForward()
    {
        for (int i = 0; i < instanceCount; i++)
            for (int n = 0; n < networkCount; n++)
            {
                agents[i][n].UpdateSensors();
                networks[i][n].RunForward(agents[i][n].SensorValues);

                for (int o = 0; 0 < networks[i][n].neurodes[networks[i][n].neurodes.Length - 1].Length; o++)
                    agents[i][n].NetworkValues[o] = networks[i][n].neurodes[networks[i][n].neurodes.Length - 1][o].Delta;

                agents[i][n].Update();
            }
     }

    public float[][][] RunAllForward(float[][][] input)
    {
        for (int i = 0; i < instanceCount; i++)
            for (int n = 0; n < networkCount; n++)
            {
                networks[i][n].RunForward(input[i][n]);
                for (int o = 0; 0 < networks[i][n].neurodes[networks[i][n].neurodes.Length - 1].Length; o++)
                {
                    outPut[i][n][o] = networks[i][n].neurodes[networks[i][n].neurodes.Length - 1][o].Delta;
                }
            }


        return outPut;
    }

    public bool IsAnyThreadRunning()
    {
        for (int i = 0; i < instanceCount; i++)
            if (instanceIsRunning[i])
                return true;
        return false;
    }

    public void SetNetworkFitness(int instanceIndex, int networkIndex, float value)
    {
        networks[instanceIndex][networkIndex].fitness = value;
    }

    public void ApplyUnitFitnessToNetworks()
    {
        for (int i = 0; i < instanceCount; i++)
            for (int n = 0; n < networkCount; n++)
                networks[i][n].fitness =  0 + agents[i][n].Fitness;
    }

    public void CalulateTopNetworks()
    {
        float[] current = new float[topNetworksCount];
        for (int i = 0; i < topNetworksCount; i++)
            current[i] = -50000;


        for (int i = 0; i < instanceCount; i++)
            for (int n = 0; n < networkCount; n++)
                for (int o = 0; o < topNetworksCount; o++)
                    if (networks[i][n].fitness >= current[o])
                    {
                        current[o] = networks[i][n].fitness;
                        topNetworks[o] = new Vector2Int(i, n);
                        break;
                    }


    }



    public void SaveNetwork()
    {
        float fitness = -50000;
        string name;
        int index = -1;

        for (int i = 0; i < topNetworksCount; i++)
        {
            if (networks[topNetworks[i].x][topNetworks[i].y].fitness > fitness)
            {
                index = i;
                fitness = networks[topNetworks[i].x][topNetworks[i].y].fitness;
            }
        }

        name = currentAgentType.ToString();// "gen" + networks[0][0].generation + "_" + currentAgentType.ToString();

        for (int l = 0; l < networks[0][0].neurodes.Length; l++)
            name += "_" + networks[0][0].neurodes.Length;

        networks[topNetworks[index].x][topNetworks[index].y].SaveNetwork(name);

    }

    public void LoadNetwork()
    {
        string name;
        int loadedNetworks = 0;

 
        name = currentAgentType.ToString();// "gen" + networks[0][0].generation + "_" + currentAgentType.ToString();

        for (int l = 0; l < networks[0][0].neurodes.Length; l++)
            name += "_" + networks[0][0].neurodes.Length;

        NeuralNetwork.NeuralNetworkSaveContainer container = XmlTools.Deserialize<NeuralNetwork.NeuralNetworkSaveContainer>(name);



        for (int i = 0; i < instanceCount; i++)
            for (int n = 0; n < networkCount; n++)
            {
                bool isTopNetwork = false;
     
                for (int o = 0; o < topNetworksCount; o++)
                    if (i == topNetworks[o].x && n == topNetworks[o].y)
                    {
                        isTopNetwork = true;
                        break;
                    }

                if (isTopNetwork)
                    continue;

                for (int k = 0; k < networks[i][n].neurodes.Length; k++)
                    for (int l = 0; l < networks[i][n].neurodes[k].Length; l++)
                        for (int o = 0; o < networks[i][n].neurodes[k][l].Weight.Length; o++)
                        {
                            networks[i][n].neurodes[k][l].Bias[o] = container.bias[k][l][o];
                            networks[i][n].neurodes[k][l].Weight[o] = container.weight[k][l][o];
                            networks[i][n].neurodes[k][l].Layer = container.layer[k][l][o];
                            networks[i][n].neurodes[k][l].Type = container.type[k][l][o];
                        }


                if (loadedNetworks > networksToLoad)
                {
                    i = instanceCount;
                    n = networkCount;
                }

                loadedNetworks++;
            }

        StartTraining();

        //!trainingIsRunning starttraining


        //networks[topNetworks[index].x][topNetworks[index].y].SaveNetwork(name);

    }


}

