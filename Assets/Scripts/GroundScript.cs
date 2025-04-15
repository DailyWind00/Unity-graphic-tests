using UnityEngine;

[ExecuteAlways] // Displace also in the Editor
public class GroundScript : MonoBehaviour
{	
	// Public variables :
	public Material	groundShader;

	[Header("--- Ground Settings ---")]

	[Range(0, 10)]
	public float heightDisplacement = 0;

	// Private variables :
	Mesh	mesh;
	float	lastHeightDisplacement;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        mesh = GetComponent<MeshFilter>().mesh;
		lastHeightDisplacement = heightDisplacement;
		ChangeGround();
    }

    // Update is called once per frame
    void Update()
    {
		// Update the ground height displacement if it has changed
		if (lastHeightDisplacement != heightDisplacement) {
			lastHeightDisplacement =  heightDisplacement;
			ChangeGround();
		}
    }

	// Change the ground height displacement of the mesh and the bounds of the mesh
	private void ChangeGround()
	{
		// Update shader displace strength // Using internal property name syntax
		groundShader.SetFloat("_DisplaceStrength", heightDisplacement);

		// Update bounds
		Bounds bounds = mesh.bounds;
		bounds.Expand(new Vector3(0, heightDisplacement, 0));
		mesh.bounds = bounds;
	}
}
