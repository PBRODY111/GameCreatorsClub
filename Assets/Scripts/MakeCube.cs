using UI;
using UnityEngine;

public class MakeCube : MonoBehaviour
{
    [SerializeField] private KeyCode key = KeyCode.F2;
    [SerializeField] private GameObject cubePrefab;
    [SerializeField] private Transform deskSpam;

    private void FixedUpdate()
    {
        if (Input.GetKey(key))
        {
            if (Player.Player.Instance.EpicModeEnabled()){
                Instantiate(cubePrefab, new Vector3(0f, 0.5f, 0f), new Quaternion(), deskSpam);
                DebugUI.NumDesks++;
            }
        }
    }
}