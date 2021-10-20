using System;

namespace Boiler.Models
{
    public class PipeInfo
    {
        private static double[][] table = new double[][]
        {
            new double[]{40,1,0.039,0.0012},
            new double[]{50,1,0.051,0.00205},
            new double[]{80,1,0.082,0.0053},
            new double[]{100,1,0.100,0.00785},
            new double[]{150,1,0.149,0.0174},
            new double[]{200,1,0.207,0.0337},
            new double[]{250,1,0.259,0.0527},
            new double[]{300,1,0.309,0.075},
            new double[]{250,2,0.259,0.1054},
            new double[]{300,2,0.309,0.150},
        };
        public static double[] SelectPipe(double area)
        {
            for (int i = 0; i < table.Length; i++)
            {
                double smaller = table[i][3];
                double bigger = table[i + 1][3];
                if (area < smaller)
                    return table[i];
                else if (area >= smaller && area <= bigger)
                {
                    if (area > smaller + 0.4 * (bigger - smaller))
                        return table[i + 1];
                    else
                        return table[i];
                }
                else
                {
                    if (i >= 8)
                        break;
                }
            }
            throw new NotImplementedException("截面积过大");
        }
    }
}
