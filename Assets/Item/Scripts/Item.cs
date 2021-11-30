using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public ParticleSystem CollectVFX; 
    public float forceUpOnCollide;
    public Vector3 centerOfMass;
    public AudioClip CollectSound;
    public GameObject user;
    public Sprite icon;
    Rigidbody m_rigid;
    [HideInInspector]
    public bool active;
    private void OnEnable()
    {
        active = true;
    }
    private void Start()
    {
        m_rigid = GetComponent<Rigidbody>();
        m_rigid.centerOfMass = centerOfMass;
    }
    public virtual void PickUpItem()
    {
        ItemManager.instance.PickUpItem(this);
    }

    public virtual void UseItem()
    {
        Debug.Log(this.name);
    }
    void OnCollect(Collider other)
    {
        if (CollectSound)
        {
            AudioUtility.CreateSFX(CollectSound, transform.position, AudioUtility.AudioGroups.Pickup, 0f);
        }
        active = false;
        if (CollectVFX)
            CollectVFX.Play();

        if (m_rigid) m_rigid.AddForce(forceUpOnCollide * Vector3.up, ForceMode.Impulse);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            user = other.transform.parent.gameObject;
            PickUpItem();
            OnCollect(other);
        }
        else if (other.tag == "AI")
        {
            user = other.transform.parent.gameObject;
            UseItem();
            OnCollect(other);
        }
    }
}
