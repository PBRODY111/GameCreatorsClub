using System.Collections;
using System.Collections.Generic;
using UnityEngine.Serialization;
using UnityEngine;
using Player.Inventory;

public class Cannon : MonoBehaviour
{
    [FormerlySerializedAs("Item")] public InventoryItem item;
    [SerializeField] private float reach;
    [SerializeField] private GameObject armCannon;
    [SerializeField] private GameObject sign;
    // Start is called before the first frame update
    void Start(){
        SaveData data2 = SaveSystem.LoadEndings();
        if(data2 == null){
            gameObject.SetActive(false);
        } else{
            Debug.Log(data2.ending);
            if(data2.ending < 1){
                gameObject.SetActive(false);
            } else{
                if(data2.ending >= 2){
                    sign.SetActive(true);
                    gameObject.SetActive(false);
                }
                if(!HasMinigames()){
                    gameObject.SetActive(false);
                }
            }
        }
    }
    private void OnMouseDown()
    {
        if (IsWithinReach()){
            armCannon.SetActive(true);
            PickUp();
        }
    }

    private void PickUp()
    {  
        Inventory.Instance.Add(item);
        Destroy(gameObject);
    }

    private bool IsWithinReach()
    {
        return Vector3.Distance(transform.position, Player.Player.Instance.transform.position) <= reach;
    }

    private bool HasMinigames(){
        SaveData data = SaveSystem.LoadMinigame();
        if(data != null){
            Debug.Log(data.styx);
            Debug.Log(data.doublePong);
            if(data.styx && data.doublePong){
                return true;
            }
        }
        return false;
    }
}
