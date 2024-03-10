using UnityEngine;

public class ShipController : MonoBehaviour
{
    [SerializeField]
    private float speed;
    private bool movingHP, movingHN, movingVP, movingVN, glidingHP, glidingHN, glidingVP, glidingVN;
    private float thrustH, thrustV, magH, magV;
    private Vector3 dir;

    void Start()
    {
        thrustH = 0f;
        thrustV = 0f;
        magH = 0f;
        magV = 0f;
        movingHP = false;
        movingHN = false;
        movingVP = false;
        movingVN = false;
        glidingHP = false;
        glidingHN = false;
        glidingVP = false;
        glidingVN = false;
    }

    // Update is called once per frame
    void Update()
    {
        float tempH = Input.GetAxisRaw("Horizontal");
        if (tempH > 0) { movingHP = true; movingHN = false; }
        else if (tempH < 0) { movingHP = false; movingHN = true; }
        else { movingHP = false; movingHN = false; }

        float tempV = Input.GetAxisRaw("Vertical");
        if (tempV > 0) { movingVP = true; movingVN = false; }
        else if (tempV < 0) { movingVP = false; movingVN = true; }
        else { movingVP = false; movingVN = false; }

        if (!movingHN && !movingHP)
        {
            if (thrustH > 0)
            {
                thrustH = Mathf.Max(0, thrustH - Time.deltaTime);
            }
            else
            {
                glidingHN = false;
                glidingHP = false;
            }
        }
        else if (movingHP)
        {
            if (glidingHN)
            {
                magH = -1;
                thrustH = Mathf.Max(0, thrustH - Time.deltaTime * 2);
                if (thrustH == 0) glidingHN = false;
            }
            else
            {
                magH = 1;
                thrustH = Mathf.Min(1, thrustH + Time.deltaTime);
                glidingHP = true;
            }
        }
        else if (movingHN)
        {
            if (glidingHP)
            {
                magH = 1;
                thrustH = Mathf.Max(0, thrustH - Time.deltaTime * 1.5f);
                if (thrustH == 0) glidingHP = false;
            }
            else
            {
                magH = -1;
                thrustH = Mathf.Min(1, thrustH + Time.deltaTime);
                glidingHN = true;
            }
        }

        if (!movingVN && !movingVP)
        {
            if (thrustV > 0)
            {
                thrustV = Mathf.Max(0, thrustV - Time.deltaTime);
            }
            else
            {
                glidingVN = false;
                glidingVP = false;
            }
        }
        else if (movingVP)
        {
            if (glidingVN)
            {
                magV = -1;
                thrustV = Mathf.Max(0, thrustV - Time.deltaTime * 2);
                if (thrustV == 0) glidingVN = false;
            }
            else
            {
                magV = 1;
                thrustV = Mathf.Min(1, thrustV + Time.deltaTime);
                glidingVP = true;
            }
        }
        else if (movingVN)
        {
            if (glidingVP)
            {
                magV = 1;
                thrustV = Mathf.Max(0, thrustV - Time.deltaTime * 1.5f);
                if (thrustV == 0) glidingVP = false;
            }
            else
            {
                magV = -1;
                thrustV = Mathf.Min(1, thrustV + Time.deltaTime);
                glidingVN = true;
            }
        }

        dir = new Vector3(magH, magV, 0);
        dir = dir.normalized * speed * Time.deltaTime;

        transform.position += new Vector3(dir.x * Mathf.Lerp(0, 1, thrustH), dir.y * Mathf.Lerp(0, 1, thrustV), 0);
    }

}
