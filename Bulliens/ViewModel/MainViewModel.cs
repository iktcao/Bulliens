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
        #region ���캯��
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

        #region ����
        // �жϳ����Ƿ���У��ģʽ������
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

        // �жϳ����Ƿ������ģʽ������
        public bool IsDesign
        {
            get { return !_isRating; }
            set
            {
                _isRating = !value;
                this.RaisePropertyChanged("IsRating");
            }
        }

        // WaterCycle����
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

        #region ��������
        // ��������ѹ��MPa(G)
        public double SatPress
        {
            get { return WaterCycle.SatPress; }
            set
            {
                WaterCycle.SatPress = value;
                this.RaisePropertyChanged("SatPress");
            }
        }

        // �����¶ȣ���
        public double SatTemp
        {
            get { return WaterCycle.SatTemp; }
            set
            {
                WaterCycle.SatTemp = value;
                this.RaisePropertyChanged("SatTemp");
            }
        }

        // ����ˮ���ܶȣ�kg/m3
        public double WaterDensity
        {
            get { return WaterCycle.WaterDensity; }
            set
            {
                WaterCycle.WaterDensity = value;
                this.RaisePropertyChanged("WaterDensity");
            }
        }

        // �����������ܶȣ�kg/m3
        public double SteamDensity
        {
            get { return WaterCycle.SteamDensity; }
            set
            {
                WaterCycle.SteamDensity = value;
                this.RaisePropertyChanged("SteamDensity");
            }
        }

        // ����ˮ��ճ�ȣ�cP
        public double WaterViscosity
        {
            get { return WaterCycle.WaterViscosity * 1000; }
            set
            {
                WaterCycle.WaterViscosity = value / 1000;
                this.RaisePropertyChanged("WaterViscosity");
            }
        }

        // ����������ճ�ȣ�cP
        public double SteamViscosity
        {
            get { return WaterCycle.SteamViscosity * 1000; }
            set
            {
                WaterCycle.SteamViscosity = value / 1000;
                this.RaisePropertyChanged("SteamViscosity");
            }
        }

        // ����ˮ���������Բ�����������
        public double SatPara
        {
            get { return WaterCycle.SatPara; }
            set
            {
                WaterCycle.SatPara = value;
                this.RaisePropertyChanged("SatPara");
            }
        }

        // ������������kg/h
        public double SteamProduction
        {
            get { return WaterCycle.SteamProduction; }
            set
            {
                WaterCycle.SteamProduction = value;
                this.RaisePropertyChanged("SteamProduction");
            }
        }

        // �����ܹ���ֱ����mm
        public double UpPipeDN
        {
            get { return WaterCycle.UpPipeDN; }
            set
            {
                WaterCycle.UpPipeDN = value;
                this.RaisePropertyChanged("UpPipeDN");
            }
        }

        // �����ܸ�����������
        public double UpPipeCount
        {
            get { return WaterCycle.UpPipeCount; }
            set
            {
                WaterCycle.UpPipeCount = value;
                this.RaisePropertyChanged("UpPipeCount");
            }
        }

        // ������ʵ�ʹ��ھ���mm
        public double UpPipeID
        {
            get { return WaterCycle.UpPipeID * 1000; }
            set
            {
                WaterCycle.UpPipeID = value / 1000;
                this.RaisePropertyChanged("UpPipeID");
            }
        }

        // �����ܽ������m2
        public double UpPipeArea
        {
            get { return WaterCycle.UpPipeArea; }
            set
            {
                WaterCycle.UpPipeArea = value;
                this.RaisePropertyChanged("UpPipeArea");
            }
        }

        // ���������½��ܽ������ֵ��������
        public double PipeAreaRatio
        {
            get { return WaterCycle.PipeAreaRatio; }
            set
            {
                WaterCycle.PipeAreaRatio = value;
                this.RaisePropertyChanged("PipeAreaRatio");
            }
        }

        // �½��ܹ���ֱ����mm
        public double DownPipeDN
        {
            get { return WaterCycle.DownPipeDN; }
            set
            {
                WaterCycle.DownPipeDN = value;
                this.RaisePropertyChanged("DownPipeDN");
            }
        }

        // �½��ܸ�����������
        public double DownPipeCount
        {
            get { return WaterCycle.DownPipeCount; }
            set
            {
                WaterCycle.DownPipeCount = value;
                this.RaisePropertyChanged("DownPipeCount");
            }
        }

        // �½���ʵ�ʹ��ھ���mm
        public double DownPipeID
        {
            get { return WaterCycle.DownPipeID * 1000; }
            set
            {
                WaterCycle.DownPipeID = value / 1000;
                this.RaisePropertyChanged("DownPipeID");
            }
        }

        // �½��ܽ������m2
        public double DownPipeArea
        {
            get { return WaterCycle.DownPipeArea; }
            set
            {
                WaterCycle.DownPipeArea = value;
                this.RaisePropertyChanged("DownPipeArea");
            }
        }

        // �����뻻���������ߵĸ߶Ȳm
        public double Height
        {
            get { return WaterCycle.Height; }
            set
            {
                WaterCycle.Height = value;
                this.RaisePropertyChanged("Height");
            }
        }

        // ���ڱھ��Դֲڶȣ�m
        public double Roughness
        {
            get { return WaterCycle.Roughness * 1000; }
            set
            {
                WaterCycle.Roughness = value / 1000;
                this.RaisePropertyChanged("Roughness");
            }
        }

        // ������ˮƽֱ�ܶγ��ȣ�m
        public double UpHorizontalPipeLength
        {
            get { return WaterCycle.UpHorizontalPipeLength; }
            set
            {
                WaterCycle.UpHorizontalPipeLength = value;
                this.RaisePropertyChanged("UpHorizontalPipeLength");
            }
        }

        // �������ܵ�������L/D��������
        public double UpToltalFittingLD
        {
            get { return WaterCycle.UpToltalFittingLD; }
            set
            {
                WaterCycle.UpToltalFittingLD = value;
                this.RaisePropertyChanged("UpToltalFittingLD");
            }
        }

        // �������ֲܾ�����ϵ��K��������
        public double UpToltalFittingK
        {
            get { return WaterCycle.UpToltalFittingK; }
            set
            {
                WaterCycle.UpToltalFittingK = value;
                this.RaisePropertyChanged("UpToltalFittingK");
            }
        }

        // �½���ˮƽֱ�ܶγ��ȣ�m
        public double DownHorizontalPipeLength
        {
            get { return WaterCycle.DownHorizontalPipeLength; }
            set
            {
                WaterCycle.DownHorizontalPipeLength = value;
                this.RaisePropertyChanged("DownHorizontalPipeLength");
            }
        }

        // �½����ܵ�������L/D��������
        public double DownToltalFittingLD
        {
            get { return WaterCycle.DownToltalFittingLD; }
            set
            {
                WaterCycle.DownToltalFittingLD = value;
                this.RaisePropertyChanged("DownToltalFittingLD");
            }
        }

        // �½����ֲܾ�����ϵ��K��������
        public double DownToltalFittingK
        {
            get { return WaterCycle.DownToltalFittingK; }
            set
            {
                WaterCycle.DownToltalFittingK = value;
                this.RaisePropertyChanged("DownToltalFittingK");
            }
        }

        // ѭ������K��������
        public double CycleRatio
        {
            get { return WaterCycle.CycleRatio; }
            set
            {
                WaterCycle.CycleRatio = value;
                this.RaisePropertyChanged("CycleRatio");
            }
        }

        // ѭ������Wo��m/s
        public double CycleVelocity
        {
            get { return WaterCycle.CycleVelocity; }
            set
            {
                WaterCycle.CycleVelocity = value;
                this.RaisePropertyChanged("CycleVelocity");
            }
        }

        // ����������������
        public string FlowRegime
        {
            get { return WaterCycle.FlowRegime; }
            set
            {
                WaterCycle.FlowRegime = value;
                this.RaisePropertyChanged("FlowRegime");
            }
        }
        #endregion ��������

        #endregion

        #region ����
        // ������м���ģʽ�л�
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

        // ����������ǿ��ViewModle��������֪ͨ�仯
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

        // ������м���
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

        // �����
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
                Title = "��",
                Filter = "����������������(*.tsg)|*.tsg"
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

        // �������
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

        // ������Ϊ
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
                Title = "���Ϊ",
                FileName = "�½�����������������",
                Filter = "����������������(*.tsg)|*.tsg"
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

        #region ������֤

        // �ֵ��ֶ�
        private Dictionary<string, string> dataErrors = new Dictionary<string, string>();

        // ���ԣ��ж��Ƿ�ͨ����֤
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

        // IDataErrorInfo�̳з���:����ָʾ��������Ĵ���
        public string Error { get => null; }

        // IDataErrorInfo�̳з���:����ָʾ�������Լ���Ĵ���

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
                        return "�������ݲ�����";
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
                        return "�������ݲ�����";
                    }
                    RemoveDic(dataErrors, columnName);
                    return null;
                }
            }
        }

        // �ֵ���Ӽ�ֵ�Եķ���
        private void AddDic(Dictionary<string, string> dic, string key)
        {
            if (!dic.ContainsKey(key))
            {
                dic.Add(key, "");
                RaisePropertyChanged("IsValid");
            }
        }

        // �ֵ��Ƴ���ֵ�Եķ���
        private void RemoveDic(Dictionary<string, string> dic, string key)
        {
            dic.Remove(key);
            RaisePropertyChanged("IsValid");
        }
        #endregion
    }
}