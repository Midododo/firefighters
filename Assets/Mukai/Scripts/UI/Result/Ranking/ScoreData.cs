using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreData : MonoBehaviour
{

    private const string RANKING_PREF_KEY = "ranking";
    private const int RANKING_NUM = 5;
    private int[] ranking = new int[RANKING_NUM];

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}


    public void LoadRanking()
    {
        // 保存してあるデータを取得
        string load_rank = PlayerPrefs.GetString(RANKING_PREF_KEY);
        if (load_rank.Length > 0)
        {
            var score = load_rank.Split(","[0]);                        // ","で区切って配列化
            ranking = new int[RANKING_NUM];                             // これ必要か？？
            for (int i = 0; i < score.Length && i < RANKING_NUM; i++)   // 配列化した分だけぶん回す
            {   // string型からint型にキャストしてrankingに保存
                ranking[i] = int.Parse(score[i]);
            }
        }

    }

    public void SaveRanking(int NewScore)
    {
        if (ranking != null)
        {
            int i_temp;
            for (int i = 0; i < ranking.Length; i++)
            {
                if (ranking[i] < NewScore)
                {
                    i_temp = ranking[i];
                    ranking[i] = NewScore;
                    NewScore = i_temp;
                }
            }
        }
        else
        {
            ranking[0] = NewScore;
        }

        string[] s_temp = new string[RANKING_NUM];

        for (int i = 0; i < ranking.Length; i++)
        {
            s_temp[i] = ranking[i].ToString();
        }
        var save_rank = string.Join(",", s_temp);
        PlayerPrefs.SetString(RANKING_PREF_KEY, save_rank);
    }

}
