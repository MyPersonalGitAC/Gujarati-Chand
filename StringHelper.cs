using System;
using System.Collections.Generic;
using System.Linq;

using System.Text.RegularExpressions;
using System.Windows.Documents;
using System.Windows.Media;
using System.IO;

using System.Windows;


namespace WpfApp3
{

    public enum SylableType
    {
        Lagu,
        Guru,
        Other
    }
    static class StringHelper

    {

        public static IEnumerable<string> ReadFile(string filename)
        {
            IEnumerable<string> data = null;
            try
            {


                using (var file = new StreamReader(filename))
                {
                    data = file.ReadToEnd().Split('\n').Select(x => x.TrimEnd().TrimStart().Trim());
                    data.Union(StringHelper.Gan.Values);

                }
            }
            catch (Exception ex)
            {

                MessageBox.Show("Words.txt not found:" + ex.Message);
            }

            return data;
        }
        public static char[] Lagu = { };
        public static char[] Guru = {'\u0A81', '\u0A83', '\u0A86', '\u0A88', '\u0A8A', '\u0A8D', '\u0A8F', '\u0A90', '\u0A91', '\u0A93', '\u0A94', '\u0ABE', '\u0AC0', '\u0AC2', '\u0AC5', '\u0AC7', '\u0AC8', '\u0AC9', '\u0ACB', '\u0ACC'
 };
        public static readonly Regex pattern = new Regex(@"([\p{Lo}]{1}[\p{Mn}\p{Mc}]+|[\p{Lo}]{1})");

        public static IDictionary<String, String> Chand = new Dictionary<String, String>(){
            { "શિખરિણી(યતી 6 અથવા 12)", "યમનસભલગા" },
            { "પૃથ્વી(યતી 8 અથવા 9)", "જસજસયલગા" },
            { "મંદાક્રાન્તા(યતી 4 અથવા 10)", "મભનતતગાગા" },
            {"શાર્દૂલવિક્રીડિત(યતી 7 અથવા 12)","મસજસતતગા" },
            {"સ્ત્રગ્ધરા(યતી 7,14,21)", "મરભનયયય" },
            {"માલિની(યતી 8)","નનમયય" },
            {"વસંત તલિકા(યતી 8)","તભજજગાગા" },
            {"હરિણી(યતી 12 અથવા 19)","નસમરસલગા" },
            {"વંશસ્થ(યતી 5)","જતજર" },
            {"ઇન્દ્રવજ્રા", "તતજગાગા" },
            {"ઉપેન્દ્રવજ્રા","જતજગાગા" },
            {"ચામર(યતી 8)","રજરજર" },
            {"તોટક(યતી:દરેક ચરણને અંતે)","સસસસ" },
            {"શાલિની(યતી 12,19)","મતતગાગા"}

        };

        public static IDictionary<string, string> Gan = new Dictionary<string, string>()
        {

            {"ય","યમાતા" },
            {"મ","માતારા" },
            {"ત","તારાજ" },
            {"ર","રાજભા" },
            {"જ","જભાન" },
            {"ભ","ભાનસ" },
            {"ન","નસલ" },
            {"સ","સલગા" },
            {"લ","લ" },
            {"ગા","ગા" },


        };

        public static string ExpandGanSutra(string input)
        {
            return string.Join("", SplitSylable(input).Select(x => Gan[x]).ToArray()).TrimStart().TrimEnd();
        }

        public static IEnumerable<string> SplitSylable(string input, out int[] positions)
        {
            var matches = pattern.Matches(input);
            List<int> pos = new List<int>();
            for (int i = 0; i < matches.Count; i++)
            {
                if (matches[i].Success & matches[i].Length > 0)
                    pos.Add(matches[i].Index);
            }
            positions = pos.ToArray();
            return pattern.Split(input).Where(x => (x.Length > 0 ? true : false));



        }

        public static IEnumerable<string> SplitSylable(string input)
        {
            var list = pattern.Split(input).Where(x => (x.Length > 0 ? true : false)).ToArray();
            List<string> stk = new List<string>();
            //return pattern.Split(input).Where(x => (x.Length > 0 ? true : false));

            for (int i = 0; i < list.Length; i++)
            {
                if (list[i].EndsWith("\u0ACD"))
                { stk.Add(list[i]); }
                else
                {

                    yield return string.Concat(stk) + list[i];
                    stk.Clear();
                }

            }


        }

