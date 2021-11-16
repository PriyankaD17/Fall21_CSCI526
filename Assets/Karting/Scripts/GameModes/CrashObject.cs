using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// This class inherits from TargetObject and represents a CrashObject.
/// </summary>
public class CrashObject : TargetObject
{
    [Header("CrashObject")]
    [Tooltip("The VFX prefab spawned when the object is collected")]
    public ParticleSystem CollectVFX;
    public GameObject levelObject;
    public bool timeRushObject;

    [Tooltip("The position of the centerOfMass of this rigidbody")]
    public Vector3 centerOfMass;

    [Tooltip("Apply a force to the crash object to make it fly up onTrigger")]
    public float forceUpOnCollide;

    Rigidbody m_rigid;

    void Start()
    {
        m_rigid = GetComponent<Rigidbody>();
        m_rigid.centerOfMass = centerOfMass;
        Register();
    }

    void OnCollect(Collider other)
    {
        if (CollectSound)
        {
            AudioUtility.CreateSFX(CollectSound, transform.position, AudioUtility.AudioGroups.Pickup, 0f);
        }
        if(timeRushObject)
            levelObject.GetComponent<Go_on_eat>().limit_eat_time = 10.0f;
        active = false;
        if (CollectVFX)
            CollectVFX.Play();
               
        if (m_rigid) m_rigid.AddForce(forceUpOnCollide*Vector3.up, ForceMode.Impulse);
        
        if (!timeRushObject)
            Objective.OnUnregisterPickup(this);

        TimeManager.OnAdjustTime(TimeGained);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!active) return;
        
        if ((layerMask.value & 1 << other.gameObject.layer) > 0 && other.gameObject.CompareTag("Player"))
            OnCollect(other);
    }
    
}
