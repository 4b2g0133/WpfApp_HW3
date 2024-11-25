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
        // 預設的筆觸顏色和填充顏色
        Color strokeColor = Colors.Black;
        Color fillColor = Colors.Aqua;

        // 預設的筆觸和填充畫刷
        Brush strokeBrush = Brushes.Black;
        Brush fillBrush = Brushes.Aqua;

        // 預設繪製的形狀類型 ("line", "rectangle", "ellipse", "polyline")
        string shapeType = "line";

        // 預設筆觸寬度
        int strokeThickness = 1;

        // 紀錄滑鼠的起始點和終點座標
        Point start, dest;

        public MainWindow()
        {
            InitializeComponent();
            // 初始化顏色選擇器的預設顏色
            strokeColorPicker.SelectedColor = strokeColor;
            fillColorPicker.SelectedColor = fillColor;
        }

        // 滑鼠進入 Canvas 時改變滑鼠指標
        private void myCanvas_MouseEnter(object sender, MouseEventArgs e)
        {
            myCanvas.Cursor = Cursors.Pen; // 設定滑鼠為「筆」圖示
        }

        // 滑鼠在 Canvas 上移動時的行為
        private void myCanvas_MouseMove(object sender, MouseEventArgs e)
        {
            dest = e.GetPosition(myCanvas); // 取得當前滑鼠位置

            if (e.LeftButton == MouseButtonState.Pressed)
            {
                // 計算起點和矩形的左上角點
                Point origin;
                origin.X = Math.Min(start.X, dest.X);
                origin.Y = Math.Min(start.Y, dest.Y);

                // 計算寬度和高度
                double width = Math.Abs(start.X - dest.X);
                double height = Math.Abs(start.Y - dest.Y);

                // 根據形狀類型更新最後新增的圖形
                switch (shapeType)
                {
                    case "line":
                        var line = myCanvas.Children.OfType<Line>().LastOrDefault();
                        if (line != null)
                        {
                            line.X2 = dest.X;
                            line.Y2 = dest.Y;
                        }
                        break;
                    case "rectangle":
                        var rectangle = myCanvas.Children.OfType<Rectangle>().LastOrDefault();
                        if (rectangle != null)
                        {
                            rectangle.Width = width;
                            rectangle.Height = height;
                            rectangle.SetValue(Canvas.LeftProperty, origin.X);
                            rectangle.SetValue(Canvas.TopProperty, origin.Y);
                        }
                        break;
                    case "ellipse":
                        var ellipse = myCanvas.Children.OfType<Ellipse>().LastOrDefault();
                        if (ellipse != null)
                        {
                            ellipse.Width = width;
                            ellipse.Height = height;
                            ellipse.SetValue(Canvas.LeftProperty, origin.X);
                            ellipse.SetValue(Canvas.TopProperty, origin.Y);
                        }
                        break;
                    case "polyline":
                        // 多邊形的行為尚未實作
                        break;
                }
            }

            // 更新滑鼠位置的狀態文字
            statusPoint.Content = $"({Convert.ToInt32(start.X)}, {Convert.ToInt32(start.Y)}) - ({Convert.ToInt32(dest.X)}, {Convert.ToInt32(dest.Y)})";
        }

        // 筆觸顏色改變事件
        private void strokeColorPicker_SelectedColorChanged(object sender, RoutedPropertyChangedEventArgs<Color?> e)
        {
            strokeColor = strokeColorPicker.SelectedColor.Value; // 更新筆觸顏色
        }

        // 填充顏色改變事件
        private void fillColorPicker_SelectedColorChanged(object sender, RoutedPropertyChangedEventArgs<Color?> e)
        {
            fillColor = fillColorPicker.SelectedColor.Value; // 更新填充顏色
        }

        // 切換形狀類型
        private void ShapeButton_Click(object sender, RoutedEventArgs e)
        {
            var targetRadioButton = sender as RadioButton; // 確保發送者為 RadioButton
            shapeType = targetRadioButton.Tag.ToString(); // 更新形狀類型
        }

        // 擦除按鈕點擊事件（尚未實作）
        private void EraseButton_Click(object sender, RoutedEventArgs e)
        {
            // 可能實現的功能：刪除 Canvas 上的最後一個圖形
        }

        // 清空 Canvas 按鈕點擊事件（尚未實作）
        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            // 可能實現的功能：清空 Canvas.Children 集合
        }

        // 筆觸寬度滑桿值改變事件
        private void strokeThicknessSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            strokeThickness = (int)strokeThicknessSlider.Value; // 更新筆觸寬度
        }

        // 滑鼠在 Canvas 上按下左鍵時的行為
        private void myCanvas_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            start = e.GetPosition(myCanvas); // 紀錄起始點座標
            myCanvas.Cursor = Cursors.Cross; // 改變滑鼠指標為十字型

            // 根據形狀類型新增圖形
            switch (shapeType)
            {
                case "line":
                    Line line = new Line
                    {
                        X1 = start.X,
                        Y1 = start.Y,
                        X2 = dest.X,
                        Y2 = dest.Y,
                        Stroke = Brushes.Gray, // 暫時使用灰色
                        StrokeThickness = 1 // 初始筆觸寬度
                    };
                    myCanvas.Children.Add(line); // 新增到 Canvas
                    break;
                case "rectangle":
                    Rectangle rectangle = new Rectangle
                    {
                        Stroke = Brushes.Gray, // 暫時使用灰色
                        Fill = Brushes.LightGray // 暫時使用灰色填充
                    };
                    myCanvas.Children.Add(rectangle);
                    rectangle.SetValue(Canvas.LeftProperty, start.X);
                    rectangle.SetValue(Canvas.TopProperty, start.Y);
                    break;
                case "ellipse":
                    Ellipse ellipse = new Ellipse
                    {
                        Stroke = Brushes.Gray, // 暫時使用灰色
                        Fill = Brushes.LightGray // 暫時使用灰色填充
                    };
                    myCanvas.Children.Add(ellipse);
                    ellipse.SetValue(Canvas.LeftProperty, start.X);
                    ellipse.SetValue(Canvas.TopProperty, start.Y);
                    break;
                case "polyline":
                    // 多邊形的行為尚未實作
                    break;
            }
        }

        // 滑鼠在 Canvas 上放開左鍵時的行為
        private void myCanvas_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            // 當滑鼠放開時，更新圖形的最終顏色和筆觸寬度
            Brush strokeBrush = new SolidColorBrush(strokeColor);
            Brush fillBrush = new SolidColorBrush(fillColor);

            switch (shapeType)
            {
                case "line":
                    var line = myCanvas.Children.OfType<Line>().LastOrDefault();
                    if (line != null)
                    {
                        line.Stroke = strokeBrush;
                        line.StrokeThickness = strokeThickness;
                    }
                    break;
                case "rectangle":
                    var rectangle = myCanvas.Children.OfType<Rectangle>().LastOrDefault();
                    if (rectangle != null)
                    {
                        rectangle.Stroke = strokeBrush;
                        rectangle.Fill = fillBrush;
                        rectangle.StrokeThickness = strokeThickness;
                    }
                    break;
                case "ellipse":
                    var ellipse = myCanvas.Children.OfType<Ellipse>().LastOrDefault();
                    if (ellipse != null)
                    {
                        ellipse.Stroke = strokeBrush;
                        ellipse.Fill = fillBrush;
                        ellipse.StrokeThickness = strokeThickness;
                    }
                    break;
                case "polyline":
                    // 多邊形的行為尚未實作
                    break;
            }
        }
    }
}