using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.ShaderKeywordFilter;
using UnityEngine;

public class Flock : MonoBehaviour
{
    public float speed = 0.001f;
    float rotationSpeed = 4.0f;
    Vector3 averageHeading;
    Vector3 averagePosition;
    float neighbourDistance = 3.0f;

    bool turning=false;
    void Start()
    {
        speed = Random.Range(0.5f,1);
    }

 
    void Update()
    {
        if (Vector3.Distance(transform.position,Vector3.zero)>=  globleflock.tankSize)
        {
            turning = true;
        }
        else
        {
            turning= false;
        }
        if (turning)
        {
            Vector3 direction = Vector3.zero - transform.position;
            transform.rotation = Quaternion.Slerp(transform.rotation,
                                                        Quaternion.LookRotation(direction),
                                                        rotationSpeed * Time.deltaTime);
            speed = Random.Range(0.5f,1);
        }
        else
        {
            if(Random.Range(0,5)<1)
                ApplyRules();
        }

        if (Random.Range(0, 5) < 1)
            ApplyRules();
        transform.Translate(0, 0, Time.deltaTime*speed) ;
    }
    void ApplyRules()
    {
        GameObject[] gos;
        gos = globleflock.allfish;

        Vector3 vcentre = Vector3.zero;
        Vector3 vavoid = Vector3.zero;
        float gSpeed = 0.1f;
        
        Vector3 goalPos = globleflock.goalPos;

        float dist;

        int groupSize = 0;
        foreach (GameObject go in gos)
        {
            if(go != this.gameObject)
            {
                dist = Vector3.Distance(go.transform.position, this.transform.position);
                if (dist <= neighbourDistance)
                {
                    vcentre += go.transform.position;
                    groupSize++;

                    if(dist < 1.0f)
                    {
                        vavoid = vavoid + (this.transform.position - go.transform.position);
                    }

                    Flock anotherFlock = go.GetComponent<Flock>(); // Assuming the component is of type Flock
                    gSpeed = gSpeed + anotherFlock.speed;
                }
            }
        }
        if(groupSize > 0) 
        {
            vcentre = vcentre/groupSize +(goalPos- this.transform.position);
            speed = speed/groupSize;

            Vector3 direction = (vcentre + vavoid)-transform.position;
            if(direction !=Vector3.zero)
                transform.rotation =Quaternion.Slerp(transform.rotation,
                                                    Quaternion.LookRotation(direction),
                                                    rotationSpeed * Time.deltaTime);
        }
    }
}
