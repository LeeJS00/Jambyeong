using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class food : MonoBehaviour
{
    // Start is called before the first frame update
    public float foodspeed;
    public float endtime;
    public Text text_Timer;
    public Text text_Score;
    GameObject cooker;

    private float playtime;
    private float nowyear;
    private int score;
    private float pressed;
    private int firstsave;
    private int phase = 1;

    //food, saving food, customers
    GameObject newfood = null;
    GameObject food1 = null;
    GameObject food2 = null;
    GameObject food3 = null;
    GameObject food4 = null;
    GameObject food5 = null;
    GameObject save1 = null;
    GameObject save2 = null;
    GameObject save3 = null;
    GameObject save4 = null;
    GameObject save5 = null;
    GameObject customer = null;
    GameObject next_customer = null;

    Vector3 init_pos = new Vector3(5.7f, 3f, 0.0f);
    Vector3 trash_pos = new Vector3(5.3f, -2.8f, 0);
    Vector3 eat_pos = new Vector3(-2.4f, 1, 0);
    Vector3 save_pos = new Vector3(-4.8f, -2.7f, 0);
    Vector3 chef_pos = new Vector3(8.8f, 1.9f, 0.0f);
    Vector3 cust_pos = new Vector3(-3.2f, 1.1f, 0);
    Vector3 nextcust_pos = new Vector3(-7.7f, 1.1f, 0);

    GameObject[] foods = new GameObject[10];
    GameObject[] makers = new GameObject[3];
    GameObject[] humans = new GameObject[4];
    AudioClip[] sound = new AudioClip[20];
    private AudioSource[] audio = new AudioSource[20];

    void Start()
    {
        playtime = 0.0f;
        score = 0;
        pressed = 0.0f;

        for (int i = 0; i < 12; i++)
        {
            audio[i] = this.gameObject.AddComponent<AudioSource>();
            sound[i] = Resources.Load<AudioClip>("sound" + i);
            this.audio[i].clip = sound[i];
            this.audio[i].loop = false;
        }
        this.audio[5].volume -= 0.7f;
        this.audio[7].volume -= 0.4f;
        for (int i = 0; i < 3; i++)
        {
            foods[i] = Resources.Load<GameObject>("food_"+i.ToString());
        }
        for (int i = 0; i < 3; i++)
        {
            humans[i] = Resources.Load<GameObject>("face_" + i.ToString());
        }

        makers[0] = Resources.Load<GameObject>("maker0");
        makers[1] = Resources.Load<GameObject>("maker1");
        makers[2] = Resources.Load<GameObject>("maker2");
        cooker = Instantiate(makers[0], chef_pos, transform.rotation) as GameObject;
        customer = Instantiate(humans[0], cust_pos, transform.rotation) as GameObject;
        next_customer = Instantiate(humans[1], nextcust_pos, transform.rotation) as GameObject;
    }

    // Update is called once per frame
    void Update()
    {
        int flag = 0;

        playtime += Time.deltaTime;

        nowyear = playtime / (endtime / 30) + 2020;
        text_Timer.text = "A.D " + Mathf.Round(nowyear);
        text_Score.text = "Score : " + Mathf.Round(score);
        if (playtime > endtime / 6 && phase == 1)
        {
            phase = 2;
        }
        if (playtime > endtime / 6 * 2 && phase == 2)
        {
            phase = 3;
            Destroy(cooker);
            this.audio[10].Play();
            cooker = Instantiate(makers[1], chef_pos, transform.rotation) as GameObject;
        }
        if (playtime > endtime / 6 * 3 && phase == 3)
        {
            phase = 4;
        }
        if (playtime > endtime / 6 * 4 && phase == 4)
        {
            phase = 5;
            Destroy(cooker);
            this.audio[11].Play();
            cooker = Instantiate(makers[2], chef_pos, transform.rotation) as GameObject;
        }
        if (playtime > endtime / 6 * 5 && phase == 5)
        {
            phase = 6;
        }
        //finish
        if (playtime > endtime)
        {
        }

        //right arrow
        if (Input.GetKeyDown("right") && flag == 0)
        {
            pressed = playtime;
            this.audio[0].Play();
            flag = 1;
        }
        //left arrow
        if (Input.GetKeyDown("left") && flag == 0)
        {
            pressed = playtime;
            this.audio[0].Play();
            flag = 2;
        }
        //up arrow
        if (Input.GetKeyDown("up") && flag == 0)
        {
            pressed = playtime;
            this.audio[0].Play();
            flag = 3;
        }
        //down arrow
        if (Input.GetKeyDown("down") && flag == 0)
        {
            pressed = playtime;
            this.audio[0].Play();
            flag = 4;
        }
        //If key selected
        if (flag != 0)
        {
            //int fin = foodcontrol(flag);
            //if(fin==0) flag = 0;
            foodcontrol(flag);
        }
    }

    void foodcontrol(int flag)
    {
        int food_num = Random.Range(0, 3);
        int human_num = Random.Range(0, 3);

        if(human_num>)

        if (flag == 2 || flag == 4)
        {
            newfood = Instantiate(foods[food_num], init_pos, transform.rotation) as GameObject;
            if(phase==1 || phase ==2)
            {
                this.audio[7].Play();
            }
            if (phase == 3 || phase == 4)
            {
                this.audio[8].Play();
            }
            if (phase == 5 || phase == 6)
            {
                this.audio[9].Play();
            }
            if (newfood != null)
            {
                if (food5 != null) food5.transform.Translate(new Vector3(0, -3f, 0));
                if (food4 != null) food4.transform.Translate(new Vector3(-2f, 0, 0));
                if (food3 != null) food3.transform.Translate(new Vector3(-2f, 0, 0));
                if (food2 != null) food2.transform.Translate(new Vector3(-2f, 0, 0));

                if (food1 != null && flag == 2)
                {
                    food1.transform.position = eat_pos;
                    this.audio[2].Play();
                    GameObject temp = next_customer;
                    Destroy(next_customer);
                    Destroy(food1, 0.2f);
                    Destroy(customer, 0.2f);
                    customer = Instantiate(temp, cust_pos, transform.rotation);
                    next_customer = Instantiate(humans[human_num], nextcust_pos, transform.rotation);
                }
                if (food1 != null && flag == 4)
                {
                    food1.transform.position = save_pos;
                    this.audio[3].Play();
                    firstsave = 5;
                    if (save5 != null)
                    {
                        save5.transform.Translate(new Vector3(1.8f, 0, 0));
                        firstsave = 4;
                    }
                    if (save4 != null)
                    {
                        save4.transform.Translate(new Vector3(1.8f, 0, 0));
                        firstsave = 3;
                    }
                    if (save3 != null)
                    {
                        save3.transform.Translate(new Vector3(1.8f, 0, 0));
                        firstsave = 2;
                    }
                    if (save2 != null)
                    {
                        save2.transform.Translate(new Vector3(1.8f, 0, 0));
                        firstsave = 1;
                    }
                    if (save1 != null)
                    {
                        firstsave = 1;
                        save1.transform.position = trash_pos;
                        this.audio[5].Play();
                        Destroy(save1, 0.2f);
                    }
                    save1 = save2;
                    save2 = save3;
                    save3 = save4;
                    save4 = save5;
                    save5 = food1;
                }
                food1 = food2;
                food2 = food3;
                food3 = food4;
                food4 = food5;
                food5 = newfood;
            }
            else
            {
                Debug.Log("bad");
            }
        }
        //flag ==1, right arrow
        else if (flag == 1 || flag == 3)
        {
            switch (firstsave)
            {
                case 1:
                    if (flag == 1)
                    {
                        save1.transform.position = trash_pos;
                        this.audio[5].Play();
                        Destroy(save1, 0.2f);
                        firstsave = 2;
                    }
                    else if (flag == 3)
                    {
                        save1.transform.position = eat_pos;
                        GameObject temp = next_customer;
                        this.audio[2].Play();
                        Destroy(next_customer);
                        Destroy(save1, 0.2f);
                        Destroy(customer, 0.2f);
                        this.audio[2].Play();
                        customer = Instantiate(temp, cust_pos, transform.rotation);
                        next_customer = Instantiate(humans[human_num], nextcust_pos, transform.rotation);
                        firstsave = 2;
                    }
                    break;
                case 2:
                    if (flag == 1)
                    {
                        save2.transform.position = trash_pos;
                        this.audio[5].Play();
                        Destroy(save2, 0.2f);
                        firstsave = 3;
                    }
                    else if (flag == 3)
                    {
                        save2.transform.position = eat_pos;
                        GameObject temp = next_customer;
                        this.audio[2].Play();
                        Destroy(next_customer);
                        Destroy(save2, 0.2f);
                        Destroy(customer, 0.2f);
                        customer = Instantiate(temp, cust_pos, transform.rotation);
                        next_customer = Instantiate(humans[human_num], nextcust_pos, transform.rotation);
                        firstsave = 3;
                    }
                    break;
                case 3:
                    if (flag == 1)
                    {
                        save3.transform.position = trash_pos;
                        this.audio[5].Play();
                        Destroy(save3, 0.2f);
                        firstsave = 4;
                    }
                    else if (flag == 3)
                    {
                        save3.transform.position = eat_pos;
                        GameObject temp = next_customer;
                        this.audio[2].Play();
                        Destroy(next_customer);
                        Destroy(save3, 0.2f);
                        Destroy(customer, 0.2f);
                        customer = Instantiate(temp, cust_pos, transform.rotation);
                        next_customer = Instantiate(humans[human_num], nextcust_pos, transform.rotation);
                        firstsave = 4;
                    }
                    break;
                case 4:
                    if (flag == 1)
                    {
                        save4.transform.position = trash_pos;
                        this.audio[5].Play();
                        Destroy(save4, 0.2f);
                        firstsave = 5;
                    }
                    else if (flag == 3)
                    {
                        save4.transform.position = eat_pos;
                        GameObject temp = next_customer;
                        this.audio[2].Play();
                        Destroy(next_customer);
                        Destroy(save4, 0.2f);
                        Destroy(customer, 0.2f);
                        customer = Instantiate(temp, cust_pos, transform.rotation);
                        next_customer = Instantiate(humans[human_num], nextcust_pos, transform.rotation);
                        firstsave = 5;
                    }
                    break;
                case 5:
                    if (flag == 1)
                    {
                        if (save5 != null)
                        {
                            save5.transform.position = trash_pos;
                            this.audio[5].Play();
                            Destroy(save5, 0.2f);
                        }
                        firstsave = 5;
                    }
                    else if (flag == 3)
                    {
                        if (save5 != null)
                        {
                            save5.transform.position = eat_pos;
                            GameObject temp = next_customer;
                            this.audio[2].Play();
                            Destroy(next_customer);
                            Destroy(save5, 0.2f);
                            Destroy(customer, 0.2f);
                            customer = Instantiate(temp, cust_pos, transform.rotation);
                            next_customer = Instantiate(humans[human_num], nextcust_pos, transform.rotation);
                        }
                        firstsave = 5;
                    }
                    break;
            }

        }
    }
}
