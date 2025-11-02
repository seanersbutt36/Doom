using UnityEngine;

public class Billboard : MonoBehaviour
{
    private Transform target;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        target = FindAnyObjectByType<PlayerMove>().transform;
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(target);
    }
}
