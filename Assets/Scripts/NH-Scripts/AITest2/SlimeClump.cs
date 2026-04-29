using UnityEngine;

public class SlimeClump : MonoBehaviour
{
    [SerializeField] private float destroyTimer = 6f;

    private void Update()
    {
        destroyTimer -= Time.deltaTime;

        if (destroyTimer <= 0f)
        {
            Destroy(gameObject);
        }
    }
}
