using System.Collections;
using UnityEngine;

public class GlitchMaterialController : MonoBehaviour
{
    public Material glitchMaterial; // Reference to the GlitchMaterial
    public Texture[] glitchTextures; // List of different textures
    public Texture originalTexture; // Original texture of the material
    public AudioSource corruptAudio; // Original texture of the material
    private bool isChangingTexture = false;

    void Start()
    {
        // Ensure glitchMaterial is assigned
        if (glitchMaterial == null)
        {
            Debug.LogError("GlitchMaterial not assigned!");
            return;
        }

        // Start the coroutine for changing textures
        glitchMaterial.SetTexture("_MainTex", originalTexture);
        StartCoroutine(ChangeTexturesPeriodically());
    }

    IEnumerator ChangeTexturesPeriodically()
    {
        while (true)
        {
            // Wait for a random time between 10 and 15 seconds
            yield return new WaitForSeconds(Random.Range(10f, 15f));
            corruptAudio.Play();

            // Change the texture every 0.4 seconds for four times
            for (int i = 0; i < glitchTextures.Length; i++)
            {
                isChangingTexture = true;
                glitchMaterial.SetTexture("_MainTex", glitchTextures[i]);
                yield return new WaitForSeconds(0.4f);
            }

            // Reset to the original texture
            glitchMaterial.SetTexture("_MainTex", originalTexture);
            isChangingTexture = false;
        }
    }
}
