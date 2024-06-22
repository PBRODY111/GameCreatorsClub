using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashrMonster : MonoBehaviour
{
    [SerializeField] private float timeToHide;
    [SerializeField] private DashR gameControl;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(HideAfter());
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Collide");
        if (collision.gameObject.CompareTag("Block"))
        {
            Debug.Log("HITMONSTER!");
            StartCoroutine(Death());
        }
    }

    void Update(){
        transform.rotation = Quaternion.Euler(0, 0, 0);
    }

    IEnumerator HideAfter(){
        yield return new WaitForSeconds(timeToHide);
        Destroy(gameObject);
    }

    IEnumerator Death(){
        yield return new WaitForSeconds(0.1f);
        gameControl.IncreasePoint();
        Destroy(gameObject);
    }
}
