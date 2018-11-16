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
        Debug.Log("Остновка погони, переход в поиск цели");
        ChangeCondition(Condition.Safety, "Alarm", "Safety");
    }

    protected override IEnumerator Safety()
    {
        float newPointTimer = 0.0f;
        float searchTimer = 0.0f;
        yield return new WaitForSeconds(2.0f);
        agent.speed = Random.Range(3.0f, 6.0f);
        agent.ResetPath();
        agent.SetDestination(ai.GetWalkPoint(visibleTarget.position, 10.0f));
        Debug.Log("Поиск цели в точке: " + agent.destination);
        while (true)
        {
            newPointTimer += Time.deltaTime;
            searchTimer += Time.deltaTime;
            Debug.Log(searchTimer);
            if (newPointTimer > 1.5f)
            {
                agent.speed = Random.Range(3.0f, 6.0f);
                agent.ResetPath();
                agent.SetDestination(ai.GetWalkPoint(visibleTarget.position, 10.0f));
                Debug.Log("Прогулка до точки: " + agent.destination);
                newPointTimer = 0.0f;
            }
            if(searchTimer > 5.0f && fow.visibleTargets.Count < 1)
            {
                agent.ResetPath();
                Debug.Log("Остановка поиска цели");
                ChangeCondition(Condition.Secure, "Safety", "Secure");
            }
            else if(searchTimer < 5.0f && fow.visibleTargets.Count > 0)
            {
                agent.ResetPath();
                Debug.Log("Возвращение в погоню");
                ChangeCondition(Condition.Alarm, "Safety", "Alarm");
            }

            yield return new WaitForSeconds(0.1f);
        }
        
    }
}
