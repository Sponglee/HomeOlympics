using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwimmingOpponentController : SwimmingSwimmer
{
    protected override void Finished(float time, LaneController controller)
    {
        SwimmingController.onFinish.Invoke(time, controller, 0f);
        base.Finished(time, controller);
    }

    private void Start()
    {
        if (PlayerInfoManager.Instance != null)
            flag = PlayerInfoManager.Instance.flags[Random.Range(0, PlayerInfoManager.Instance.flags.Length)];
        else
            flag = GameManager.Instance.playerFlag;

        StartCoroutine(StartBotBehaviour());
    }


    private IEnumerator StartBotBehaviour()
    {
        while (true)
        {
            if(CanMove)
            {
                int decsision = Random.Range(0, 100);

                if (decsision > 30f)
                {
                    if (StressLevel / stressLimit < 0.95f)
                    {

                        Push();
                    }
                }

            }

            yield return new WaitForSecondsRealtime(0.2f);
        }
    }
}
