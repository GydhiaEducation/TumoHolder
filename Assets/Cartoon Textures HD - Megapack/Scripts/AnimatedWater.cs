using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatedWater : MonoBehaviour
{
	
	public Vector2 animRateAlbedo ;
	public Vector2 animRateDetail ;

	private Vector2 uvOffsetAlbedo ;
	private Vector2 uvOffsetDetail ;
    private Material selfMaterial ;

	void Start()
	{
        selfMaterial = GetComponent<Renderer>().material ;
		uvOffsetAlbedo = selfMaterial.GetTextureOffset("_MainTex") ;
		uvOffsetDetail = selfMaterial.GetTextureOffset("_DetailAlbedoMap") ;
	}
	void Update()
	{
		uvOffsetAlbedo += (animRateAlbedo * Time.deltaTime) / 100f ;
		uvOffsetDetail += (animRateDetail * Time.deltaTime) / 100f ;
		selfMaterial.SetTextureOffset("_MainTex", uvOffsetAlbedo) ;
        selfMaterial.SetTextureOffset("_DetailAlbedoMap", uvOffsetDetail) ;
	}

}
