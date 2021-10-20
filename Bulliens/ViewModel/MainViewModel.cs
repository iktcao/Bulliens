using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using Microsoft.Win32;
using Boiler.Helper;
using Boiler.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Reflection;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows;

namespace Boiler.ViewModel
{

    [MetadataType(typeof(MainViewModel))]

    public class MainViewModel : ViewModelBase, IDataErrorInfo
    {
        #region 构造函数
        public MainViewModel()
        {
            if (IsInDesignMode)
            {

            }
            else
            {
                InitWaterCycle();
            }
        }

        private void InitWaterCycle()
        {
            WaterCycle wc = new WaterCycle();
            this.WaterCycle = wc;
        }

        private string currentFileName;
        #endregion

        #region 属性
        // 判断程序是否在校核模式下运行
        private bool _isRating = false;
        public bool IsRating
        {
            get { return _isRating; }
            set
            {
                _isRating = value;
                this.RaisePropertyChanged("IsRating");
            }
        }

        // 判断程序是否在设计模式下运行
        public bool IsDesign
        {
            get { return !_isRating; }
            set
            {
                _isRating = !value;
                this.RaisePropertyChanged("IsRating");
            }
        }

        // WaterCycle属性
        private WaterCycle _waterCycle;

        public WaterCycle WaterCycle
        {
            get { return _waterCycle; }
            set
            {
                _waterCycle = value;
                this.RaisePropertyChanged("WaterCycle");
            }
        }

        #region 代理属性
        // 饱和蒸汽压，MPa(G)
        public double SatPress
        {
            get { return WaterCycle.SatPress; }
            set
            {
                WaterCycle.SatPress = value;
                this.RaisePropertyChanged("SatPress");
            }
        }

        // 饱和温度，℃
        public double SatTemp
        {
            get { return WaterCycle.SatTemp; }
            set
            {
                WaterCycle.SatTemp = value;
                this.RaisePropertyChanged("SatTemp");
            }
        }

        // 饱和水的密度，kg/m3
        public double WaterDensity
        {
            get { return WaterCycle.WaterDensity; }
            set
            {
                WaterCycle.WaterDensity = value;
                this.RaisePropertyChanged("WaterDensity");
            }
        }

        // 饱和蒸汽的密度，kg/m3
        public double SteamDensity
        {
            get { return WaterCycle.SteamDensity; }
            set
            {
                WaterCycle.SteamDensity = value;
                this.RaisePropertyChanged("SteamDensity");
            }
        }

        // 饱和水的粘度，cP
        public double WaterViscosity
        {
            get { return WaterCycle.WaterViscosity * 1000; }
            set
            {
                WaterCycle.WaterViscosity = value / 1000;
                this.RaisePropertyChanged("WaterViscosity");
            }
        }

        // 饱和蒸汽的粘度，cP
        public double SteamViscosity
        {
            get { return WaterCycle.SteamViscosity * 1000; }
            set
            {
                WaterCycle.SteamViscosity = value / 1000;
                this.RaisePropertyChanged("SteamViscosity");
            }
        }

        // 饱和水、汽的特性参数，无量纲
        public double SatPara
        {
            get { return WaterCycle.SatPara; }
            set
            {
                WaterCycle.SatPara = value;
                this.RaisePropertyChanged("SatPara");
            }
        }

        // 蒸汽发生量，kg/h
        public double SteamProduction
        {
            get { return WaterCycle.SteamProduction; }
            set
            {
                WaterCycle.SteamProduction = value;
                this.RaisePropertyChanged("SteamProduction");
            }
        }

        // 上升管公称直径，mm
        public double UpPipeDN
        {
            get { return WaterCycle.UpPipeDN; }
            set
            {
                WaterCycle.UpPipeDN = value;
                this.RaisePropertyChanged("UpPipeDN");
            }
        }

        // 上升管根数，无量纲
        public double UpPipeCount
        {
            get { return WaterCycle.UpPipeCount; }
            set
            {
                WaterCycle.UpPipeCount = value;
                this.RaisePropertyChanged("UpPipeCount");
            }
        }

