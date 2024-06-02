using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WeepingAngel : MonoBehaviour
{
    public NavMeshAgent ai;
    public Transform player;
    Vector3 dest;
    public Camera playerCam;
    public float aiSpeed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Plane[] planes = GeometryUtility.CalculateFrustumPlanes(playerCam);
        float distance = Vector3.Distance(transform.position, player.position);
        if(GeometryUtility.TestPlanesAABB(planes, this.gameObject.GetComponent<Renderer>().bounds)){
            ai.speed = 0;
            ai.SetDestination(transform.position);
        }
        if(!GeometryUtility.TestPlanesAABB(planes, this.gameObject.GetComponent<Renderer>().bounds)){
            ai.speed = aiSpeed;
            dest = player.position;
            ai.destination = dest;
        }
    }

    public IEnumerator MusicBoxJumpscare(){
        yield return new WaitForSeconds(2f);
        transform.position = new Vector3(Player.Player.Instance.transform.position.x, gameObject.transform.position.y, Player.Player.Instance.transform.position.z);
    }
}
