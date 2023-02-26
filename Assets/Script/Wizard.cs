using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wizard : MonoBehaviour
{
    //[SerializeField] bool yerde;
    private bool space;
    private Rigidbody2D rb;
    private BoxCollider2D bc;
    private CapsuleCollider2D cp;
    private Vector3 ray1, ray2;
    private float kuvvet;
    private float time = 2;
    private float horizontal;
    private bool yerde;
    private bool yerdeonceki;
    [SerializeField] float maxkuvvet;
    [SerializeField] Animator anim;
    [SerializeField] LayerMask lm;
    [SerializeField] Camera cam;

    void Start()
    {
        kuvvet = 0;
        //yerde = false;
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        bc = GetComponent<BoxCollider2D>();
        cp = GetComponent<CapsuleCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (yerdemi())
            dondur();
        if ((Input.GetKey(KeyCode.A)|| Input.GetKey(KeyCode.D)) && yerdemi() && !space)
        {
            move();
        }
        else
        {
            anim.SetBool("yuru", false);
        }
        if (Input.GetKeyDown(KeyCode.Space) && yerdemi())
        {
            anim.SetBool("zipla", true);
            space = true;
            time = 2;
            StartCoroutine("ziplamakuvvet");
        }
        if (Input.GetKeyUp(KeyCode.Space) && space)
        {
            anim.SetBool("zipla", false);
            space = false;
            StopCoroutine("ziplamakuvvet");
            if (yerdemi())
                zipla();
        }



    }

    /*private void yerdedegisim()
    {
        
        if(yerde==false&&yerdeonceki==true)
        {
            //rb.velocity = new Vector2(horizontal, rb.velocity.y);
        }
        if (yerde != yerdeonceki)
            yerdeonceki = yerde;
    }*/
    IEnumerator ziplamakuvvet()
    {
        
        while (true)
        {
            if (time < 1)
            {
                zipla();
                break;
            }
            time -= Time.deltaTime;
            kuvvet = (time) switch
            {
                (<2)and(>=1.8f)=>maxkuvvet*.2f,
                (<1.8f) and (>= 1.6f) => maxkuvvet * .36f,
                (<1.6f) and (>= 1.4f) => maxkuvvet * .52f,
                (<1.4f) and (>= 1.2f) => maxkuvvet * .68f,
                (<1.2f) and (>= 1) => maxkuvvet * .84f,
                (<1)=> maxkuvvet,
                _ => 0
            };
            //kuvvet = maxkuvvet / time;
            yield return 0;
        }

    }

    /*private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "plat")
        {
            foreach (var item in collision.contacts)
            { 
                if (item.point.y<transform.position.y)
                    yerde = true;
            }
           // hareketsizkal();
        }
    }*/
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "plat")
        {
            
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.transform.tag == "plat")
        {
            
        }
    }
    private void zipla()
    {
        
        space = false;
        anim.SetBool("zipla", false);
      
        if (Input.GetAxis("Horizontal") > 0)
           // rb.velocity = new Vector3((Input.GetAxis("Horizontal")+rb.velocity.x)* 5f,2, 0);
         rb.AddForce(new Vector2(1.1f, 2) * kuvvet);
        else if (Input.GetAxis("Horizontal") < 0)
            rb.AddForce(new Vector2(-1.1f, 2) * kuvvet);
        else
            rb.AddForce(new Vector2(0, 2) * kuvvet);


    }

    void dondur()
    {
        float x = Input.GetAxis("Horizontal");
        if (x < 0)
        {
            //transform.rotation = Quaternion.Euler(new Vector3(0, -180, 0));
            GetComponent<SpriteRenderer>().flipX = true;
        }
        else if (x > 0)
        {
            GetComponent<SpriteRenderer>().flipX = false;
            //transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
        }
    }
    void move()
    {
       
        anim.SetBool("yuru", true);
        horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        //rb.velocity = new Vector3(horizontal*5f, rb.velocity.y, 0);
        transform.Translate(new Vector2(horizontal, 0)*Time.deltaTime*3f);
    }
    void hareketsizkal()
    {
        rb.constraints = RigidbodyConstraints2D.FreezeAll;
        Invoke("hareketiac", 0.05f);
    }
    void hareketiac()
    {
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
    }
    private bool yerdemi()
    {
       if(Physics2D.CircleCast(transform.position,.45f,new Vector2(0,-1), .5f,lm))
       {
            yerde = true;
           return true;
       }
       // if(Physics2D.BoxCast(transform.position, new Vector2(1.5f,1.5f),0, new Vector2(0, -1),1f,lm))
       // {
       //     return true;
       // }
            
        else
        {
            yerde = false;
            return false;

        }
        
        //return Physics2D.BoxCast(bc.bounds.center, bc.size, 0, Vector2.down, .1f,lm);
        //bool a = Physics2D.BoxCast(cp.bounds.center, cp.size, 0, Vector2.down, .001f, lm);
        //return a;

    }
    private Vector2 raynokta()
    {
        if(GetComponent<SpriteRenderer>().flipX == true)
        {
            return new Vector2(transform.position.x + .5f, transform.position.y);
        }
        else
        {
            return new Vector2(transform.position.x - .5f, transform.position.y);
        }
    }
  
}
