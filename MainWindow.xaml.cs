using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

namespace Rectangles_over_Images
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext= this;
        }

        public double xcoord1;
        public double xcoord2;
        public double ycoord1;
        public double ycoord2;
        
        //Image img;
        BitmapImage bi = new BitmapImage();
        private void UploadButton_Click(object sender, RoutedEventArgs e) //Called when the user attempts to upload a picture
        {
            OpenFileDialog opendialog = new OpenFileDialog();
            opendialog.Filter = "Image files|*.bmp;*.jpg;*.png";
            opendialog.Multiselect= false; //Allows user to only upload one picture
            opendialog.FilterIndex = 1;
            if (opendialog.ShowDialog() == true)
            {
                this.bi = new BitmapImage();
                picture.Source = new BitmapImage(new Uri(opendialog.FileName));
            }

        }

 
        public void Save_Click(object sender, RoutedEventArgs e) //Saves the image with the rectangles on the image as a new image
        {

            SaveFileDialog savefile = new SaveFileDialog();

            savefile.DefaultExt = "jpg";
            savefile.FileName = ".jpg";
            savefile.Filter = "jpg files (*.jpg)|*.jpg";

            if (savefile.ShowDialog() == true)
            {
               
            if (bi != null)
            {
                    try
                    {
                        var encoder = new JpegBitmapEncoder(); // Or PngBitmapEncoder, or whichever encoder you want
                        encoder.Frames.Add(BitmapFrame.Create(bi));
                        using (var stream = savefile.OpenFile())
                        {
                            encoder.Save(stream);
                        }
                    }
                    catch (System.Exception c) { Console.Write("Exception" + c); }
                }



        }

            //BitmapImage bitmap = new BitmapImage();
            //bitmap.BeginInit();
            //bitmap.UriSource = new Uri(@"C:\\Users\SavedPicture.JPG");
            //bitmap.EndInit();
        }

        bool mouseDown = false; //Will be set to true when mouse is held down
        Point mouseDownPos;
        public Rectangle rect; //initializes rectangle but features are not set
        public void canvas_MouseDown(object sender, MouseButtonEventArgs e) //When user CLICKS DOWN on mouse to start drawing
        {
            // Commence forming of rectangle

            rect = new Rectangle
            {
                Stroke = Brushes.Black,
                StrokeThickness = 2,
                Fill= Brushes.Red,
            };

            // Plan is to capture and track the mouse
            mouseDown = true;
            mouseDownPos = e.GetPosition(canvas);
            //xcoord1= mouseDownPos.X;
            //ycoord1= mouseDownPos.Y;
            //canvas.CaptureMouse();

            //Point 1 of Drag selection
           // Canvas.SetLeft(rect, mouseDownPos.X);
            //Canvas.SetTop(rect, mouseDownPos.Y);
            canvas.Children.Add(rect); //adds rectangle to the canvas
            //Make the drag selection box visible
            //rect.Visibility = Visibility.Visible;
        }

        public void canvas_MouseUp(object sender, MouseButtonEventArgs e) //When user RELEASES the click to stop drawing
        {
            mouseDown = false;
            rect= null;

            // 
            

            
        }

        public void canvas_MouseMove(object sender, MouseEventArgs e) //Function to capture position of the mouse
        {
            if (mouseDown) //Only operates when user is attempting to make a rectangle. 
            {
                Point mousePos = e.GetPosition(canvas);

                var x = Math.Min(mousePos.X, mouseDownPos.X);
                var y = Math.Min(mousePos.Y, mouseDownPos.Y);
                var x1 = Math.Max(mouseDownPos.X, mousePos.X);
                var y1 = Math.Max(mouseDownPos.Y, mousePos.Y);

                var w = Math.Max(mousePos.X, mouseDownPos.X) - x;
                var h = Math.Max(mousePos.Y, mouseDownPos.Y) - y;

                rect.Width= w;
                rect.Height = h;

                Canvas.SetLeft(rect, x);
                Canvas.SetRight(rect, x1+w);
                Canvas.SetTop(rect, y);
                Canvas.SetBottom(rect, y1+h);
                
            }
        }
        


        /* public void CreateRectangle()
         {
             Rectangle? rect = null;
             if (rect == null)
             {
                 rect = new Rectangle();
                 rect.Height = Math.Abs(ycoord1 - ycoord2);
                 canvas.CaptureMouse();
                 rect.Width = Math.Abs(xcoord1 - xcoord2);
                 double midY = Math.Abs(ycoord2- ycoord1);
                 double midX = Math.Abs(xcoord2- xcoord1);


                 SolidColorBrush colorBrush = new SolidColorBrush();
                 colorBrush.Color = Colors.Yellow;
                 colorBrush.Opacity = 0;

                 rect.Stroke = colorBrush;
                 rect.StrokeThickness = 1;
                 rect.Fill = colorBrush;
                 canvas.Children.Add(rect);


             }
         }*/
    }
}
