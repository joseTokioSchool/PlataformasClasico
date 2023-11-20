using System.Collections;
using UnityEngine;

public class FlagPole : MonoBehaviour // Script para la animacion de tocar la bandera y avanzar hasta el castillo.
{
    public Transform flag;
    public Transform poleBottom;
    public Transform castle;
    public float speed = 6f;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            AudioManager.AudioInstance.FlagClip();

            StartCoroutine(MoveTo(flag, poleBottom.position));
            StartCoroutine(LevelCompleteSequence(collision.transform));
        }
    }
    private IEnumerator LevelCompleteSequence(Transform player) // Corrutina para la animación de finalizar el nivel y pasar al siguiente.
    {
        player.GetComponent<PlayerMovement>().enabled = false;

        yield return MoveTo(player, poleBottom.position);
        yield return MoveTo(player, player.position + Vector3.right);
        yield return MoveTo(player, player.position + Vector3.right + Vector3.down);
        yield return MoveTo(player, castle.position);

        player.gameObject.SetActive(false);

        yield return new WaitForSeconds(2f);

        CheckRecordManager();
        GameManager.Instance.LoadLevel();
    }
    private IEnumerator MoveTo(Transform subject, Vector3 destination) // Corrutina para la animación de finalizar el nivel y pasar al siguiente.
    {
        while (Vector3.Distance(subject.position, destination) > 0.125f)
        {
            subject.position = Vector3.MoveTowards(subject.position, destination, speed * Time.deltaTime);
            yield return null;
        }

        subject.position = destination;
    }
    private void CheckRecordManager()
    {
        if (PlayerPrefs.GetInt("Levels") <= 1)
        {
            PlayerPrefs.SetInt("Levels", 2);
        }
        else if (PlayerPrefs.GetInt("Levels") == 2)
        {
            PlayerPrefs.SetInt("Levels", 3);
        }
        else if (PlayerPrefs.GetInt("Levels") >= 3)
        {
            PlayerPrefs.SetInt("Levels", 4);
        }
    }
}
