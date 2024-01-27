using System.Collections;
using UnityEngine;

public class Wall : MonoBehaviour
{

        public Sprite dmgSprite;
        public int hp = 4;


        private SpriteRenderer spriteRenderer;
    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void Damage (int loss)
    {
        spriteRenderer.sprite = dmgSprite;
        hp -= loss;
        if (hp <= 0)
        gameObject.SetActive(false);
    }
}
