
using UnityEngine;

public class FighterController : MonoBehaviour
{
    public float raySpacing = 1f;
    public Transform eyesPosition;
    public interface FighterState
    {

        void handleInput(FighterController fighter);
        void update(FighterController fighter);
    }

    public class BouncingState : FighterState
    {
        public void handleInput(FighterController fighter)
        {

            fighter.transform.RotateAround(fighter.transform.position, Vector3.up, 20 * Time.deltaTime);


            for (int i = 0; i < 5; i++)
            {
                RaycastHit hit;

                Vector3 direction = (fighter.transform.forward + fighter.transform.right * (i - 2) * fighter.raySpacing).normalized;

                // Usar a posição dos olhos como origem do raio
                Vector3 rayOrigin = fighter.eyesPosition.position;

                Ray ray = new Ray(rayOrigin, direction);

                if (Physics.Raycast(ray, out hit, 1000f))
                {
                    Debug.DrawRay(rayOrigin, direction * hit.distance, Color.yellow);
                    if (hit.transform.gameObject.tag == "Bubble") {

                        var r = Random.Range(0,100);

                        if (r <= 20)
                        {

                            fighter._state = new GuardState();
                        }
                        else if (r > 20 && r <= 40)
                        {
                            fighter._state = new MahTawaState();
                        }
                        else if (r > 40 && r <= 60)
                        {
                            fighter._state = new JasunbalTolioState();
                        }
                        else if (r > 60 && r <= 80)
                        {
                            fighter._state = new MondolioRurioState();
                        }
                        else
                        {
                            fighter._state = new DubonTolioState();
                        }

                        Debug.Log("bubble");

                    }
                   
                }
                else
                {
                    Debug.DrawRay(rayOrigin, direction * 10f, Color.red);
                    Debug.Log("no hit");
                }
            }
            /*
            if (Input.GetKeyDown(KeyCode.J))
            {
                fighter._state = new GuardState();
            }
            if (Input.GetKeyDown(KeyCode.K))
            {
                fighter._state = new MahTawaState();
            }
            if (Input.GetKeyDown(KeyCode.L))
            {
                fighter._state = new JasunbalTolioState();
            }
            if (Input.GetKeyDown(KeyCode.N))
            {
                fighter._state = new MondolioRurioState();
            }
            if (Input.GetKeyDown(KeyCode.M))
            {
                fighter._state = new DubonTolioState();
            }*/
        }
        public void update(FighterController fighter)
        {

        }
    }

    public class GuardState : FighterState
    {
       public void handleInput(FighterController fighter)
        {

        }
       public void update(FighterController fighter)
        {
            fighter.animator.SetTrigger("Guard");
            fighter._state = new BouncingState();
        }
    }

    public class MahTawaState : FighterState
    {
        public void handleInput(FighterController fighter)
        {

        }
        public void update(FighterController fighter)
        {
            fighter.animator.SetTrigger("MahTawa");
            fighter._state = new BouncingState();
        }
    }

    public class JasunbalTolioState : FighterState
    {
        public void handleInput(FighterController fighter)
        {

        }
        public void update(FighterController fighter)
        {
            fighter.animator.SetTrigger("JasunbalTolio");
            fighter._state = new BouncingState();
        }
    }

    public class MondolioRurioState : FighterState
    {
        public void handleInput(FighterController fighter)
        {

        }
        public void update(FighterController fighter)
        {
            fighter.animator.SetTrigger("MondolioRurio");
            fighter._state = new BouncingState();
        }
    }

    public class DubonTolioState : FighterState
    {
        public void handleInput(FighterController fighter)
        {

        }
        public void update(FighterController fighter)
        {
            fighter.animator.SetTrigger("DubonTolio");
            fighter._state = new BouncingState();
        }
    }

    private FighterState _state;
    private Animator animator;
    private bool entardecendo;

    public Material skyboxMaterial; // O material do Skybox
    public Light directionalLight; // A luz que representa o Sol
    public float dayDuration = 30f; // Duração de um ciclo completo (em segundos)

    private float timeElapsed = 0f;

    public Camera mainCamera;

    public Color color1 = Color.cyan;
    public Color color2 = Color.blue;
    public float duration = 3.0F;
    public int framesDay = 0;
    public int secondsDay = 0;



    void Start()
    {
        animator = GetComponent<Animator>();
        _state = new BouncingState();

        mainCamera = Camera.main;
        mainCamera.backgroundColor = color1;
        entardecendo = true;
    }

    void Update()
    {
        _state.handleInput(this);
        _state.update(this);
        /*
        framesDay++;
        if (framesDay >= 60) {
            framesDay = 0;
            secondsDay++;
            if (secondsDay >= 60) {
                secondsDay = 0;
                float t = Mathf.PingPong(Time.time, duration) / duration;
                if (entardecendo)
                {
                    mainCamera.backgroundColor = Color.Lerp(color1, color2, t);
                }
                else
                {
                    mainCamera.backgroundColor = Color.Lerp(color2, color1, t);
                }
                if (t == 1)
                {
                    entardecendo = !entardecendo;
                }
            }
        }*/

        if (skyboxMaterial != null && directionalLight != null)
        {
            // Calcula o progresso do dia (0 a 1)
            timeElapsed += Time.deltaTime;
            float dayProgress = Mathf.Max(Mathf.PingPong(timeElapsed / dayDuration, 1f),0.25f);

            // Ajusta a exposição do Skybox (brilho)
            float exposure = Mathf.Lerp(0.2f, 1.2f, dayProgress); // Valores de exposição para noite e dia
            skyboxMaterial.SetFloat("_Exposure", exposure);

            // Ajusta a rotação da luz (para simular o Sol se movendo)
            float sunAngle = Mathf.Lerp(0, 360, dayProgress);
            directionalLight.transform.rotation = Quaternion.Euler(sunAngle - 90, 170, 0);

            // Ajusta a intensidade da luz para dia e noite
            directionalLight.intensity = Mathf.Lerp(0.1f, 1.0f, dayProgress);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("A esfera entrou em um gatilho com: " + other.gameObject.name);

        // Exemplo: Verifica a tag do objeto
        if (other.CompareTag("Bubble"))
        {
            Debug.Log("A esfera entrou em contato com um inimigo!");
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        // Exibe informações sobre o objeto que colidiu
        Debug.Log("Colisão detectada com: " + collision.gameObject.name);

        // Verifica se o objeto tem uma tag específica
        if (collision.gameObject.CompareTag("Bubble"))
        {
            Debug.Log("O Player colidiu com a malha!");
        }
    }


}
