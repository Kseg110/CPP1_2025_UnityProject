using UnityEngine;
// create a better follow camera solution rather than simply setting the camera object as the child of the player object
public class CameraFollow : MonoBehaviour
{
    [SerializeField] private float minXPos;
    [SerializeField] private float maxXPos;

    [SerializeField] private Transform target;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (!target)
            Debug.LogError("Target not set for CameraFollow script. Please assign a target in the inspector.");
    }

    // Update is called once per frame
    void Update()
    {
        if (!target) return;

        Vector3 pos = transform.position;
        pos.x = Mathf.Clamp(target.position.x, minXPos, maxXPos);
        transform.position = pos;
    }
}
