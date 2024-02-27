using System.Collections;
using UnityEngine;

public class Breaker0 : MonoBehaviour
{
    [SerializeField] private float reach;
    [SerializeField] private GameObject intText;
    [SerializeField] private bool hasPower;
    [SerializeField] private AudioSource breakerAudio;
    
    // Start is called before the first frame update
    void Start()
    {
        hasPower = true;
        StartCoroutine(SetBreaker());
    }

    private void OnMouseExit()
    {
        intText.SetActive(false);
    }

    private void OnMouseOver()
    {
        intText.SetActive(IsWithinReach() && !hasPower);
        if (Input.GetKeyDown(KeyCode.E) && IsWithinReach() && !hasPower)
        {
            StartCoroutine(SetBreaker());
        }
    }

    private IEnumerator SetBreaker()
    {
        transform.rotation = Quaternion.Euler(-90f, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);
        breakerAudio.Play();
        hasPower = true;
        yield return new WaitForSeconds(Random.Range(15f, 30f));
        hasPower = false;
        transform.rotation = Quaternion.Euler(-220f, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);
        breakerAudio.Play();
    }

    private bool IsWithinReach()
    {
        return Vector3.Distance(transform.position, Player.Player.Instance.transform.position) <= reach;
    }
}
