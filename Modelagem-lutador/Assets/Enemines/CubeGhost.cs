
using UnityEngine;

public class CubeGhost : MonoBehaviour
{
    public GameObject fighter;


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
        horizontalFrames = Random.Range(0, 720);
        verticalFrames = Random.Range(0, 720);
    }

    // Update is called once per frame
    void Update()
    {
        if (fighter.GetComponent<FighterController>().dayProgress <= 0.8f)
        {
            Destroy(gameObject);
        }


        //frames++;
        if (frames >= maxFrames)
        {


            //Destroy(gameObject);
        }

        var nextSpeedX = Random.Range(-360f, 360f);
        var nextSpeedZ = Random.Range(-360f, 360f);

        //transform.position += Vector3.left * nextSpeedX * Time.deltaTime;

        if (paraEsquerda)
        {
            transform.position += Vector3.left * speed * Time.deltaTime;
            horizontalFrames++;
            if (horizontalFrames >= 720)
            {
                //paraEsquerda = false;
            }
        }
        else
        {
            transform.position -= Vector3.left * speed * Time.deltaTime;
            horizontalFrames--;
            if (horizontalFrames <= 0)
            {
                //paraEsquerda = true;
            }
        }

        if (subindo)
        {
            transform.position += Vector3.up * speed * Time.deltaTime;
            verticalFrames++;
            if (verticalFrames >= 720)
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
            if (depthFrames >= 720)
            {
                //paraFrente = false;
            }
        }
        else
        {
            transform.position -= Vector3.forward * speed * Time.deltaTime;
            depthFrames--;
            if (depthFrames <= 0)
            {
                // paraFrente = true;
            }
        }

    }


    void OnTriggerEnter(Collider other)
    {
        Debug.Log("A esfera entrou em um gatilho com: " + other.gameObject.name);

        // Exemplo: Verifica a tag do objeto
        if (other.CompareTag("Player"))
        {
            Debug.Log("O fantasma entrou em contato com um player!");
            Destroy(gameObject);
        }
    }

}
