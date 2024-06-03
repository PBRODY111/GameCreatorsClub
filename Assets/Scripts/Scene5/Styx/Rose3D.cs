using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rose3D : MonoBehaviour
{
    [SerializeField] private Styx3D _gameController;
    // Start is called before the first frame update
    void Start()
    {
        _gameController = FindObjectOfType<Styx3D>();
        if (_gameController == null) Debug.LogError("GameController not found in the scene.");
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.name == "Zagreus")
        {
            _gameController.IncrementPointValue();
            gameObject.SetActive(false);
        }
    }
}
