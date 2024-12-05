<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Student_Data_Management.aspx.cs" Inherits="Student_Data_Management.Student_Data_Management" %>

<!DOCTYPE html>
<script runat="server">

    protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
</script>


<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .auto-style1 {
            width: 106px;
        }
        .auto-style2 {
            width: 292px;
        }
        .auto-style3 {
            margin-left: 12px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div style="font-family: 'Showcard Gothic'; font-size: x-large; font-style: italic; background-color: #00FFFF; text-align: center;">
            Student Details</div>
    <table align="center" style="margin-right: 350px;">
        <tr>
            <td class="auto-style1">Student Id : </td>
            <td class="auto-style2">
                <asp:TextBox ID="TextBox1" runat="server" Width="250px"></asp:TextBox>
            </td>
            <td rowspan="4">
                <asp:Image ID="Image1"  runat="server" Height="200px" Width="200px" BorderStyle="Groove" />
               
            </td>
        </tr>
        <tr>
            <td class="auto-style1">Student Name :</td>
            <td class="auto-style2">
                <asp:TextBox ID="TextBox2" runat="server" Width="250px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="auto-style1">Student Class :</td>
            <td class="auto-style2">
                <asp:TextBox ID="TextBox3" runat="server" Width="250px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="auto-style1">Annual Fees :</td>
            <td class="auto-style2">
                <asp:TextBox ID="TextBox4" runat="server" Width="250px"></asp:TextBox>
            </td>
   
        </tr>
        <tr>
            <td colspan="3">
                <asp:Button ID="Button1" runat="server" Text="Select" Width="100px" BackColor="#0033CC" ForeColor="White" OnClick="Button1_Click" />
                <asp:Button ID="Button2" runat="server" Text="Insert" Width="100px" BackColor="#006699" ForeColor="#CCFFFF" OnClick="Button2_Click" />
                 <asp:FileUpload ID="FileUpload2" runat="server" Width="270px" />
            </td>
            
               
                
            
            
        </tr>
        <tr>
            <td colspan="3">
                <asp:Button ID="Button3" runat="server" Text="Update" Width="100px" BackColor="Lime" ForeColor="#CC3300" OnClick="Button3_Click" />
                <asp:Button ID="Button4" runat="server" Text="Delete" Width="100px" BackColor="Red" ForeColor="White" OnClick="Button4_Click" />
                <asp:Button ID="Button5" runat="server" Text="Upload Image" Width="325px" BackColor="Aqua" CssClass="auto-style3" ForeColor="#009900" />

           
        </tr>
        <tr>
            <td colspan="3">
                <asp:Button ID="Button6" runat="server" BackColor="#0033CC" ForeColor="#66FFFF" Text="Reset All" Width="578px" OnClick="Button6_Click" />
            </td>
            
            
        </tr>
        
    </table>
        <asp:Label ID="Label1" runat="server" ForeColor="Red"></asp:Label>
        <br/><br/>
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False"  HorizontalAlign="Center" OnSelectedIndexChanged="GridView1_SelectedIndexChanged">
        <Columns>
            <asp:BoundField DataField="Sid" HeaderText="SID" />
            <asp:BoundField DataField="Name" HeaderText="Name" />
            <asp:BoundField DataField="Class" HeaderText="Class" />
            <asp:BoundField DataField="Fees" HeaderText="Fees" />
            
             <asp:BoundField DataField="Status" HeaderText="Status" />
            
            <asp:ImageField DataImageUrlField="PhotoBinary" HeaderText="Image">
                
            </asp:ImageField>
        </Columns>
</asp:GridView>
    </form>
    </body>
</html>
