using UnityEngine;
using UnityEngine.UI;

public static class ExtensionMethods
{

    /// <summary>
    /// Match another Game Object's Position to this Game Object's Position.
    /// </summary>
    /// <param name="transform">The Transform Component</param>
    /// <param name="gameObject">The game object the transform should match the position of.</param>
    public static void MatchPosition(this Transform transform, GameObject gameObject)
    {
        transform.position = gameObject.transform.position;
    }

    /// <summary>
    /// Set the transform's position to a specified offset.
    /// </summary>
    /// <param name="transform">Transform that will be offset.</param>
    /// <param name="offset">Coordinates to offset the transforms position.</param>
    public static void OffsetPosition(this Transform transform, Vector3 offset)
    {
        transform.position += offset;
    }

    public static void MoveRight(this Transform transform, float speed)
    {
        transform.Translate(Vector3.right * (speed * Time.deltaTime));
    }

    public static void MoveLeft(this Transform transform, float speed)
    {
        transform.Translate(Vector3.left * (speed * Time.deltaTime));
    }

    public static void AddImpulseForce(this Rigidbody2D rigidbody2D, Vector3 force)
    {
        rigidbody2D.AddForce(force, ForceMode2D.Impulse);
    }

    public static void ResetImageFill(this Image image)
    {
        image.fillAmount = 1.0f;
    }

    public static void SetFill(this Image image, float amount, float max)
    {
        image.fillAmount = amount / max;
    }
}