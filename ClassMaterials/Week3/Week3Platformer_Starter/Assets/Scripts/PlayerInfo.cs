using UnityEngine;

public class PlayerInfo : MonoBehaviour
{
    public void Die()
    {
        Debug.Log("Oof, oww, my bones.");
        Destroy(gameObject);
    }
}
