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


namespace CartoonTranslate
{
    /// <summary>
    /// MainWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    /// 

    public partial class MainWindow : Window
    {
        // using “OCR” or “Recognize Text”
        private string Engine;
        // source language, English or Japanese
        private string Language;
        // OCR result object
        private OcrResult.Rootobject ocrResult;
        //private string Version;

       // public object CognitiveServiceAgent { get; private set; }

        public MainWindow()
        {
            InitializeComponent();
        }


        private void btn_Show_Click(object sender, RoutedEventArgs e)
        {
            if (!Uri.IsWellFormedUriString(this.tb_Url.Text, UriKind.Absolute))
            {
                // show warning message
                return;
            }

            // show image at imgSource
            BitmapImage bi = new BitmapImage();
            bi.BeginInit();
            bi.UriSource = new Uri(this.tb_Url.Text);
            bi.EndInit();
            this.imgSource.Source = bi;
            this.imgTarget.Source = bi;
        }

        private async void btn_OCR_Click(object sender, RoutedEventArgs e)
        {
            this.Engine = GetEngine();
            this.Language = GetLanguage();

            if (Engine == "OCR")
            {
                ocrResult = await CognitiveServiceAgent.DoOCR(this.tb_Url.Text, Language);
                foreach (OcrResult.Region region in ocrResult.regions)
                {
                    foreach (OcrResult.Line line in region.lines)
                    {
                        if (line.Convert())
                        {
                            Rectangle rect = new Rectangle()
                            {
                                Margin = new Thickness(line.BB[0], line.BB[1], 0, 0),
                                Width = line.BB[2],
                                Height = line.BB[3],
                                Stroke = Brushes.Red,
                                //Fill =Brushes.White
                            };
                            this.canvas_1.Children.Add(rect);
                        }
                    }
                }
            }
            else
            {
            }
        }
        private async void btn_Translate_Click(object sender, RoutedEventArgs e)
        {
            List<string> listTarget = await this.Translate();
            this.ShowTargetText(listTarget);
        }

        private async Task<List<string>> Translate()
        {
            List<string> listSource = new List<string>();
            List<string> listTarget = new List<string>();


            if (Engine == "OCR")
            {
                foreach (OcrResult.Region region in ocrResult.regions)
                {
                    foreach (OcrResult.Line line in region.lines)
                    {
                        listSource.Add(line.TEXT);
                        if (listSource.Count >= 25)
                        {
                            List<string> listOutput = await CognitiveServiceAgent.DoTranslate(listSource, Language, "zh-Hans");
                            listTarget.AddRange(listOutput);
                            listSource.Clear();
                        }
                    }
                }
                if (listSource.Count > 0)
                {
                    List<string> listOutput = await CognitiveServiceAgent.DoTranslate(listSource, Language, "zh-Hans");
                    listTarget.AddRange(listOutput);
                }
            }

            return listTarget;
        }
        private void ShowTargetText(List<string> listTarget)
        {
            int i = 0;
            foreach (OcrResult.Region region in ocrResult.regions)
            {
                foreach (OcrResult.Line line in region.lines)
                {
                    string translatedLine = listTarget[i];

                    Rectangle rect = new Rectangle()
                    {
                        Margin = new Thickness(line.BB[0], line.BB[1], 0, 0),
                        Width = line.BB[2],
                        Height = line.BB[3],
                        Stroke = null,
                        Fill = Brushes.White
                    };
                    this.canvas_2.Children.Add(rect);

                    TextBlock tb = new TextBlock()
                    {
                        Margin = new Thickness(line.BB[0], line.BB[1], 0, 0),
                        Height = line.BB[3],
                        Width = line.BB[2],
                        Text = translatedLine,
                        FontSize = 16,
                        TextWrapping = TextWrapping.Wrap,
                        Foreground = Brushes.Red
                    };
                    this.canvas_2.Children.Add(tb);
                    i++;
                }
            }
        }

        private void rb_V1_Click(object sender, RoutedEventArgs e)
        {
            this.rb_Japanese.IsEnabled = true;
        }

        private void rb_V2_Click(object sender, RoutedEventArgs e)
        {
            this.rb_English.IsChecked = true;
            this.rb_Japanese.IsChecked = false;
            this.rb_Japanese.IsEnabled = false;
        }
        private string GetLanguage()
        {
            if (this.rb_English.IsChecked == true)
            {
                return "en";
            }
            else
            {
                return "ja";
            }
        }

        private string GetEngine()
        {
            if (this.rb_V1.IsChecked == true)
            {
                return "OCR";
            }
            else
            {
                return "RecText";
            }
        }
    }
}