        // 上升管实际管内径，mm
        public double UpPipeID
        {
            get { return WaterCycle.UpPipeID * 1000; }
            set
            {
                WaterCycle.UpPipeID = value / 1000;
                this.RaisePropertyChanged("UpPipeID");
            }
        }

        // 上升管截面积，m2
        public double UpPipeArea
        {
            get { return WaterCycle.UpPipeArea; }
            set
            {
                WaterCycle.UpPipeArea = value;
                this.RaisePropertyChanged("UpPipeArea");
            }
        }

        // 上升管与下降管截面积比值，无量纲
        public double PipeAreaRatio
        {
            get { return WaterCycle.PipeAreaRatio; }
            set
            {
                WaterCycle.PipeAreaRatio = value;
                this.RaisePropertyChanged("PipeAreaRatio");
            }
        }

        // 下降管公称直径，mm
        public double DownPipeDN
        {
            get { return WaterCycle.DownPipeDN; }
            set
            {
                WaterCycle.DownPipeDN = value;
                this.RaisePropertyChanged("DownPipeDN");
            }
        }

        // 下降管根数，无量纲
        public double DownPipeCount
        {
            get { return WaterCycle.DownPipeCount; }
            set
            {
                WaterCycle.DownPipeCount = value;
                this.RaisePropertyChanged("DownPipeCount");
            }
        }

        // 下降管实际管内径，mm
        public double DownPipeID
        {
            get { return WaterCycle.DownPipeID * 1000; }
            set
            {
                WaterCycle.DownPipeID = value / 1000;
                this.RaisePropertyChanged("DownPipeID");
            }
        }

        // 下降管截面积，m2
        public double DownPipeArea
        {
            get { return WaterCycle.DownPipeArea; }
            set
            {
                WaterCycle.DownPipeArea = value;
                this.RaisePropertyChanged("DownPipeArea");
            }
        }

        // 汽包与换热器中心线的高度差，m
        public double Height
        {
            get { return WaterCycle.Height; }
            set
            {
                WaterCycle.Height = value;
                this.RaisePropertyChanged("Height");
            }
        }

        // 管内壁绝对粗糙度，m
        public double Roughness
        {
            get { return WaterCycle.Roughness * 1000; }
            set
            {
                WaterCycle.Roughness = value / 1000;
                this.RaisePropertyChanged("Roughness");
            }
        }

        // 上升管水平直管段长度，m
        public double UpHorizontalPipeLength
        {
            get { return WaterCycle.UpHorizontalPipeLength; }
            set
            {
                WaterCycle.UpHorizontalPipeLength = value;
                this.RaisePropertyChanged("UpHorizontalPipeLength");
            }
        }

        // 上升管总当量长度L/D，无量纲
        public double UpToltalFittingLD
        {
            get { return WaterCycle.UpToltalFittingLD; }
            set
            {
                WaterCycle.UpToltalFittingLD = value;
                this.RaisePropertyChanged("UpToltalFittingLD");
            }
        }

        // 上升管总局部阻力系数K，无量纲
        public double UpToltalFittingK
        {
            get { return WaterCycle.UpToltalFittingK; }
            set
            {
                WaterCycle.UpToltalFittingK = value;
                this.RaisePropertyChanged("UpToltalFittingK");
            }
        }

        // 下降管水平直管段长度，m
        public double DownHorizontalPipeLength
        {
            get { return WaterCycle.DownHorizontalPipeLength; }
            set
            {
                WaterCycle.DownHorizontalPipeLength = value;
                this.RaisePropertyChanged("DownHorizontalPipeLength");
            }
        }

        // 下降管总当量长度L/D，无量纲
        public double DownToltalFittingLD
        {
            get { return WaterCycle.DownToltalFittingLD; }
            set
            {
                WaterCycle.DownToltalFittingLD = value;
                this.RaisePropertyChanged("DownToltalFittingLD");
            }
        }

