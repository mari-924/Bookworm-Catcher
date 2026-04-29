using UnityEngine;

public class FollowTarget : MonoBehaviour
{
    
    [SerializeField] private Transform target;
    [SerializeField] private Vector3 offset;
    
    [SerializeField] [Range(0, 10)] private float smoothFactor;
    
    [SerializeField] private float xMin = 0f;
    [SerializeField] private float xMax = 42f;
    
    [SerializeField] private float yMin = 8f;
    [SerializeField] private float yMax = 42f;
    
    void FixedUpdate()
    {
        Follow();
    }

    private void Follow()
    {
        Vector3 cameraPosition = target.position + offset;
        cameraPosition.x = Mathf.Clamp(cameraPosition.x, xMin, xMax);
        cameraPosition.y = Mathf.Clamp(cameraPosition.y, yMin, yMax);
        cameraPosition.z = transform.position.z;
        
        
        transform.position = Vector3.Lerp(transform.position, cameraPosition, Time.fixedDeltaTime * smoothFactor);
    }
}
