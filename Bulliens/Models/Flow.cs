using System;

namespace Boiler.Models
{
    public class Flow
    {
        // 计算管道雷诺数
        // 参数为管道内径di（m）、流体的密度（kg/m3）、流速(m/s), 流体的运动粘度u（Pa*s）
        public static double CalcReynolds(double di, double rho, double vel, double u)
        {
            return rho * di * vel / u;
        }

        // 计算流动的摩擦因子Friction Factor
        // 参数为流体的雷诺数Re、管壁绝对粗糙度（m）、管道内径di（m）
        public static double CalcFrictionFactor(double Re, double epsilon, double di)
        {
            double f;
            if (Re < 2000)
                f = 64 / Re;
            else if (Re <= 4000)
                f = 0.3164 * (Math.Pow(Re, -0.25));
            else
            {
                double A, B, C;
                A = -2 * Math.Log10((epsilon / (3.7 * di)) + (12 / Re));
                B = -2 * Math.Log10((epsilon / (3.7 * di)) + (2.51 * A / Re));
                C = -2 * Math.Log10((epsilon / (3.7 * di)) + (2.51 * B / Re));
                f = Math.Pow((A - ((B - A) * (B - A) / (C - 2 * B + A))), -2);
            }
            return f;
        }

        // 计算管道阻力(压降与密度的乘积），kg/m2
        public static double CalcDP(double f, double HL, double VL, double Le, double K,
                                double di, double v, double rho)
        {
            double hL = (f * ((HL + VL) / di + Le) + K) * v * v / 2 / 9.81 * rho;
            return hL;
        }
    }
}
