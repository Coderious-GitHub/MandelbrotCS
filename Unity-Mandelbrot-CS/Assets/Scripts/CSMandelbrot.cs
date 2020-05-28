using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CSMandelbrot : MonoBehaviour
{
    //Shader resources
    public ComputeShader shader;
    ComputeBuffer buffer;
    RenderTexture renderTexture;
    public RawImage rawImage;

    //GUI Resources
    public TextMeshProUGUI real, imag, w, h, ite, frame;
    public int increment = 3;
    public float zoomSpeed = 0.5f;


    //Mandelbrot parameters
    double width = 5;
    double height = 2.5;
    double rStart = -3.25;
    double iStart = -1.4;
    int maxIteration = 1024;



    public struct dataStruct
    {
        public double w, h, r, i;
        public int screenWidth, screenHeight;
    }

    dataStruct[] data = new dataStruct[1];


    // Start is called before the first frame update
    void Start()
    {
        height = width * Screen.height / Screen.width;

        data[0] = new dataStruct
        {
            w = width,
            h = height,
            r = rStart,
            i = iStart,
            screenWidth = Screen.width,
            screenHeight = Screen.height
        };

        buffer = new ComputeBuffer(1, 40);

        renderTexture = new RenderTexture(Screen.width, Screen.height, 0);
        renderTexture.enableRandomWrite = true;
        renderTexture.Create();

        UpdateTexture();
    }

    private void Update()
    {
        if(Input.GetMouseButton(0))
        {
            StartCoroutine(ZoomingIn());
        }

        if (Input.GetMouseButton(1))
        {
            StartCoroutine(ZoomingOut());
        }

        if (Input.GetMouseButtonDown(2))
        {
            CenterScreen();
        }
    }

    IEnumerator ZoomingIn()
    {
        yield return StartCoroutine(ZoomIn());
    }

    IEnumerator ZoomingOut()
    {
        yield return StartCoroutine(ZoomOut());
    }


    IEnumerator ZoomIn()
    {
        maxIteration = Mathf.Max(maxIteration, maxIteration + increment);

        double wFactor = width * zoomSpeed * Time.deltaTime;
        double hFactor = height * zoomSpeed * Time.deltaTime;
        width -= wFactor;
        height -= hFactor;
        rStart += wFactor / 2.0f;
        iStart += hFactor / 2.0f;

        data[0].w = width;
        data[0].h = height;
        data[0].r = rStart;
        data[0].i = iStart;

        UpdateTexture();

        yield return null;
    }

    IEnumerator ZoomOut()
    {
        maxIteration = Mathf.Max(maxIteration, maxIteration - increment);

        double wFactor = width * zoomSpeed * Time.deltaTime;
        double hFactor = height * zoomSpeed * Time.deltaTime;
        width += wFactor;
        height += hFactor;
        rStart -= wFactor / 2.0f;
        iStart -= hFactor / 2.0f;

        data[0].w = width;
        data[0].h = height;
        data[0].r = rStart;
        data[0].i = iStart;

        UpdateTexture();

        yield return null;
    }

    void CenterScreen()
    {
        rStart += (Input.mousePosition.x - (Screen.width / 2.0f)) / Screen.width * width;
        iStart += (Input.mousePosition.y - (Screen.height / 2.0f)) / Screen.height * height;

        data[0].r = rStart;
        data[0].i = iStart;

        UpdateTexture();
    }


    void UpdateTexture()
    {
        int kernelHandle = shader.FindKernel("CSMain");

        buffer.SetData(data);
        shader.SetBuffer(kernelHandle, "buffer", buffer);

        shader.SetInt("iterations", maxIteration);
        shader.SetTexture(kernelHandle, "Result", renderTexture);

        shader.Dispatch(kernelHandle, Screen.width / 24, Screen.height / 24, 1);

        RenderTexture.active = renderTexture;
        rawImage.material.mainTexture = renderTexture;

        real.text = rStart.ToString();
        imag.text = iStart.ToString();
        w.text = width.ToString();
        h.text = height.ToString();
        ite.text = maxIteration.ToString();

        frame.text = Time.deltaTime.ToString();
    }


    void OnDestroy()
    {
        buffer.Dispose();
    }

}
