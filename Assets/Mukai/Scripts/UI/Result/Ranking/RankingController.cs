using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RankingController : MonoBehaviour
{
    private const int RANKING_NUM = 5;                                      // 順位の数
    private string pass = "Assets/Mukai/Resources/Text/Ranking.txt";        // パス

    [SerializeField]
    private Text player;        // プレイヤーのスコア
    [SerializeField]
    private Text ran_1;
    [SerializeField]
    private Text ran_2;
    [SerializeField]
    private Text ran_3;
    [SerializeField]
    private Text ran_4;
    [SerializeField]
    private Text ran_5;

    private string[] array = new string[RANKING_NUM];
    private int[] ranking = new int[RANKING_NUM+1];         // ランキング + プレイヤー


    private int num;
    private string data = "";

    private int player_rank;

    private IEnumerator coroutine;

    // Use this for initialization
    void Start()
    {
        coroutine = RankInSE();

        // Rankinng.txtから情報を引っ張ってくる
        data = FileIO.Read(pass);

        // 取得したデータを配列化
        array = data.Split(char.Parse("\n"));

        // 配列化したものをint型に変換
        for (int i = 0; i < RANKING_NUM; i++)
        {
            ranking[i] = int.Parse(array[i]);
        }
        // 配列の最後にプレイヤー情報を挿入
        ranking[RANKING_NUM] = int.Parse(player.text);

        Debug.Log("1st : " + ranking[0]);
        Debug.Log("2st : " + ranking[1]);
        Debug.Log("3st : " + ranking[2]);
        Debug.Log("4st : " + ranking[3]);
        Debug.Log("5st : " + ranking[4]);
        Debug.Log("プレイヤー : " + ranking[5]);

        // 整列
        for (int i = 0; i < (RANKING_NUM + 1) - 1; i++)
        {
            for (int j = i + 1; j < (RANKING_NUM + 1); j++)
            {
                if (ranking[i] < ranking[j])
                {
                    int i_temp;

                    i_temp = ranking[i];
                    ranking[i] = ranking[j];
                    ranking[j] = i_temp;
                }
            }
        }

        player_rank = 6;
        for (int i = 0; i < RANKING_NUM; i++)
        {
            if (ranking[i] == int.Parse(player.text))
            {// プレイヤーがランクインしているかの探査
                player_rank = i;
            }
        }

        // プレイヤーがランクインしていた場合光らせる
        CheckSetBlinker(player_rank);
        Debug.Log("PlayerRank : " + player_rank);

        // テキスト変更
        ran_1.text = ranking[0].ToString();
        ran_2.text = ranking[1].ToString();
        ran_3.text = ranking[2].ToString();
        ran_4.text = ranking[3].ToString();
        ran_5.text = ranking[4].ToString();

        // 文字型に再び変換
        for (int i = 0; i < RANKING_NUM; i++)
        {
            array[i] = ranking[i].ToString();
        }

        string temp = "";

        // 文字配列を結合させて保存
        for (int i = 0; i < RANKING_NUM; i++)
        {
            temp = temp + array[i] + "\n";
        }
        data = temp;

        // ファイルへの書き込み
        FileIO.Write(pass, data);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void CheckSetBlinker(int num)
    {
        GameObject temp = null;

        if (num >= 5)
        {
            return;
        }
        else if (num == 0)
        {
            temp = GameObject.Find("1stScore");
        }
        else if (num == 1)
        {
            temp = GameObject.Find("2ndScore");
        }
        else if (num == 2)
        {
            temp = GameObject.Find("3rdScore");
        }
        else if (num == 3)
        {
            temp = GameObject.Find("4thScore");
        }
        else if (num == 4)
        {
            temp = GameObject.Find("5thScore");
        }
        StartCoroutine(coroutine);
        temp.AddComponent<TextBlinker>();
    }

    IEnumerator RankInSE()
    {// コルーチン.
        yield return new WaitForSeconds(1.5f);
        AudioManager.Instance.PlaySe("RankIn");
        StopCoroutine(coroutine);
    }
}



