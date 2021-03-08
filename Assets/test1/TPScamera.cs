using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TPScamera : MonoBehaviour
{
    [SerializeField]
    private bool VerticallyInverted = false;
    [Range(0f, 10f)]
    public float SensitivityX = 1.0f;
    [Range(0f, 10f)]
    public float SensitivityY = 1.0f;
    [Range(-0.999f, -0.5f)]
    public float minYAngle = -0.5f;
    [Range(0.5f, 0.999f)]
    public float maxYAngle = 0.5f;

    void Update()
    {
        //マウスのX,Y軸がどれほど移動したかを取得します
        float X_Rotation = Input.GetAxis("Mouse X");
        float Y_Rotation = VerticallyInverted ? Input.GetAxis("Mouse Y") : -Input.GetAxis("Mouse Y");
        //設定感度の倍率掛ける
        X_Rotation *= SensitivityX;
        Y_Rotation *= SensitivityY;
        //Y軸を更新します（キャラクターを回転）取得したX軸の変更をキャラクターのY軸に反映します
        transform.Rotate(0, X_Rotation, 0);

        //次はY軸の設定です。
        float nowAngle = transform.localRotation.x;
        //最大値、または最小値を超えた場合、カメラをそれ以上動かない用にしています。
        //カメラが一回転しないようにするのを防ぎます。
        if (-Y_Rotation != 0)
        {
            if (0 < Y_Rotation)
            {
                if (minYAngle <= nowAngle)
                {
                    transform.Rotate(Y_Rotation, 0, 0);
                }
            }
            else
            {
                if (nowAngle <= maxYAngle)
                {
                    transform.Rotate(Y_Rotation, 0, 0);
                }
            }
        }
        //操作していると、Z軸がだんだん動いていくので、0に設定してください。
        transform.eulerAngles = new Vector3(this.transform.rotation.eulerAngles.x, this.transform.rotation.eulerAngles.y, 0f);
    }
}
