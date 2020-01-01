using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private Transform player_transform;
    private Camera cam;
    private Vector3 velocity = Vector3.zero;
    [SerializeField] private float smoothTime;

    // Start is called before the first frame update
    void Start()
    {
        player_transform = GameObject.FindWithTag("Player").GetComponent<Transform>();
        cam = GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {

        
    }

    private void FixedUpdate()
    {
        Vector3 crosshairPos = cam.ScreenToWorldPoint(Input.mousePosition);
        Vector3 CamPos = player_transform.position + (crosshairPos - player_transform.position) / 5;

        Vector3 camTarget = new Vector3(CamPos.x, CamPos.y, transform.position.z);
        transform.position = Vector3.SmoothDamp(transform.position, camTarget, ref velocity, smoothTime);
    }
}
