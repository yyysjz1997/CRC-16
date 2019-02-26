using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CRC
{
    public partial class CRC : Form
    {
        public CRC()
        {
            InitializeComponent();
        }
        int CRC_F;
        private String u;
        private void button1_Click_1(object sender, EventArgs e)
        {
            try
            {
                string t;
                int q;
               
                string s = textBox1.Text;  //输入框的信息
                int f = textBox1.Text.Length;  //输入框信息的长度
                s = s.Replace(" ", "");  //将空格删去

                string[] c = new string[s.Length];
                if (s.Length / 2 != 0)
                    q = s.Length - 1;
                else
                    q = s.Length;
                
                for (int i = 2, j = 0, k = 0; j < q; j += 2, k++)
                {
                    c[k] = s.Substring(j, i);//将s的字符串每两个字节赋值给string数组的一个内存空间
                }
                for (int i = 0; i < c.Length / 2; i++)
                {
                    c[i] = Convert.ToString(Convert.ToInt32(c[i], 16));//把c中的每个元素，转化为16进制
                }

                byte[] by = new byte[c.Length / 2];
                for (int i = 0; i < c.Length / 2; i++)
                {
                    by[i] = Convert.ToByte(c[i]);//将c转化为字符数组by               
                }


                int CRC = 0xFFFF;  //初始为0xFFFF
                for (int k = 0; k < by.Length; k++)
                {
                
                    CRC ^= by[k];
                    for (int i = 0; i < 8; i++)
                    {
                        int j = Convert.ToInt32(CRC & 1);  //判断首位为1还是0
                        if (j == 1)
                            CRC = (CRC >> 1) ^ 0xA001;// A001是8005反过来，8005是CRC-16的多项式对应的二进制数  
                        else
                            CRC = (CRC >> 1);
                    }
                    
                    byte hi = (byte)((CRC & 0xFF00) >> 8);  //高位
                    byte lo = (byte)(CRC & 0x00FF);         //低位
                    CRC_F = hi << 8 | lo;  //最终的CRC-16编码码字
                    
                }

                t = Convert.ToString(CRC_F, 16);  //格式转化



                string[] n = new string[2];
                for (int i = 2, j = 0, k = 0; j <= n.Length; j += 2, k++)
                {
                    n[k] = t.Substring(j, i);
                }
                Array.Reverse(n);

                for (int i = 0; i < n.Length; i++)
                {
                    u += n[i];
                }
                int int_len = s.Length;
                byte[] bytes = new byte[int_len];
                for (int i = 0; i < int_len; i++)
                {
                    bytes[i] = (byte)(s[i]);
                }

                u = u.Insert(2, " ");  //一个字节中间用一个空格隔开
                textBox2.Text = u.ToUpper();  //字母变大写
            }
            catch (Exception)
            {
                MessageBox.Show("请输入正确的数值");
            }
            u = "";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
            textBox2.Text = "";
            CRC_F = 0;
            u = "";
    }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();  //退出系统
        }
    }
}
