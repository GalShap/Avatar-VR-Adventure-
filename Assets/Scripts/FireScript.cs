using System.Collections;
using System.Collections.Generic;
using System.Net.Http;
using UnityEditor;
using UnityEngine;
using Unity.Mathematics;

using Vector3 = UnityEngine.Vector3;
public class FireScript : MonoBehaviour
{
    [SerializeField] private GameObject rightHandObject;
    [SerializeField] private GameObject leftHandObject;
    [SerializeField] private GameObject textObeject;
    [SerializeField] private GameObject textObeject2;
    [SerializeField] private GameObject textObeject3;
    [SerializeField] private GameObject textObeject4;


    public InputMaster myControls;
    private bool rightPreesed = false;
    private bool leftPreesed = false;
    [SerializeField] private GameObject firePrefab;
    private GameObject fire;
    [SerializeField] private GameObject head;
    private TextMesh text1;
    private TextMesh text2;
    private TextMesh text3;
    private TextMesh text4;

    private bool standbyFireFlag;

    private ProjectileFire projectileScript;
    private Rigidbody projectileRigitbpdy;


    // Start is called before the first frame update
    void Start()
    {
        text1 = textObeject.GetComponent<TextMesh>();
        text2 = textObeject2.GetComponent<TextMesh>();
        text3 = textObeject3.GetComponent<TextMesh>();
        text4 = textObeject4.GetComponent<TextMesh>();
        myControls = rightHandObject.GetComponent<HandScript>().getControls();


        StartCoroutine(SetUp());
      
    }

    IEnumerator SetUp()
    {
        myControls.player.fireLeftPressed.performed += _ => FireLeft();
        myControls.player.fireRightPressed.performed += _ => FireRight();
        standbyFireFlag = false;
        yield return null;
    }
    private void FireRight()
    {
        StartCoroutine(Fire());
    }
    private void FireLeft()
    {
        StartCoroutine(Fire());
    }

    bool CheckValidMovement(Vector3 distance)
    {
        if (distance.magnitude < 0.3f)
            return true;
        
        if (math.abs(Vector3.Dot(Vector3.up.normalized, distance.normalized)) > 0.35 || math.abs(Vector3.Dot(head.transform.forward.normalized, distance.normalized)) > 0.35)
        {
            text2.text = "up dot product " + Vector3.Dot(Vector3.up.normalized, distance.normalized);
            text3.text = "head.transform.forward product " +
                         Vector3.Dot(head.transform.forward.normalized, distance.normalized);
            projectileScript.playCrooked();
            return false;
        }
        return true;
    }

    void UpdateFireState(Vector3 movement)
    {
        if(movement.magnitude > 1.3f && projectileScript.state == ProjectileFire.Move.InMove)
        {
            projectileScript.state = ProjectileFire.Move.GoodMove;
            projectileScript.playGong();
        }
    }

    void UpdateFire(Vector3 distance, Vector3 positionRight, Vector3 positionLeft)
    {
        if (projectileScript.state != ProjectileFire.Move.InMove) return;
        
        fire.transform.localScale = (new Vector3(distance.magnitude + 1, distance.magnitude + 1, distance.magnitude+1) * 0.3f);
        projectileRigitbpdy.position = (positionRight + positionLeft)/2 + head.transform.forward * 0.35f * (1 + distance.magnitude);


    }

    bool CheckShoot(Vector3 distance)
    {
        if (projectileScript.state == ProjectileFire.Move.GoodMove && distance.magnitude < 0.1f)
        {
            
            projectileScript.shoot();
            return true;
        }

        return false;
    }
    // ReSharper disable Unity.PerformanceAnalysis
    private IEnumerator Fire()
    {
        
        Vector3 distance = rightHandObject.transform.position - leftHandObject.transform.position;
 
        if (myControls.player.fireLeftPressed.ReadValue<float>() > 0.6f &&
            myControls.player.fireRightPressed.ReadValue<float>() > 0.6f && 
            distance.magnitude < 0.4)
            {
                

                try
                {
                    fire = Instantiate(firePrefab,
                        (rightHandObject.transform.position + leftHandObject.transform.position) / 2 +
                        0.3f * head.transform.forward,
                        quaternion.identity);
                }
                catch
                {
                    text3.text = "ERR! cant instantiate";
                    yield break;
                }
                text4.text = "inside if, after Instantiate";

                projectileScript = fire.GetComponent<ProjectileFire>();
                projectileRigitbpdy = fire.GetComponent<Rigidbody>();
                projectileScript.setPersonPosition(head.transform.position - new Vector3(0, 0.3f, 0));
                startHaptics();
                
                text3.text = "while condition is  " + (myControls.player.fireLeftPressed.ReadValue<float>() > 0.6f &&
                             myControls.player.fireRightPressed.ReadValue<float>() > 0.6f && !CheckShoot(distance)
                             && CheckValidMovement(distance));


                while (myControls.player.fireLeftPressed.ReadValue<float>() > 0.6f &&
                       myControls.player.fireRightPressed.ReadValue<float>() > 0.6f &&
                       !CheckShoot(distance)
                       && CheckValidMovement(distance))
                    {

                        var positionRight = rightHandObject.transform.position;
                        var positionLeft = leftHandObject.transform.position;
                        distance = positionRight - positionLeft;
                        
                        UpdateFireState(distance);
                        text4.text = "after UpdateFireState";

                        UpdateFire(distance, positionRight, positionLeft);
                        text4.text = "after UpdateFire";

                        yield return null;
                    }
                stopHaptics();
                text2.text = "release press!";

                projectileScript.releasePress();

                
            }
        text4.text = "fire end";

    }
    void startHapticsRight()
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
    
    void stopHapticsRight()
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
    void startHapticsLeft()
    {
        List<UnityEngine.XR.InputDevice> devices = new List<UnityEngine.XR.InputDevice>();
        UnityEngine.XR.InputDevices.GetDevicesWithRole(UnityEngine.XR.InputDeviceRole.LeftHanded, devices);

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
    
    void stopHapticsLeft()
    {
        List<UnityEngine.XR.InputDevice> devices = new List<UnityEngine.XR.InputDevice>();
        UnityEngine.XR.InputDevices.GetDevicesWithRole(UnityEngine.XR.InputDeviceRole.LeftHanded, devices);

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

    void startHaptics()
    {
        startHapticsRight();
        startHapticsLeft();
    }

    void stopHaptics()
    {
        stopHapticsRight();
        stopHapticsLeft();
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
