using UnityEngine;

public class PlayerController : MonoBehaviour {
    private float speed = 10f;
    private float accuracy = 0.5f;
    public void MoveToPoint(Vector2 targetPoint)
    {
        Vector2 targetX = new Vector2(targetPoint.x, transform.position.y);
        while (Vector2.Distance(transform.position, targetX) > accuracy)
        {
            transform.position = Vector2.MoveTowards(transform.position, targetX, speed * Time.deltaTime);

        }
        transform.position = targetX;
    }

    public void GoToItem(ItemData item)
    {

        MoveToPoint(item.GoToPoint.position);

    }

    
}