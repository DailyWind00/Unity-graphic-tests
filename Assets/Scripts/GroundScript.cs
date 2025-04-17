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
	Mesh			mesh;
	ComputeShader	grassCS;
	float			lastHeightDisplacement;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        mesh = GetComponent<MeshFilter>().sharedMesh; // Use sharedMesh to avoid creating a new mesh instance
		grassCS = GetComponent<ComputeShader>();
		
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

		// Grass shader update
		if (!Application.isPlaying) {
			// Todo : Update grass shader here
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
