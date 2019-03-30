using UnityEngine;
using System.Collections;

public class TextureColor : MonoBehaviour
{

	public Camera cam;
	void Start()
	{
		cam = Camera.main;
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
		Debug.Log("XYY:::"+pixelUV);
		pixelUV.x *= tex.width;
		pixelUV.y *= tex.height;

		Debug.Log("X:::"+pixelUV.x +" Y::"+pixelUV.y);
		Color32 c;

		c = tex.GetPixel ((int)pixelUV.x, (int)pixelUV.y);

		Debug.Log("color::"+ c);
		Debug.Log( c.ToString());
	}



}