using Boiler.Helper;
using System;
using System.Collections.Generic;

namespace Boiler.Models
{
    [Serializable]
    public class WaterCycle
    {
        // IF97水蒸汽物性参数枚举
        private enum AquaProps
        {
            Pressure = 0,
            Temperature = 1,
            Density = 2,
            SpecificVolume = 3,
            Enthalpy = 4,
            Entropy = 5,
            Exergy = 6,
            InternalEnergy = 7,
            IsobaricHeatCapacity = 8,
            IsochoricHeatCapacity = 9,
            Sound = 10,
            IsentropicExponent = 11,
            Helmholtz = 12,
            Gibbs = 13,
            CompressibilityFactor = 14,
            VaporFraction = 15,
            Region = 16,
            ExpansionCoefficient = 17,
            IsothermalCompressibility = 18,
            dVdT = 19,
            dVdP = 20,
            dPdT = 21,
            IsothermalJ_TCoefficient = 22,
            J_TCoefficient = 23,
            DynamicViscosity = 24,
            KinematicViscosity = 25,
            ThermalConductivity = 26,
            ThermalDiffusivity = 27,
            Prandtl = 28,
            SurfaceTension = 29,
        }

        // 构造函数
        public WaterCycle()
        {

        }

        #region 物性参数

        public double SatPress { get; set; }                    // 饱和蒸汽压，MPa(G)

        public double SatTemp { get; set; }                     // 饱和温度，℃

        public double WaterDensity { get; set; }                // 饱和水的密度，kg/m3

        public double SteamDensity { get; set; }                // 饱和蒸汽的密度，kg/m3

        public double WaterViscosity { get; set; }              // 饱和水的粘度，kg/(m*s)

        public double SteamViscosity { get; set; }              // 饱和蒸汽的粘度，kg/(m*s)

        public double SatPara { get; set; }                     // 饱和水、汽的特性参数，无量纲

        // 根据饱和压力初始化物性
        public void InitPhysicalProperty()
        {
            double P = PressConverter(SatPress);

            this.SatTemp = IF97.PX(P, 1, (int)AquaProps.Temperature);
            this.WaterDensity = IF97.PX(P, 0, (int)AquaProps.Density);
            this.SteamDensity = IF97.PX(P, 1, (int)AquaProps.Density);
            this.WaterViscosity = IF97.PX(P, 0, (int)AquaProps.DynamicViscosity);
            this.SteamViscosity = IF97.PX(P, 1, (int)AquaProps.DynamicViscosity);
            this.SatPara = CalcSatPara();
        }

        // 饱和水、汽的特性参数，无量纲
        private double CalcSatPara()
        {
            double psi = Math.Pow((SteamDensity / WaterDensity), 0.5)
            * Math.Pow((WaterViscosity / SteamViscosity), 0.1);
            return psi;
        }

        // 压力单位转换：MPaG => MPaA
        private double PressConverter(double p)
        {
            return p + 101.325 / 1000;
        }
        #endregion

        #region 管子结构参数


        public double SteamProduction { get; set; }             // 蒸汽发生量，kg/h

        public double UpPipeDN { get; set; }                    // 上升管公称直径，mm

        public double UpPipeCount { get; set; }                 // 上升管根数，无量纲

        public double UpPipeID { get; set; }                    // 上升管实际管内径，m

        public double UpPipeArea { get; set; }                  // 上升管截面积，m2

        public double PipeAreaRatio { get; set; }               // 上升管与下降管截面积比值，无量纲

        public double DownPipeDN { get; set; }                  // 下降管公称直径，mm

        public double DownPipeCount { get; set; }               // 下降管根数，无量纲

        public double DownPipeID { get; set; }                  // 下降管实际管内径，m

        public double DownPipeArea { get; set; }                // 下降管截面积，m2

        // 根据蒸汽发生量初始化管子信息
        public void InitPipeInfo()
        {
            CalcUpPipeInfo();
            CalcDownPipeInfo();
        }

        // 根据给定管子结构参数计算管子截面积，m2
        public void InitPipeArea()
        {
            this.UpPipeArea = Math.PI * UpPipeID * UpPipeID * UpPipeCount / 4;
            this.DownPipeArea = Math.PI * DownPipeID * DownPipeID * DownPipeCount / 4;
            this.PipeAreaRatio = UpPipeArea / DownPipeArea;
        }

        // 计算上升管信息
        private void CalcUpPipeInfo()
        {
            // 计算上升管截面积的初值，m2
            double D = SteamProduction;
            double f_g = D / (2.55E6 * SatPara);
            double[] UpPipeInfo = PipeInfo.SelectPipe(f_g);
            this.UpPipeDN = UpPipeInfo[0];
            this.UpPipeCount = UpPipeInfo[1];
            this.UpPipeID = UpPipeInfo[2];
            this.UpPipeArea = UpPipeInfo[3];

        }

        // 计算下降管信息，返回管子信息数组
        private void CalcDownPipeInfo()
        {
            double beta = 1.5;
            double f_x = UpPipeArea / beta;
            double[] DownPipeInfo = PipeInfo.SelectPipe(f_x);
            this.DownPipeDN = DownPipeInfo[0];
            this.DownPipeCount = DownPipeInfo[1];
            this.DownPipeID = DownPipeInfo[2];
            this.DownPipeArea = DownPipeInfo[3];
            this.PipeAreaRatio = UpPipeArea / DownPipeInfo[3];
        }
        #endregion

        #region 循环倍率

        public double CycleRatio { get; set; }                  // 循环倍率K，无量纲

        public double Height { get; set; }                      // 汽包与换热器中心线的高度差，m

        public double Roughness { get; set; } = 0.00005;      // 管内壁绝对粗糙度，m

