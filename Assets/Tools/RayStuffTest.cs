using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Unity.AI.Navigation;


public class RayStuffTest : MonoBehaviour
{
    //public ProjectileSoundManager soundManager;

    //public NavMeshSurface surfaceA;
    //public NavMeshSurface surfaceB;

    //Vector3 currentPosition;
    //Vector3 lastPosition;
    //Vector3 offset;

    //private List<GameObject> gameObjects = new List<GameObject>();
    //private List<int> indexes = new List<int>();


    //public AudioClip clip;
    //public AudioClip clipB;

    //public AudioSource source;


    //private void OnTriggerStay(Collider other)
    //{
    //    //other.gameObject.GetInstanceID();
    //    //other.gameObject.transform.position += offset;

    //}

    //private void OnTriggerEnter(Collider other)
    //{
      
    //}

    //private void OnTriggerExit(Collider other)
    //{
        


    //}

    //private void Awake()
    //{
    //    surfaceA.BuildNavMesh();

    //}

    //void Start()
    //{
    //    NavMeshAgent agent;

    //}

    //float curTime = 0;
    //float maxTime = 3f;


    //public float ya = 0;
    //private float yb = 0;

    //// Update is called once per frame
    //void Update()
    //{

    //    if (Input.GetKey(KeyCode.W))
    //        ya -= 1;
    //    if (Input.GetKey(KeyCode.S))
    //        ya += 1;

    //    Debug.Log((ya - yb) * (ya - yb) + (yb - ya) * (yb - ya));         



    //    //lastPosition = currentPosition;
    //    //currentPosition = this.transform.position;
    //    //offset = currentPosition - lastPosition;
    //    ////this.transform.position = new Vector3(this.transform.position.x - Time.deltaTime * 3f, this.transform.position.y, this.transform.position.z);
    //    ////player.transform.position += offset;
    //    //if (castRay)
    //    //{
    //    //    castRay = false;
    //    //    CastRay();
    //    //}

    //    //curTime += Time.deltaTime;

    //    //if (curTime > maxTime)
    //    //{
    //    //    source.PlayOneShot(clip);
    //    //    source.PlayOneShot(clipB);
    //    //    curTime = 0;
    //    //}
    //}



    //public GameObject player;
    //public bool castRay = false;
    //// Start is called before the first frame update
    //private void CastRay()
    //{
    //    int count = 10;
    //    RaycastHit hitinfo = new RaycastHit();

    //    Vector3 dir = Vector3.zero; // new Vector3(Random.Range(-.75f,.75f), 0, Random.Range(-.75f, .75f));

    //    if (Random.value > .5f)
    //        dir.x = Random.Range(.25f, .75f);
    //    else
    //        dir.x = Random.Range(-.75f, -.25f);

    //    if (Random.value > .5f)
    //        dir.z = Random.Range(.25f, .75f);
    //    else
    //        dir.z = Random.Range(-.75f, -.25f);

    //    Vector3 lastPosition = Vector3.zero;
    //    Vector3 origin = Vector3.zero;

    //    while (true && count != 0)
    //    {
    //        if (count == 10)
    //            Physics.Raycast(player.transform.position, dir, out hitinfo, 100f, 1 << LayerMask.NameToLayer("Bounding"));
    //        else
    //        {
    //            dir = Vector3.Reflect(dir, hitinfo.normal);
    //            dir.y = 0;

    //            origin = hitinfo.point + dir;

    //            Physics.Raycast(origin, dir, out hitinfo, 100f, 1 << LayerMask.NameToLayer("Bounding"));
    //            //Debug.DrawLine(hitinfo.point, lastPosition, Color.grey, 30f);

    //            if (hitinfo.collider == null)
    //            {
    //                Debug.DrawLine(origin, origin + dir * 100f, Color.black, 30f);

    //                Debug.DrawLine(origin, origin + Vector3.up, Color.black, 30f);
    //                Debug.DrawLine(origin + dir * 100f, origin + dir * 100f + Vector3.up, Color.black, 30f);

    //                Debug.Log(origin);
    //                Debug.Log(new Vector3(hitinfo.normal.x, 0, hitinfo.normal.z));
    //                Debug.Log(dir);

    //                return;
    //            }
    //        }

    //        count--;
    //        lastPosition = hitinfo.point;
    //    }

    //}
}
