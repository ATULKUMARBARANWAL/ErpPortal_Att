<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Section.aspx.vb" Inherits="Examinationjune_Section" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
     <script src="../Bootstrap5/js/bootstrap.min.js" type="text/javascript"></script>
    <link href="../Bootstrap5/css/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <script src="../Bootstrap5/js/bootstrap.bundle.min.js" type="text/javascript"></script>
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.4.0/css/all.min.css"  />
    <style>
         body
        {
            
            overflow-x: hidden;
            background-color:#f2f3f5;
            background-repeat: no-repeat;
            background-size: 100% 100%;
        }
      .button
          {
            font-size: 18px !important;
            font-weight: 500;
            height: 40px;
            cursor: pointer;
            color:White;
            background-color:#1ed085;
            border:none;
            width:23%;
            margin-top:-12px;
          }
  .button:hover
          {
           color:White;
           background-color:#1aad6f;
           border:none;
           box-shadow:0px 1px 5px 1px #dcdcdc;
          }
   .card-header
         {
           background-color:#152837;
           color:#fff;
           font-weight:500;
           font-size:22px;
           letter-spacing:1px;
         }
 .labeltext
         {
          color:#15283c;
          font-size:17px;
          font-weight:450;
          letter-spacing:1px;
         }
    </style>
