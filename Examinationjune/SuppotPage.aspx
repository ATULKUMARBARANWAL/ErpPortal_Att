<%@ Page Language="VB" AutoEventWireup="false" CodeFile="SuppotPage.aspx.vb" Inherits="Examinationjune_SuppotPage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../Bootstrap5/css/bootstrap.min.css" rel="stylesheet" type="text/css" />

    <link href=" https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.4.0/css/all.min.css" rel="stylesheet" type="text/css">

    <script src="../Bootstrap5/js/bootstrap.min.js" type="text/javascript"></script>
    <style>
       .maincontainer
{
     
    border:1px solid #e1e2e3;
    border-radius:8px;
    box-shadow:0 2px 10px 2px #e1e2e3;
    padding-top:24px;
    margin-top:12px;
    background-color:#fff;
    padding:12px 18px;
}
    </style>
</head>
<body>
    <form id="form1" runat="server">
      <div class="container-fluid maincontainer">
          <div class="row">
            <div class="col-md-12">
             <asp:GridView ID="grdProgram"  AutoGenerateColumns="False" runat="server"
                        ShowHeaderWhenEmpty="true" DataKeyNames="Courseid"
            Class="table table-bordered" OnRowEditing="OnRowEditing" OnRowDeleting="OnRowDeleting">
      
           <Columns>
           
           
     
           
         <asp:TemplateField HeaderText="S.No.">
        <ItemTemplate>
             <%#Container.DataItemIndex+1 %>
        </ItemTemplate>
       </asp:TemplateField>
                <asp:BoundField HeaderText="Courseid" ItemStyle-CssClass="hiddencol1" HeaderStyle-CssClass="hiddencol1" DataField="Courseid"></asp:BoundField>
                <asp:BoundField HeaderText="Program" ReadOnly="true" DataField="Course"></asp:BoundField>
                <asp:BoundField HeaderText="Program Code"  ReadOnly="true" DataField="Coursecode"></asp:BoundField>
                <asp:BoundField HeaderText="Program Prefix"  ReadOnly="true" DataField="Courseprefix"></asp:BoundField>

            <asp:TemplateField HeaderText="Delete">
                   <ItemTemplate>
                       <asp:ImageButton ID="lnkDelete" CommandName="Delete" CausesValidation="False" ImageUrl="../ExaminationNImages/img/url.png" 
                          
                        CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>'  Width="20px"  runat="server" />
                   </ItemTemplate>
                  
             </asp:TemplateField>
            </Columns>
         
          
         </asp:GridView>
            </div>
          </div>
      </div>
    </form>
</body>
</html>
