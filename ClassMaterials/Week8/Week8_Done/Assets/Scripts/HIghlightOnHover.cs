using UnityEngine;

public class HighlightOnHover : MonoBehaviour
{
    Material originalMat;
    [SerializeField] Material highlightMat;
    SpriteRenderer spriteRenderer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalMat = spriteRenderer.material;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(1))
            spriteRenderer.material = highlightMat;
        if (Input.GetMouseButtonUp(1))
            spriteRenderer.material = originalMat;
    }
}
