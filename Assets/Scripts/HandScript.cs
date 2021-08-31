using System.Collections;
using UnityEngine;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Unity.Mathematics;
using UnityEngine.InputSystem.XR.Haptics;
using UnityEngine.XR;
using Vector3 = UnityEngine.Vector3;
using static UnityEngine.XR.HapticCapabilities;



[SuppressMessage("ReSharper", "Unity.PerformanceCriticalCodeInvocation")]
public class HandScript : MonoBehaviour
{
    public static int MOVEMENT_ARRAY_SIZE = 10;

    [SerializeField] private GameObject rightHandObject;

    [SerializeField] private GameObject projectile;
    [SerializeField] private Camera head;
    private GameObject rock;
    public bool standbyProjectile;
    private Vector3[] up = new Vector3[MOVEMENT_ARRAY_SIZE];
    Queue<Vector3> rightHand_positions = new Queue<Vector3>();

    
    public InputMaster myControls;

    void Awake()
    {
        //initalize up vector
        for (int i = 0; i < MOVEMENT_ARRAY_SIZE; i++)
        {
            up[i] = new Vector3(0, i * 0.1f + 0.1f, 0);
        }

        myControls = new InputMaster();
        myControls.player.record.performed += _ => record();
    }

    void record()
    {
        
        if (standbyProjectile)
        {
            rock.GetComponent<Projectile>().handPosition = transform.position;
        }
        else
        {
            StartCoroutine(recordCoroutine());
        }
        
        
    }

    public InputMaster getControls()
    {
        while (myControls == null)
        {
            var temp = new WaitForSeconds(0.1f);
        }
        return myControls;
    }

    void startHaptics()
    {
        List<UnityEngine.XR.InputDevice> devices = new List<UnityEngine.XR.InputDevice>();
        UnityEngine.XR.InputDevices.GetDevicesWithRole(UnityEngine.XR.InputDeviceRole.RightHanded, devices);
        foreach (var device in devices)
        {
            UnityEngine.XR.HapticCapabilities capabilities;
            if (device.TryGetHapticCapabilities(out capabilities))
            {
                if (capabilities.supportsImpulse)
                {
                    uint channel = 0;
                    float amplitude = 0.5f;
                    float duration = 15.0f;
                    device.SendHapticImpulse(channel, amplitude, duration);
                }
            }
        }
    }
    
    void stopHaptics()
    {
        List<UnityEngine.XR.InputDevice> devices = new List<UnityEngine.XR.InputDevice>();
        UnityEngine.XR.InputDevices.GetDevicesWithRole(UnityEngine.XR.InputDeviceRole.RightHanded, devices);
        foreach (var device in devices)
        {
            UnityEngine.XR.HapticCapabilities capabilities;
            if (device.TryGetHapticCapabilities(out capabilities))
            {
                if (capabilities.supportsImpulse)
                {
                    uint channel = 0;
                    float amplitude = 0f;
                    float duration = 0.1f;
                    device.SendHapticImpulse(channel, amplitude, duration);
                }
            }
        }
    }
    
    private IEnumerator recordCoroutine()
    {
        startHaptics();
        rock = Instantiate(projectile,
            //   transform.position - new Vector3(0,0.1f,0) + 0.3f * head.transform.forward,
            rightHandObject.transform.position +  0.2f * rightHandObject.transform.forward,
                quaternion.identity, rightHandObject.transform);
        
        Queue<Vector3> movement = new Queue<Vector3>();
        Vector3 prime = new Vector3(0, 0, 0);
        while (myControls.player.record.ReadValue<float>() > 0)
        {
            if (movement.Count == 0)
            {
                prime = rightHandObject.transform.position;
            }
            Vector3 cur = rightHandObject.transform.position - prime;

            if (movement.Count == MOVEMENT_ARRAY_SIZE)
            {
                movement.Dequeue();
            }

            movement.Enqueue(cur);
            yield return null;
        }

        standbyProjectile = checkMove(movement);
        projectile.GetComponent<Projectile>().play_idle();
        if (standbyProjectile)
        {
            Camera main_cam = GameObject.Find("Main Camera").GetComponent<Camera>();
            rock.transform.parent = main_cam.transform;
            rock.transform.position = main_cam.transform.position + 0.75f * main_cam.transform.forward;
            rock.GetComponent<Projectile>().headPosInit = main_cam.transform.position;
            rock.GetComponent<Projectile>().state = Projectile.Move.GoodMove;
        }
        else
        {
            rock.GetComponent<Projectile>().state = Projectile.Move.BadMove;
            Destroy(rock);
            movement.Clear();
            
        }
        
    }

    private bool checkMove(Queue<Vector3> movement)
    {
        stopHaptics();        
        if (movement.Count != MOVEMENT_ARRAY_SIZE)
        {
            return false;
        }

        if (movement.Peek().magnitude < 1)
        {
            return false;
        }


        if (Vector3.Dot(up[9], movement.Peek().normalized) < 0.8)
        {

            return false;
        }

        
        return true;
    }

    void OnEnable()
    {
        myControls.Enable();
    }
    void OnDisable()
     {
         myControls.Disable();
     }





     //private InputDevice rightHand = InputDevices.GetDeviceAtXRNode(XRNode.RightHand);

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void FixedUpdate()
    {
            rightHand_positions.Enqueue(rightHandObject.transform.position);
        
    }   
    
}