        public double UpHorizontalPipeLength { get; set; }      // 上升管水平直管段长度，m

        public double UpToltalFittingLD { get; set; }           // 上升管总当量长度L/D，无量纲

        public double UpToltalFittingK { get; set; }            // 上升管总局部阻力系数K，无量纲

        public double DownHorizontalPipeLength { get; set; }    // 下降管水平直管段长度，m

        public double DownToltalFittingLD { get; set; }         // 下降管总当量长度L/D，无量纲

        public double DownToltalFittingK { get; set; }          // 下降管总局部阻力系数K，无量纲

        public double CycleVelocity { get; set; }               // 循环流速，m/s

        public string FlowRegime { get; set; } = string.Empty;                 // 上升管两相流流型

        // 计算指定高差下的循环倍率
        public void CalcCycleRatio()
        {
            double H = Height;
            this.CycleRatio = SolveK(H);
        }

        // 计算适宜循环倍率下的高差范围
        public void CalcHeightRange()
        {
            Dictionary<double, double> heightDic = new Dictionary<double, double>();
            double H = 0.5;
            for (int i = 0; i < 100; i++)
            {
                double K = SolveK(H);
                if (K < 10)
                    H = H + 0.5;
                else if (K <= 30)
                {
                    heightDic.Add(H, K);
                    H = H + 0.5;
                }
                else
                {
                    break;
                }
            }
            foreach (KeyValuePair<double, double> kvp in heightDic)
            {
                Console.WriteLine("Key = {0}, Value = {1}", kvp.Key, kvp.Value);
            }
        }

        // 根据高差迭代求解循环倍率
        private double SolveK(double H)
        {
            // 循环倍率给初值
            double K;
            if (SatPress < 20)
                K = 20;
            else
                K = 15;

            // 循环标记
            bool flag;
            // 迭代步长初值
            double step = 1;
            // 记录上一次的误差值
            double lastError = 0;

            double W_o;                         // 循环流速，m/s
            double gamma_s = WaterDensity;      // 水相密度，kg/m3
            double X_tt;                        // 两相流动特性参数，无量纲

            do
            {
                // 循环流速，m/s
                double D = SteamProduction;
                double f_g = UpPipeArea;
                W_o = K * D / (3600 * f_g * gamma_s);

                // 下降管内水流速，m/s
                double beta = PipeAreaRatio;
                double W_x = W_o * beta;

                // 上升管内水相流速，m/s
                double W_s = W_o * (K - 1) / K;

                // 上升管雷诺数，无量纲
                double D_g = UpPipeID;
                double mu_s = WaterViscosity;
                double Re_g = Flow.CalcReynolds(D_g, gamma_s, W_s, mu_s);

                // 下降管雷诺数，无量纲
                double D_x = DownPipeID;
                double Re_x = Flow.CalcReynolds(D_x, gamma_s, W_x, mu_s);

                // 上升管的摩擦系数，无量纲
                double epsilon = Roughness;
                double L_mg = Flow.CalcFrictionFactor(Re_g, epsilon, D_g);

                // 下降管的摩擦系数，无量纲
                double L_mx = Flow.CalcFrictionFactor(Re_x, epsilon, D_x);

                // 两相流动特性参数，无量纲
                double psi = SatPara;
                X_tt = Math.Pow(K - 1, 0.9) * psi;

                // 容积含汽量，无量纲
                double R_s = 0.07754 + 0.1612 * X_tt - 0.02203 * X_tt * X_tt;

                // 两相摩擦校正因数，无量纲
                double phi2 = Math.Pow(1 / R_s, 1.95);

                // 下降管的阻力，kg/m2		
                double DP_x = Flow.CalcDP(L_mx, DownHorizontalPipeLength, H,
                                DownToltalFittingLD, DownToltalFittingK, D_x, W_x, gamma_s);

                // 上升管的阻力，kg/m2
                double DP_g = Flow.CalcDP(L_mg, UpHorizontalPipeLength, H,
                                UpToltalFittingLD, UpToltalFittingK, D_g, W_s, gamma_s) * phi2;

                // 总阻力，kg/m2
                double DP = DP_x + DP_g;

                // 推动力，kg/m2
                double gamma_q = SteamDensity;
                double gamma_qs = gamma_s * R_s + gamma_q * (1 - R_s);
                double P_t = H * (gamma_s - gamma_qs);

                // 计算误差
                double error = DP - P_t;

                // 误差变号时令步长减半
                if (lastError != 0 && lastError * error < 0)
                    step = step / 2;
                lastError = error;

                if (error < -100)
                {
                    K = K + step;
                    flag = true;
                }

                else if (error > 100)
                {
                    K = K - step;
                    flag = true;
                }

                else
                    flag = false;

            } while (flag);

            this.CycleVelocity = W_o;

            this.FlowRegime = SolveRegime(X_tt, W_o, gamma_s);

            return K;
        }

        // 上升管两相流流型判断函数
        private string SolveRegime(double X_tt, double W_o, double gamma_s)
        {
            double U = Math.Log(1 / X_tt);
            double V = Math.Log(W_o * gamma_s);
            if (V < GetY(1.01, 7.86, U) && V > GetY(1.25, 6.11, U))
            {
                return "环状流";
            }
            else if (V <= GetY(1.25, 6.11, U) && V >= GetY(1.38, 4.29, U))
            {
                return "柱状流";
            }
            else if (V < GetY(1.38, 4.29, U))
            {
                return "泡状流";
            }
            else if (V >= GetY(1.01, 7.86, U))
            {
                return "雾状流";
            }
            else
            {
                return "";
            }

        }

        // 上升管两相流流型判断边界线辅助函数
        private double GetY(double A, double B, double X)
        {
            return B - A * X;
        }

        #endregion

    }
}