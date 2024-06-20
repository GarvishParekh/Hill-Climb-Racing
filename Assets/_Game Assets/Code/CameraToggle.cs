using UnityEngine;

public class CameraToggle : MonoBehaviour
{
    private enum CameraType
    {
        ORTHO,
        BACK
    }
    [SerializeField] private CameraType cameraType;
    [SerializeField] private GameObject orthoCamera;
    [SerializeField] private GameObject backCamera;


    public void CameraChange()
    {
        if (cameraType == CameraType.ORTHO)
            cameraType = CameraType.BACK;
        else if (cameraType == CameraType.BACK)
            cameraType = CameraType.ORTHO;

        SwitchCamera();
    }

    private void SwitchCamera()
    {
        switch (cameraType)
        {
            case CameraType.ORTHO:
                orthoCamera.SetActive(true);
                backCamera.SetActive(false);
                break;
            case CameraType.BACK:
                orthoCamera.SetActive(false);
                backCamera.SetActive(true);
                break;
        }
    }
}
