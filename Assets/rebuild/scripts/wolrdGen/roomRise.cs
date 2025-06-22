using UnityEngine;

using System;
using System.Collections;
using System.Collections.Generic;

public class roomRise : MonoBehaviour {
    [Header("values")]
    public float EndValue;
    public float StartValue;

    [Header("config")]
    public RiseType type;
    public float speed = 5f;

    public enum RiseType {
        PositionY,
        RotationX,
        enemyRotationX
    }

    public bool start;

    void OnEnable() {
        switch (type.ToString()) {
            case "RotationX":
                transform.rotation = Quaternion.LookRotation( new Vector3(StartValue, 0, 0));

                break;
            case "enemyRotationX":
                transform.rotation = Quaternion.LookRotation( new Vector3(StartValue, 0, 0));

                break;
        }
    }

    void Update() {    
        switch (type.ToString()) {
            case "PositionY":
                if(Vector3.Distance(transform.position, new Vector3(transform.position.y, EndValue, transform.position.z)) > 0.01f) 
                    transform.position = new Vector3(transform.position.x,  Mathf.Lerp(transform.position.y, EndValue, Time.deltaTime * speed), transform.position.z);

                break;
            case "RotationX":
                if (Mathf.Abs(transform.eulerAngles.y - EndValue) > 0.01f)
                    transform.rotation = Quaternion.LookRotation(new Vector3(Mathf.Lerp(transform.eulerAngles.y, EndValue, Time.deltaTime * speed), 0, 0));

                break;
            case "enemyRotationX":
                if (Mathf.Abs(transform.eulerAngles.y - EndValue) > 0.01f)
                    transform.rotation = Quaternion.LookRotation(new Vector3(Mathf.Lerp(transform.eulerAngles.y, EndValue, Time.deltaTime * speed), 0, 0));
                else if (!transform.GetComponent<enemyRebuild>().active)
                    transform.GetComponent<enemyRebuild>().active = true;

                break;
        }
    }
}
