using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tile : MonoBehaviour
{

    // [SerializeField] private GameObject tileObject;
    [SerializeField] private SpriteRenderer _renderer;
    [SerializeField] private Text tileNumber;

    public void Init(Color baseColor, int number) {
        _renderer.color = baseColor;
        // Debug.Log("isOffset :: " + isOffset.ToString());
        Debug.Log("color :: " + _renderer.color.ToString());
        tileNumber.text = number.ToString();
    }

    public void updateColor(Color color) {
        _renderer.color = color;
    }

    public void destroy() {
        // Destroy(tileObject);
        Destroy(this);
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