        public static IEnumerable<SylableType> LaghuGuru(string input)
        {
            string[] sylable = SplitSylable(input).ToArray();
            int length = sylable.Length;

            for (int i = 0; i < length; i++)
            {

                // if (sylable[i].EndsWith("\u0ACD") || sylable[i] == " ")
                if (sylable[i].IndexOfAny(" .?/{}()abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ,;'\"\t".ToCharArray())>=0)
                { yield return SylableType.Other; }
                else if ((sylable[i].IndexOfAny(Guru)) >= 0)
                {

                    yield return SylableType.Guru;
                }
                else if (sylable[i].Contains("\u0A82"))
                {
                    if (i < length - 1 && sylable[i + 1] != " ")
                        yield return SylableType.Guru;
                    else
                        yield return SylableType.Lagu;
                }
                else if (i < length - 1)
                {
                    //  if (sylable[i].EndsWith("\u0A82") && sylable[i + 1] != " ")
                    //    yield return true;

                    if (sylable[i + 1].Contains("\u0ACD"))
                    {
                        yield return SylableType.Guru;
                        // yield return false;
                    }
                    else
                    {
                        yield return SylableType.Lagu;
                    }



                }
                else if (i == length - 1)
                    yield return SylableType.Lagu;


                else
                    yield return SylableType.Other;
            }

        }

        public static IEnumerable<SylableType> OnlyLughuGuru(string input)
        {
            return LaghuGuru(input).Where(x => (x != SylableType.Other));
        }
        public static int Matra(string input)
        {
            return LaghuGuru(input).Select(new Func<SylableType, int>(

                x =>
                {
                    switch (x)
                    {
                        case SylableType.Lagu:
                            return 1;
                        case SylableType.Guru:
                            return 2;
                        case SylableType.Other:
                            return 0;
                        default:
                            return 0;
                    }
                }


                )).Sum();
        }
        public static string LuguGuruBinaryString(string input)
        {

            return new string((LaghuGuru(input).Select(new Func<SylableType, char>
                (
                x =>
                {

                    switch (x)
                    {
                        case SylableType.Lagu:
                            return '0';

                        case SylableType.Guru:
                            return '1';

                        case SylableType.Other:
                            return '.';

                        default:
                            return '-';


                    }

                })
                )).ToArray());

        }

        public struct Locators
        {
            public Type ElementType;
            public TextPointer Pointer;

        }

        public static IEnumerable<Locators> ScanDoc(FlowDocument Doc, TextPointer Start, TextPointer End)
        {

            var pointer = (Start.Paragraph == null) ? Start : Start.Paragraph.ContentStart;
            


            while (End.CompareTo(pointer) > 0)
            {
                switch (pointer.Parent)
                {
                    case Run P:
                        if (P.ContentEnd.CompareTo(pointer) == 0)
                        {

                            yield return new Locators { ElementType = P.GetType(), Pointer = pointer };
                        }
                        if (P.ContentStart.CompareTo(pointer) == 0)
                        {
                            yield return new Locators { ElementType = P.GetType(), Pointer = pointer };

                        }
                        break;
                    case Paragraph P:
                        if (P.ContentEnd.CompareTo(pointer) == 0)
                        {

                            yield return new Locators { ElementType = P.GetType(), Pointer = pointer };
                        }
                        if (P.ContentStart.CompareTo(pointer) == 0)
                        {
                            yield return new Locators { ElementType = P.GetType(), Pointer = pointer };

                        }
                        break;
                    case Span P:
                        if (P.ContentEnd.CompareTo(pointer) == 0)
                        {

                            yield return new Locators { ElementType = P.GetType(), Pointer = pointer };
                        }
                        if (P.ContentStart.CompareTo(pointer) == 0)
                        {
                            yield return new Locators { ElementType = P.GetType(), Pointer = pointer };

                        }
                        break;
                    default:
                        break;
                }

                pointer = pointer.GetNextContextPosition(LogicalDirection.Forward);
                if (pointer == null) break;
            }


        }

    }

    public class StructuredString
    {

        public String Structure { get; set; }

        public SylableType[] StructurePattern {

            get
            {
                return StringHelper.OnlyLughuGuru(Structure.TrimEnd()).ToArray();
            }
        }

        public string Content { get;set;}
        public IEnumerable<SylableType> Filter
        {
            get
            {

                return StructurePattern.Skip(StringHelper.OnlyLughuGuru(Content.ToString()).Count());

            }
        }

        //public void Append(string input)
        //{
        //    if (Content.Length != 0)
        //        Content.Append(" ");
        //    Content.Append(input);
        //}

        //public void Clear()
        //{ Content.Clear(); }

        public void RemoveOne()
        {
            if (Strs.Any())
            {
                int temp = Strs.Last().Length;
                Content.Remove(Content.Length - temp, temp);
            }
        }
        public StructuredString()
        {
            Structure = String.Empty;
           // MyParagraph = new Paragraph();
            
           // MyParagraph.KeyDown += MyParagraph_KeyDown;

        }

