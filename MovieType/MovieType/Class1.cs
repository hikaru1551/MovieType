using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using CKIP;
using System.IO;
using System.Text.RegularExpressions;
using System.Collections;
using System.Threading;
using System.Net;
using System.Net.Sockets;

namespace MovieType
{    
    class Class1
    {
    }
    public abstract class Cws
    {
        ArrayList match_arylist = new ArrayList();   //用來儲存去除CKIP詞性的斷詞        
        protected string _OrginArticle;

        public string OrginArticle
        {
            get
            {
                return _OrginArticle;
            }

            set
            {
                CkipClient c_client = new CkipClient("h_s1551", "351551");
                int str_start = 0;//用來做輸出字串的起始位置            
                StringBuilder sb = new StringBuilder();//儲存CKIP轉換後第一階段的詞
                StringBuilder sb_2 = new StringBuilder();//儲存CKIP轉換後剩下的詞

                //由於斷字有上傳字數限制，故透過迴圈分批上傳319字
                while (value.Length >= 320) //如果字串長度大於320字，則分批上傳
                {
                    if (value.Length - str_start <= 319)//總字數如果扣除起始字數則跳出迴圈
                    {
                        break;
                    }
                    c_client.Send(value.Substring(str_start, 319));//傳送字串長度起始字數算起(加上)319字)

                    SegmentationResult result = c_client.GetResult();//client傳送的結果
                    if (result.StatusCode == StatusCode.Success)//如果傳送成功的話則輸出於richTextBox
                    {
                        //StringBuilder sb = new StringBuilder();
                        foreach (Term term in result.GetTerms())
                        {
                            sb.Append(term.ToString());
                        }
                        //Console.WriteLine(sb.ToString());
                        //richTextBox2.Text += sb.ToString();
                        str_start += 319;
                        //sb.Clear();
                        Thread.Sleep(5000);//每傳送一次則停止五秒鐘
                    }
                    else
                    {
                        MessageBox.Show(string.Format("Error: {0}", result.StatusDescription));
                    }
                }

                c_client.Send(value.Substring(str_start, value.Length - str_start));//用來傳送小於319字的字串或是剩餘字串
                SegmentationResult last_result = c_client.GetResult();
                foreach (Term term in last_result.GetTerms())
                {
                    sb_2.Append(term.ToString());
                }

                //以下Regex規則語法"(?:[^\(\)A-Z][\w]*)"，以" 中央社(N) "為例，(?:)為匹配pattern但不獲取匹配結果、[^\(\)A-Z]為匹配未包含的任意字元，如去除()及A-Z、[\w]匹配包括底線的任何單詞字元、*為匹配前面的子運算式零次或多次，故規則化結果為　中央社
                MatchCollection matches = Regex.Matches(sb.ToString() + sb_2.ToString(), @"(?:[^\(\)A-Z][\w]*)", RegexOptions.IgnoreCase);

                foreach (Match match in matches)
                {
                    //將符合的字串加入ArrayList中，並去除空白字元                     
                    match_arylist.Add(match.Value.Trim());
                }

                foreach (string str_1 in match_arylist)
                {
                    if (str_1 != "")
                    {
                        _OrginArticle += str_1.ToString() + "|";
                    }
                }
                _OrginArticle = _OrginArticle.Remove(_OrginArticle.Length - 1); //刪除最後一個"|"
                
                value = "";
                match_arylist.Clear();
            }
        }        
        
        //定義Cws抽象類別的Answer抽象方法
        public abstract string Answer();        
    }

