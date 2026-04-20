using FMODUnity;
using System.IO;
using UnityEngine;

public class FMODFootstepController : MonoBehaviour
{
    [FMODUnity.EventRef]
    public string eventPath = "event:/Footsteps";

    FMOD.Studio.EventInstance Run;
    private float MaterialValue;
    private float RunValue;
    public float distance = 2f;
    public float volume = 2f;
    public LayerMask lm;

    [SerializeField] PlayerMovement playerMovement;

    private RaycastHit rh;

    public Transform feet;

    Vector3 pos;
    GameObject go;

    private void Start()
    {
    }

    private void Update()
    {

    }

    void PlayFootstepsEvent()
    {
        MaterialCheck();
        RunCheck();
        Run = FMODUnity.RuntimeManager.CreateInstance(eventPath);
        Run.setParameterByName("SurfaceType", MaterialValue, false);
        Run.setParameterByName("PlayerSpeed", RunValue, false);
        FMODUnity.RuntimeManager.AttachInstanceToGameObject(Run, feet, GetComponent<Rigidbody>());
        Run.start();
        Run.release();
    }

    void MaterialCheck()
    {
        if (Physics.Raycast(transform.position, Vector3.down, out rh, distance, lm))
        {
            switch (rh.collider.tag)
            {
                case "Tile":
                    MaterialValue = 1;
                    break;
                case "Wood":
                    MaterialValue = 0;
                    break;
                default:
                    MaterialValue = 0;
                    break;
            }
        }
        else
        {
            MaterialValue = 0;
        }

        Debug.Log("Parameter Material: " + MaterialValue);
    }

    void RunCheck()
    {
        RunValue = playerMovement.IsRunning ? 1 : 0;
        Debug.Log("Parameter Run: " + RunValue);

    }
}