        // 下降管总局部阻力系数K，无量纲
        public double DownToltalFittingK
        {
            get { return WaterCycle.DownToltalFittingK; }
            set
            {
                WaterCycle.DownToltalFittingK = value;
                this.RaisePropertyChanged("DownToltalFittingK");
            }
        }

        // 循环倍率K，无量纲
        public double CycleRatio
        {
            get { return WaterCycle.CycleRatio; }
            set
            {
                WaterCycle.CycleRatio = value;
                this.RaisePropertyChanged("CycleRatio");
            }
        }

        // 循环流速Wo，m/s
        public double CycleVelocity
        {
            get { return WaterCycle.CycleVelocity; }
            set
            {
                WaterCycle.CycleVelocity = value;
                this.RaisePropertyChanged("CycleVelocity");
            }
        }

        // 上升管两相流流型
        public string FlowRegime
        {
            get { return WaterCycle.FlowRegime; }
            set
            {
                WaterCycle.FlowRegime = value;
                this.RaisePropertyChanged("FlowRegime");
            }
        }
        #endregion 代理属性

        #endregion

        #region 命令
        // 命令：运行计算模式切换
        private RelayCommand _changePattern;

        public RelayCommand ChangePattern
        {
            get
            {
                if (_changePattern == null)
                    _changePattern = new RelayCommand(() => ExcuteChangePattern());
                return _changePattern;
            }
            set { _changePattern = value; }
        }

        private void ExcuteChangePattern()
        {
            RaiseAllPropertyChanged();
        }

        // 辅助方法：强制ViewModle所有属性通知变化
        private void RaiseAllPropertyChanged()
        {
            var props = WaterCycle.GetType().GetProperties();
            foreach (PropertyInfo propertyInfo in props)
            {
                object propVal = propertyInfo.GetValue(WaterCycle, null);
                string propName = propertyInfo.Name;
                if (propVal.GetType() == typeof(double))
                {
                    RaisePropertyChanged(propName);
                }
                RaisePropertyChanged("FlowRegime");
            }
        }

        // 命令：运行计算
        private RelayCommand _runCmd;

        public RelayCommand RunCmd
        {
            get
            {
                if (_runCmd == null) return new RelayCommand(() => ExcuteRunCmd());
                return _runCmd;
            }
            set { _runCmd = value; }
        }

        private void ExcuteRunCmd()
        {
            if (IsDesign)
            {
                WaterCycle.InitPhysicalProperty();
                WaterCycle.InitPipeInfo();
            }
            else
            {
                WaterCycle.InitPhysicalProperty();
                WaterCycle.InitPipeArea();
            }

            WaterCycle.CalcCycleRatio();

            RaiseAllPropertyChanged();
        }

        // 命令：打开
        private RelayCommand _openCmd;

        public RelayCommand OpenCmd
        {
            get
            {
                if (_openCmd == null)
                    _openCmd = new RelayCommand(() => ExcuteOpenCmd());
                return _openCmd;
            }
            set { _openCmd = value; }
        }

