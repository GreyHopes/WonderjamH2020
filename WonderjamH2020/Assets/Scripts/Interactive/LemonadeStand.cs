﻿using Interactive.Base;
using System.Collections.Generic;
using UnityEngine;

public class LemonadeStand : QTEBehaviour
{
    [SerializeField]
    private int lemonadePrice;
    [SerializeField]
    private LimonadeQueue queue;
    [SerializeField]
    private int lemonsPerLemonade = 1;

    [SerializeField]
    private AudioSource sellLemonadeAudio;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Renderer>().sortingOrder = Mathf.RoundToInt(transform.position.y * 100f) * -1;
    }

    public void SellLemonade(Player player)
    {
        sellLemonadeAudio.Play();
        queue.ServeFirst();
        player.SellLemonade(lemonadePrice, lemonsPerLemonade);
    }

    public override UserAction GetAction(Player contextPlayer)
    {
        if (!contextPlayer.CanMakeLemonade(lemonsPerLemonade) || queue.IsEmpty)
        {
            return null;
        }
        else
        {
            return new ComboAction(contextPlayer.inputManager, new List<string> { "←", "→" }, 2, () => SellLemonade(contextPlayer), string.Format("Sell - ${0}/lemon"+(lemonsPerLemonade > 1 ? "s" : ""),lemonadePrice,lemonsPerLemonade));

        }
    }
}
