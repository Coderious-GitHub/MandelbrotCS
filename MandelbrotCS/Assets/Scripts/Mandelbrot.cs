using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Mandelbrot : MonoBehaviour
{
	//Mandelbrot set parameters
    double width = 4.5;
    double height = 4.5;
    double rStart = -2.0;
    double iStart = -1.25;
    int maxIteration = 500;
    int zoom = 10;
    Texture2D display;
    public Image image;

    public TextMeshProUGUI real, imag, ite;

    // Start is called before the first frame update
    void Start()
    {
        height = width * (9.0 / 16.0);
        display = new Texture2D(Screen.width, Screen.height);

        RunMandelbrot();
    }

    void RunMandelbrot()
    {
        for (int x = 0; x < display.width; x++)
        {
            for (int y = 0; y < display.height; y++)
            {
                //float result = (float)FindMandelbrot(rStart + width * ((double)x / display.width), iStart + height * ((double)y / display.height)) / maxIteration;
                display.SetPixel(x, y, ColorSet(FindMandelbrot(rStart + width * ((double)x / display.width), iStart + height * ((double)y / display.height))));
            }
        }

        display.Apply();
        image.sprite = Sprite.Create(display, new Rect(0, 0, display.width, display.height), new Vector2(0.5f, 0.5f));

        real.text = rStart.ToString();
        imag.text = iStart.ToString();
        ite.text = maxIteration.ToString();
    }

    int FindMandelbrot(double cReal, double cImag)
    {
        double real = cReal;
        double imag = cImag;
        int mValue = 0;

        for (int i = 0; i < maxIteration; i++)
        {
            double real2 = real * real;
            double imag2 = imag * imag;

            if (real2 + imag2 > 4.0)
            {
                break;
            }
            else
            {
                imag = 2.0 * real * imag + cImag;
                real = real2 - imag2 + cReal;
                mValue++;
            }
        }

        return mValue;

    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            rStart = rStart + (Input.mousePosition.x - (Screen.width / 2.0)) / Screen.width * width;
            iStart = iStart + (Input.mousePosition.y - (Screen.height / 2.0)) / Screen.height * height;

            RunMandelbrot();
        }

        if (Input.mouseScrollDelta.y != 0)
        {
            double wFactor = width * (double)Input.mouseScrollDelta.y / zoom;
            double hFactor = height * (double)Input.mouseScrollDelta.y / zoom;
            width -= wFactor;
            height -= hFactor;
            rStart += wFactor / 2.0;
            iStart += hFactor / 2.0;

            RunMandelbrot();
        }
    }

    Color ColorSet(int value)
    {
		Vector4 CalcColor = new Vector4(0, 0, 0, 1f);

		if (value != maxIteration)
		{
			int colorNr = value % 16;

			switch (colorNr)
			{
				case 0:
					{
						CalcColor[0] = 66.0f / 255.0f;
						CalcColor[1] = 30.0f / 255.0f;
						CalcColor[2] = 15.0f / 255.0f;

						break;
					}
				case 1:
					{
						CalcColor[0] = 25.0f / 255.0f;
						CalcColor[1] = 7.0f / 255.0f;
						CalcColor[2] = 26.0f / 255.0f;
						break;
					}
				case 2:
					{
						CalcColor[0] = 9.0f / 255.0f;
						CalcColor[1] = 1.0f / 255.0f;
						CalcColor[2] = 47.0f / 255.0f;
						break;
					}

				case 3:
					{
						CalcColor[0] = 4.0f / 255.0f;
						CalcColor[1] = 4.0f / 255.0f;
						CalcColor[2] = 73.0f / 255.0f;
						break;
					}
				case 4:
					{
						CalcColor[0] = 0.0f / 255.0f;
						CalcColor[1] = 7.0f / 255.0f;
						CalcColor[2] = 100.0f / 255.0f;
						break;
					}
				case 5:
					{
						CalcColor[0] = 12.0f / 255.0f;
						CalcColor[1] = 44.0f / 255.0f;
						CalcColor[2] = 138.0f / 255.0f;
						break;
					}
				case 6:
					{
						CalcColor[0] = 24.0f / 255.0f;
						CalcColor[1] = 82.0f / 255.0f;
						CalcColor[2] = 177.0f / 255.0f;
						break;
					}
				case 7:
					{
						CalcColor[0] = 57.0f / 255.0f;
						CalcColor[1] = 125.0f / 255.0f;
						CalcColor[2] = 209.0f / 255.0f;
						break;
					}
				case 8:
					{
						CalcColor[0] = 134.0f / 255.0f;
						CalcColor[1] = 181.0f / 255.0f;
						CalcColor[2] = 229.0f / 255.0f;
						break;
					}
				case 9:
					{
						CalcColor[0] = 211.0f / 255.0f;
						CalcColor[1] = 236.0f / 255.0f;
						CalcColor[2] = 248.0f / 255.0f;
						break;
					}
				case 10:
					{
						CalcColor[0] = 241.0f / 255.0f;
						CalcColor[1] = 233.0f / 255.0f;
						CalcColor[2] = 191.0f / 255.0f;
						break;
					}
				case 11:
					{
						CalcColor[0] = 248.0f / 255.0f;
						CalcColor[1] = 201.0f / 255.0f;
						CalcColor[2] = 95.0f / 255.0f;
						break;
					}
				case 12:
					{
						CalcColor[0] = 255.0f / 255.0f;
						CalcColor[1] = 170.0f / 255.0f;
						CalcColor[2] = 0.0f / 255.0f;
						break;
					}
				case 13:
					{
						CalcColor[0] = 204.0f / 255.0f;
						CalcColor[1] = 128.0f / 255.0f;
						CalcColor[2] = 0.0f / 255.0f;
						break;
					}
				case 14:
					{
						CalcColor[0] = 153.0f / 255.0f;
						CalcColor[1] = 87.0f / 255.0f;
						CalcColor[2] = 0.0f / 255.0f;
						break;
					}
				case 15:
					{
						CalcColor[0] = 106.0f / 255.0f;
						CalcColor[1] = 52.0f / 255.0f;
						CalcColor[2] = 3.0f / 255.0f;
						break;
					}
			}
		}
		return CalcColor;
	}
}
