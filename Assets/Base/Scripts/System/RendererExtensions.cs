using UnityEngine;
using System.Collections.Generic;

public static class RendererExtensions
{
    private static bool _IsVisibleFrom(Bounds _Bounds, Camera _Cam)
    {
        Plane[] planes = GeometryUtility.CalculateFrustumPlanes(_Cam);

        return (GeometryUtility.TestPlanesAABB(planes, _Bounds));
    }

    public static bool IsVisibleFrom(this Renderer _Renderer, Camera _Cam)
    {
        return (_IsVisibleFrom(_Renderer.bounds, _Cam));
    }
}
