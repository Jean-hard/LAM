using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TwistManager : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer accueilSprite;
    [SerializeField]
    private SpriteRenderer couloirSprite;
    [SerializeField]
    private SpriteRenderer closeUpSprite;

    [SerializeField]
    private Sprite accueilTordu1;
    [SerializeField]
    private Sprite accueilTordu2;
    [SerializeField]
    private Material accueilTordu1Mat;
    [SerializeField]
    private Material accueilTordu2Mat;

    [SerializeField]
    private Sprite couloirTordu1;
    [SerializeField]
    private Sprite couloirTordu2;
    [SerializeField]
    private Material couloirTordu1Mat;
    [SerializeField]
    private Material couloirTordu2Mat;

    [SerializeField]
    private Sprite closeUpTordu1;
    [SerializeField]
    private Sprite closeUpTordu2;
    [SerializeField]
    private Material closeUpTordu1Mat;
    [SerializeField]
    private Material closeUpTordu2Mat;

    //test
    private bool twist1Done = false;

    public void TwistEnvironnement1()
    {
        accueilSprite.sprite = accueilTordu1;
        accueilSprite.material = accueilTordu1Mat;

        couloirSprite.sprite = couloirTordu1;
        couloirSprite.material = couloirTordu1Mat;

        closeUpSprite.sprite = closeUpTordu1;
        closeUpSprite.material = closeUpTordu1Mat;
    }

    public void TwistEnvironnement2()
    {
        accueilSprite.sprite = accueilTordu2;
        accueilSprite.material = accueilTordu2Mat;

        couloirSprite.sprite = couloirTordu2;
        couloirSprite.material = couloirTordu2Mat;

        closeUpSprite.sprite = closeUpTordu2;
        closeUpSprite.material = closeUpTordu2Mat;
    }

    public void TestTwist()
    {
        if (twist1Done)
            TwistEnvironnement2();
        else
            TwistEnvironnement1();

        twist1Done = true;
    }
}
