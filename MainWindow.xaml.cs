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
using System.Diagnostics;

namespace WpfApp3
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
      
        public StructuredString MyStructureString=new StructuredString();
        
        
        static string[] words;

        public MainWindow()
        {
            
            InitializeComponent();
            words = StringHelper.ReadFile("Words.txt").ToArray();//get all words from stored files
            MainList.ItemsSource = StringHelper.Chand;
            MainList.SelectedIndex = 0;
            LBox.ToolTip = (new ToolTip()).Content = "Select the Word first than click Tab to Insert";
            MyStructureString.Structure = StringHelper.ExpandGanSutra(((KeyValuePair<string,string>)MainList.SelectedItem).Value);
            //txt.Document.Blocks.Add(StructuredString.MyParagraph);
            
            
        }

       

        private void Button_Click(object sender, RoutedEventArgs e)
        {
          

        }

        private void T_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            throw new NotImplementedException();
        }

        public void Showlist()
        {
            
            if (!LBox.Items.IsEmpty)
            {
               
                LBox.Visibility = Visibility.Visible;
                
                
            }
        }

        private void txt_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            var position = txt.CaretPosition.GetCharacterRect(LogicalDirection.Forward);

            

            if (LBox != null)
            {
                LBox.SetValue(Canvas.LeftProperty, position.X);
                LBox.SetValue(Canvas.TopProperty, position.Y + 25);
            }

            //if ((Control.ModifierKeys & Keys.Shift) == Keys.Shift)
            //{
            //    MessageBox.Show("Pressed " + Keys.Shift);
            //}

            if (e.KeyboardDevice.IsKeyDown(Key.LeftCtrl)|| e.KeyboardDevice.IsKeyDown(Key.RightCtrl))
            {
                if(e.Key==Key.F)
                {
                    if (txt.Selection.Text != "")
                    {
                        LBox.ItemsSource = MyStructureString.GetWords(words, StringHelper.OnlyLughuGuru(txt.Selection.Text));
                        Showlist();
                    }
                    e.Handled = true;
                }
            }

            if(e.Key==Key.Escape)
            {
                LBox.SelectedItem = null;
                LBox.Visibility = Visibility.Hidden;
            }
            //switch (e.Key)
            //{
            //    case Key.Space:
            //        Showlist();
            //        e.Handled =false;
            //        break;
            //    case Key.Up:
            //        if (LBox.Visibility == Visibility.Visible)
            //        {
                        
            //            LBox.SelectedIndex = (LBox.SelectedIndex < 1) ? 0 : LBox.SelectedIndex - 1;
                        
                        
            //        }
            //       e.Handled = true;
                    
            //        break;
            //    case Key.Down:
            //        if (LBox.Visibility==Visibility.Visible)
            //        {
                        
            //            LBox.SelectedIndex = (LBox.Items.Count == LBox.SelectedIndex + 1) ? LBox.Items.Count - 1 : LBox.SelectedIndex + 1;
                        
            //        }
            //           e.Handled = true;
                   
            //        break;
                   

                    
            //    case Key.Enter:
            //        if (MyStructureString.IsComplete)
            //        {
            //            //myParagraph = new Paragraph();
            //            //txt.Document.Blocks.Add(myParagraph);
            //            //StructuredString.MyParagraph.Inlines.Add(new LineBreak(StructuredString.MyParagraph.ContentEnd));
            //            //StructuredString.MyParagraph.ElementEnd.InsertLineBreak();
            //            e.Handled =true;
                        
            //        }
            //        else
            //        { e.Handled = true;

                        
            //        }
            //        break;
            //    case Key.Tab:
            //        if (LBox.SelectedIndex >= 0)
            //        {
                        
            //           // StructuredString.MyParagraph.Inlines.Add(new Run(LBox.SelectedItem.ToString()));
            //            // ((Run)myParagraph.Inlines.LastInline).Text = ((Run)myParagraph.Inlines.LastInline).Text + LBox.SelectedItem.ToString().TrimEnd();

                       
            //            txt.CaretPosition = txt.CaretPosition.DocumentEnd;
                        
            //            LBox.SelectedItem = null;
                      
            //            LBox.Visibility = Visibility.Hidden;

            //        }
            //        e.Handled = true;
            //        break;


            //    case Key.Escape:
            //        LBox.Visibility = Visibility.Hidden;
            //        break;
            //    case Key.Back:
            //        LBox.Visibility = Visibility.Hidden;
            //        e.Handled = false;
                    
            //        {
                        
            //            //MyStructureString.RemoveOne();
            //            //myParagraph.Inlines.Clear();
            //            //myParagraph.Inlines.AddRange(MyStructureString.Spans);
            //            //txt.CaretPosition = txt.CaretPosition.DocumentEnd;
                        
            //        }
            //        break;
                
                  
            //    default:
                    
            //        if(!LBox.IsVisible)
            //        Showlist();

                    
                   
                 
            //        e.Handled = false;
                    
            //        break;
            //}

            
                
        }

        private void LBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
           
           
            
        }

        private void LBox_PreviewKeyDown_1(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Tab)
            {
                e.Handled = true;

                //StructuredString.MyParagraph.Inlines.Add(new Run(LBox.SelectedItem.ToString()));


                txt.Selection.Text = LBox.SelectedItem.ToString();

                        

                LBox.SelectedItem = null;

                

                //  txt.Focus();
                LBox.Visibility = Visibility.Hidden;
                LBox.MoveFocus(new TraversalRequest(FocusNavigationDirection.Previous));
            }

            if (e.Key==Key.Escape)
            {
                LBox.SelectedItem = null;
                LBox.Visibility = Visibility.Hidden;
                e.Handled = true;
            }

        }

       

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            String HelpMsg = "ટેક્ષ બોક્ષ માં પંકતિ લખો\n સમાન શબ્દ સોધવા માટે શબ્દ ને હાઇ લાઇટ કરી Ctrl + F પ્રેસ કરો\n લિસ્ટમાં શબ્દ પસંદ કરી TAB દબાવો\n ઉપર થી છંદ પણ પસંદ કરી શકાય છે, જે આપમેળે ભુલ બતાવશે\n શબ્દકોશ \"Words.txt\" ફાઇલ માં છે, આ ફાઇલ માં નવા શબ્દ ઉમેરી શકાય છે    ";
            MessageBox.Show(HelpMsg);
        }

        

        private void MainList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
           
            MyStructureString.Structure = StringHelper.ExpandGanSutra(((KeyValuePair<string, string>)MainList.SelectedItem).Value);
            txt.TextChanged -= txt_TextChanged;
            SyntaxHilighter.CheckGrammer(txt.Document, txt.Document.ContentStart, txt.Document.ContentEnd, MyStructureString);
            txt.TextChanged += txt_TextChanged;
        

        }

        private void txt_TextChanged_1(object sender, TextChangedEventArgs e)
        {
            
          //  appendhelper();
            
        }

        private void txt_PreviewKeyUp(object sender, KeyEventArgs e)
        {
          


        }

        private void txt_TextChanged(object sender, TextChangedEventArgs e)
        {
            var txtStatus = sender as System.Windows.Controls.RichTextBox;
            
            if (txtStatus.Document == null)
                return;
            txtStatus.TextChanged -= txt_TextChanged;
            //  SyntaxHilighter.FormatDocument(txtStatus.Document, txt.Document.ContentStart, txt.Document.ContentEnd);




            TextChange Change=null;
            if (e.Changes.Any())
            {
                Change = e.Changes.OrderBy(x => x.Offset).First();
            }
            
               

            if(Change!=null)
            {

                    TextPointer St=null, En=null;

                if (txt.Document.ContentStart.GetPositionAtOffset(Change.Offset).Paragraph != null)
                    St = txt.Document.ContentStart.GetPositionAtOffset(Change.Offset).Paragraph.ContentStart;
                else if (txt.CaretPosition.GetNextContextPosition(LogicalDirection.Backward) != null)
                    St = txt.CaretPosition.GetNextContextPosition(LogicalDirection.Backward).Paragraph.ContentStart;
                else
                    St = txt.Document.ContentStart;

                

                   
                En = txt.CaretPosition.GetNextContextPosition(LogicalDirection.Forward);
                if (En == null)
                    En = txt.Document.ContentEnd;
                string temp = (new TextRange(St, En)).Text;
               
                
                blk.Text ="લઘુ અને ગુરુ: "+StringHelper.OnlyLughuGuru(temp).Count().ToString() +"\tમાત્રા: "+ StringHelper.Matra(temp).ToString();



                 //   SyntaxHilighter.FormatDocument(txt.Document,St,En);

                  SyntaxHilighter.CheckGrammer(txt.Document, St, En,MyStructureString);


                
            }

            txtStatus.TextChanged += txt_TextChanged;
        }
    
}
    }

