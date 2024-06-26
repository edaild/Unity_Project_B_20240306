using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;                                                       //UI를 사용하기 위해서
 using UnityEngine.SceneManagement;                                          //Scene 이동 하기 위해서

public class ExRayCast : MonoBehaviour
{
    public int Point = 0;                                                       //포인트 계산 변수
    public GameObject TargetObject;                                             // 타겟 프리펩
    public float CheckTime = 0;                                                 //타겟 Gen 시간 변수
    public float GameTime = 30.0f;

    public Text pointUI;                                                        //Unity UI 설정
    public Text TimeUI;                                                         // Unity UI 설정

    // Update is called once per frame
    void Update()
    {
        CheckTime += Time.deltaTime;                                        // 프레임이 누적되어시간을 계산하게 한다.
        GameTime -= Time.deltaTime;

        if(GameTime <= 0)
        {
            PlayerPrefs.SetInt("Point", Point);
            SceneManager.LoadScene("MainScene");
        }

        pointUI.text = Point.ToString();                                    //UI 점수 표시
        TimeUI.text = "남은시간 : " + GameTime.ToString() + "s";            // UI 남은 시간 표시

        if (CheckTime >= 0.5f)                                               //0.5초 마다 행동을 한다.
        {
            int RandomX = Random.Range(0, 12);                                  
            int RandomY = Random.Range(0, 12);
            GameObject temp = Instantiate(TargetObject);                            //Instantiacte 함수를 통해서 프리팹을 생성한다.
            temp.transform.position = new Vector3(-6 + RandomX, -6 + RandomY, 0); //렌덤값 더해서 -6 ~ 5 사이의 값을 랜덤하게 배치
            Destroy(temp, 1.0f);                                                        // 1초후 파괴
            CheckTime = 0;                                                        // 시간 초기화 (0.5초 마다 반복하게 하기 위해서)

        }
        if (Input.GetMouseButtonDown(0))                                        // 마우스 오은쪽 버튼이 눌렸을 경우
        {
            Ray cast = Camera.main.ScreenPointToRay(Input.mousePosition);       //카메라를 화면 시점에서 마우스 포지션에서 Ray를 쏜다.

            RaycastHit hit;                                                     //쏜 Ray에  검출된것이 있으면

            if (Physics.Raycast(cast, out hit))                                // Ray에 검출된것이 있으면
            {         
                Debug.Log(hit.collider.gameObject.name);                      // 검출된 오브젝트 이름을 출력 해준다.
                Debug.DrawLine(cast.origin, hit.point, Color.red, 2.0f);      // Ray 라인으로 표현 해주는 함수
                
                if(hit.collider.gameObject.tag == "Target")                    // 검출된 오브젝트의 Tag 가 Target일 경우
                {
                    Point += 1;
                    Destroy(hit.collider.gameObject);                         // 해당 게임 오브젝트를 파괴한다
                }
            }   
        }       
    }           
}               
                