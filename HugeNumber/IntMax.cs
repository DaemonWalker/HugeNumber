using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HugeNumber
{
    public class IntMax
    {
        public static int StepLength = 10000;
        private List<int> number = new List<int>();
        public bool IsMinus = false;

        #region 数字运算符
        /// <summary>
        /// 减法
        /// </summary>
        /// <param name="num1"></param>
        /// <param name="num2"></param>
        /// <returns></returns>
        public static IntMax operator -(IntMax num1, IntMax num2)
        {
            if (num2.IsMinus)
            {
                return num1 + (-num2);
            }
            if (num1 < num2)
            {
                var r = num2 - num1;
                return -r;
            }

            if (num1.IsMinus)
            {
                if (num2.IsMinus == false)
                {
                    var r = new IntMax(num1.number) + num2;
                    r.IsMinus = true;
                    return r;
                }
                else
                {
                    return new IntMax(num2.number) + num1;
                }
            }
            if (num2.IsMinus)
            {
                return num1 + (-num2);
            }

            var list = new List<int>();
            for (int i = 0; i < num2.number.Count; i++)
            {
                list.Add(num1.number[i] - num2.number[i]);
            }
            for (int i = num2.number.Count; i < num1.number.Count; i++)
            {
                list.Add(num1.number[i]);
            }
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i] < 0)
                {
                    list[i] = list[i] + IntMax.StepLength;
                    list[i + 1] = list[i + 1] - 1;
                }
            }
            return new IntMax(list);
        }

        /// <summary>
        /// 含有long的减法
        /// </summary>
        /// <param name="num1"></param>
        /// <param name="num2"></param>
        /// <returns></returns>
        public static IntMax operator -(long num1, IntMax num2)
        {
            return new IntMax(num1) - num2;
        }

        /// <summary>
        /// 含有long的减法
        /// </summary>
        /// <param name="num1"></param>
        /// <param name="num2"></param>
        /// <returns></returns>
        public static IntMax operator -(IntMax num1, long num2)
        {
            return num1 - new IntMax(num2);
        }

        /// <summary>
        /// 加法
        /// </summary>
        /// <param name="num1"></param>
        /// <param name="num2"></param>
        /// <returns></returns>
        public static IntMax operator +(IntMax num1, IntMax num2)
        {
            if (num1.IsMinus && num2.IsMinus)
            {
                return -((-num1) + (-num2));
            }
            if (num1.IsMinus)
            {
                return num2 - (-num1);
            }
            if (num2.IsMinus)
            {
                return num1 - num2;
            }

            var list = new List<int>();
            if (num1.Length >= num2.Length)
            {
                for (int i = 0; i < num2.number.Count; i++)
                {
                    list.Add(num1.number[i] + num2.number[i]);
                }
                for (int i = num2.number.Count; i < num1.number.Count; i++)
                {
                    list.Add(num1.number[i]);
                }
            }
            else
            {
                for (int i = 0; i < num1.number.Count; i++)
                {
                    list.Add(num2.number[i] + num1.number[i]);
                }
                for (int i = num1.number.Count; i < num2.number.Count; i++)
                {
                    list.Add(num2.number[i]);
                }
            }


            return new IntMax(AddMethodCheck(list));
        }

        private static List<int> AddMethodCheck(List<int> list)
        {
            for (int i = 0; i < list.Count - 1; i++)
            {
                list[i + 1] = list[i + 1] + list[i] / IntMax.StepLength;
                list[i] = list[i] % IntMax.StepLength;
            }

            if (list.Last() >= IntMax.StepLength)
            {
                var head = list.Last() / IntMax.StepLength;
                list[list.Count - 1] = list.Last() % IntMax.StepLength;
                list.Add(head);
            }

            return list;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="num1"></param>
        /// <param name="num2"></param>
        /// <returns></returns>
        public static IntMax operator +(long num1, IntMax num2)
        {
            return new IntMax(num1) + num2;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="num1"></param>
        /// <param name="num2"></param>
        /// <returns></returns>
        public static IntMax operator +(IntMax num1, long num2)
        {
            return num1 + new IntMax(num2);
        }

        public static IntMax operator -(IntMax num)
        {
            return new IntMax(!num.IsMinus, num.number);
        }
        #endregion

        #region 逻辑运算符
        /// <summary>
        /// 大于运算符
        /// </summary>
        /// <param name="num1"></param>
        /// <param name="num2"></param>
        /// <returns></returns>
        public static bool operator >(IntMax num1, IntMax num2)
        {
            if (num1.IsMinus != num2.IsMinus)
            {
                if (num1.IsMinus)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            if (num1.IsMinus && num2.IsMinus)
            {
                return !(new IntMax(num1.number) > new IntMax(num2.number));
            }

            if (num1.Length > num2.Length)
            {
                return true;
            }
            if (num1.Length < num2.Length)
            {
                return false;
            }
            for (int i = num1.number.Count - 1; i >= 0; i--)
            {
                if (num1.number[i] < num2.number[i])
                {
                    return false;
                }
                if (num1.number[i] > num2.number[i])
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// 等于运算符
        /// </summary>
        /// <param name="num1"></param>
        /// <param name="num2"></param>
        /// <returns></returns>
        public static bool operator ==(IntMax num1, IntMax num2)
        {
            if (object.Equals(num1, null) && object.Equals(num2, null))
            {
                return true;
            }
            if (object.Equals(num1, null) != object.Equals(num2, null))
            {
                return false;
            }


            if (num1.IsMinus != num2.IsMinus)
            {
                return false;
            }
            if (num1.Length != num2.Length)
            {
                return false;
            }
            for (int i = 0; i < num1.number.Count; i++)
            {
                if (num1.number[i] != num2.number[i])
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// 整形判断
        /// </summary>
        /// <param name="num1"></param>
        /// <param name="num2"></param>
        /// <returns></returns>
        public static bool operator ==(long num1, IntMax num2)
        {
            if (num2 == null)
            {
                return false;
            }
            return num2.ToString() == num1.ToString();
        }

        /// <summary>
        /// 不等于运算符
        /// </summary>
        /// <param name="num1"></param>
        /// <param name="num2"></param>
        /// <returns></returns>
        public static bool operator !=(long num1, IntMax num2)
        {
            return !(num1 == num2);
        }

        /// <summary>
        /// 不等于运算符
        /// </summary>
        /// <param name="num1"></param>
        /// <param name="num2"></param>
        /// <returns></returns>
        public static bool operator !=(IntMax num1, IntMax num2)
        {
            return !(num1 == num2);
        }

        /// <summary>
        /// 小于运算符
        /// </summary>
        /// <param name="num1"></param>
        /// <param name="num2"></param>
        /// <returns></returns>
        public static bool operator <(IntMax num1, IntMax num2)
        {
            if (num1 > num2)
            {
                return false;
            }
            if (num1 == num2)
            {
                return false;
            }
            return true;
        }

        public static bool operator >=(IntMax num1, IntMax num2)
        {
            return !(num1 < num2);
        }

        public static bool operator <=(IntMax num1, IntMax num2)
        {
            return !(num1 > num2);
        }

        #endregion

        #region 实体方法
        public IntMax(List<int> num)
        {
            var i = RemoveHead0(num);
            for (int k = 0; k <= i; k++)
            {
                this.number.Add(num[k]);
            }
        }
        public IntMax()
        {
            this.number.Add(0);
        }
        public IntMax(bool isMinus, List<int> num)
        {
            var i = IntMax.RemoveHead0(num);
            for (int k = 0; k <= i; k++)
            {
                this.number.Add(num[k]);
            }

            this.IsMinus = isMinus;
        }

        public IntMax(long num)
        {
            if (num < 0)
            {
                this.IsMinus = true;
                num = -num;
            }

            while (num >= IntMax.StepLength)
            {
                this.number.Add(Convert.ToInt32(num % IntMax.StepLength));
                num = num / IntMax.StepLength;
            }
            this.number.Add(Convert.ToInt32(num));

            //var step = IntMax.StepLength.ToString().Length - 1;
            //var str = num.ToString();
            //if (str.Length <= step)
            //{
            //    this.number.Add(Convert.ToInt32(str));
            //    return;
            //}
            //var pos = str.Length - step;
            //do
            //{
            //    var temp = str.Substring(pos, step);
            //    this.number.Add(Convert.ToInt32(temp));
            //} while (pos > str.Length);
            //this.number.Add(Convert.ToInt32(str.Substring(0, pos)));
        }

        /// <summary>
        /// 数字长度
        /// </summary>
        public int Length
        {
            get
            {
                return (this.number.Count - 1) * (IntMax.StepLength.ToString().Length - 1) + this.number.Last().ToString().Length;
            }
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            if (this.IsMinus)
            {
                sb.Append("-");
            }

            var i = IntMax.RemoveHead0(this);
            //首位不需要用0补齐
            sb.Append(this.number[i].ToString());
            i--;
            //拼接数字
            for (; i >= 0; i--)
            {
                sb.Append(this.number[i].ToString(IntMax.StepLength.ToString().Substring(1)));
            }
            return sb.ToString();
        }

        public string ToShortString(int numLength = 4)
        {
            var fullStr = this.ToString();
            return $"{fullStr.First()}.{fullStr.Substring(1, numLength - 1)}E+{fullStr.Length - 1}";
        }
        #endregion

        #region 工具方法
        private static int RemoveHead0(IntMax num)
        {
            var i = num.number.Count - 1;
            //去除前面为0的状况
            while (num.number[i] == 0 && i > 0)
            {
                i--;
            }
            return i;
        }

        private static int RemoveHead0(List<int> number)
        {
            var i = number.Count - 1;
            //去除前面为0的状况
            while (number[i] == 0 && i > 0)
            {
                i--;
            }
            return i;
        }
        #endregion
    }
}
