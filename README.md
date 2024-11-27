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


