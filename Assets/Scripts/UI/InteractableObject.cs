using UnityEngine;

namespace UI
{
    public class InteractableObject : MonoBehaviour
    {
        [SerializeField] protected GameObject intText;
        [SerializeField] protected float reach;
        
        public void OnMouseExit() => intText.SetActive(false);
        
        public void OnMouseOver() => intText.SetActive(IsWithinReach());
        
        protected bool IsWithinReach() => Vector3.Distance(transform.position, Player.Player.Instance.transform.position) <= reach;
    }
}