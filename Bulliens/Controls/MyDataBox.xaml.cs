using System.Windows;
using System.Windows.Controls;


namespace Boiler.Controls
{
    /// <summary>
    /// MyDataBox.xaml 的交互逻辑
    /// </summary>
    public partial class MyDataBox : UserControl
    {
        public MyDataBox()
        {
            InitializeComponent();
            // 设置错误验证显示
            Validation.SetValidationAdornerSite(this, this.txtValue);
        }

        #region 依赖属性

        public string DataName
        {
            get { return (string)GetValue(DataNameProperty); }
            set { SetValue(DataNameProperty, value); }
        }

        // Using a DependencyProperty as the backing store for DataName.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DataNameProperty =
            DependencyProperty.Register("DataName", typeof(string), typeof(MyDataBox), new PropertyMetadata(""));

        public string DataUnit
        {
            get { return (string)GetValue(DataUnitProperty); }
            set { SetValue(DataUnitProperty, value); }
        }

        // Using a DependencyProperty as the backing store for DataUnit.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DataUnitProperty =
            DependencyProperty.Register("DataUnit", typeof(string), typeof(MyDataBox), new PropertyMetadata(""));

        public string DataValue
        {
            get { return (string)GetValue(DataValueProperty); }
            set { SetValue(DataValueProperty, value); }
        }

        // Using a DependencyProperty as the backing store for DataValue.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DataValueProperty =
            DependencyProperty.Register("DataValue", typeof(string), typeof(MyDataBox), new PropertyMetadata(""));


        public bool IsDataEnabled
        {
            get { return (bool)GetValue(IsDataEnabledProperty); }
            set { SetValue(IsDataEnabledProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsDataEnabled.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsDataEnabledProperty =
            DependencyProperty.Register("IsDataEnabled", typeof(bool), typeof(MyDataBox), new PropertyMetadata(true));



        public bool IsDataReadOnly
        {
            get { return (bool)GetValue(IsDataReadOnlyProperty); }
            set { SetValue(IsDataReadOnlyProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsDataReadOnly.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsDataReadOnlyProperty =
            DependencyProperty.Register("IsDataReadOnly", typeof(bool), typeof(MyDataBox), new PropertyMetadata(false));



        public int DataFontSize
        {
            get { return (int)GetValue(DataFontSizeProperty); }
            set { SetValue(DataFontSizeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for DataFontSize.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DataFontSizeProperty =
            DependencyProperty.Register("DataFontSize", typeof(int), typeof(MyDataBox), new PropertyMetadata(12));



        public string DataToolTip
        {
            get { return (string)GetValue(DataToolTipProperty); }
            set { SetValue(DataToolTipProperty, value); }
        }

        // Using a DependencyProperty as the backing store for DataToolTip.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DataToolTipProperty =
            DependencyProperty.Register("DataToolTip", typeof(string), typeof(MyDataBox), new PropertyMetadata(""));




        #endregion

    }
}
