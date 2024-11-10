using System.Text;
using UnityEngine;

namespace CustomUtils
{
    public static class TextUtility
    {
        public static string GivePointColor(
            string keyword, string text, Color textColor)
        {
            string replacedDescription = text;

            #region colorCode Description
            ///<summary>
            ///<param name="colorCode">
            ///if color == Color.Red
            ///return colorCode = "FF0000"
            ///</param> 
            ///</summary>
            #endregion

            string colorCode = ColorUtility.ToHtmlStringRGB(textColor);

            replacedDescription = text.Replace(keyword,
                $"<color=#{colorCode}>{keyword}</color>");
            //스트링 문자열에 원래 키워드를 마크다운 문법을 통하여 색깔을 넣어준다.
            return replacedDescription;
        }

        public static string CombineTextWithEnter(string origin, string newText)
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine(origin).AppendLine(newText);

            return sb.ToString();
        }

        public static string CombineText(string origin, string newText)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append(origin).Append(newText);

            return sb.ToString();
        }
    }
}
