using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundTextureController : MonoBehaviour
{
    #region Editor Fields
    [SerializeField]
    private string horizontalInput;
    [SerializeField]
    private float horizontalSpeed;
    [SerializeField]
    private float verticalSpeed;

    #endregion
    #region Private Fields
    public Material _mat;
    public Vector2 newOffset;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        _mat = GetComponent<Renderer>().materials[0];
        
    }

    // Update is called once per frame
    void Update()
    {
        newOffset.y += Time.deltaTime * verticalSpeed;
        newOffset.x += Input.GetAxis(horizontalInput) * Time.deltaTime * horizontalSpeed;
        _mat.mainTextureOffset = newOffset;
    }
}