</head>
<body>
    <form id="form1" runat="server">
     <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
   <asp:UpdatePanel ID="updatepanel1" runat="server">
   <ContentTemplate>
     <div class="cotainer">
        <div class="row justify-content-center mt-1">
          <div class="col-md-8">
            <div class="card">
            <div class="card-header"><h5 class="subject">Section Master</h5></div>
            <div class="card-body">  
            <asp:DetailsView ID="DetailsView1" runat="server" AutoGenerateRows="False" DataKeyNames="ClassesID"
                   CssClass="EU_DataTable" DataSourceID="sdsinseert" DefaultMode="Insert" 
                  GridLines="None" HorizontalAlign="Center">
                <Fields >
                
                    <asp:TemplateField HeaderText="Section : "   HeaderStyle-Font-Bold="true" SortExpression="Code" >
                        <ItemTemplate>
                            <asp:Label ID="Label2"  runat="server" Text='<%# Bind("Code") %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="TextBox2" runat="server" Text='<%# Bind("Code") %>' CssClass=" form-control" Width="300px" ></asp:TextBox>
                        </EditItemTemplate>
                        <InsertItemTemplate>
                            <asp:TextBox ID="TextBox2" runat="server" Text='<%# Bind("Code") %>' CssClass=" form-control" Width="300px"></asp:TextBox>
                        </InsertItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField  HeaderStyle-Font-Bold="true" SortExpression="Classes">
                        <ItemTemplate>
                            <asp:Label ID="Label1"  Visible="false"  runat="server" Text='<%# Bind("Classes") %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("Classes") %>' CssClass="form-control"
                             Visible="false" TabIndex="1"></asp:TextBox>
                        </EditItemTemplate>
                        <InsertItemTemplate>
                            <asp:TextBox ID="TextBox1" runat="server"  Visible="false"  Text='<%# Bind("Classes") %>' CssClass="form-control" ></asp:TextBox>
                        </InsertItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField ShowHeader="False">
                        <EditItemTemplate>
                            <asp:Button ID="Button1" runat="server" CausesValidation="True" CommandName="Update"
                                Text="Update" CssClass="btn button"></asp:Button>
                            &nbsp;<asp:Button ID="Button2" runat="server" CausesValidation="False" CommandName="Cancel"
                                Text="Cancel" CssClass="btn button"></asp:Button>
                        </EditItemTemplate>
                        <InsertItemTemplate>
                            <asp:Button ID="Button1" runat="server" CssClass="btn button" CausesValidation="True"
                                CommandName="Insert" Text="Save"></asp:Button>
                            &nbsp;<asp:Button ID="Button2" runat="server" CssClass="btn button" CausesValidation="False"
                                CommandName="Cancel" Text="Cancel"></asp:Button>
                        </InsertItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                </Fields>
            </asp:DetailsView>
          <br />
            <asp:GridView ID="GridView1" runat="server" AllowPaging="True" AllowSorting="True" CssClass="table table-bordered"
                     AutoGenerateColumns="False" CellPadding="4" DataKeyNames="ClassesID" 
                   DataSourceID="sdsdelete" Width="100%">
                <Columns>
                       <asp:TemplateField Visible="false" HeaderText="Select">
                              <ItemTemplate>
                                  <asp:ImageButton ID="imgselect" Visible="false" CommandName="Select" CausesValidation="False"  ImageUrl="../img/logo_valider.png" Width="17px"  runat="server" />
                                </ItemTemplate>
                               </asp:TemplateField>
                                   <asp:TemplateField HeaderText="Delete">
                               <ItemTemplate>
                           <asp:ImageButton ID="lnkDelete" CommandName="Delete" CausesValidation="False"  ImageUrl="~/img/url.png" Width="17px" 
                           OnClientClick="return confirm('Really!Do u want to Delete')" runat="server" />
                           </ItemTemplate>
                      </asp:TemplateField>
               
                    <asp:BoundField DataField="Code" HeaderText="Section" SortExpression="Code">
                      
                    </asp:BoundField>
                </Columns>
                
            </asp:GridView>

             </div>
    </div>
    </div>
    </div>
    </div>
            <asp:SqlDataSource ID="sdsinseert" runat="server" ConnectionString="<%$ ConnectionStrings:myconnection %>"
                InsertCommand="INSERT INTO Classes(Classes, Code) VALUES (@Classes, @Code)" SelectCommand="SELECT classesid,Code, Classes FROM Classes WHERE (ClassesID = @ClassesID)"
                UpdateCommand="UPDATE Classes SET Code = @Code, Classes = @Classes WHERE (ClassesID = @Classesid)">
                <SelectParameters>
                    <asp:ControlParameter ControlID="GridView1" Name="ClassesID" PropertyName="SelectedValue" />
                </SelectParameters>
                <UpdateParameters>
                    <asp:Parameter Name="Code" />
                    <asp:Parameter Name="Classes" />
                    <asp:Parameter Name="Classesid" />
                </UpdateParameters>
                <InsertParameters>
                    <asp:Parameter Name="Classes" />
                    <asp:Parameter Name="Code" />
                </InsertParameters>
            </asp:SqlDataSource>
       
     
    <asp:SqlDataSource ID="sdsclassroom" runat="server" ConflictDetection="CompareAllValues"
        ConnectionString="<%$ ConnectionStrings:myconnection %>" InsertCommand="INSERT INTO Classes(Code, Classes) VALUES (@Code, @Classes)"
        SelectCommand="SELECT Code, Classes, ClassesID FROM Classes WHERE (ClassesID = @ClassesID)"
        UpdateCommand="UPDATE Classes SET Classes = @Classes, Code = @Code WHERE (ClassesID = @ClassesID)">
        <SelectParameters>
            <asp:ControlParameter ControlID="GridView1" Name="ClassesID" PropertyName="SelectedValue" />
        </SelectParameters>
        <UpdateParameters>
            <asp:Parameter Name="Classes" />
            <asp:Parameter Name="Code" />
            <asp:Parameter Name="ClassesID" />
        </UpdateParameters>
        <InsertParameters>
            <asp:Parameter Name="Code" />
            <asp:Parameter Name="Classes" />
        </InsertParameters>
    </asp:SqlDataSource>
    <asp:SqlDataSource ID="sdsdelete" runat="server" ConnectionString="<%$ ConnectionStrings:myconnection %>"
        DeleteCommand="DELETE FROM Classes WHERE (ClassesID = @ClassesID)" SelectCommand="SELECT Classes, Code, ClassesID FROM Classes ORDER BY ClassesID">
        <DeleteParameters>
            <asp:Parameter Name="ClassesID" />
        </DeleteParameters>
    </asp:SqlDataSource>
   
    </ContentTemplate>
    </asp:UpdatePanel>
    </form>
</body>
</html>
