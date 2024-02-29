using UnityEngine;
using UnityEngine.EventSystems;

namespace Scene1
{
    public class LadderScrewHole : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] private GameObject screws;
        public void OnPointerClick(PointerEventData eventData) => screws.transform.GetChild(transform.GetSiblingIndex()).gameObject.SetActive(true);
    }
}