using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Go_on_eat : MonoBehaviour
{
    public GameObject this_manager;
    public float limit_eat_time;
    float start_time = 3.0f;
    public Text eat_time;
    bool gameover = false;
    // Start is called before the first frame update
    void Start()
    {
  
    }

    // Update is called once per frame
    void Update()
    {
        if (!gameover)
        {
            if (start_time > 0)
                start_time -= Time.deltaTime;
            else
            {
                eat_time.text = limit_eat_time.ToString("F2");
                if (limit_eat_time > 0.0f)
                    limit_eat_time -= Time.deltaTime;
                else
                {
                    this_manager.GetComponent<GameFlowManager>().EndGame(false);
                    gameover = true;
                }
            }
        }
    }
}
