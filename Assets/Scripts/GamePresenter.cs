using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GamePresenter : MonoBehaviour
{

    public CameraController cameraController;
    public BallController[] balls = new BallController[0];
    public UnityEngine.UI.Slider ballSpeedSlider;


    private int currentBallIndex = 0;




    void Start()
    {
        
        if (balls.Length == 0)
        {
            return;
        }
        balls[0].SetPositions(PathLoader.Load("ball_path"));
        balls[1].SetPositions(PathLoader.Load("ball_path2"));
        balls[2].SetPositions(PathLoader.Load("ball_path3"));
        balls[3].SetPositions(PathLoader.Load("ball_path4"));
        cameraController.target = balls[0].transform;
        SetBall(currentBallIndex);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
        {
            PrevBall();
        }

        if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
        {
            NextBall();
        }
        ballSpeedSlider.value = balls[currentBallIndex].speed;
        
    }

    public void SetBall(int index)
    {
        balls[currentBallIndex].inFocus = false;
        balls[currentBallIndex].SelectedMaterial();
        balls[index].inFocus = true;
        balls[index].SelectedMaterial();
        balls[index].speed = balls[currentBallIndex].speed;
        balls[currentBallIndex].speed = 0;
        currentBallIndex = index;

        ballSpeedSlider.value = balls[index].speed;
        

        cameraController.target = balls[index].transform;
        

    }

  

    public void NextBall()
    {
        SetBall((currentBallIndex + 1) % balls.Length);
    }

    public void PrevBall()
    {
        SetBall((balls.Length + currentBallIndex - 1) % balls.Length);
    }


    public void DisableCameraRotation()
    {
        cameraController.enableCamRotation = false;
    }

    public void EnableCameraRotation()
    {
        cameraController.enableCamRotation = true;
    }
    public void OnUpdateSliderValue()
    {
        balls[currentBallIndex].speed = ballSpeedSlider.value;

    }

    
}
