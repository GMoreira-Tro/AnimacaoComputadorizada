
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
            
            //verificar inimigos a frente
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
                    if (hit.transform.gameObject.tag == "Bubble" || hit.transform.gameObject.tag == "Ghost") {

                        fighter.transform.position += fighter.transform.forward * Time.deltaTime;

                        var r = Random.Range(0,60);

                        if (r <= 20)
                        {
                            //defesa
                            fighter._state = new GuardState();
                        }
                        else if (r > 20 && r <= 40)
                        {
                            //chute sobre passo para frente
                            fighter._state = new JasunbalTolioState();
                        }
                        else
                        {
                            //pedalada 
                            fighter._state = new DubonTolioState();
                        }

                        Debug.Log("bubble front");

                    }
                   
                }
                else
                {
                    Debug.DrawRay(rayOrigin, direction * 10f, Color.red);
                    Debug.Log("no hit");
                }
            }

            //para o lado
            for (int i = 0; i < 5; i++)
            {
                RaycastHit hit;

                Vector3 direction = (fighter.transform.right * (i - 2) * fighter.raySpacing).normalized;

                // Usar a posição dos olhos como origem do raio
                Vector3 rayOrigin = fighter.eyesPosition.position;

                Ray ray = new Ray(rayOrigin, direction);

                if (Physics.Raycast(ray, out hit, 1000f))
                {
                    Debug.DrawRay(rayOrigin, direction * hit.distance, Color.yellow);
                    if (hit.transform.gameObject.tag == "Bubble" || hit.transform.gameObject.tag == "Ghost")
                    {

                        fighter.transform.position += fighter.transform.right * Time.deltaTime;

                        var r = Random.Range(0, 40);
                         if (r <= 20)
                        {
                            //soco cruzado e girando para o lado
                            fighter._state = new MahTawaState();
                        }
                        else if (r > 20 && r <= 40)
                        {
                            //chute giratório para o lado
                            fighter._state = new MondolioRurioState();
                        }
                        else
                        {
                            //pedalada 
                            //fighter._state = new DubonTolioState();
                        }

                        Debug.Log("bubble right");

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
            //defesa
                fighter._state = new GuardState();
            }
            if (Input.GetKeyDown(KeyCode.K))
            {
            //soco cruzado e girando para o lado
                fighter._state = new MahTawaState();
            }
            if (Input.GetKeyDown(KeyCode.L))
            {//chute sobre passo para frente
                fighter._state = new JasunbalTolioState();
            }
            if (Input.GetKeyDown(KeyCode.N))
            {//chute giratório para o lado
                fighter._state = new MondolioRurioState();
            }
            if (Input.GetKeyDown(KeyCode.M))
            {//pedalada 
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

    public float dayProgress;

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
            dayProgress = Mathf.Max(Mathf.PingPong(timeElapsed / dayDuration, 1f),0.25f);

            // Ajusta a exposição do Skybox (brilho)
            float exposure = Mathf.Lerp(0.2f, 1.2f, dayProgress); // Valores de exposição para noite e dia
            skyboxMaterial.SetFloat("_Exposure", exposure);

            // Ajusta a rotação da luz (para simular o Sol se movendo)
            float sunAngle = Mathf.Lerp(0, 360, dayProgress);
            directionalLight.transform.rotation = Quaternion.Euler(sunAngle - 90, 170, 0);

            // Ajusta a intensidade da luz para dia e noite
            directionalLight.intensity = Mathf.Lerp(0.1f, 1.0f, dayProgress);

            Debug.Log("Day progress: " + dayProgress);
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
