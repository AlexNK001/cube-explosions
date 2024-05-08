using UnityEngine;

public class ColorChanging : PullUser
{
    [SerializeField] private Color _startingColor = Color.green;
    [SerializeField] private Color _contactColor = Color.blue;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.TryGetComponent(out Cube cube))
        {
            cube.SetColor(_contactColor);
        }
    }

    private void GiveColorResultingCube(Cube cube)
    {
        cube.SetColor(_startingColor);
    }

    protected override void Subscribe()
    {
        _cubePool.CubeIssued += GiveColorResultingCube;
    }

    protected override void Unsubscribe()
    {
        _cubePool.CubeIssued -= GiveColorResultingCube;
    }
}