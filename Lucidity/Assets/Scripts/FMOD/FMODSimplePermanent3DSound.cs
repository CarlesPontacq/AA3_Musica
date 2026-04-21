using UnityEngine;

public class FMODSimplePermanent3DSound : MonoBehaviour
{
    [FMODUnity.EventRef]
    public string eventPath = "";
    public FMOD.Studio.STOP_MODE stopMode;
    public bool isStatic = true;

    FMOD.Studio.EventInstance SoundEvent;

    void Start()
    {
        SoundEvent = FMODUnity.RuntimeManager.CreateInstance(eventPath);

        if (isStatic)
            SoundEvent.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(transform.position));
        else
            FMODUnity.RuntimeManager.AttachInstanceToGameObject(SoundEvent, transform, GetComponent<Rigidbody>());

        SoundEvent.start();
        SoundEvent.release();
    }

    void OnDestroy()
    {
        SoundEvent.stop(stopMode);
    }
}
