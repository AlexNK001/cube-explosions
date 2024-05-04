using UnityEngine;

public class ColorHandler : PullUser
{
    [SerializeField] private Color _startingColor = Color.green;
    [SerializeField] private Color _contactColor = Color.blue;

    protected override void Subscribe()
    {
        _cubePool.CubeCreated += SubscribeContact;
        _cubePool.CubeIssued += GiveColorResultingCube;
        _cubePool.CubeDestroyed += UnsubscribeContact;
    }

    private void SubscribeContact(Cube cube)
    {
        cube.Contact+= GiveColorOnContact;
    }

    private void UnsubscribeContact(Cube cube)
    {
        cube.Contact -= GiveColorOnContact;
    }

    private void GiveColorOnContact(Cube cube)
    {
        cube.SetColor(_contactColor);
    }

    private void GiveColorResultingCube(Cube cube)
    {
        cube.SetColor(_startingColor);
    }

    protected override void Unsubscribe()
    {
        _cubePool.CubeCreated -= SubscribeContact;
        _cubePool.CubeIssued -= GiveColorResultingCube;
        _cubePool.CubeDestroyed -= UnsubscribeContact;
    }
}