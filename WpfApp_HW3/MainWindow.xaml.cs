using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApp_HW3
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        // 宣告線條顏色，初始為黑色
        Color strokeColor = Colors.Black;
        // 宣告線條畫筆，初始為黑色畫刷
        Brush strokeBrush = Brushes.Black;
        // 宣告起點與終點座標
        Point start, dest;

        // 構造函數
        public MainWindow()
        {
            InitializeComponent(); // 初始化元件
            strokeColorPicker.SelectedColor = strokeColor; // 設定顏色選擇器的初始顏色
        }

        // 滑鼠進入畫布時觸發
        private void myCanvas_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            myCanvas.Cursor = Cursors.Pen; // 設定滑鼠游標為筆的形狀
        }

        // 當滑鼠在畫布上移動時觸發
        private void myCanvas_MouseMove(object sender, MouseEventArgs e)
        {
            // 設定終點座標為目前滑鼠位置
            dest = e.GetPosition(myCanvas);
            // 在狀態欄顯示起點與終點座標
            statusPoint.Content = $"({Convert.ToInt32(start.X)}, {Convert.ToInt32(start.Y)}) - ({Convert.ToInt32(dest.X)}, {Convert.ToInt32(dest.Y)})";
        }

        // 當滑鼠左鍵在畫布上釋放時觸發
        private void myCanvas_MouseUp(object sender, MouseButtonEventArgs e)
        {
            // 創建新的畫刷，使用選擇的線條顏色
            var brush = new SolidColorBrush(strokeColor);
            // 創建一條線段，設定起點、終點、顏色及粗細
            Line line = new Line
            {
                X1 = start.X, // 起點 X 座標
                Y1 = start.Y, // 起點 Y 座標
                X2 = dest.X, // 終點 X 座標
                Y2 = dest.Y, // 終點 Y 座標
                Stroke = brush, // 線條顏色
                StrokeThickness = 2 // 線條粗細
            };
            myCanvas.Children.Add(line); // 將線條加入畫布
        }

        // 當線條顏色選擇器顏色改變時觸發
        private void strokeColorPicker_SelectedColorChanged(object sender, RoutedPropertyChangedEventArgs<Color?> e)
        {
            strokeColor = strokeColorPicker.SelectedColor.Value; // 更新線條顏色為新選擇的顏色
        }

        // 當滑鼠左鍵按下時觸發
        private void myCanvas_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            start = e.GetPosition(myCanvas); // 設定起點為目前滑鼠位置
            myCanvas.Cursor = Cursors.Cross; // 更改滑鼠游標為十字形
        }
    }

}