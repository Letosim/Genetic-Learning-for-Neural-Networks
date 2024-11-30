using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkTest : MonoBehaviour
{
    public int instanceCount = 3;
    public int networkCount = 125;
    public int maxSteps = 250;
    public int maxDrawSteps = 5;
    public int maxDrawAdvancedSteps = 100;
    public bool saveNetwork = false;
    public bool loadNetwork = false;

    //NeuralNetwork network;
    public GameObject moveCube;
    NetworkManager networkManager;
    NN2DCarWorldData carWorldData;

    public bool useThreading = false;
    // Start is called before the first frame update
    void Start()
    {



        NetworkLayout[] networkLayout = new NetworkLayout[4];   // new NetworkLayout(NetworkLayout.NeurodeType.TanNeurode,new int[] { 3, 6, 6, 3 });
                                                                //networkLayout[0] = new NetworkLayout(Neurode.NeurodeType.TanNeurode, new int[] {      0, 0, 0 , 0 , 0 });
                                                                //networkLayout[1] = new NetworkLayout(Neurode.NeurodeType.TanNeurode, new int[] {      9, 0, 0, 0 , 5 });
                                                                //networkLayout[2] = new NetworkLayout(Neurode.NeurodeType.TanNeurode, new int[] {       0, 6, 6 , 6 , 0 });
                                                                //networkLayout[3] = new NetworkLayout(Neurode.NeurodeType.ShortMemoryNeurode, 0);
                                                                //networkLayout = new NetworkLayout[3];   // new NetworkLayout(NetworkLayout.NeurodeType.TanNeurode,new int[] { 3, 6, 6, 3 });
                                                                //networkLayout[0] = new NetworkLayout(Neurode.NeurodeType.SigmoidNeurode, new int[] { 13, 9, 9, 5 });
        networkLayout = new NetworkLayout[2];
        networkLayout[0] = new NetworkLayout(Neurode.NeurodeType.TanNeurode, new int[] { 25, 13, 10, 5 });
        networkLayout[1] = new NetworkLayout(Neurode.NeurodeType.MemoryNeurode, new int[] { 0, 4, 3, 0 });
        //networkLayout[2] = new NetworkLayout(Neurode.NeurodeType.ShortMemoryNeurode, 5);
        networkManager = new NetworkManager(instanceCount, networkCount, networkLayout, NetworkManager.AgentType.Flyer2D,0);

        //networkLayout = new NetworkLayout[1];
        //networkLayout[0] = new NetworkLayout(Neurode.NeurodeType.TanNeurode, new int[] { 13, 9, 9, 5 });
        //networkLayout[1] = new NetworkLayout(Neurode.NeurodeType.MemoryNeurode, new int[] { 0, 4, 2, 0 });
        //networkLayout[2] = new NetworkLayout(Neurode.NeurodeType.ShortMemoryNeurode, 3);


        carWorldData = new NN2DCarWorldData(13, 20, 25);

        //network = new NeuralNetwork(new int[] { 6, 12, 12, 6 }); 
        //network = new NeuralNetwork(networkLayout);

        


        //NetworkLayout[] networkLayout = new NetworkLayout[4];   // new NetworkLayout(NetworkLayout.NeurodeType.TanNeurode,new int[] { 3, 6, 6, 3 });
        //networkLayout[0] = new NetworkLayout(Neurode.NeurodeType.TanNeurode, new int[] { 9, 0, 0 });
        //networkLayout[1] = new NetworkLayout(Neurode.NeurodeType.TanNeurode, new int[] { 0, 0, 0 });
        //networkLayout[2] = new NetworkLayout(Neurode.NeurodeType.TanNeurode, new int[] { 0, 6, 12});
        //networkLayout[3] = new NetworkLayout(Neurode.NeurodeType.ShortMemoryNeurode, 3);


        networkManager.maxSteps = 250;
        networkManager.autostartTraining = true;
        networkManager.startTraining = false;
        networkManager.useThreading = useThreading;
    }


    float currentTime = 0;
    float maxTime = 1f;
    // Update is called once per frame
    void Update()
    {
        if (useThreading && (saveNetwork || loadNetwork))
        {
            saveNetwork = false;
            loadNetwork = false;
        }

        if (saveNetwork)
        {
            saveNetwork = false;
            networkManager.SaveNetwork(); 
        }
        if (loadNetwork)
        {
            loadNetwork = false;
            networkManager.SaveNetwork(); 
        }



        networkManager.maxSteps = maxSteps;

        float rnd = Random.value;

        if(rnd > .65f)
            networkManager.Update(Neurode.MergeType.Schuffle);
            else if (rnd > .4f)
                networkManager.Update(Neurode.MergeType.Merge);
                    else if (rnd > .25f)
                        networkManager.Update(Neurode.MergeType.Lerp);

        networkManager.Draw(maxDrawSteps, maxDrawAdvancedSteps);
        networkManager.useThreading = useThreading;


    //network.DrawConnections();
    //if (currentTime > maxTime)
    //{
    //network.RunForward(new float[] { moveCube.transform.position.x, moveCube.transform.position.y, moveCube.transform.position.z, moveCube.transform.rotation.x, moveCube.transform.rotation.y, moveCube.transform.rotation.z });
    //currentTime = 0;
    //}
    //currentTime += Time.deltaTime;
}
}
