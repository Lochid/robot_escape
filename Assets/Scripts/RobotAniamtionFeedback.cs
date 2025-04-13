using UnityEngine;

public class RobotAniamtionFeedback : MonoBehaviour
{
    public AudioSource stepSound;
    public void Step()
    {
        stepSound.Play();
    }

    public void Step2()
    {
        stepSound.Play();
    }
}
