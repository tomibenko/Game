using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ItemGenerator : MonoBehaviour
{

    public GameObject itemPrefab;
    public Sprite[] itemSprites;
    public int numOfItems = 3;
    public float spacing = 2.0f;
    public Vector2 itemSize = new Vector2(1.0f, 1.0f);

    // Start is called before the first frame update
    void Start()
    {
        GenerateItems();
    }

    void GenerateItems()
    {
        float startOffset = -(numOfItems - 1) * spacing / 2.0f;
        for (int i = 0; i < numOfItems; i++) 
        {
            Vector3 position = new Vector3(startOffset + i * spacing, 0, 0);
            GameObject newItem = Instantiate(itemPrefab, position, Quaternion.identity);

            int randomHealth = Random.Range(1, 30);
            int randomAttack = Random.Range(0, 30);

            Sprite randomSprite = itemSprites[Random.Range(0, itemSprites.Length)];

            Item itemScript = newItem.GetComponent<Item>();
            itemScript.InitializeItem(randomSprite, randomHealth, randomAttack);

            ResizeItem(newItem, itemSize);
        }
    }

    void ResizeItem(GameObject item, Vector2 newSize)
    {
        SpriteRenderer spriteRenderer = item.GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            Vector2 spriteSize = spriteRenderer.sprite.bounds.size;

            Vector3 scale = item.transform.localScale;
            scale.x = newSize.x / spriteSize.x;
            scale.y = newSize.y / spriteSize.y;
            item.transform.localScale = scale;
        }
    }
}
