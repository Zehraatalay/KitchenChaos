using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSounds : MonoBehaviour
{
    //SoundManager ile etkileþimli olmuþ oluyor normalde 2 class'ýn birbiriyle etkileþimli
    //olmaasý pek doðru deðil ancak bu durumda ikisi de sound yönetimini saðladýðý için
    //problem yok 

    private Player player;
    private float footstepTimer;
    private float footstepTimerMax = .1f;

    private void Awake()
    {
        player = GetComponent<Player>();
    }

    private void Update()
    {
        footstepTimer -= Time.deltaTime;
        if(footstepTimer < 0f)
        {
            footstepTimer = footstepTimerMax;
            //sadece player yürüdüðünde ses efekti olmalý 
            if (player.IsWalking())
            {
                float volume = 1f;
                SoundManager.Instance.PlayFootstepsSound(player.transform.position, volume);            
            }

        }
    }


}
