using UnityEngine;
using UnityEngine.EventSystems;

namespace Scene1
{
    public class LadderScrew : MonoBehaviour, IPointerClickHandler
    {
        public void OnPointerClick(PointerEventData eventData)
        {
            if(eventData.button == PointerEventData.InputButton.Right) transform.Rotate(0, 0, 45);
            else transform.localScale = transform.localScale.x.Equals(1f)
                ? new Vector3(0.5f, 0.5f, 0.5f)
                : new Vector3(1, 1, 1);
        }
    }
}