using UnityEngine;

public class StartLevel : MonoBehaviour
{
    public Vector3 startPosition;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GameManager.Instance.StartLevel(startPosition);
    }
}