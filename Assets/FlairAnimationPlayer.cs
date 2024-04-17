using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;

using UnityEngine;

public class FlairAnimationPlayer : MonoBehaviour
{
    private Animator animator;
    // Start is called before the first frame update
    
    void Start()
    {
        animator = GetComponent<Animator>();
        if (animator != null)
        {
            animator.SetTrigger("OnFlair");
        }
    }
    
}
