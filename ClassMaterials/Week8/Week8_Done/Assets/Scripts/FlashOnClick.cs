using UnityEngine;
using System.Collections;

public class FlashOnClick : MonoBehaviour
{
    [SerializeField] float duration = 0.25f;

    //this name needs to match the one in the description of the shader input in ShaderGraph
    int hitEffectAmount = Shader.PropertyToID("_HitEffectAmount");

    SpriteRenderer spriteRenderer;
    Material material;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        material = spriteRenderer.material;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
            StartCoroutine(ApplyHitEffect(0f, 1f));
    }

    IEnumerator ApplyHitEffect(float from, float to)
    {
        float timeLeft = duration;
        while(timeLeft > 0)
        {
            float lerpedAmount = Mathf.Lerp(from, to, (duration - timeLeft) / duration);
            material.SetFloat(hitEffectAmount, lerpedAmount);

            timeLeft -= Time.deltaTime;
            yield return null;
        }

        //dim the sprite back if we just made it bright
        if (from == 0)
            StartCoroutine(ApplyHitEffect(to, from));
    }
}
