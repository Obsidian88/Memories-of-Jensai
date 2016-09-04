using UnityEngine;
using System.Collections;
using System;

public class GridHandler : MonoBehaviour {

    public Transform parent;
    public string[] ignore;
    public GameObject[] tiles;
    public Vector2[] positions;
    public float offset;
    public Vector2 dimensions;
    public float scale;
    public float channel;
    public float channelslow;
    public float cooldown;
    public bool attacking;
    public bool channeling;
    public bool iterrupted;

    private Vector2 reference = new Vector2(0,-1);
    private float ccd = 0;
    private float cdcd=0;
    private bool active;
    private float normalSpeed;

    private CharacterMovement move;
    private Rigidbody body;
    private CharacterStatus stat;

    // Use this for initialization
    void Start ()
    {
        body = parent.GetComponent<Rigidbody>();
        move = parent.GetComponent<CharacterMovement>();
        stat = parent.GetComponent<CharacterStatus>();
    }
	
	// Update is called once per frame
	void Update () {

        if (cdcd > 0)
        {
            attacking = true;
            cdcd -= Time.deltaTime;
            move.enabled = false;
            return;
        }
        else
        {
            attacking = false;
            move.enabled = true;
        }

        if (channeling) {
            checkInterrupt();
                }
        else
        {
            iterrupted = false;
        }

        if (ccd > 0)
        {
            ccd -= Time.deltaTime;
        }
        else if (channeling)
        {
            move.MaxSpeed = normalSpeed;
            channeling = false;
            foreach (Transform child in transform)
                {
                    child.gameObject.SendMessage("Activate");
                }
                cdcd = cooldown;
        }
        else if(iterrupted)
        {
            move.MaxSpeed = normalSpeed;
            channeling = false;
            ccd = 0;
            destroyChildren();
        }


        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit[] hits;
        hits = Physics.RaycastAll(ray, 1000);

        foreach (RaycastHit hit in hits)

        {
            string hittag = hit.collider.tag;
            bool skip = false;
            foreach(string tag in ignore)
            {
                if (hittag == tag) skip=true;
            }
            if (skip) continue;
            Vector3 difference = hit.point - parent.position;
            Vector2 direction = new Vector2(0, 0);
            direction.x = difference.x;
            direction.y = difference.z;
            float sign = (direction.x > reference.x) ? -1.0f : 1.0f;
            float angle = Vector2.Angle(direction, reference) * sign;
            transform.eulerAngles = new Vector3(0, angle, 0);

            }

        if (Input.GetMouseButton(1))
        {
            active = false;
            destroyChildren();
        }

        if (Input.GetMouseButtonDown(0))
        {
            for (int i = 0; i < positions.Length; i++)
            {
                GameObject tile = Instantiate(tiles[i]);
                tile.transform.parent = transform;
                Vector2 pos = positions[i];
                Vector2 topcorner;
                topcorner.x = -((scale * dimensions.x) / 2) + scale / 2;
                topcorner.y = -offset - scale / 2;
                tile.transform.localPosition = new Vector3(topcorner.x + scale * pos.x, tile.transform.localPosition.y, topcorner.y - scale * pos.y);
                tile.transform.localRotation = new Quaternion(0, 0, 0, 0);
                tile.transform.localScale = new Vector3(scale, transform.localScale.y, scale);
            }
            active = true;

        }

        if (Input.GetMouseButtonUp(0))
        {
            
            if (active)
            {
                channeling = true;
                normalSpeed = move.MaxSpeed;
                move.MaxSpeed *= channelslow;
                ccd = channel;
            }
        }

    }

    private void checkInterrupt()
    {
        if (Input.GetMouseButton(1) ||
            stat.takingDamage)
        {
            iterrupted = true;
            channeling = false;
            ccd = 0;
        }
    }

    private void destroyChildren()
    {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
    }
}
