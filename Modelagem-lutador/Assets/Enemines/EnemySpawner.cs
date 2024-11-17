
using System.Collections.Specialized;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{

    public GameObject fighter;
    public GameObject bubble;

    private int frames = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        frames = 0;
    }

    // Update is called once per frame
    void Update()
    {
        frames++;
        if (frames >= 60)
        {
            frames = 0;
            if (Random.Range(-10f,10f) >= 0f) {
                var fx = fighter.transform.position.x;
                var fy = fighter.transform.position.y;
                var fz = fighter.transform.position.z;

                var inFront = 1;
                if (Random.Range(-10f,10f) <= 0f)
                {
                    inFront = -1;
                }

                var onLeft = 1;
                if (Random.Range(-10f, 10f) <= 0f)
                {
                    onLeft = -1;
                }

                var nextX = fx + onLeft * Random.Range(20f, 25f);
                var nextY = fy + Random.Range(0f, 20f);
                var nextZ = fz + inFront * Random.Range(20f, 25f);

                var newBubble = Instantiate(bubble, new Vector3(nextX, nextY, nextZ), Quaternion.identity);
            }
        }
    }
}
