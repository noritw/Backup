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
        //�b���������n�s�����
        pData.pos = playerObj.transform.position;
        pData.achievements = playerObj.GetComponent<AchievementTest>().achievements;

        //��ݭn������নJson
        string jsonData = JsonUtility.ToJson(pData);
        Debug.Log(jsonData);


        //�ˬd��m�O�_�s�b
        if (!Directory.Exists(Path.GetDirectoryName(internalPath)))
        {
            //�Y��m���s�b�h���ͦ�m
            Directory.CreateDirectory(Path.GetDirectoryName(internalPath));
            Debug.Log(internalPath);
            Debug.Log(Path.GetDirectoryName(internalPath));
        }

        
        StreamWriter sw = new StreamWriter(internalPath);//�A�}�g�J
        sw.Write(jsonData);//��Ƽg�i�h
        sw.Close();//���_��

    }

    public void LoadTest()
    {
        if (File.Exists(internalPath))
        {
            StreamReader sr = new StreamReader(internalPath);
            string jsonStr = sr.ReadToEnd();
            Debug.Log("Load:" + jsonStr);

            pData = JsonUtility.FromJson<PlayerTestData>(jsonStr);//��i��ƧκA��

            //��ƶ�^�����W������
            playerObj.transform.position = pData.pos;
            playerObj.GetComponent<AchievementTest>().achievements = pData.achievements;
            playerObj.GetComponent<AchievementTest>().refreshToggles();
        }
        else
        {
            Debug.Log("�ɮפ��s�b");
        }
    }

    public class PlayerTestData
    {
        public Vector3 pos;
        public bool[] achievements;
    }
}
