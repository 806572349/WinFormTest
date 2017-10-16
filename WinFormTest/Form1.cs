using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinFormTest
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private WebRequest request;
        private WebResponse response;
        private string strURL;
        /// <summary>
        /// get
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void simpleButton1_Click(object sender, EventArgs e)
        {
            //http://localhost:8080/girls
            try
            {
                strURL = textEdit1.Text;
                request = (System.Net.HttpWebRequest)WebRequest.Create(strURL);
                response = (System.Net.HttpWebResponse)request.GetResponse();
                System.IO.StreamReader streamReader = new System.IO.StreamReader(response.GetResponseStream(), Encoding.UTF8);
                string responseText = streamReader.ReadToEnd();
                streamReader.Close();
                memoEdit1.Text = responseText;
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
           
           
        }
        /// <summary>
        /// post
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void simpleButton2_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dt= this.gridControl1.DataSource as DataTable;
                StringBuilder sb=new StringBuilder();
                if (dt.Rows.Count>1)
                {
                    foreach (DataRow dataRow in dt.Rows)
                    {
                        sb.Append($"{dataRow[0]}={dataRow[1]}");
                        sb.Append("&");
                    }
                    string postData= sb.ToString();
                    postData=postData.Substring(0, postData.Length - 1);
                    strURL = textEdit1.Text;
                    request = (System.Net.HttpWebRequest)WebRequest.Create(strURL);
                    request.Method = "POST";
                    request.ContentType = "application/x-www-form-urlencoded";
                    //string postData = string.Format("age={0}&cupSize={1}", "20", "A");
                    //string param = "age=20";
                    ASCIIEncoding encoding = new ASCIIEncoding();
                    byte[] data = encoding.GetBytes(postData);
                    request.ContentLength = data.Length;
                    System.IO.Stream stream = request.GetRequestStream();
                    stream.Write(data, 0, data.Length);
                    stream.Close();
                    response = (System.Net.HttpWebResponse)request.GetResponse();
                    System.IO.StreamReader streamReader = new System.IO.StreamReader(response.GetResponseStream(), Encoding.UTF8);
                    string responseText = streamReader.ReadToEnd();
                    streamReader.Close();
                    memoEdit1.Text = responseText;
                }
               

               
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
          
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            DataTable dt=new DataTable();
            dt.Columns.Add("Name", typeof (string));
            dt.Columns.Add("Parameter", typeof(string));
            gridControl1.DataSource = dt;
        }
    }
}
