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
            IEnumerable<string> data=null;
            try
            {
                
                
                using (var file = new StreamReader(AppDomain.CurrentDomain.BaseDirectory+@"\"+filename))
                {
                    data=file.ReadToEnd().Split('\n').Select(x => x.TrimEnd().TrimStart().Trim());
                    data.Union(StringHelper.Gan.Values);
                    
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show("Words.txt not found:"+ex.Message);
            }
            finally
            {
                
            }
            return data;
        }
        public StructuredString MyStructureString=new StructuredString();
        Paragraph myParagraph = new Paragraph();

        
        static string[] words = ReadFile("words.txt").ToArray();//get all words from stored files

        
        public MainWindow()
        {
            
            InitializeComponent();
            MainList.ItemsSource = StringHelper.Chand;
            MainList.SelectedIndex = 0;
            LBox.ToolTip = (new ToolTip()).Content = "Select the Word first than click Tab to Insert";


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
            if (!LBox.Items.IsEmpty)
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
                    //string cur = (new TextRange(txt.Document.ContentStart, txt.Document.ContentEnd).Text).TrimEnd();
                    //if (cur != string.Empty)
                    {
                        // cur = cur.Substring(0, cur.Length - 1);
                        //MyStructureString.Clear();
                        //MyStructureString.Append(cur);
                        //myParagraph.Inlines.Clear();

                        MyStructureString.RemoveOne();
                        //MyStructureString.Append(String.Join(String.Empty, myParagraph.Inlines.Select(line => line.ContentStart.GetTextInRun(LogicalDirection.Forward))));
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
            if (e.Key == Key.Tab)
            {
                e.Handled = true;

                MyStructureString.Append(LBox.SelectedItem.ToString().TrimEnd());
                myParagraph.Inlines.Clear();
                myParagraph.Inlines.AddRange(MyStructureString.Spans);

                txt.CaretPosition = txt.CaretPosition.DocumentEnd;

                LBox.SelectedItem = null;

                //  txt.Focus();
                LBox.Visibility = Visibility.Hidden;
                LBox.MoveFocus(new TraversalRequest(FocusNavigationDirection.Previous));
            }

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
