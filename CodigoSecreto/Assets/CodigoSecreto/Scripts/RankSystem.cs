using UnityEngine;
using System.Collections;
using PlatformIntegration.RM;
using PlatformIntegration.SVO;

public class RankSystem : MonoBehaviour
{
    private static RankSystem instance;
    public static RankSystem Instance
    {
        get
        {
            return instance;
        }
    }

    void Awake()
    {
        instance = this;
    }


    public void SaveRank(float score, int stars)
    {
        RequestManager.savePlayerScore(score, 1, Mathf.RoundToInt(Time.time), stars, OnSavePlayerScoreRequestSuccess, OnRequestError);

        //Debug.Log("SaveRank()");
        //Debug.Log("name: " + SystemInfo.deviceName);
        //Debug.Log("score: " + score);
    }

    void OnRequestError(BaseRequestModel request)
    {
        Debug.Log("(ERROR) OnRequestSuccess called  :: obj received: " + request.errorMessage);
        ranking = new GetMinigameRankingSVO();
    }

    void OnSavePlayerScoreRequestSuccess(BaseRequestModel request)
    {
        SavePlayerScoreRM r = request as SavePlayerScoreRM;
        //Debug.Log("OnSavePlayerScoreRequestSuccess called  :: obj received: " + r.svo.jsonReceived);
    }


    public GetMinigameRankingSVO ranking = null;

    void OnGetMinigameRankingRequestSuccess(BaseRequestModel request)
    {
        GetMinigameRankingRM r = request as GetMinigameRankingRM;
        ranking = r.svo;
    }

    public void GetRanking(string period, int page)
    {
        ranking = null;
        RequestManager.getMinigameRanking(period, page, 10, OnGetMinigameRankingRequestSuccess, OnRequestError);
    }

}
