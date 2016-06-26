using Littlenew_Text.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;

namespace Littlenew_Text
{
    /// <summary>
    /// 代码高亮
    /// </summary>
    class HightLightUtils
    {

        /// <summary>
        /// 获取所有内容
        /// </summary>
        /// <param name="document"></param>
        /// <returns></returns>
        public static IEnumerable<TextRange> GetAllWordRanges(FlowDocument document)
        {
            string pattern = @"[^\W\d](\w|[-']{1,2}(?=\w))*";
            //Paragraph para = CaretPosition.Paragraph;
            TextPointer pointer = document.ContentStart;
            while (pointer != null)
            {
                if (pointer.GetPointerContext(LogicalDirection.Forward) == TextPointerContext.Text)
                {
                    string textRun = pointer.GetTextInRun(LogicalDirection.Forward);
                    MatchCollection matches = Regex.Matches(textRun, pattern);
                    foreach (Match match in matches)
                    {
                        int startIndex = match.Index;
                        int length = match.Length;
                        TextPointer start = pointer.GetPositionAtOffset(startIndex);
                        TextPointer end = start.GetPositionAtOffset(length);
                        yield return new TextRange(start, end);
                    }
                }

                pointer = pointer.GetNextContextPosition(LogicalDirection.Forward);
            }
        }

        public static IEnumerable<TextRange> GetAllWordRanges1(TextPointer pointer)
        {
            string pattern = @"[^\W\d](\w|[-']{1,2}(?=\w))*";
            //Paragraph para = CaretPosition.Paragraph;
            //TextPointer pointer = document.ContentStart;
            while (pointer != null)
            {
                if (pointer.GetPointerContext(LogicalDirection.Forward) == TextPointerContext.Text)
                {
                    string textRun = pointer.GetTextInRun(LogicalDirection.Forward);
                    MatchCollection matches = Regex.Matches(textRun, pattern);
                    foreach (Match match in matches)
                    {
                        int startIndex = match.Index;
                        int length = match.Length;
                        TextPointer start = pointer.GetPositionAtOffset(startIndex);
                        TextPointer end = start.GetPositionAtOffset(length);
                        yield return new TextRange(start, end);
                    }
                }

                pointer = pointer.GetNextContextPosition(LogicalDirection.Forward);
            }
        }

        static bool flag = false;
        /// <summary>
        /// 刷新高亮内容
        /// </summary>
        /// <param name="rtbEditor"></param>
        public static void Refresh(RichTextBox rtbEditor)
        {
            TextPointer tp1 = rtbEditor.Selection.Start.GetLineStartPosition(0);
            TextPointer tp2 = rtbEditor.Selection.Start;

            TextRange range = new TextRange(tp1, tp2);
            Console.WriteLine(range.Text);


            IEnumerable<TextRange> wordRanges = GetAllWordRanges1(tp1);
            //IEnumerable<TextRange> wordRanges = 
            //List<TextRange> wordRanges = new List<TextRange>();
            //wordRanges.Add(range);

            HightLight hightlight = Constants.HightLights.Find(c => c.Language.ToLower().Equals("java"));
            BrushConverter brushConverter = new BrushConverter();
            
            foreach (TextRange wordRange in wordRanges)
            {
                flag = false;

                foreach (var item in hightlight.Keywords)
                {
                    if (item.Name.Equals(wordRange.Text))
                    {
                        wordRange.ApplyPropertyValue(TextElement.ForegroundProperty,
                            (Brush)brushConverter.ConvertFromString(item.Color));
                        flag = true;
                    }
                }
                // 如果不是关键字, 默认为白色
                if (!flag)
                {
                    wordRange.ApplyPropertyValue(TextElement.ForegroundProperty,Brushes.White);
                }
            }
        }
    }
}
