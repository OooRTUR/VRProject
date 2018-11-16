using System.Collections;
using UnityEngine;

public class BearMotor : AnimalMotor {
    protected override IEnumerator Alarm()
    {
        Debug.Log("Overrided Alarm for Bear started");
        agent.speed = 7.0f;
        float chaseTime = 0.0f;
        while (true) {
            chaseTime += Time.deltaTime;
            agent.ResetPath();
            agent.SetDestination(visibleTarget.position);
            if (chaseTime > 1.0f && fow.visibleTargets.Count < 1)
                break;
            yield return new WaitForSeconds(0.1f);
        }
        agent.ResetPath();
        Debug.Log("Остновка погони");
        ChangeCondition(Condition.Safety, "Alarm", "Safety");
    }

    protected override IEnumerator Safety()
    {
        
        for (int i = 0; i < Random.Range(2, 4); i++)
        {
            yield return new WaitForSeconds(2.0f);
            agent.speed = Random.Range(3.0f, 6.0f);
            agent.ResetPath();
            agent.SetDestination(ai.GetWalkPoint(visibleTarget.position));
            Debug.Log("Прогулка до точки: " + agent.destination);
            yield return new WaitForSeconds(4.0f);
        }
        agent.ResetPath();
        Debug.Log("Остановка поиска цели");
        ChangeCondition(Condition.Secure, "Safety", "Secure");
    }
}
