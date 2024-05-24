using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    private const string IS_WALKING = "IsWalking";

    [SerializeField] private Player player;
    private Animator animator;

    private void Awake() {
        animator = GetComponent<Animator>();
    }
/*
    public void Start() {
        if(player != null)
            player = GameObject.Find("player");
    }
*/

    //every single frame'de update edilmesini istiyoruz
    private void Update() {
        animator.SetBool(IS_WALKING, player.IsWalking());
    }


}
