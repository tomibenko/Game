using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{

    private List<Unit> allUnits;
    private Vector3 mOffset;
    private float mZCoord;
    public string name;
    public int health;
    public int attackDamage;

    public Item(string _name, int _health, int _attackDamage)
    {
        name = _name;
        health = _health;
        attackDamage = _attackDamage;
    }


    void OnMouseDown()
    {
        mZCoord = Camera.main.WorldToScreenPoint(gameObject.transform.position).z;

        // Store offset = gameobject world pos - mouse world pos
        mOffset = gameObject.transform.position - GetMouseWorldPos();
        Debug.Log(gameObject.name + " was clicked.");
    }

    private Vector3 GetMouseWorldPos()
    {
        // Pixel coordinates (x,y)
        Vector3 mousePoint = Input.mousePosition;

        // z coordinate of game object on screen
        mousePoint.z = mZCoord;

        // Convert it to world points
        return Camera.main.ScreenToWorldPoint(mousePoint);
    }

    void OnMouseDrag()
    {
        transform.position = GetMouseWorldPos() + mOffset;
    }

    void OnMouseUp()
    {
        int layerMask = (1 << LayerMask.NameToLayer("Item"));
        layerMask = ~layerMask;  // Bitwise invert to ignore the 'Item' layer

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        // Visualize the ray in the scene view
        Debug.DrawRay(ray.origin, ray.direction * 100, Color.red, 2f);  // Draws a red ray for 2 seconds

        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, layerMask))
        {
            Debug.Log("Raycast hit: " + hit.collider.name); // Check what the raycast hits
            Unit unit = hit.collider.GetComponent<Unit>();
            if (unit != null)
            {
                AddHealthToUnit(unit);
            }
            else
            {
                Debug.Log("No Unit component found on hit object.");
            }
        }
        else
        {
            Debug.Log("No hit detected.");
        }
    }


    private void AddHealthToUnit(Unit unit)
    {
        if (unit != null)
        {
            unit.GetItem(health,attackDamage);
            Debug.Log("Item applied to " + unit.gameObject.name);
            Destroy(gameObject);
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        allUnits = new List<Unit>(FindObjectsOfType<Unit>());
        if (allUnits.Count == 0)
        {
            Debug.LogError("No Unit scripts found in the scene.");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
