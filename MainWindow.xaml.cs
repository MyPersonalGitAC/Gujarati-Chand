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
using System.IO;

namespace WpfApp3
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        static IEnumerable<string> ReadFile(string filename)
        {
            using (var file = new StreamReader(filename))
            {
                return file.ReadToEnd().Split('\n').Select(x=>x.TrimEnd().TrimStart().Trim());
            }
        }
        public StructuredString MyStructureString=new StructuredString();
        Paragraph myParagraph = new Paragraph();

        
        static string[] words = ReadFile("words.txt").ToArray();//get all words from stored files

        

        static readonly char[] guj = new char[]{'\u0A80',    '\u0A81',   '\u0A82',   '\u0A83',   '\u0A84',   '\u0A85',   '\u0A86',   '\u0A87',   '\u0A88',   '\u0A89',   '\u0A8A',   '\u0A8B',   '\u0A8C',   '\u0A8D',   '\u0A8E',   '\u0A8F',
'\u0A90',   '\u0A91',   '\u0A92',   '\u0A93',   '\u0A94',   '\u0A95',   '\u0A96',   '\u0A97',   '\u0A98',   '\u0A99',   '\u0A9A',   '\u0A9B',   '\u0A9C',   '\u0A9D',   '\u0A9E',   '\u0A9F',
'\u0AA0',   '\u0AA1',   '\u0AA2',   '\u0AA3',   '\u0AA4',   '\u0AA5',   '\u0AA6',   '\u0AA7',   '\u0AA8',   '\u0AA9',   '\u0AAA',   '\u0AAB',   '\u0AAC',   '\u0AAD',   '\u0AAE',   '\u0AAF',
'\u0AB0',   '\u0AB1',   '\u0AB2',   '\u0AB3',   '\u0AB4',   '\u0AB5',   '\u0AB6',   '\u0AB7',   '\u0AB8',   '\u0AB9',   '\u0ABA',   '\u0ABB',   '\u0ABC',   '\u0ABD',   '\u0ABE',   '\u0ABF',
'\u0AC0',   '\u0AC1',   '\u0AC2',   '\u0AC3',   '\u0AC4',   '\u0AC5',   '\u0AC6',   '\u0AC7',   '\u0AC8',   '\u0AC9',   '\u0ACA',   '\u0ACB',   '\u0ACC',   '\u0ACD',   '\u0ACE',   '\u0ACF',
'\u0AD0',   '\u0AD1',   '\u0AD2',   '\u0AD3',   '\u0AD4',   '\u0AD5',   '\u0AD6',   '\u0AD7',   '\u0AD8',   '\u0AD9',   '\u0ADA',   '\u0ADB',   '\u0ADC',   '\u0ADD',   '\u0ADE',   '\u0ADF',
'\u0AE0',   '\u0AE1',   '\u0AE2',   '\u0AE3',   '\u0AE4',   '\u0AE5',   '\u0AE6',   '\u0AE7',   '\u0AE8',   '\u0AE9',   '\u0AEA',   '\u0AEB',   '\u0AEC',   '\u0AED',   '\u0AEE',   '\u0AEF',
'\u0AF0',   '\u0AF1',   '\u0AF2',   '\u0AF3',   '\u0AF4',   '\u0AF5',   '\u0AF6',   '\u0AF7',   '\u0AF8',   '\u0AF9',   '\u0AFA',   '\u0AFB',   '\u0AFC',   '\u0AFD',   '\u0AFE',   '\u0AFF',
            ' ', '\n', '\r','.',',','0','1','2','3','4','5','6','7','8','9','(',')','[',']','{','}','"','?',';','!','%','-'

                                        };

        public static readonly int[] cht = new int[]
        {
       0,   4,  4,  4,  0,  2,  3,  3,  3,  3,  3,  1,  1,  3,  0,  3,
3,  3,  0,  3,  3,  2,  2,  2,  2,  1,  2,  2,  2,  2,  1,  2,
2,  2,  2,  2,  2,  2,  2,  2,  2,  0,  2,  2,  2,  2,  2,  2,
2,  0,  2,  2,  0,  2,  2,  2,  2,  2,  0,  0,  4,  1,  4,  4,
4,  4,  4,  4,  4,  4,  0,  4,  4,  4,  0,  4,  4,  6,  0,  0,
1,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,
1,  1,  1,  1,  0,  0,  5,  5,  5,  5,  5,  5,  5,  5,  5,  5,
1,  1,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,7,13,10,8,9,5,5,5,5,5,5,5,5,5,5,11,11,11,11,11,11,12,12,12,12,14,12

        };

        

        

        
        public MainWindow()
        {
            
            InitializeComponent();
            MainList.ItemsSource = StringHelper.Chand;
            MainList.SelectedIndex = 0;
            

            
            MyStructureString.Structure = StringHelper.ExpandGanSutra(((KeyValuePair<string,string>)MainList.SelectedItem).Value);
            
            txt.Document.Blocks.Add(myParagraph);

        }

        private void txt_TextChanged(object sender, TextChangedEventArgs e)
        {
          


          

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
           // TextRange rng = new TextRange(txt.Document.ContentStart, txt.Document.ContentEnd);


           // string r = rng.Text.TrimEnd();
           // // var ab = string.Join("-",StringHelper.SplitSylable(r));
           // var str = StringHelper.SplitSylable(r).ToArray();
           // var bl = StringHelper.LaghuGuru(r).ToArray();
           //// MessageBox.Show(string.Join("-", str) + "\n" + StringHelper.LuguGuruBinaryString(r.TrimEnd()));

           // TextPointer pnt = txt.Document.ContentStart;
           // pnt.GetPositionAtOffset(1);
           // txt.Document.Blocks.Clear();
           // Paragraph myParagraph = new Paragraph();
           // bool k = false;

           // List<Span> sp = new List<Span>();
           // //sp.Add(new Span(new Run(StringHelper.Matra(r).ToString() + "-")));
           // for(int p=0;p<str.Length;p++)
           // {
           //     sp.Add(new Span(new Run(str[p].Trim(new char[] {' '}))));

           //     switch (bl[p])
           //     {
           //         case SylableType.Guru:
           //             sp[p].Background = Brushes.LightPink;
           //             break;
           //         case SylableType.Lagu:
           //             sp[p].Background = Brushes.LightCyan;
           //             break;
           //         case SylableType.Other:
           //             sp[p].Background = Brushes.Transparent;
           //             break;
           //         default:
           //             break;
           //     }

               

           // }

           // myParagraph.Inlines.AddRange(sp

               
           // );
            

           // txt.Document.Blocks.Add(myParagraph);
            


        }

        public void showlist()
        {
            LBox.ItemsSource = MyStructureString.GetWords(words);
            if (LBox.Items.Count > 0)
            {
               
                LBox.Visibility = Visibility.Visible;
            }
        }

        private void LBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            

        }

        private void txt_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            
            RichTextBox R = sender as RichTextBox;
            
            
            var position = R.CaretPosition.GetCharacterRect(LogicalDirection.Forward);


            if (LBox != null)
            {
                LBox.SetValue(Canvas.LeftProperty, position.X);
                LBox.SetValue(Canvas.TopProperty, position.Y + 25);
            }

            switch (e.Key)
            {
                case Key.Space:
                    showlist();
                    e.Handled = true;
                   // LBox.Focus();
                    break;
                case Key.Up:
                    if (LBox.Visibility == Visibility.Visible)
                    {
                        
                        LBox.SelectedIndex = (LBox.SelectedIndex < 1) ? 0 : LBox.SelectedIndex - 1;
                        
                        
                    }
                   e.Handled = true;
                    
                    break;
                case Key.Down:
                    if (LBox.Visibility==Visibility.Visible)
                    {
                        
                        LBox.SelectedIndex = (LBox.Items.Count == LBox.SelectedIndex + 1) ? LBox.Items.Count - 1 : LBox.SelectedIndex + 1;
                        
                    }
                       e.Handled = true;
                   
                    break;
                    //LBox.Focus();

                    
                case Key.Enter:
                    if (MyStructureString.IsComplete)
                    {
                        myParagraph = new Paragraph();
                        txt.Document.Blocks.Add(myParagraph);
                        MyStructureString.Clear();
                       
                        //txt.CaretPosition = txt.CaretPosition.DocumentEnd;
                        e.Handled = true;  
                    }
                    else
                    { e.Handled = true; }
                    break;
                case Key.Tab:
                    if (LBox.SelectedIndex >= 0)
                    {
                        MyStructureString.Append(LBox.SelectedItem.ToString().TrimEnd());
                        myParagraph.Inlines.Clear();
                        myParagraph.Inlines.AddRange(MyStructureString.Spans);
                        txt.CaretPosition = txt.CaretPosition.DocumentEnd;
                        
                        LBox.SelectedItem = null;
                      //  txt.Focus();
                        LBox.Visibility = Visibility.Hidden;

                        // txt.Focus();
                       
                        
                    }
                    e.Handled = true;
                    break;


                case Key.Escape:
                    LBox.Visibility = Visibility.Hidden;
                    break;
                case Key.Back:
                    LBox.Visibility = Visibility.Hidden;
                    e.Handled = true;
                    string cur = (new TextRange(txt.Document.ContentStart, txt.Document.ContentEnd).Text).TrimEnd();
                    if (cur != string.Empty)
                    {
                        cur = cur.Substring(0, cur.Length - 1);
                        MyStructureString.Clear();
                        MyStructureString.Append(cur);
                        myParagraph.Inlines.Clear();
                        myParagraph.Inlines.AddRange(MyStructureString.Spans);
                        txt.CaretPosition = txt.CaretPosition.DocumentEnd;
                    }
                    break;
                
                  
                default:
                    //txt.Focus();
                    showlist();
                    e.Handled = true;
                    break;
            }

            //e.Handled = true;
                
        }

        private void LBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // MessageBox.Show(LBox.SelectedItem.ToString());
           
            
            
        }

        private void LBox_PreviewKeyDown_1(object sender, KeyEventArgs e)
        {
            
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(StringHelper.ExpandGanSutra("યમનગાગાગાગા"));
            
        }

        private void MainList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            MyStructureString.Clear();
            MyStructureString.Structure= StringHelper.ExpandGanSutra(((KeyValuePair<string, string>)MainList.SelectedItem).Value);
        }
    }
}
