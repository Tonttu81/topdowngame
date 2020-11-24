using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraScript : MonoBehaviour
{
    public static CameraScript Instance { get; private set; }

    CinemachineVirtualCamera cam;
    public GameObject player;
    public float cameraSizeMin;
    public float cameraSizeMax;
    float zoom;
    float timer;

    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
        cam = GetComponent<CinemachineVirtualCamera>();
        zoom = cameraSizeMin;
    }

    // Update is called once per frame
    void Update()
    {
        zoom = Mathf.Clamp(zoom - Input.mouseScrollDelta.y, cameraSizeMin, cameraSizeMax);
        cam.m_Lens.OrthographicSize = Mathf.Clamp(zoom, cameraSizeMin, cameraSizeMax);
        if (player != null)
        {
            transform.position = new Vector3(player.transform.position.x, player.transform.position.y, -10f);
        }

        if (timer > 0)
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                CinemachineBasicMultiChannelPerlin cinemachineBasicMultiChannelPerlin = cam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

                cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = 0f;
            }
        }
    }

    private void FixedUpdate()
    {
        
    }

    public void ShakeCamera(float magnitude, float time)
    {
        CinemachineBasicMultiChannelPerlin cinemachineBasicMultiChannelPerlin = cam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

        cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = magnitude;
        timer = time;
    }
}