        public IEnumerable<(int,int,bool,string)> ErrorLocation {

            get {
                int l = Sbl.Count();
                int m1=0,m2 = 0;
                int j = 0;
                SylableType[] arr = Sbl.ToArray();
                String[] lst = this.Strs.ToArray();
                for (int i = 0;(i < l); i++)
                {
                    m2 = m1 + lst[i].Length;

                    if (arr[i] == SylableType.Other)
                        yield return (m1, m2, true,lst[i]);
                    else 
                    {
                        if (j < StructurePattern.Length)
                        {
                            yield return (m1, m2, StructurePattern[j] == arr[i], lst[i]);
                            j = j + 1;
                        }
                        else
                            yield return (m1, m2, false, lst[i]);
                    }
                    
                    m1 = m2;
                }

                    }
                }

        public IEnumerable<string> Strs { get { return StringHelper.SplitSylable(Content.ToString()); } }
        public IEnumerable<SylableType> Sbl { get { return StringHelper.LaghuGuru(Content.ToString()); } }

        //public IEnumerable<Span> Spans
        //{
        //    get
        //    {

        //        var str = Strs.ToArray();
        //        var bl = Sbl.ToArray();
        //        List<Span> sp = new List<Span>();
        //        for (int p = 0; p < str.Length; p++)
        //        {

        //            sp.Add(new Span(new Run(str[p])));



        //            switch (bl[p])
        //            {
        //                case SylableType.Guru:
        //                    sp[p].Background = Brushes.LightPink;
        //                    break;
        //                case SylableType.Lagu:
        //                    sp[p].Background = Brushes.LightCyan;
        //                    break;
        //                case SylableType.Other:
        //                    sp[p].Background = Brushes.Transparent;
        //                    break;
        //                default:
        //                    break;
        //            }



        //        }

        //        return sp;
        //    }
        //}

        public IEnumerable<string> GetWords(IEnumerable<string> Words, IEnumerable<SylableType> Sorter)
        {
            return Words.Where(new Func<string, bool>(

            x =>
            {
                var u = StringHelper.OnlyLughuGuru(x);

                return Sorter.Take(u.Count()).SequenceEqual(u);
            }


            )).OrderBy(y => y).Distinct();
        }

        public bool IsComplete
        {
            get
            {
                return (Filter.Count() == 0);
            }
        }

        //public static Paragraph MyParagraph { get; set; }

        //public static  IEnumerable<string> Lines {
        //    get{

        //        return string.Join("", MyParagraph.Inlines.Where(x => (x.GetType() == typeof(Run))).Select(x => ((Run)x).Text)).Split(',').Where(x=>x.Length>0);

                

        //         }                 }

    }

    public static class SyntaxHilighter
    {
     public  struct Tag
        {
            public TextPointer StartPosition;
            public TextPointer EndPosition;
            public SylableType SType;
            public string Word;
        }

        public static List<Tag> CheckWordsInRun(Run theRun) //do not hightlight keywords in this method
        {
            List<Tag> m_tags = new List<Tag>();
            //How, let's go through our text and save all tags we have to save.               

            string text = theRun.Text;
            var lg = StringHelper.LaghuGuru(text);
            var wd = StringHelper.SplitSylable(text);
            int i = 0;
            int addedlength = 0;
            foreach (var item in wd)
            {
                Tag t = new Tag();
                t.StartPosition = theRun.ContentStart.GetPositionAtOffset(addedlength, LogicalDirection.Forward);
                t.EndPosition = theRun.ContentStart.GetPositionAtOffset(item.Length +addedlength, LogicalDirection.Forward);
                t.Word = item;
                t.SType = lg.ElementAt(i);
                m_tags.Add(t);
                addedlength = addedlength + item.Length;
                i++;

            }

            return m_tags;

            
            
        }

        public static void FormatDocument(FlowDocument Doc, TextPointer Start, TextPointer End)
        {

            


            if (Doc == null || Start == null || End == null)
                return;

            string text;
            List<SyntaxHilighter.Tag> m_tags = new List<SyntaxHilighter.Tag>();
            
            TextRange documentRange = new TextRange(Start, End);
            
            documentRange.ClearAllProperties();
           

            
            TextPointer navigator = Start;

            while (navigator.CompareTo(End) < 0)
            {
                TextPointerContext context = navigator.GetPointerContext(LogicalDirection.Backward);
                if (context == TextPointerContext.ElementStart && navigator.Parent is Run)
                {
                    text = ((Run)navigator.Parent).Text; //fix 2
                    if (text != "")
                    {
                        m_tags.AddRange(SyntaxHilighter.CheckWordsInRun((Run)navigator.Parent));


                    }


                }

                navigator = navigator.GetNextContextPosition(LogicalDirection.Forward);
            }

            //only after all keywords are found, then we highlight them
            for (int i = 0; i < m_tags.Count; i++)
            {
                try
                {
                    TextRange range = new TextRange(m_tags[i].StartPosition, m_tags[i].EndPosition);

                    switch (m_tags[i].SType)
                    {
                        case SylableType.Lagu:
                            range.ApplyPropertyValue(TextElement.ForegroundProperty, new SolidColorBrush(Colors.DarkCyan));
                            break;
                        case SylableType.Guru:
                            range.ApplyPropertyValue(TextElement.ForegroundProperty, new SolidColorBrush(Colors.Blue));
                            range.ApplyPropertyValue(TextElement.FontWeightProperty, FontWeights.Bold);
                            break;
                        case SylableType.Other:
                            break;
                        default:
                            break;
                    }


                }
                catch { }
            }


        }


