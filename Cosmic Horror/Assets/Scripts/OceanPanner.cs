using UnityEngine;
using System.Collections;

public class OceanPanner : MonoBehaviour 
{
	public int materialIndex = 0;
	public Vector2 uvAnimationRate = new Vector2( 1.0f, 0.0f );
	public string[] textureName;
	public Renderer render;

	Vector2 uvOffset = Vector2.zero;




	void LateUpdate() 
	{
		uvOffset += ( uvAnimationRate * Time.deltaTime );
		if( render.enabled )
		{
			foreach (string tex in textureName) {

				render.materials [materialIndex].SetTextureOffset (tex, uvOffset);
			}
		}
	}
}