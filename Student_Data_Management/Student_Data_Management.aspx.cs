using System;
using System.Collections.Generic;
using System.IO;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Student_Data_Management
{
   
    public partial class Student_Data_Management : System.Web.UI.Page
    {
        SqlCommand cmd;
        SqlConnection con;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                TextBox1.Focus();
            }
            con = new SqlConnection("Data Source=DESKTOP-4HBJUI9\\MSSQLSERVER01;Initial Catalog=Crud_operations;Integrated Security=True");
            cmd = new SqlCommand();
            cmd.Connection = con;
            LoadData();
        }

        protected void Button5_Click(object sender, EventArgs e)
        {
            if (FileUpload2.HasFiles)
            {
                HttpPostedFile selectedFile = FileUpload2.PostedFile;
                string fileExtension = Path.GetExtension(selectedFile.FileName);
                if (fileExtension == ".jpg" || fileExtension == ".bmp" || fileExtension == ".png")
                {
                    string imgName = selectedFile.FileName;
                    string folderPath = Server.MapPath("~/Images/");
                    if (!Directory.Exists(folderPath))
                    {
                        Directory.CreateDirectory(folderPath);
                    }
                    selectedFile.SaveAs(folderPath + imgName);
                    Image1.ImageUrl = "~/Images/" + imgName;
                    BinaryReader br = new BinaryReader(selectedFile.InputStream);
                    //Converting the image into byte[] (Binary Format)
                    byte[] imgData = br.ReadBytes(selectedFile.ContentLength);
                    //Storing image name & image binary values in session to access them in Insert & Update
                    Session["PhotoName"] = imgName;
                    Session["PhotoBinary"] = imgData;
                }
                else
                {
                    Response.Write("<script>alert('Supported image file formats are .jpg, .bmp and .png only.')</script>");
                }
            }
            else
            {
                Response.Write("<script>alert('Please select an image file to upload.')</script>");
            }

        }

        private void LoadData()
        {
            cmd.CommandText = "Select Sid,Name,Class,Fees,PhotoName,PhotoBinary,Status From  dbo.Student Where Status = 1 Order By Sid";
            con.Open();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            GridView1.DataSource = ds;
            GridView1.DataBind();
            con.Close();
        }

        private void ClearData()
        {
            TextBox1.Text = TextBox2.Text = TextBox3.Text = TextBox4.Text = Label1.Text = Image1.ImageUrl = "";
            Session["PhotoName"] = Session["PhotoBinary"];
            TextBox1.Focus();
        }

        protected void Button6_Click(object sender, EventArgs e)
        {
            ClearData();
        }

        private void AddParameters()
        {
            cmd.Parameters.AddWithValue("@Sid", TextBox1.Text);
            cmd.Parameters.AddWithValue("@Name", TextBox2.Text);
            cmd.Parameters.AddWithValue("@Class", TextBox3.Text);
            cmd.Parameters.AddWithValue("@Fees", TextBox4.Text);
            if (Session["PhotoName"] != null)
            {
                cmd.Parameters.AddWithValue("@PhotoName", Session["PhotoName"].ToString());
            }
            else
            {
                cmd.Parameters.AddWithValue("@PhotoName",DBNull.Value);
                cmd.Parameters["@PhotoName"].SqlDbType = SqlDbType.VarChar;
            }

            if (Session["PhotoBinary"] != null)
            {
                cmd.Parameters.AddWithValue("@PhotoBinary", Session["PhotoBinary"].ToString());
            }
            else
            {
                cmd.Parameters.AddWithValue("@PhotoBinary", DBNull.Value);
                cmd.Parameters["@PhotoBinary"].SqlDbType = SqlDbType.VarChar;
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            cmd.CommandText = "Select Sid,Name,Class,Fees from dbo.Student where Status=1 and Sid=" + TextBox1.Text;
            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                TextBox2.Text = dr["Name"].ToString();
                TextBox3.Text = dr["Class"].ToString();
                TextBox4.Text = dr["Fees"].ToString();
                if (dr["PhotoName"] !=  DBNull.Value)
                {
                    Image1.ImageUrl = "~/Images/" + dr["PhotoName"];
                    Session["PhotoName"] = dr["PhotoName"].ToString();
                }
                else
                {
                    Image1.ImageUrl = "";
                    Session["PhotoName"] = null;
                }

                if (dr["PhotoBinary"] != DBNull.Value)
                {
                    Session["PhotoBinary"] = (byte[])dr["PhotoBinary"];
                }
                else
                {
                    Session["PhotoBinary"] = null;
                }
            }
            else
            {
                Response.Write("<script>alert('No student exists with the given Id')</script>");
                ClearData();
            }

            con.Close();
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            cmd.CommandText = "Insert into dbo.Student(Sid,Name,Class,Fees,PhotoName,PhotoBinary) Values (@Sid,@Name,@Class,@Fees,@PhotoName,CONVERT(varbinary(max), @PhotoBinary))";
            AddParameters();
            con.Open();
            cmd.ExecuteNonQuery();
            Response.Write("<script>alert('Record is inserted into the table')</script>");
            con.Close() ;
        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            cmd.CommandText = "Update dbo.Student Set Name=@Name,Class=@Class,Fees=@Fees,PhotoName=@PhotoName,PhotoBinary=CONVERT(varbinary(max), @PhotoBinary) Where Sid = @Sid";
            AddParameters();
            con.Open(); 
            cmd.ExecuteNonQuery();
            Response.Write("<script>alert('Record is updated successfully in the table')</script>");
            con.Close() ;
        }

        protected void Button4_Click(object sender, EventArgs e)
        {
            cmd.CommandText = "Update dbo.Student Set Status = 0 Where Sid = @Sid";
            cmd.Parameters.AddWithValue("@Sid", TextBox1.Text);
            con.Open();
            cmd.ExecuteNonQuery();
            Response.Write("<script>alert('Record is deleted successfully in the table')</script>");
            con.Close();
        }
    }
}