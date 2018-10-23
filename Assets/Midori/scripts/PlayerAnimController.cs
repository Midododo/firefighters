using UnityEngine;
using System.Collections;

public class PlayerAnimController : MonoBehaviour
{
    private Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (Input.GetKeyDown("up"))
        {
            Debug.Log(Input.mousePosition);
            anim.SetBool("Run", true);
        }
        else
        {
            anim.SetBool("Run", false);
        }
        //bool jump = false;
        //if (Input.GetKeyDown(KeyCode.Space))
        //{
        //    jump = true;
        //}
        //GetComponent<Animator>().SetBool("Jump", jump);

    }
}