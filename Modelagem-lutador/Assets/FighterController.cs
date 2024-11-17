
using UnityEngine;

public class FighterController : MonoBehaviour
{
    public float raySpacing = 1f;
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

           
            for (int i = 0; i < 5; i++) {

                RaycastHit hit;

                Vector3 direction = fighter.transform.forward + new Vector3(i * fighter.raySpacing, 0f, 0f);
                Ray ray = new Ray(fighter.transform.position, direction.normalized);
                if (Physics.Raycast(ray, out hit, 1000f))
                {
                    Debug.DrawRay(fighter.transform.position, direction * hit.distance, Color.yellow);

                    Debug.Log("bubble");

                }
                else
                {
                    Debug.DrawRay(fighter.transform.position, direction, Color.yellow);
                    Debug.Log("no hit");
                }
            }
          
            if (Input.GetKeyDown(KeyCode.J))
            {
                fighter._state = new GuardState();
                //fighter.animator.SetTrigger("Guard");
            }
            if (Input.GetKeyDown(KeyCode.K))
            {
                fighter._state = new MahTawaState();
                //fighter.animator.SetTrigger("MahTawa");
            }
            if (Input.GetKeyDown(KeyCode.L))
            {
                fighter._state = new JasunbalTolioState();
                //fighter.animator.SetTrigger("JasunbalTolio");
            }
            if (Input.GetKeyDown(KeyCode.N))
            {
                fighter._state = new MondolioRurioState();
                //fighter.animator.SetTrigger("MondolioRurio");
            }
            if (Input.GetKeyDown(KeyCode.M))
            {
                fighter._state = new DubonTolioState();
                //fighter.animator.SetTrigger("DubonTolio");
            }
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

    void Start()
    {
        animator = GetComponent<Animator>();
        _state = new BouncingState();
    }

    void Update()
    {
        /*
        if (Input.GetKeyDown(KeyCode.J))
        {
            animator.SetTrigger("Guard");
        }
        if (Input.GetKeyDown(KeyCode.K))
        {
            animator.SetTrigger("MahTawa");
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            animator.SetTrigger("JasunbalTolio");
        }
        if (Input.GetKeyDown(KeyCode.N))
        {
            animator.SetTrigger("MondolioRurio");
        }
        if (Input.GetKeyDown(KeyCode.M))
        {
            animator.SetTrigger("DubonTolio");
        }*/

        _state.handleInput(this);
        _state.update(this);

    }
}
