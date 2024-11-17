
using UnityEngine;

public class Bubble : MonoBehaviour
{

    private int frames = 0;
    private int maxFrames = 3600;

    private bool subindo = true;
    private bool paraFrente = true;
    private bool paraEsquerda = true;
    private float speed = 10f;

    private int horizontalFrames = 0;
    private int verticalFrames = 0;
    private int depthFrames = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        frames = 0;
        subindo = true;
        horizontalFrames = Random.Range(0, 360);
        verticalFrames = Random.Range(0,360);
    }

    // Update is called once per frame
    void Update()
    {
        frames++;
        if (frames >= maxFrames) {


            Destroy(gameObject);
        }

        var nextSpeedX = Random.Range(-360f, 360f);
        var nextSpeedZ = Random.Range(-360f, 360f);

        //transform.position += Vector3.left * nextSpeedX * Time.deltaTime;

        if (paraEsquerda)
        {
            transform.position += Vector3.left * speed * Time.deltaTime;
            horizontalFrames++;
            if (horizontalFrames >= 360)
            {
                paraEsquerda = false;
            }
        }
        else
        {
            transform.position -= Vector3.left * speed * Time.deltaTime;
            horizontalFrames--;
            if (horizontalFrames <= 0)
            {
                paraEsquerda = true;
            }
        }

        if (subindo)
        {
            transform.position += Vector3.up *speed  * Time.deltaTime;
            verticalFrames++;
            if (verticalFrames >= 360)
            {
                subindo = false;
            }
        }
        else
        {
            transform.position -= Vector3.up * speed * Time.deltaTime;
            verticalFrames--;
            if (verticalFrames <= 0)
            {
                subindo = true;
            }
        }
        //transform.position += Vector3.forward * nextSpeedZ * Time.deltaTime;

        if (paraFrente)
        {
            transform.position += Vector3.forward * speed * Time.deltaTime;
            depthFrames++;
            if (depthFrames >= 360)
            {
                paraFrente = false;
            }
        }
        else
        {
            transform.position -= Vector3.forward * speed * Time.deltaTime;
            depthFrames--;
            if (depthFrames <= 0)
            {
                paraFrente = true;
            }
        }

    }
}
