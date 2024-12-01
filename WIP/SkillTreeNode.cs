using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillTreeNode : MonoBehaviour
{
    public bool isRoot = false;
    public bool isActive = false;
    public SkillTreeNode[] connectedNodes;
    public MeshRenderer[] connectors;

    public int nodeID = -1;
    public Material activeMaterial;
    public Material inactiveMaterial;

    public bool activate = false;
    public bool deactivate = false;



    public void Activate()
    {
        for (int i = 0; i < connectedNodes.Length; i++)
            if (connectedNodes[i].isActive)
            {
                this.transform.position = this.transform.position;
                isActive = true;
                this.transform.GetComponent<MeshRenderer>().material = activeMaterial;
                this.transform.localScale = Vector3.one;
                connectors[i].material = activeMaterial;
                return;
            }
    }

    public void Deactivate()
    {
        if (isRoot)
            return;

        List<int> excludedNodes = new List<int>();

        for (int i = 0; i < connectedNodes.Length; i++)
            if (connectedNodes[i].isActive)
            {
                excludedNodes.Clear();
                excludedNodes.Add(nodeID);
                if (!connectedNodes[i].IsConnectedToRoot(excludedNodes))
                    return;
            }

        this.transform.position = this.transform.position;
        this.transform.localScale = new Vector3(.8f, .8f, .8f);
        this.transform.GetComponent<MeshRenderer>().material = inactiveMaterial;

        for (int i = 0; i < connectedNodes.Length; i++)
            connectors[i].material = inactiveMaterial;

        isActive = false;
    }

    public bool IsConnectedToRoot(List<int> excludedNodes)
    {
        if (isRoot)
            return true;
        Debug.DrawLine(this.transform.position, this.transform.position + Vector3.up,Color.red,60);
        for (int i = 0; i < connectedNodes.Length; i++)
            if (connectedNodes[i].isActive)
            {
                bool alreadyChecked = false;
                for (int n = 0; n < excludedNodes.Count; n++)
                    if (connectedNodes[i].nodeID == excludedNodes[n])
                    {
                        alreadyChecked = true;
                        break;
                    }
                if (!alreadyChecked)
                {
                    excludedNodes.Add(nodeID);
                    if (connectedNodes[i].isRoot)
                        return true;
                    else
                        return connectedNodes[i].IsConnectedToRoot(excludedNodes);
                }
            }
        Debug.DrawLine(this.transform.position, this.transform.position + Vector3.right, Color.blue, 60);

        return false;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (activate)
        {
            Activate();
            activate = false;
        }
        if (deactivate)
        {
            Deactivate();
            deactivate = false;
        }

        for (int i = 0; i < connectedNodes.Length; i++)
            Debug.DrawLine(this.transform.position, connectedNodes[i].transform.position);

    }



}
