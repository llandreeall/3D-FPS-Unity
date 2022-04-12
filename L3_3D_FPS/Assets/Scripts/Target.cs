using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Target : MonoBehaviour
{
    public float health = 50f;
    public int type;
    public GameObject destroyedCube;
    public GameObject ammo;
    public int maxHealth = 100;
    public Slider sl;
    public Animator anim;

    private void Start()
    {
        if (type == 1)
        {
            health = maxHealth;
            sl.value = health;
        }
    }

    public void TakeDamage(float val)
    {
        health -= val;
        if(health <= 0)
        {
            health = 0;
            Die();
        }
        if (type == 1)
            sl.value = health;
    }

    void Die()
    {
        //Destroy(gameObject);
        if(type == 0)
        {
            GameObject child = transform.GetChild(0).gameObject;
            child.transform.parent = null;
            //Instantiate(ammo, transform.position, ammo.transform.rotation);
            GameObject clone = Instantiate(destroyedCube, transform.position, transform.rotation);
            Destroy(clone, 5f);
            Destroy(gameObject);
        } else if(type == 1)
        {
            anim.SetBool("IsDead", true);
            StartCoroutine(Death(this.gameObject));
            sl.gameObject.SetActive(false);
            gameObject.GetComponent<EnemyAI>().enabled = false;
            this.enabled = false;
        }
    }

    IEnumerator Death(GameObject g)
    {
        yield return new WaitForSeconds(3.5f);
        Destroy(g);
    }
}
