using UnityEngine;
using System.Collections.Generic;
using System;

public class TextureColorWithText : MonoBehaviour
{   
    public Dictionary<System.Drawing.Color, string> dictionary = new Dictionary<System.Drawing.Color, string>();
    
    public Camera cam;

    int screenWidth = Screen.width;
    int screenHeight = Screen.height;

    private bool showText = false, someRandomCondition = true;
    private float currentTime = 0.0f, executeTime = 0.0f, timeToWait = 5.0f;
    private int counter = 0, timeout = 0, timelevel = 0, timelimit = 80;

    int tolerance = 10;
    // bool locationFound = false;

    string toDisplay = "Let's Begin";

    public bool IsInValidRange(Color32 guess, System.Drawing.Color check, int threshold)
    {
        if( check.R-threshold <= guess.r && check.R+threshold >= guess.r && 
            check.G-threshold <= guess.g && check.G+threshold >= guess.g &&
            check.B-threshold <= guess.b && check.B+threshold >= guess.b ) 
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
            // (Screen.width / 2) - (toDisplay.Length / 2)
            GUI.Label(new Rect(40, 40, 2000, 500), toDisplay);

            timeout++;
            if (timeout == timelimit)
            {
                timeout = 0;
                if (timelevel == 1)
                {
                    showText = false;
                    timelevel = 0;
                    counter++;

                    //if(locationFound) {
                        // Goto next question
                    //}
                    
                }
                else timelevel++;
            }
        }
    }

    void Start()
    {
        cam = Camera.main;
        
        dictionary.Add(System.Drawing.Color.FromArgb(252,227,88), "Germany");
        dictionary.Add(System.Drawing.Color.FromArgb(185, 255, 55), "Argentina");
        dictionary.Add(System.Drawing.Color.FromArgb(0, 198, 188), "Brazil");
        dictionary.Add(System.Drawing.Color.FromArgb(181, 91, 181), "Spain");
        dictionary.Add(System.Drawing.Color.FromArgb(255, 138, 60), "Japan");
        dictionary.Add(System.Drawing.Color.FromArgb(255, 243, 15), "South Korea");
        dictionary.Add(System.Drawing.Color.FromArgb(34, 177, 76), "Italy");
        dictionary.Add(System.Drawing.Color.FromArgb(91, 64, 247), "Afganistan");
        dictionary.Add(System.Drawing.Color.FromArgb(64, 171, 242), "Egypt");
        dictionary.Add(System.Drawing.Color.FromArgb(247, 88, 49), "Turkey");
        dictionary.Add(System.Drawing.Color.FromArgb(196, 243, 95), "Mongolia");
        dictionary.Add(System.Drawing.Color.FromArgb(254, 90, 119), "India");
        dictionary.Add(System.Drawing.Color.FromArgb(203, 182, 141), "Congo");
        dictionary.Add(System.Drawing.Color.FromArgb(136, 189, 157), "Papua New Guinea");
        dictionary.Add(System.Drawing.Color.FromArgb(255, 9, 9), "Taiwan");
        dictionary.Add(System.Drawing.Color.FromArgb(166, 191, 106), "Netherlands");
        dictionary.Add(System.Drawing.Color.FromArgb(143, 35, 35), "Denmark");
        dictionary.Add(System.Drawing.Color.FromArgb(131, 165, 245), "Kazakhstan");
        dictionary.Add(System.Drawing.Color.FromArgb(255, 220, 113), "China");
        dictionary.Add(System.Drawing.Color.FromArgb(211, 184, 199), "United States");
        dictionary.Add(System.Drawing.Color.FromArgb(243, 148, 176), "Canada");
        dictionary.Add(System.Drawing.Color.FromArgb(204, 141, 123), "Saudi Arabia");
        dictionary.Add(System.Drawing.Color.FromArgb(130, 154, 129), "Mexico");
        dictionary.Add(System.Drawing.Color.FromArgb(188, 241, 139), "Niger");
        dictionary.Add(System.Drawing.Color.FromArgb(222, 250, 197), "Nigeria");
        dictionary.Add(System.Drawing.Color.FromArgb(77, 234, 199), "France");
        dictionary.Add(System.Drawing.Color.FromArgb(85, 60, 200), "Ukraine");
        dictionary.Add(System.Drawing.Color.FromArgb(159, 130, 247), "Oman");
        dictionary.Add(System.Drawing.Color.FromArgb(150, 20, 230), "Zimbabwe");
        dictionary.Add(System.Drawing.Color.FromArgb(10, 150, 20), "New Zealand");
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

        // Debug.Log("Color::"+ c);

        // if (dictionary.ContainsKey(System.Drawing.Color.FromArgb(c.r, c.g, c.b)))
        // {
        //     Debug.Log(dictionary[System.Drawing.Color.FromArgb(c.r, c.g, c.b)]);
        // }
        // else
        // {
        //     Debug.Log("Better luck next time");
        // }

        if(Input.GetMouseButtonDown(0))
        {
            // showText = true;

            foreach (KeyValuePair<System.Drawing.Color, string> entry in dictionary)
            {
                // do something with entry.Value or entry.Key
                System.Drawing.Color checkColor = entry.Key;
    
                if (IsInValidRange(c, checkColor, tolerance)) 
                {
                    Debug.Log("Country is " + entry.Value);
                    toDisplay = String.Concat("You found ", entry.Value);
                    showText = true;
                    // locationFound = true;
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