using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerEyes : MonoBehaviour
{
    [SerializeField] Transform target;
    // Start is called before the first frame update

    TextMeshPro text;
    void Start()
    {
        text = FindObjectOfType<TextMeshPro>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 heading = target.position - transform.position;
        var dirNum = AngleDir(transform.forward, heading, transform.up);
        //this.transform.position += transform.forward * Time.deltaTime;
        if (dirNum == 1) text.text = "Direita";
        if (dirNum == -1) text.text = "Esquerda";
    }

    private float AngleDir(Vector3 fwd, Vector3 targetDir, Vector3 up)
    {
        Vector3 perp = Vector3.Cross(fwd, targetDir);
        float dir = Vector3.Dot(perp, up);

        if (dir > 0.0f)
        {
            return 1.0f;
        }
        else if (dir < 0.0f)
        {
            return -1.0f;
        }
        else
        {
            return 0.0f;
        }
    }
}