        private void ExcuteOpenCmd()
        {
            OpenFileDialog dialog = new OpenFileDialog()
            {
                Title = "打开",
                Filter = "蒸汽发生器计算书(*.tsg)|*.tsg"
            };

            if (dialog.ShowDialog() == true)
            {
                try
                {
                    string fileName = dialog.FileName;
                    using (FileStream fileStream = new FileStream(fileName, FileMode.Open))
                    {
                        BinaryFormatter formatter = new BinaryFormatter();
                        this.WaterCycle = (WaterCycle)formatter.Deserialize(fileStream);
                        this.IsRating = (bool)formatter.Deserialize(fileStream);
                        fileStream.Close();
                    }
                    currentFileName = fileName;
                    RaiseAllPropertyChanged();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        // 命令：保存
        private RelayCommand _saveCmd;

        public RelayCommand SaveCmd
        {
            get
            {
                if (_saveCmd == null)
                    _saveCmd = new RelayCommand(() => ExcuteSaveCmd());
                return _saveCmd;
            }
            set { _saveCmd = value; }
        }

        private void ExcuteSaveCmd()
        {
            try
            {
                string fileName = currentFileName;
                if (System.IO.File.Exists(fileName))
                {
                    System.IO.File.Delete(fileName);
                    ArrayList arrayList = new ArrayList() {
                        this.WaterCycle,this.IsRating };
                    SerializeHelper.BinarySerialize(fileName, arrayList);
                }
                else
                {
                    ExcuteSaveAsCmd();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        // 命令：另存为
        private RelayCommand _saveAsCmd;

        public RelayCommand SaveAsCmd
        {
            get
            {
                if (_saveAsCmd == null)
                    _saveAsCmd = new RelayCommand(() => ExcuteSaveAsCmd());
                return _saveAsCmd;
            }
            set { _saveAsCmd = value; }
        }

        private void ExcuteSaveAsCmd()
        {
            SaveFileDialog dialog = new SaveFileDialog()
            {
                Title = "另存为",
                FileName = "新建蒸汽发生器计算书",
                Filter = "蒸汽发生器计算书(*.tsg)|*.tsg"
            };

            if (dialog.ShowDialog() == true)
            {
                try
                {
                    string fileName = dialog.FileName;
                    ArrayList arrayList = new ArrayList() {
                        this.WaterCycle,this.IsRating };
                    SerializeHelper.BinarySerialize(fileName, arrayList);
                    currentFileName = fileName;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        #endregion

        #region 数据验证

        // 字典字段
        private Dictionary<string, string> dataErrors = new Dictionary<string, string>();

        // 属性：判断是否通过验证
        //private bool _isValid;
        //public bool IsValid
        //{
        //    get 
        //    {
        //        if (dataErrors != null && dataErrors.Count > 0)
        //            _isValid= false;
        //        else
        //            _isValid =true;
        //        return _isValid;
        //    }
        //    set
        //    {
        //        _isValid = value;
        //        this.RaisePropertyChanged("IsValid");
        //    }
        //}
        public bool IsValid
        {
            get
            {
                if (dataErrors != null && dataErrors.Count > 0)
                    return false;
                return true;
            }
        }

        // IDataErrorInfo继承方法:用于指示整个对象的错误
        public string Error { get => null; }

        // IDataErrorInfo继承方法:用于指示单个属性级别的错误

        public string this[string columnName]
        {
            get
            {
                if (IsDesign)
                {
                    double dbl = double.Parse(this.GetType().GetProperty(columnName).GetValue(this).ToString());
                    bool required = (columnName == "SatPress" || columnName == "SteamProduction" ||
                        columnName == "Height" || columnName == "Roughness");
                    if (required && dbl == 0)
                    {
                        if (!dataErrors.ContainsKey(columnName))
                            AddDic(dataErrors, columnName);
                        return "输入数据不完整";
                    }
                    RemoveDic(dataErrors, columnName);
                    return null;
                }
                else
                {
                    double dbl = double.Parse(this.GetType().GetProperty(columnName).GetValue(this).ToString());
                    bool required = (columnName == "SatPress" || columnName == "SteamProduction" ||
                        columnName == "UpPipeCount" || columnName == "DownPipeCount" ||
                        columnName == "UpPipeID" || columnName == "DownPipeID" ||
                        columnName == "Height" || columnName == "Roughness");
                    if (required && dbl == 0)
                    {
                        if (!dataErrors.ContainsKey(columnName))
                            AddDic(dataErrors, columnName);
                        return "输入数据不完整";
                    }
                    RemoveDic(dataErrors, columnName);
                    return null;
                }
            }
        }

        // 字典添加键值对的方法
        private void AddDic(Dictionary<string, string> dic, string key)
        {
            if (!dic.ContainsKey(key))
            {
                dic.Add(key, "");
                RaisePropertyChanged("IsValid");
            }
        }

        // 字典移除键值对的方法
        private void RemoveDic(Dictionary<string, string> dic, string key)
        {
            dic.Remove(key);
            RaisePropertyChanged("IsValid");
        }
        #endregion
    }
}