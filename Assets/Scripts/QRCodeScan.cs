using UnityEngine;
using ZXing;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class QRCodeScan : MonoBehaviour
{
    public static QRCodeScan Instance;
    [SerializeField]
    private RawImage _rawImageBackground;
    [SerializeField]
    private AspectRatioFitter _aspectRatioFitter;
    [SerializeField]
    private TextMeshProUGUI _textOut;
    [SerializeField]
    private RectTransform _scanZone;

    private bool _isCamAvaible;
    private WebCamTexture _cameraTexture;
    void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        SetUpCamera();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateCameraRender();
        CheckQR();
    }

    void CheckQR(){
        if(_textOut.text == "123"){
            Home.Instance.NoticeTxt.text = "123";
            Home.Instance.Canvas.gameObject.SetActive(true);
            SceneManager.LoadScene("Home");
        }else if(_textOut.text == "1234"){
            Home.Instance.Canvas.gameObject.SetActive(true);
            SceneManager.LoadScene("Home");
        }
    }

    private void SetUpCamera()
    {
        WebCamDevice[] devices = WebCamTexture.devices;
        if (devices.Length == 0)
        {
            _isCamAvaible = false;
            return;
        }
        for (int i = 0; i < devices.Length; i++)
        {
            if (devices[i].isFrontFacing == false)
            {
                _cameraTexture = new WebCamTexture(devices[i].name, (int)_scanZone.rect.width, (int)_scanZone.rect.height);
                break;
            }
        }

        if (_cameraTexture == null){
            return;
        }

        _cameraTexture.Play();
        _rawImageBackground.texture = _cameraTexture;
        _isCamAvaible = true;
    }

    private void UpdateCameraRender()
    {
        if (_isCamAvaible == false)
        {
            return;
        }
        float ratio = (float)_cameraTexture.width / (float)_cameraTexture.height;
        _aspectRatioFitter.aspectRatio = ratio;

        int orientation = _cameraTexture.videoRotationAngle;
        orientation = orientation * 3;
        _rawImageBackground.rectTransform.localEulerAngles = new Vector3(0, 0, orientation);

        //  float scaleY = _cameraTexture.videoVerticallyMirrored ? -1f: 1f;
        // _rawImageBackground.rectTransform.localScale = new Vector3(1f, scaleY, 1f);

        // float orient = -_cameraTexture.videoRotationAngle;
        // _rawImageBackground.rectTransform.localEulerAngles = new Vector3(0, 0, orient);

    }
    public void OnClickScan()
    {
        Scan();
    }
    private void Scan()
    {
        try
        {
            IBarcodeReader barcodeReader = new BarcodeReader();
            Result result = barcodeReader.Decode(_cameraTexture.GetPixels32(), _cameraTexture.width, _cameraTexture.height);
            if (result != null)
            {
                _textOut.text = result.Text;
            }
            else {
                _textOut.text = "Failed to Read QR CODE";
            }
        }
        catch
        {
            _textOut.text = "FAILED IN TRY";
        }
    }
}
