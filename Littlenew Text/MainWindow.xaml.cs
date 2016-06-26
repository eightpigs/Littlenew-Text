using Littlenew_Text.utils;
using ScintillaNET;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Littlenew_Text
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            Uri iconUri = new Uri(Directory.GetCurrentDirectory()+"/logo.png", UriKind.RelativeOrAbsolute);
            Icon = BitmapFrame.Create(iconUri);
            imgLogo.Source = Icon;
        }

        #region 预定义变量

        // 行号和列号
        int[] lineAndCol = null;

        #endregion

        private void lblTitle_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            // 修改窗体标题栏 / 主内容区域 Margin值
            updateMargin();

            // 调整组件大小
            rtbEditor.Width = SystemParameters.PrimaryScreenWidth - 260;


        }

        private void btnMin_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void btnMax_Click(object sender, RoutedEventArgs e)
        {
            MaxOrNormal();
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        int i = 0;
        private void titlePanel_MouseDown(object sender, MouseButtonEventArgs e)
        {
            DoubleClickShowMaxOrNormal(sender , e);

        }


        #region 功能方法

        Thickness titlePanelMax = new Thickness(0, 6, 0, 0);
        Thickness titlePanelMin = new Thickness(0, 0, 0, 0);

        Thickness mainPanelMax = new Thickness(0, 36, 0, 0);
        Thickness mainPanelMin = new Thickness(0, 30, 0, 0);
        
        /// <summary>
        ///  更新窗体标题栏的Margin值
        /// </summary>
        private void updateMargin()
        {
            if (this.WindowState == WindowState.Maximized)
            {
                titlePanel.Margin = titlePanelMax;
                mainPanel.Margin = mainPanelMax;
                statusPanel.Height = 34;
            }
            else
            {
                titlePanel.Margin = titlePanelMin;
                mainPanel.Margin = mainPanelMin;
                statusPanel.Height = 30;
            }
        }

        /// <summary>
        /// 窗体最大化或正常状态
        /// </summary>
        private void MaxOrNormal()
        {
            if (this.WindowState == WindowState.Normal)
            {
                this.WindowState = WindowState.Maximized;
            }
            else
            {
                this.WindowState = WindowState.Normal;
                this.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            }
        }

        /// <summary>
        /// 左键双击最大化窗口或正常
        /// </summary>
        private void DoubleClickShowMaxOrNormal(object sender, MouseButtonEventArgs e)
        {
            if (e.MiddleButton != MouseButtonState.Pressed && e.RightButton != MouseButtonState.Pressed)
            {
                i += 1;

                DispatcherTimer timer = new DispatcherTimer();

                timer.Interval = new TimeSpan(0, 0, 0, 0, 300);

                timer.Tick += (s, e1) => { timer.IsEnabled = false; i = 0; };

                timer.IsEnabled = true;

                if (i % 2 == 0)

                {

                    timer.IsEnabled = false;

                    i = 0;

                    MaxOrNormal();

                }
            }
        }

        /// <summary>
        /// 获取行号
        /// </summary>
        /// <returns></returns>
        private int[] GetLineNmuber()
        {
            TextPointer tp1 = rtbEditor.Selection.Start.GetLineStartPosition(0);
            TextPointer tp2 = rtbEditor.Selection.Start;

            int column = tp1.GetOffsetToPosition(tp2);

            int someBigNumber = int.MaxValue;
            int lineMoved, currentLineNumber;

            //rtbEditor.Document.ContentStart.GetLineStartPosition(-someBigNumber, out lineMoved);
            //rtbEditor.CaretPosition.GetLineStartPosition(-someBigNumber, out lineMoved);

            //rtbEditor.CaretPosition.ge

            rtbEditor.Selection.Start.GetLineStartPosition(-someBigNumber, out lineMoved);
            currentLineNumber = -lineMoved;

            //MessageBox.Show(text.Text);

            return new int[] { currentLineNumber, column };
        }

        /// <summary>
        /// 更新状态栏的显示内容
        /// </summary>
        /// <param name="lineAndCol"></param>
        private void RefreshStatusBar(int[] lineAndCol)
        {
            lblStatus.Content = "Line " + (lineAndCol[0] + 1) + ", Column " + lineAndCol[1];
        }

        private void RefreshLineNumber(int[] lineAndCol)
        {
            lblLine.Text += (lineAndCol[0] + 1) +"\n";
        }

        #endregion

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Init.LoadHightLight();

            // 加载语法高亮插件
            PluginLoader.loadPlugins(Directory.GetCurrentDirectory() + "/Plugins/Language/" , "Language_PluginEntrance");

            //PluginLoader.ExecPluginsMethod("Highlighter",Constants.Plugins["Language_PluginEntrance"][0]);

            // 启动默认获取焦点
            rtbEditor.Focus();

            // 初始测试内容
            rtbEditor.Document.Blocks.Add(new Paragraph(new Run(@"public static void main")));

            // 初始内容的高亮
            HightLightUtils.Refresh(rtbEditor);

            int[] lineAndCol = GetLineNmuber();
            // 刷新状态栏
            RefreshStatusBar(lineAndCol);

            // 刷新行号
            RefreshLineNumber(lineAndCol);

        }

        private void rtbEditor_KeyDown(object sender, KeyEventArgs e)
        {
            HightLightUtils.Refresh(rtbEditor);
        }

        private void rtbEditor_KeyUp(object sender, KeyEventArgs e)
        {
            HightLightUtils.Refresh(rtbEditor);
        }

        private void rtbEditor_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            lineAndCol = GetLineNmuber();
            RefreshStatusBar(lineAndCol);
            // 如果是回车事件,那么刷新行号
            if (e.Key == Key.Return || e.Key == Key.Enter)
            {
                RefreshLineNumber(lineAndCol);
                lblLine.ScrollToEnd();
            }
        }

        private void rtbEditor_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            // 获取编辑窗口垂直偏移量并让行号跟着滚动
            lblLine.ScrollToVerticalOffset(rtbEditor.VerticalOffset);
        }

        private void lblLine_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            // 获取行号垂直偏移量并让编辑窗口跟着滚动
            rtbEditor.ScrollToVerticalOffset(lblLine.VerticalOffset);
        }
    }
}
