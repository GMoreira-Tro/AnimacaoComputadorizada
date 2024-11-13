using UnityEngine;

public class FighterController : MonoBehaviour
{
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
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
        }
    }
}
