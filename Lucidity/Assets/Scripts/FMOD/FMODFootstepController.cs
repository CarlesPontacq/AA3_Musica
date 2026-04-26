using FMODUnity;
using System.IO;
using UnityEngine;
using UnityEngine.Audio;

public class FMODFootstepController : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioResource walkingTileSound;
    [SerializeField] private AudioResource walkingWoodSound;


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

    void PlayFootstepSound()
    {
        Debug.Log("PlayingSound");
        MaterialCheck();
        RunCheck();
        audioSource.resource = MaterialValue == 1 ? walkingTileSound : walkingWoodSound; 
        audioSource.Play();
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