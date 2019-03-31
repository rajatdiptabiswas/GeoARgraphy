using UnityEngine;
using System.Collections.Generic;
using System;

public class TextureColorTextGame : MonoBehaviour
{
    public Dictionary<string, System.Drawing.Color> dictionary = new Dictionary<string, System.Drawing.Color>();

    public Camera cam;

    int screenWidth = Screen.width;
    int screenHeight = Screen.height;

    private bool showText = false, someRandomCondition = true;
    private float currentTime = 0.0f, executeTime = 0.0f, timeToWait = 5.0f;
    private int timeout = 0, timelevel = 0, timelimit = 80;

    int tolerance = 10; // threshold for the tolerance in RGB values

    public int countryIdx = 0;
    public string currentCountry = "";
    
    int limit = 0;
    string question = "";

    string toDisplay = "Let's Begin";

    string[] countryNames = {"Germany", "India", "Canada", "China", "Japan", "South Korea",
        "Italy", "Afganistan", "Egypt", "Turkey", "Mongolia", "Argentina", "Congo",
        "Papua New Guinea", "Taiwan", "Netherlands", "Denmark", "Kazakhstan", "Spain",
        "United States", "Brazil", "Saudi Arabia", "Mexico", "Niger", "Nigeria", "France",
        "Ukraine", "Oman", "Zimbabwe", "New Zealand"};

    // allows a tolerance for the RGB values
    public bool IsInValidRange(Color32 guess, System.Drawing.Color check, int threshold)
    {
        if (check.R - threshold <= guess.r && check.R + threshold >= guess.r &&
            check.G - threshold <= guess.g && check.G + threshold >= guess.g &&
            check.B - threshold <= guess.b && check.B + threshold >= guess.b)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    void OnGUI()
    {
        GUI.skin.label.fontSize = 70;

        if (showText)
        {
            GUI.Label(new Rect(40, 40, 2000, 500), toDisplay);

            timeout++;
            if (timeout == timelimit)
            {
                timeout = 0;
                if (timelevel == 1)
                {
                    showText = false;
                    timelevel = 0;
                    countryIdx++;

                    if (countryIdx == limit) return;

                    currentCountry = countryNames[countryIdx];
                    
                }
                else timelevel++;
            }
        }

        else
        {
            toDisplay = "Where is " + currentCountry + "?";

            GUI.Label(new Rect(40, 40, 2000, 500), toDisplay);
        }

    }
    void Start()
    {
        cam = Camera.main;

        dictionary.Add("Germany", System.Drawing.Color.FromArgb(252, 227, 88));
        dictionary.Add("Argentina", System.Drawing.Color.FromArgb(185, 255, 55));
        dictionary.Add("Brazil", System.Drawing.Color.FromArgb(0, 198, 188));
        dictionary.Add("Spain", System.Drawing.Color.FromArgb(181, 91, 181));
        dictionary.Add("Japan", System.Drawing.Color.FromArgb(255, 138, 60));
        dictionary.Add("South Korea", System.Drawing.Color.FromArgb(255, 243, 15));
        dictionary.Add("Italy", System.Drawing.Color.FromArgb(34, 177, 76));
        dictionary.Add("Afganistan", System.Drawing.Color.FromArgb(91, 64, 247));
        dictionary.Add("Egypt", System.Drawing.Color.FromArgb(64, 171, 242));
        dictionary.Add("Turkey", System.Drawing.Color.FromArgb(247, 88, 49));
        dictionary.Add("Mongolia", System.Drawing.Color.FromArgb(196, 243, 95));
        dictionary.Add("India", System.Drawing.Color.FromArgb(254, 90, 119));
        dictionary.Add("Congo", System.Drawing.Color.FromArgb(203, 182, 141));
        dictionary.Add("Papua New Guinea", System.Drawing.Color.FromArgb(136, 189, 157));
        dictionary.Add("Taiwan", System.Drawing.Color.FromArgb(255, 9, 9));
        dictionary.Add("Netherlands", System.Drawing.Color.FromArgb(166, 191, 106));
        dictionary.Add("Denmark", System.Drawing.Color.FromArgb(143, 35, 35));
        dictionary.Add("Kazakhstan", System.Drawing.Color.FromArgb(131, 165, 245));
        dictionary.Add("China", System.Drawing.Color.FromArgb(255, 220, 113));
        dictionary.Add("United States", System.Drawing.Color.FromArgb(211, 184, 199));
        dictionary.Add("Canada", System.Drawing.Color.FromArgb(243, 148, 176));
        dictionary.Add("Saudi Arabia", System.Drawing.Color.FromArgb(204, 141, 123));
        dictionary.Add("Mexico", System.Drawing.Color.FromArgb(130, 154, 129));
        dictionary.Add("Niger", System.Drawing.Color.FromArgb(188, 241, 139));
        dictionary.Add("Nigeria", System.Drawing.Color.FromArgb(222, 250, 197));
        dictionary.Add("France", System.Drawing.Color.FromArgb(77, 234, 199));
        dictionary.Add("Ukraine", System.Drawing.Color.FromArgb(85, 60, 200));
        dictionary.Add("Oman", System.Drawing.Color.FromArgb(159, 130, 247));
        dictionary.Add("Zimbabwe", System.Drawing.Color.FromArgb(150, 20, 230));
        dictionary.Add("New Zealand", System.Drawing.Color.FromArgb(10, 150, 20));

        currentCountry = countryNames[countryIdx];

        limit = dictionary.Count;
    }

    void Update()
    {
        if (!Input.GetMouseButton(0))
            return;

        RaycastHit hit;
        if (!Physics.Raycast(cam.ScreenPointToRay(Input.mousePosition), out hit))
            return;

        Renderer rend = hit.transform.GetComponent<Renderer>();
        MeshCollider meshCollider = hit.collider as MeshCollider;
        if (rend == null || rend.sharedMaterial == null || rend.sharedMaterial.mainTexture == null || meshCollider == null)
            return;

        Texture2D tex = rend.material.mainTexture as Texture2D;
        Vector2 pixelUV = hit.textureCoord;
        // Debug.Log("XY::"+pixelUV);
        pixelUV.x *= tex.width;
        pixelUV.y *= tex.height;

        // Debug.Log("X::"+pixelUV.x +" Y::"+pixelUV.y);
        Color32 c;

        c = tex.GetPixel((int)pixelUV.x, (int)pixelUV.y);
        

        if (Input.GetMouseButtonDown(0))
        {
            // showText = true;
            foreach (KeyValuePair<string, System.Drawing.Color> entry in dictionary) {
                
                // if not same country, give user chances to learn
                if (string.Compare(currentCountry, entry.Key) != 0) continue;

                System.Drawing.Color checkColor = entry.Value;

                if (IsInValidRange(c, checkColor, tolerance))
                {
                    Debug.Log("Country is " + entry.Key);
                    toDisplay = String.Concat("You found ", entry.Key);
                    showText = true;
                    break;
                }
                // else
                // {
                //     Debug.Log("Not Found");
                //     continue;
                // }
            }
        }

    }

}