        public static void CheckGrammer(FlowDocument Doc, TextPointer Start, TextPointer End, StructuredString Rule)
        {




            if (Doc == null || Start == null || End == null || Rule==null)
                return;

            (new TextRange(Start, End)).ClearAllProperties();

            var Pointers=StringHelper.ScanDoc(Doc, Start, End).ToList();
            
            if(Pointers.Any())
            {
                if(Pointers.First().ElementType!=typeof(Paragraph))
                    if(Pointers.First().Pointer.Paragraph!=null)
                    {
                        var pr = new StringHelper.Locators();
                        pr.ElementType = typeof(Paragraph);
                        pr.Pointer = Pointers.First().Pointer.Paragraph.ContentStart;

                        Pointers.Prepend(pr);
                    }

                if (Pointers.Last().ElementType != typeof(Paragraph))
                    if (Pointers.Last().Pointer.Paragraph != null)
                    {
                        var er = new StringHelper.Locators();
                        er.ElementType = typeof(Paragraph);
                        er.Pointer = Pointers.Last().Pointer.Paragraph.ContentEnd;

                        Pointers.Add(er);
                    }
            }
            bool open=false;
            List<SyntaxHilighter.Tag> m_tags=new List<Tag>();
            string text;
            foreach (var item in Pointers)
            {
               if(item.ElementType==typeof(Paragraph))
                {
                    if (item.Pointer.GetPointerContext(LogicalDirection.Forward) == TextPointerContext.ElementStart)
                    {
                        open = true;
                        m_tags = new List<Tag>();
                        text = "";
                    }
                    else
                    {
                        open = false;
                        m_tags = m_tags.Where(x => x.SType != SylableType.Other).ToList();

                        
                        for (int i = 0; i < m_tags.Count; i++)
                        {
                            try
                            {
                                TextRange range = new TextRange(m_tags[i].StartPosition, m_tags[i].EndPosition);
                                
                                switch (m_tags[i].SType)
                                {
                                    case SylableType.Lagu:
                                        range.ApplyPropertyValue(TextElement.ForegroundProperty, new SolidColorBrush(Colors.DarkCyan));
                                        if (i >= Rule.StructurePattern.Length)
                                            range.ApplyPropertyValue(TextElement.BackgroundProperty, new SolidColorBrush(Colors.Plum));
                                        else if (m_tags[i].SType != Rule.StructurePattern[i])
                                            range.ApplyPropertyValue(TextElement.BackgroundProperty, new SolidColorBrush(Colors.Plum));
                                        break;
                                    case SylableType.Guru:
                                        range.ApplyPropertyValue(TextElement.ForegroundProperty, new SolidColorBrush(Colors.Blue));
                                        range.ApplyPropertyValue(TextElement.FontWeightProperty, FontWeights.Bold);
                                        if (i >= Rule.StructurePattern.Length)
                                            range.ApplyPropertyValue(TextElement.BackgroundProperty, new SolidColorBrush(Colors.Plum));
                                        else if (m_tags[i].SType != Rule.StructurePattern[i])
                                            range.ApplyPropertyValue(TextElement.BackgroundProperty, new SolidColorBrush(Colors.Plum));
                                        break;

                                    default:
                                        break;



                                    

                                }


                            }
                            catch { }
                        }

                    }
                }
               else if(item.ElementType==typeof(Run) && open && item.Pointer.GetPointerContext(LogicalDirection.Forward)==TextPointerContext.Text )
                {
                    text = ((Run)item.Pointer.Parent).Text;
                    if (text != "")
                    {
                        Rule.Content += text;
                        m_tags.AddRange(SyntaxHilighter.CheckWordsInRun((Run)item.Pointer.Parent));
                    }
                }
            }

            
             
           


            // var ranges = err.Where(a=>a.Item3==false).Select(x => new TextRange(((Run)St.Parent).ContentStart.GetPositionAtOffset(x.Item1,LogicalDirection.Backward), ((Run)St.Parent).ContentStart.GetPositionAtOffset(x.Item2,LogicalDirection.Forward)));

            //foreach (var item in ranges)
            //{

            //        item.ApplyPropertyValue(TextElement.BackgroundProperty, Brushes.Red);

            //}












        }

    }
}
