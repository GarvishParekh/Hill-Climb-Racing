using UnityEngine;
using UnityEngine.SceneManagement;

public enum InputType
{
    KEYBOARD,
    TOUCH
}
public class InputManager : MonoBehaviour
{
    [SerializeField] private InputType inputType;
    [SerializeField] private GameObject inputCanvas;
    [SerializeField] private float SterringSensi= 0;

    float gasDirection = 0;
    float sterringDirection = 0;
    float lerpedSterringDirection = 0;


    private void Awake()
    {
        switch (inputType)
        {
            case InputType.TOUCH:
                inputCanvas.SetActive(true);
                break;
            case InputType.KEYBOARD:
                inputCanvas.SetActive(false);
                break;
        }
    }

    public void ApplyBreaks()
        => gasDirection = -1;

    public void ApplyGas()
        => gasDirection = 1;

    public void ApplyNothing()
        => gasDirection = 0;

    public void LeftSterring()
        => sterringDirection = -1;

    public void RightSterring()
        => sterringDirection = 1;

    public void ResetSterring()
        => sterringDirection = 0;


    public float GetGasDirection()
    {
        return gasDirection;
    }

    public float GetSterringDirection()
    {
        return sterringDirection;
    }

    public float LerpedSterring()
    {
        lerpedSterringDirection = Mathf.MoveTowards(lerpedSterringDirection, sterringDirection, SterringSensi * Time.deltaTime);
        return lerpedSterringDirection;
    }

    public void RestartScene()
    {
        SceneManager.LoadScene(0);
    }
}
