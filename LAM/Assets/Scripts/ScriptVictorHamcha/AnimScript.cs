using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimScript : MonoBehaviour
{
    public Animator seijiAnimator;//animator du personnage seiji
    private PlayerManager playerManager;
    public bool lancerAnim;
    private void Start()
    {
        playerManager = FindObjectOfType<PlayerManager>();
    }

    private void Update()
    {
        if (playerManager.targetPosition==playerManager.playerBasePose && !seijiAnimator.GetCurrentAnimatorStateInfo(0).IsName("IDLE"))//on verifie seiji est entrain de se déplacer si ce n'est pas le cas alors on lance l'anim idle
        {
            AnimationSeiji("animIDLE");
        }
        
    }


    public void AnimationSeiji(string animName)//fonction qu'on met au bouton avec le quelle on veut que t'elle anim se lance (marche pour toutes les anim de seji qu'on aura)
    {
        if (lancerAnim)
        seijiAnimator.SetTrigger(animName);
        else
            seijiAnimator.SetTrigger("animIDLE");

    }


}
