using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class InverseMatrixFunc
{
    //来源于render.pipelines.core源码
    /*
    real3 TransformWorldToTangent(real3 dirWS, real3x3 tangentToWorld)
    {
        // Note matrix is in row major convention with left multiplication as it is build on the fly
        float3 row0 = tangentToWorld[0];
        float3 row1 = tangentToWorld[1];
        float3 row2 = tangentToWorld[2];

        // these are the columns of the inverse matrix but scaled by the determinant
        float3 col0 = cross(row1, row2);
        float3 col1 = cross(row2, row0);
        float3 col2 = cross(row0, row1);

        float determinant = dot(row0, col0);
        float sgn = determinant<0.0 ? (-1.0) : 1.0;

        // inverse transposed but scaled by determinant
        real3x3 matTBN_I_T = real3x3(col0, col1, col2);

        // remove transpose part by using matrix as the first arg in mul()
        // this makes it the exact inverse of what TransformTangentToWorld() does.
        return SafeNormalize( sgn * mul(matTBN_I_T, dirWS) );
    }
    */
    
    
}
