using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WorkoutManager : MonoBehaviour
{
    public GameObject ballPrefab;
    public Transform ballSpawnPoint;
    public TextAsset jsonFile;

    private List<WorkoutInfo> workoutInfos;

    private void Start()
    {
        LoadWorkoutInfo();
    }

    private void LoadWorkoutInfo()
    {
        string json = jsonFile.text;
        WorkoutInfoList list = JsonUtility.FromJson<WorkoutInfoList>(json);
        workoutInfos = list.workoutInfo;

        foreach (var workoutInfo in workoutInfos)
        {
            Button button = GameObject.Find(workoutInfo.workoutName).GetComponent<Button>();
            button.onClick.AddListener(() => OnButtonClick(workoutInfo.workoutID));
            Text buttonText = button.GetComponentInChildren<Text>();
            buttonText.text = workoutInfo.workoutName + "\n" + workoutInfo.description;
        }
    }

    public void OnButtonClick(int workoutId)
    {
        WorkoutInfo workoutInfo = workoutInfos.Find(info => info.workoutID == workoutId);
        if (workoutInfo != null)
        {
            foreach (var workoutDetail in workoutInfo.workoutDetails)
            {
                GameObject ballGO = Instantiate(ballPrefab, ballSpawnPoint.position, Quaternion.identity);
                Ball ball = ballGO.GetComponent<Ball>();
                ball.speed = workoutDetail.speed;
                ball.direction = workoutDetail.ballDirection;
            }
        }
    }

    [System.Serializable]
    public class WorkoutInfoList
    {
        public List<WorkoutInfo> workoutInfo;
    }

    [System.Serializable]
    public class WorkoutInfo
    {
        public int workoutID;
        public string workoutName;
        public string description;
        public string ballType;
        public List<WorkoutDetail> workoutDetails;
    }

    [System.Serializable]
    public class WorkoutDetail
    {
        public int ballId;
        public float speed;
        public float ballDirection;
    }
}
