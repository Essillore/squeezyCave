using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class WaterShapeController : MonoBehaviour
{
    [SerializeField]
    private float springStiffness = 0.1f;
    [SerializeField]
    private List<WaterSpring> springs = new();
    [SerializeField]
    private float dampening = 0.03f;
    public float spread = 0.006f;
    private int CorsnersCount = 2;
    [SerializeField]
    private SpriteShapeController spriteShapeController;
    [SerializeField]
    private int WavesCount = 5;
    private void SetWaves()
    {
        Spline waterSpline = spriteShapeController.spline;
        int waterPointsCount = waterSpline.GetPointCount();
        //remove middle points for the waves
        //keep only the corners
        //removing 1 point at ta time we can remove only the 1st point
        //this means every time we remove 1st point the 2nd point becomes first
        for (int i = CorsnersCount; i < waterPointsCount - CorsnersCount; i++)
        {
            waterSpline.RemovePointAt(CorsnersCount);
        }
        Vector3 waterTopLeftCorner = waterSpline.GetPosition(1);
        Vector3 waterTopRightCorner = waterSpline.GetPosition(2);
        float waterWidth = waterTopRightCorner.x - waterTopLeftCorner.x;

        float spacingPerWave = waterWidth / (WavesCount + 1);

        for (int i = WavesCount; i > 0; i--)
        {
            int index = CorsnersCount;

            float xPosition = waterTopLeftCorner.x + (spacingPerWave * 1);
            Vector3 wavePoint = new Vector3(xPosition, waterTopLeftCorner.y, waterTopLeftCorner.z);
            waterSpline.InsertPointAt(index, wavePoint);
            waterSpline.SetHeight(index, 0.1f);
            waterSpline.SetCorner(index, false);
        }
    }


    private void FixedUpdate()
    {
        foreach(WaterSpring waterSpringComponent in springs)
        {
            waterSpringComponent.WaveSpringUpdate(springStiffness, dampening);
        }
        UpdateSprings();
    }

    private void UpdateSprings()
    {
        int count = springs.Count;
        float[] left_deltas = new float[count];
        float[] right_deltas = new float[count];
        for(int i = 0; i < count; i++)
        {
            if (i > 0)
            {

            left_deltas[i] = spread * (springs[i].height - springs[i - 1].height);
            springs[i - 1].velocity += left_deltas[i];
            }
            if (i < springs.Count-1)
            {
                right_deltas[i] = spread * (springs[i].height - springs[i + 1].height);
                springs[i + 1].velocity += right_deltas[i];
            }
        }
    }
    private void Splash(int index, float speed)
    {
        if (index>= 0 && index < springs.Count)
        {
            springs[index].velocity += speed;
        }
    }
}
