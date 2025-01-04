using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Pellet : MonoBehaviour {
    public int points = 10;

    protected void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Pacman")) {
            Eat();
        }
    }

    protected virtual void Eat() {
        GameManager.Instance.PelletEaten(this);
    }
}
