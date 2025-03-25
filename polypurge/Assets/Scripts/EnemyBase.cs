using UnityEngine;



public abstract class EnemyBase : MonoBehaviour
{
    protected enum Enemybeheviour
    {
        patrolling,
        chasing,
        searching
    }

    protected Enemybeheviour currentBeheviour;

    // Update is called once per frame
    void Update()
    {
        switch (currentBeheviour)
        {
            case Enemybeheviour.patrolling:
                Patrolling();
                break;
            case Enemybeheviour.chasing:
                Chasing();
                break;
            case Enemybeheviour.searching:
                Searching();
                break;
        }
    }

    protected virtual void Patrolling()
    {
        Debug.Log("Patrolling");
    }

    protected virtual void Chasing()
    {
        Debug.Log("Chasing");
    }

    protected virtual void Searching()
    {
        Debug.Log("Searching");
    }

    protected void SetBeheviour(Enemybeheviour beheviour)
    {
        currentBeheviour = beheviour;
    }
}
