using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class FileSaveTest : MonoBehaviour
{
    public GameObject playerObj;
    PlayerTestData pData;
    public string _path = "/Save/TestSave.txt";
    [SerializeField] string internalPath;

    private void Start()
    {
        playerObj = GameObject.FindGameObjectWithTag("Player");
        pData = new PlayerTestData();

        internalPath = Application.dataPath + _path;
    }

    public void SaveTest()
    {
        //在場景中找到要存的資料
        pData.pos = playerObj.transform.position;
        pData.achievements = playerObj.GetComponent<AchievementTest>().achievements;

        //把需要的資料轉成Json
        string jsonData = JsonUtility.ToJson(pData);
        Debug.Log(jsonData);


        //檢查位置是否存在
        if (!Directory.Exists(Path.GetDirectoryName(internalPath)))
        {
            //若位置不存在則產生位置
            Directory.CreateDirectory(Path.GetDirectoryName(internalPath));
            Debug.Log(internalPath);
            Debug.Log(Path.GetDirectoryName(internalPath));
        }

        
        StreamWriter sw = new StreamWriter(internalPath);//再開寫入
        sw.Write(jsonData);//資料寫進去
        sw.Close();//關起來

    }

    public void LoadTest()
    {
        if (File.Exists(internalPath))
        {
            StreamReader sr = new StreamReader(internalPath);
            string jsonStr = sr.ReadToEnd();
            Debug.Log("Load:" + jsonStr);

            pData = JsonUtility.FromJson<PlayerTestData>(jsonStr);//塞進資料形態裡

            //資料塞回場景上的物件
            playerObj.transform.position = pData.pos;
            playerObj.GetComponent<AchievementTest>().achievements = pData.achievements;
            playerObj.GetComponent<AchievementTest>().refreshToggles();
        }
        else
        {
            Debug.Log("檔案不存在");
        }
    }

    public class PlayerTestData
    {
        public Vector3 pos;
        public bool[] achievements;
    }
}
