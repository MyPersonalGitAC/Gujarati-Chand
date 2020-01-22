using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Windows.Documents;
using System.Windows.Media;

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
        public static char[] Lagu= { };
        public static char[] Guru= {'\u0A81', '\u0A83', '\u0A86', '\u0A88', '\u0A8A', '\u0A8D', '\u0A8F', '\u0A90', '\u0A91', '\u0A93', '\u0A94', '\u0ABE', '\u0AC0', '\u0AC2', '\u0AC5', '\u0AC7', '\u0AC8', '\u0AC9', '\u0ACB', '\u0ACC'
 };
        public static readonly Regex pattern = new Regex(@"([\p{Lo}]{1}[\p{Mn}\p{Mc}]+|[\p{Lo}]{1})");

        public static IDictionary<String, String> Chand = new Dictionary<String,String>(){ 
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
            for(int i=0;i<matches.Count; i++)
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

            for(int i=0;i<list.Length;i++)
            {
                if (list[i].EndsWith("\u0ACD"))
                { stk.Add(list[i]); }
                else
                {
                    
                    yield return   string.Concat(stk)+ list[i];
                    stk.Clear();
                }

            }


        }

        public static IEnumerable<SylableType> LaghuGuru(string input)
        {
            string[] sylable = SplitSylable(input).ToArray();
            int length = sylable.Length;
          
            for(int i=0;i<length;i++)
            {

                // if (sylable[i].EndsWith("\u0ACD") || sylable[i] == " ")
                if (sylable[i] == " ")
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
               x => {

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

    }

    public class StructuredString
    {
        public String Structure { get; set; }
        private StringBuilder Content;
        public IEnumerable<SylableType> Filter
        {
            get {

               return  StringHelper.OnlyLughuGuru(Structure.TrimEnd()).Skip(StringHelper.OnlyLughuGuru(Content.ToString()).Count());

            }
        }

        public void Append(string input)
        {
            if (Content.Length != 0)
                Content.Append(" ");
            Content.Append(input);
        }

        public void Clear()
        { Content.Clear(); }
        public StructuredString()
        {
            Structure = String.Empty;
            Content = new StringBuilder();
            
        }

        public IEnumerable<Span> Spans
        {
            get {

                var str = StringHelper.SplitSylable(Content.ToString()).ToArray();
                var bl = StringHelper.LaghuGuru(Content.ToString()).ToArray();
                List<Span> sp = new List<Span>();
                for (int p = 0; p < str.Length; p++)
                {
                    
                    sp.Add(new Span(new Run(str[p])));

                    

                    switch (bl[p])
                    {
                        case SylableType.Guru:
                            sp[p].Background = Brushes.LightPink;
                            break;
                        case SylableType.Lagu:
                            sp[p].Background = Brushes.LightCyan;
                            break;
                        case SylableType.Other:
                            sp[p].Background = Brushes.Transparent;
                            break;
                        default:
                            break;
                    }

                    

                }

                return sp;
            }
        }

        public IEnumerable<string> GetWords(IEnumerable<string> Words)
        {
            return Words.Where(new Func<string, bool>(

            x =>
            {
                var u = StringHelper.LaghuGuru(x);

                return Filter.Take(u.Count()).SequenceEqual(u);
            }


            )).OrderBy(y => y).Distinct() ;
        }

        public bool IsComplete
        {
            get
            {
              return(Filter.Count() == 0);
            }
        }
    }
}
