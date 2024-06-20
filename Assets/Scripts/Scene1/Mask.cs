using System.Collections;
using UnityEngine;

public class Mask : MonoBehaviour
{
    public GameObject maskObject; // Reference to the GameObject named "maskObject"
    public GameObject intText2; // Reference to the GameObject named "maskObject"
    private bool isMoving = false; // Flag to prevent multiple simultaneous movements
    public Scene1.Toxin toxin;

    void Update()
    {
        // Check for right-click input
        if (Input.GetMouseButtonDown(1) && Player.Player.Instance.IsHolding("Mask")) // Right mouse button
        {
            // If not already moving, start the coroutine to move the maskObject
            if (!isMoving)
            {
                StartCoroutine(MoveMaskObject());
            } else {
                StartCoroutine(RemoveMaskObject());
            }
        }
        if (Input.GetKeyDown(KeyCode.Q) && Player.Player.Instance.IsHolding("Mask") && isMoving)
            StartCoroutine(RemoveMaskObject());
    }

    IEnumerator MoveMaskObject()
    {
        // Unhide the maskObject
        if (maskObject != null)
        {
            maskObject.SetActive(true);
            maskObject.GetComponent<Animator>().SetBool("isMask", true);
        }

        // Move the maskObject's Y position up by 2 units over 0.5 seconds
        float elapsedTime = 0f;
        Vector3 startPosition = maskObject.transform.position;

        /*while (elapsedTime < 0.5f)
        {
            float yOffset = Mathf.Lerp(0f, 2f, elapsedTime / 0.5f); // Calculate the Y offset using Mathf.Lerp
            maskObject.transform.Translate(Vector3.up * yOffset); // Move only along the Y-axis
            elapsedTime += Time.unscaledDeltaTime;
            yield return null;
        }*/
        yield return null;

        // Reset the flag after the movement is complete
        isMoving = true;
        intText2.SetActive(true);
        toxin.timerDelay = 0.5f;
    }

    IEnumerator RemoveMaskObject()
    {
        // Move the maskObject's Y position up by 2 units over 0.5 seconds
        float elapsedTime = 0f;
        Vector3 startPosition = maskObject.transform.position;

        /*while (elapsedTime < 0.5f)
        {
            float yOffset = Mathf.Lerp(0f, 2f, elapsedTime / 0.5f); // Calculate the Y offset using Mathf.Lerp
            maskObject.transform.Translate(Vector3.up * yOffset); // Move only along the Y-axis
            elapsedTime += Time.unscaledDeltaTime;
            yield return null;
        }*/
        yield return null;

        if (maskObject != null)
        {
            maskObject.GetComponent<Animator>().SetBool("isMask", false);
            yield return new WaitForSeconds(0.5f);
            maskObject.SetActive(false);
        }

        // Reset the flag after the movement is complete
        isMoving = false;
        intText2.SetActive(false);
        toxin.timerDelay = 0.1f;
    }
}
