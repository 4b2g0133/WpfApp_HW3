using Microsoft.Win32;
using System.IO;
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
        // 定義屬性：繪圖線條顏色、填充顏色、線條筆刷、填充筆刷、繪圖形狀類型、線條粗細、起始點和終點
        Color strokeColor = Colors.Black; // 線條顏色
        Color fillColor = Colors.Aqua; // 填充顏色
        Brush strokeBrush = Brushes.Black; // 線條筆刷
        Brush fillBrush = Brushes.Aqua; // 填充筆刷
        string shapeType = "line"; // 繪圖類型（預設為直線）
        int strokeThickness = 1; // 線條粗細（預設為1）
        Point start, dest; // 起始點和終點
        string actionType = "draw"; // 動作類型：繪圖或擦除

        public MainWindow()
        {
            InitializeComponent();

            // 初始化顏色選擇器的預設值
            strokeColorPicker.SelectedColor = strokeColor;
            fillColorPicker.SelectedColor = fillColor;
        }

        // 滑鼠進入畫布時更改游標樣式
        private void MyCanvas_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            myCanvas.Cursor = Cursors.Pen; // 設定游標為筆形狀
        }

        // 滑鼠左鍵釋放後，完成形狀的樣式設置
        private void MyCanvas_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            // 使用選擇的顏色更新線條和填充筆刷
            Brush strokeBrush = new SolidColorBrush(strokeColor);
            Brush fillBrush = new SolidColorBrush(fillColor);

            // 根據繪圖類型應用樣式
            switch (shapeType)
            {
                case "line":
                    var line = myCanvas.Children.OfType<Line>().LastOrDefault();
                    line.Stroke = strokeBrush;
                    line.StrokeThickness = strokeThickness;
                    break;
                case "rectangle":
                    var rectangle = myCanvas.Children.OfType<Rectangle>().LastOrDefault();
                    rectangle.Stroke = strokeBrush;
                    rectangle.Fill = fillBrush;
                    rectangle.StrokeThickness = strokeThickness;
                    break;
                case "ellipse":
                    var ellipse = myCanvas.Children.OfType<Ellipse>().LastOrDefault();
                    ellipse.Stroke = strokeBrush;
                    ellipse.Fill = fillBrush;
                    ellipse.StrokeThickness = strokeThickness;
                    break;
                case "polyline":
                    var polyline = myCanvas.Children.OfType<Polyline>().LastOrDefault();
                    polyline.Stroke = strokeBrush;
                    polyline.Fill = fillBrush;
                    polyline.StrokeThickness = strokeThickness;
                    break;
            }
            DisplayStatus(); // 更新狀態欄
        }

        // 滑鼠左鍵按下時，開始繪製形狀
        private void MyCanvas_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            start = e.GetPosition(myCanvas); // 紀錄起始點
            myCanvas.Cursor = Cursors.Cross; // 設定游標為十字形

            // 當處於繪圖模式時，根據形狀類型添加新形狀到畫布
            if (actionType == "draw")
            {
                switch (shapeType)
                {
                    case "line":
                        Line line = new Line
                        {
                            X1 = start.X,
                            Y1 = start.Y,
                            X2 = dest.X,
                            Y2 = dest.Y,
                            Stroke = Brushes.Gray,
                            StrokeThickness = 1
                        };
                        myCanvas.Children.Add(line);
                        break;
                    case "rectangle":
                        Rectangle rectangle = new Rectangle
                        {
                            Stroke = Brushes.Gray,
                            Fill = Brushes.LightGray
                        };
                        myCanvas.Children.Add(rectangle);
                        rectangle.SetValue(Canvas.LeftProperty, start.X);
                        rectangle.SetValue(Canvas.TopProperty, start.Y);
                        break;
                    case "ellipse":
                        Ellipse ellipse = new Ellipse
                        {
                            Stroke = Brushes.Gray,
                            Fill = Brushes.LightGray
                        };
                        myCanvas.Children.Add(ellipse);
                        ellipse.SetValue(Canvas.LeftProperty, start.X);
                        ellipse.SetValue(Canvas.TopProperty, start.Y);
                        break;
                    case "polyline":
                        Polyline polyline = new Polyline
                        {
                            Stroke = Brushes.Gray,
                            Fill = Brushes.LightGray
                        };
                        myCanvas.Children.Add(polyline);
                        break;
                }
            }
            DisplayStatus(); // 更新狀態欄
        }

        // 滑鼠移動時的事件處理
        private void MyCanvas_MouseMove(object sender, MouseEventArgs e)
        {
            dest = e.GetPosition(myCanvas); // 紀錄目前滑鼠位置
            DisplayStatus(); // 更新狀態欄

            // 根據目前動作類型執行相應操作
            switch (actionType)
            {
                case "draw": // 繪圖模式
                    if (e.LeftButton == MouseButtonState.Pressed)
                    {
                        Point origin;
                        origin.X = Math.Min(start.X, dest.X);
                        origin.Y = Math.Min(start.Y, dest.Y);
                        double width = Math.Abs(start.X - dest.X);
                        double height = Math.Abs(start.Y - dest.Y);

                        switch (shapeType)
                        {
                            case "line":
                                var line = myCanvas.Children.OfType<Line>().LastOrDefault();
                                line.X2 = dest.X;
                                line.Y2 = dest.Y;
                                break;
                            case "rectangle":
                                var rectangle = myCanvas.Children.OfType<Rectangle>().LastOrDefault();
                                rectangle.Width = width;
                                rectangle.Height = height;
                                rectangle.SetValue(Canvas.LeftProperty, origin.X);
                                rectangle.SetValue(Canvas.TopProperty, origin.Y);
                                break;
                            case "ellipse":
                                var ellipse = myCanvas.Children.OfType<Ellipse>().LastOrDefault();
                                ellipse.Width = width;
                                ellipse.Height = height;
                                ellipse.SetValue(Canvas.LeftProperty, origin.X);
                                ellipse.SetValue(Canvas.TopProperty, origin.Y);
                                break;
                            case "polyline":
                                var polyline = myCanvas.Children.OfType<Polyline>().LastOrDefault();
                                polyline.Points.Add(dest);
                                break;
                        }
                    }
                    break;
                case "erase": // 橡皮擦模式
                    var shape = e.OriginalSource as Shape; // 獲取滑鼠下的形狀
                    myCanvas.Cursor = Cursors.Hand; // 更改游標為手型
                    myCanvas.Children.Remove(shape); // 移除該形狀
                    if (myCanvas.Children.Count == 0) myCanvas.Cursor = Cursors.Arrow; // 畫布空時恢復為箭頭游標
                    break;
            }
        }

        // 顯示當前狀態（形狀數量、動作類型等）
        private void DisplayStatus()
        {
            if (actionType != "draw")
                statusAction.Content = $"{actionType}";
            else
                statusAction.Content = $"繪圖模式: {shapeType}";

            statusPoint.Content = $"({Convert.ToInt32(start.X)}, {Convert.ToInt32(start.Y)}) - ({Convert.ToInt32(dest.X)}, {Convert.ToInt32(dest.Y)})";

            int lineCount = myCanvas.Children.OfType<Line>().Count();
            int rectangleCount = myCanvas.Children.OfType<Rectangle>().Count();
            int ellipseCount = myCanvas.Children.OfType<Ellipse>().Count();
            int polylineCount = myCanvas.Children.OfType<Polyline>().Count();

            statusShape.Content = $"Lines: {lineCount}, Rectangles: {rectangleCount}, Ellipses: {ellipseCount}, Polylines: {polylineCount}";
        }

        private void StrokeColorPicker_SelectedColorChanged(object sender, RoutedPropertyChangedEventArgs<Color?> e)
        {
            strokeColor = strokeColorPicker.SelectedColor.Value;
        }

        private void FillColorPicker_SelectedColorChanged(object sender, RoutedPropertyChangedEventArgs<Color?> e)
        {
            fillColor = fillColorPicker.SelectedColor.Value;
        }

        private void ShapeButton_Click(object sender, RoutedEventArgs e)
        {
            var targetRadioButton = sender as RadioButton;
            shapeType = targetRadioButton.Tag.ToString();
            actionType = "draw";
        }

        private void EraseButton_Click(object sender, RoutedEventArgs e)
        {
            actionType = "erase";
            DisplayStatus();
        }

        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            actionType = "clear";
            myCanvas.Children.Clear();
            DisplayStatus();
        }

        private void SaveCanvas_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                Title = "儲存畫布",
                Filter = "PNG Files (*.png)|*.png|JPG Files (*.jpg)|*.jpg|All Files (*.*)|*.*",
                DefaultExt = ".png"
            };

            if (saveFileDialog.ShowDialog() == true)
            {
                int w = Convert.ToInt32(myCanvas.RenderSize.Width);
                int h = Convert.ToInt32(myCanvas.RenderSize.Height);

                RenderTargetBitmap renderBitmap = new RenderTargetBitmap(w, h, 96d, 96d, PixelFormats.Pbgra32);

                renderBitmap.Render(myCanvas);

                BitmapEncoder encoder;
                string extension = System.IO.Path.GetExtension(saveFileDialog.FileName).ToLower();
                switch (extension)
                {
                    case ".jpg":
                        encoder = new JpegBitmapEncoder();
                        break;
                    default:
                        encoder = new PngBitmapEncoder();
                        break;
                }

                encoder.Frames.Add(BitmapFrame.Create(renderBitmap));

                using (FileStream outStream = new FileStream(saveFileDialog.FileName, FileMode.Create))
                {
                    encoder.Save(outStream);
                }
            }
        }

        private void StrokeThicknessSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            strokeThickness = (int)strokeThicknessSlider.Value;
        }

    }

}