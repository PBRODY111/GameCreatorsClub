using UnityEngine;

public class Attack1 : MonoBehaviour
{
    public float t;
    [SerializeField] private Vector3 startPosition;
    public Vector3 target;
    [SerializeField] private float timeToReachTarget;
    [SerializeField] private float rotationTime;
    public bool jumpscare;
    private Quaternion _lookRotation;

    // Start is called before the first frame update
    private void Start()
    {
        startPosition = transform.position;
    }

    // Update is called once per frame
    private void Update()
    {
        t += Time.deltaTime / timeToReachTarget;
        if (!jumpscare) return;

        var thisTransform = transform;
        thisTransform.position = Vector3.Lerp(startPosition, target, t);
        startPosition = thisTransform.position;
        
        var position = Player.Instance.transform.position;
        target = new Vector3(position.x, 0.4f, position.z);
        _lookRotation = Quaternion.LookRotation(target);
        thisTransform.LookAt(target);
        //transform.rotation = Quaternion.Slerp(transform.rotation, _lookRotation, Time.deltaTime * rotationTime);
    }
}