using UnityEngine;
using System.Collections;
public class MovingBackground : MonoBehaviour
{
    // De array van Transforms waar het object langs moet bewegen
    public Transform[] points;
    // Snelheid van de beweging
    public float speed = 1.0f;
    // Rotatiesnelheid
    public float rotationSpeed = 5.0f; // Toegevoegd: snelheid waarmee het object roteert
    // De tijd die het duurt om tussen de punten te bewegen
    private float moveDuration;
    private int currentTargetIndex = 0;

    // Start wordt aangeroepen bij het begin
    void Start()
    {
        if (points.Length > 0)
        {
            moveDuration = Vector3.Distance(transform.position, points[0].position) / speed;
            StartCoroutine(MoveThroughPoints());
        }
    }

    // Coroutine om het object soepel door de array van Transforms te bewegen
    IEnumerator MoveThroughPoints()
    {
        while (true) // Blijft doorgaan
        {
            // Beweeg naar het huidige doel
            Vector3 startPos = transform.position;
            Vector3 targetPos = points[currentTargetIndex].position;
            float elapsedTime = 0f;

            // Beweeg naar het doelpunt en pas de rotatie aan
            while (elapsedTime < moveDuration)
            {
                // Beweging
                transform.position = Vector3.Lerp(startPos, targetPos, elapsedTime / moveDuration);

                // Draai naar het doelpunt
                Vector3 direction = targetPos - transform.position;
                if (direction != Vector3.zero)
                {
                    Quaternion targetRotation = Quaternion.LookRotation(direction);
                    transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
                }

                elapsedTime += Time.deltaTime;
                yield return null;
            }

            // Zorg ervoor dat we exact bij het doel komen
            transform.position = targetPos;

            // Stel het volgende doel in de array in
            currentTargetIndex++;

            // Als we het einde van de array hebben bereikt, keer terug naar het begin
            if (currentTargetIndex >= points.Length)
            {
                currentTargetIndex = 0;
            }

            // Pas de moveDuration aan voor de volgende overgang
            moveDuration = Vector3.Distance(transform.position, points[currentTargetIndex].position) / speed;
        }
    }
}