using System;
using System.Globalization;
using System.IO;
using System.Threading;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Passport_MRZ.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            //CultureInfo ldtPckrcultureInfo = new CultureInfo("en-US");
            //DateTimeFormatInfo dateInfo = new DateTimeFormatInfo();
            //dateInfo.ShortDatePattern = "dd MMM yyyy";
            //ldtPckrcultureInfo.DateTimeFormat = dateInfo;
            //dtpStartDate.Culture = ldtPckrcultureInfo;

            CultureInfo ci = new CultureInfo("en-US");
            ci.DateTimeFormat.ShortDatePattern = "dd MMM yyyy";
            Thread.CurrentThread.CurrentCulture = ci;

            var lang = System.Windows.Markup.XmlLanguage.GetLanguage("en-US");
            Dateofbirth.Language = lang;
            Dateofissue.Language = lang;
            Dateofexpiry.Language = lang;
            ImageSource imageSource = new BitmapImage(new Uri(Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + "\\user.jpg"));
            ImgUser.Source = imageSource;
        }
    }
}