    public class CwsWord : Cws
    { 
        public override string Answer()
        {        
            //經斷詞後的文章
            return _OrginArticle;               
        }
    }

    
    public class CwsType : Cws
    {
        string TypeResult = "";
        public override string Answer()
        {            
            string path = System.AppDomain.CurrentDomain.BaseDirectory;//獲取當前執行檔目錄位置並加"/"            

            //特別斷詞，斷詞只出現一次
            string o_funy = System.IO.File.ReadAllText(path + "WordWeights\\o_funy.txt",Encoding.Default);
            string[] funy_word = o_funy.Split('|');
            string o_action = System.IO.File.ReadAllText(path + "WordWeights\\o_action.txt", Encoding.Default);
            string[] action_word = o_action.Split('|');
            string o_science = System.IO.File.ReadAllText(path + "WordWeights\\o_science.txt", Encoding.Default);
            string[] science_word = o_science.Split('|');
            string o_scary = System.IO.File.ReadAllText(path + "WordWeights\\o_scary.txt", Encoding.Default);
            string[] scary_word = o_scary.Split('|');

            //斷詞重複兩次，S為科幻、a為動作、s(小寫)為懸疑、f為喜劇
            //為方便計算，故將各類的斷詞集合起來計分
            string f_2 = System.IO.File.ReadAllText(path + "WordWeights\\f_2.txt", Encoding.Default);
            string[] f_2_word = f_2.Split('|');
            string a_2 = System.IO.File.ReadAllText(path + "WordWeights\\a_2.txt", Encoding.Default);
            string[] a_2_word = a_2.Split('|');
            string S_2 = System.IO.File.ReadAllText(path + "WordWeights\\S_2.txt", Encoding.Default);
            string[] S_2_word = S_2.Split('|');
            string s_2 = System.IO.File.ReadAllText(path + "WordWeights\\sc_2.txt", Encoding.Default);
            string[] s_2_word = s_2.Split('|');

            //斷詞重複三次
            string f_3 = System.IO.File.ReadAllText(path + "WordWeights\\f_3.txt", Encoding.Default);
            string[] f_3_word = f_3.Split('|');
            string a_3 = System.IO.File.ReadAllText(path + "WordWeights\\a_3.txt", Encoding.Default);
            string[] a_3_word = a_3.Split('|');
            string S_3 = System.IO.File.ReadAllText(path + "WordWeights\\S_3.txt", Encoding.Default);
            string[] S_3_word = S_3.Split('|');
            string s_3 = System.IO.File.ReadAllText(path + "WordWeights\\sc_3.txt", Encoding.Default);
            string[] s_3_word = s_3.Split('|');

                        
            int Weight_score = 0; //計算總權重分數
            int max_score = 0; //計算得分最高的類別
            int funy_score = 0, action_score = 0, science_score = 0, scary_score = 0;
            int f_1_score = 0, a_1_score = 0, S_1_score = 0, s_1_score = 0;
            int f_2_score = 0, a_2_score = 0, S_2_score = 0, s_2_score = 0;
            int f_3_score = 0, a_3_score = 0, S_3_score = 0, s_3_score = 0;

            string[] str = _OrginArticle.Split('|');
            
                        
            for (int i = 0; i < str.Count(); i++)
            {
                #region 喜劇計分
                //特別斷詞
                for (int k = 0; k < funy_word.Count(); k++)
                {
                    if (str[i].Length == funy_word[k].Length)
                    {
                        if (str[i].Contains(funy_word[k]))
                        {
                            funy_score += 3;
                            f_1_score += 1;
                            TypeResult += str[i] + ",";
                        }
                    }
                }
                //喜劇權重2
                for (int k = 0; k < f_2_word.Count(); k++)
                {
                    if (str[i].Length == f_2_word[k].Length)
                    {
                        if (str[i].Contains(f_2_word[k]))
                        {
                            funy_score += 2;
                            f_2_score += 1;
                            //richTextBox2.Text += str[i] + ",";
                        }
                    }
                }
                //喜劇權重3
                for (int k = 0; k < f_3_word.Count(); k++)
                {
                    if (str[i].Length == f_3_word[k].Length)
                    {
                        if (str[i].Contains(f_3_word[k]))
                        {
                            funy_score += 1;
                            f_3_score += 1;
                            //richTextBox2.Text += str[i] + ",";
                        }
                    }
                }
                #endregion

                #region 動作計分
                //特別斷詞
                for (int a = 0; a < action_word.Count(); a++)
                {
                    if (str[i].Length == action_word[a].Length)
                    {
                        if (str[i].Contains(action_word[a]))
                        {
                            action_score += 3;
                            a_1_score += 1;
                            TypeResult += str[i] + ",";
                        }
                    }
                }
                //動作權重2
                for (int k = 0; k < a_2_word.Count(); k++)
                {
                    if (str[i].Length == a_2_word[k].Length)
                    {
                        if (str[i].Contains(a_2_word[k]))
                        {
                            action_score += 2;
                            a_2_score += 1;
                            //richTextBox2.Text += str[i] + ",";
                        }
                    }
                }
                //動作權重3
                for (int k = 0; k < a_3_word.Count(); k++)
                {
                    if (str[i].Length == a_3_word[k].Length)
                    {
                        if (str[i].Contains(a_3_word[k]))
                        {
                            action_score += 1;
                            a_3_score += 1;
                            //richTextBox2.Text += str[i] + ",";
                        }
                    }
                }
                #endregion

                #region 科幻計分
                //特別斷詞
                for (int b = 0; b < science_word.Count(); b++)
                {
                    if (str[i].Length == science_word[b].Length)
                    {
                        if (str[i].Contains(science_word[b]))
                        {
                            science_score += 3;
                            S_1_score += 1;
                            TypeResult += str[i] + ",";                            
                        }
                    }
                }
                //科幻權重2
                for (int k = 0; k < S_2_word.Count(); k++)
                {
                    if (str[i].Length == S_2_word[k].Length)
                    {
                        if (str[i].Contains(S_2_word[k]))
                        {
                            science_score += 2;
                            S_2_score += 1;
                            //richTextBox2.Text += str[i] + ",";
                        }
                    }
                }
                //科幻權重3
                for (int k = 0; k < S_3_word.Count(); k++)
                {
                    if (str[i].Length == S_3_word[k].Length)
                    {
                        if (str[i].Contains(S_3_word[k]))
                        {
                            science_score += 1;
                            S_3_score += 1;
                            //richTextBox2.Text += str[i] + ",";
                        }
                    }
                }
                #endregion

                #region 懸疑計分
                //特別斷詞
                for (int b = 0; b < scary_word.Count(); b++)
                {
                    if (str[i].Length == scary_word[b].Length)
                    {
                        if (str[i].Contains(scary_word[b]))
                        {
                            scary_score += 3;
                            s_1_score += 1;
                            TypeResult += str[i] + ",";
                        }
                    }
                }
                //懸疑權重2
                for (int k = 0; k < s_2_word.Count(); k++)
                {
                    if (str[i].Length == s_2_word[k].Length)
                    {
                        if (str[i].Contains(s_2_word[k]))
                        {
                            scary_score += 2;
                            s_2_score += 1;
                            //richTextBox2.Text += str[i] + ",";
                        }
                    }
                }
                //懸疑權重3
                for (int k = 0; k < s_3_word.Count(); k++)
                {
                    if (str[i].Length == s_3_word[k].Length)
                    {
                        if (str[i].Contains(s_3_word[k]))
                        {
                            scary_score += 1;
                            s_3_score += 1;
                            //richTextBox2.Text += str[i] + ",";
                        }
                    }
                }
                #endregion
            }

            TypeResult = TypeResult.Remove(TypeResult.Length - 1);//刪除最後一個逗點
            //richTextBox2.Text = richTextBox2.Text.Remove(richTextBox2.Text.Length - 1); //刪除最後一個逗點                       
            #region 判斷得分最高的電影類型

            max_score = funy_score > action_score ? funy_score : action_score;
            max_score = max_score > science_score ? max_score : science_score;
            max_score = max_score > scary_score ? max_score : scary_score;
            
            TypeResult += Environment.NewLine;
            
            if (funy_score == max_score)
            {
                TypeResult += "此篇為喜劇類電影，" + "喜:" + funy_score + "分" + ";" + "動:" + action_score + "分" + ";" + "科:" + science_score + "分" + ";" + "懸:" + scary_score + "分";
                TypeResult += Environment.NewLine + "權重1的出現次數，" + "喜:" + f_1_score + "動:" + a_1_score + "科:" + S_1_score + "懸:" + s_1_score;
                TypeResult += Environment.NewLine + "權重2的出現次數，" + "喜:" + f_2_score + "動:" + a_2_score + "科:" + S_2_score + "懸:" + s_2_score;
                TypeResult += Environment.NewLine + "權重3的出現次數，" + "喜:" + f_3_score + "動:" + a_3_score + "科:" + S_3_score + "懸:" + s_3_score;
            }
            else if (action_score == max_score)
            {
                TypeResult += "此篇為動作類電影，" + "喜:" + funy_score + "分" + ";" + "動:" + action_score + "分" + ";" + "科:" + science_score + "分" + ";" + "懸:" + scary_score + "分";
                TypeResult += Environment.NewLine + "權重1的出現次數，" + "喜:" + f_1_score + "動:" + a_1_score + "科:" + S_1_score + "懸:" + s_1_score;
                TypeResult += Environment.NewLine + "權重2的出現次數，" + "喜:" + f_2_score + "動:" + a_2_score + "科:" + S_2_score + "懸:" + s_2_score;
                TypeResult += Environment.NewLine + "權重3的出現次數，" + "喜:" + f_3_score + "動:" + a_3_score + "科:" + S_3_score + "懸:" + s_3_score;
            }
            else if (science_score == max_score)
            {
                TypeResult += "此篇為科幻類電影，" + "喜:" + funy_score + "分" + ";" + "動:" + action_score + "分" + ";" + "科:" + science_score + "分" + ";" + "懸:" + scary_score + "分";
                TypeResult += Environment.NewLine + "權重1的出現次數，" + "喜:" + f_1_score + "動:" + a_1_score + "科:" + S_1_score + "懸:" + s_1_score;
                TypeResult += Environment.NewLine + "權重2的出現次數，" + "喜:" + f_2_score + "動:" + a_2_score + "科:" + S_2_score + "懸:" + s_2_score;
                TypeResult += Environment.NewLine + "權重3的出現次數，" + "喜:" + f_3_score + "動:" + a_3_score + "科:" + S_3_score + "懸:" + s_3_score;
            }
            else if (scary_score == max_score)
            {
                TypeResult += "此篇為懸疑類電影，" + "喜:" + funy_score + "分" + ";" + "動:" + action_score + "分" + ";" + "科:" + science_score + "分" + ";" + "懸:" + scary_score + "分";
                TypeResult += Environment.NewLine + "權重1的出現次數，" + "喜:" + f_1_score + "動:" + a_1_score + "科:" + S_1_score + "懸:" + s_1_score;
                TypeResult += Environment.NewLine + "權重2的出現次數，" + "喜:" + f_2_score + "動:" + a_2_score + "科:" + S_2_score + "懸:" + s_2_score;
                TypeResult += Environment.NewLine + "權重3的出現次數，" + "喜:" + f_3_score + "動:" + a_3_score + "科:" + S_3_score + "懸:" + s_3_score;
            }
            
            return TypeResult;
            
            #endregion

            _OrginArticle = "";
            TypeResult = "";
        }
    }
}
