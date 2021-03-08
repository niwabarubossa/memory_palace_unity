using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine;
using System.IO;


public class Root : MonoBehaviour
{
    // Start is called before the first frame update
    //public string[] mindMap = { "word crowd", "カッパ効果", "幻肢痛", "インラタクティブタッチ", "Sketch＆Stitch", "東洋思想", "", "lidarセンサー", "Human Augmentation", "文化人類学", "ghost science", "kegon", "muscle memoru", "" };
    //public string[][] mindMap = null;
    public List<string> mindMap = null;


    TextAsset csvFile; // CSVファイル
    List<string[]> csvDatas = new List<string[]>(); // CSVの中身を入れるリスト;

    public int oneLineCharacterAmonut = 10;
    public GameObject targetUnityChan;


    void Start()
    {
        this.readAndSetCSVFiles();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey("up"))
        {
            this.createText();
        }
    }

    private void readAndSetCSVFiles()
    {
        csvFile = Resources.Load("mindMap") as TextAsset; // Resouces下のCSV読み込み
        StringReader reader = new StringReader(csvFile.text);

        while (reader.Peek() != -1) // reader.Peaekが-1になるまで
        {
            string line = reader.ReadLine(); // 一行ずつ読み込み
            string newLine = "";
            for (int i = 10; i < line.Length ; i+= this.oneLineCharacterAmonut)
            {
                if (i == 10)
                {//初回のみ
                    newLine = line.Insert(i, "\n");
                }
                else
                {//２回目以降
                    newLine = newLine.Insert(i, "\n");
                }
            }
            mindMap.Add(newLine);
        }
    }

    private void createText()
    {
        GameObject obj = (GameObject)Resources.Load("Mesh");
        //x,zが-5~5は弾く
        Vector3 randomPosition = new Vector3(Random.Range(-20f, 20f), Random.Range(-20f, 20f), Random.Range(-20f, 20f));
        var randomInt = Random.Range(0, (mindMap.Count) - 1);
        obj.GetComponent<TextMesh>().text = mindMap[randomInt];

        var instaniateObj = Instantiate(obj, randomPosition, Quaternion.identity);
        var randomScale = Random.Range(0.3f, 4f);
        instaniateObj.transform.localScale = new Vector3(randomScale, randomScale, randomScale);
        instaniateObj.transform.LookAt(this.targetUnityChan.transform.position);
        instaniateObj.transform.Rotate(new Vector3(0, 1, 0), 180f);
    }
}