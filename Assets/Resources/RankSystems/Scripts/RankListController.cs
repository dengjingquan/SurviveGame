using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class RankListController : MonoBehaviour {
    //读取的数据文件路径(根目录为项目根目录)
    public string FileName;
    //特殊排名颜色
    public Color RankFirstBgColor;
    public Color RankFirstMedalColor;
    public Color RankSecondBgColor;
    public Color RankSecondMedalColor;
    public Color RankThirdBgColor;
    public Color RankThirdMedalColor;

    // Use this for initialization
    void Start () {
        //从文件读取排行榜数据
        string[] Lines = File.ReadAllLines(FileName);
        ArrayList RankItems = new ArrayList();
        foreach (string line in Lines)
        {
            Debug.Log(line);
            string Score = line.Split(' ')[1];
            string User = line.Split(' ')[0];
            RankItems.Add(new RankDataItem(float.Parse(Score), User));
        }
        RankItems.Sort(new RankDataItemComparetor());
        //加载预制件
        GameObject ListItemPrefab = (GameObject)Resources.Load("RankSystems/Prefabs/RankListItem");
        //将成绩前15个添加到排行榜
        for (int i = 0; i < RankItems.Count&& i < 15; i++)
        {
            //实例化预制件
            GameObject Item = Instantiate(ListItemPrefab);
            //填充实例数据
            Item.transform.Find("RankText").GetComponent<Text>().text = (i+1).ToString();
            Item.transform.Find("UserText").GetComponent<Text>().text = ((RankDataItem)(RankItems[i])).User;
            Item.transform.Find("ScoreText").GetComponent<Text>().text = ((RankDataItem)(RankItems[i])).Score.ToString("0");
            //对前三名颜色进行特殊处理
            if(i < 3)
            {
                switch (i)
                {
                    case 0:
                        Item.transform.Find("ItemBg").GetComponent<Image>().color = RankFirstBgColor;
                        Item.transform.Find("MedalImg").GetComponent<Image>().color = RankFirstMedalColor;
                        break;
                    case 1:
                        Item.transform.Find("ItemBg").GetComponent<Image>().color = RankSecondBgColor;
                        Item.transform.Find("MedalImg").GetComponent<Image>().color = RankSecondMedalColor;
                        break;
                    case 2:
                        Item.transform.Find("ItemBg").GetComponent<Image>().color = RankThirdBgColor;
                        Item.transform.Find("MedalImg").GetComponent<Image>().color = RankThirdMedalColor;
                        break;
                }
            }
            //将实例加载到场景
            Item.transform.parent = this.transform;
            Item.transform.localScale = new Vector3(1, 1, 1);
            Item.transform.localPosition=new Vector3(Item.transform.localPosition.x, Item.transform.localPosition.y,0);

        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}

// 成绩数据类
class RankDataItem
{
    public float Score;
    public string User;

    public RankDataItem(float Score,string User)
    {
        this.Score = Score;
        this.User = User;
    }
}

class RankDataItemComparetor : IComparer
{
    // 比较器 按完成游戏使用时间升序排序
    int IComparer.Compare(object x, object y)
    {
        RankDataItem NewX = (RankDataItem)x;
        RankDataItem NewY = (RankDataItem)y;
        if(NewX.Score > NewY.Score)
        {
            return -1;
        }
        if (NewX.Score < NewY.Score)
        {
            return 1;
        }
        return 0;
    }
}