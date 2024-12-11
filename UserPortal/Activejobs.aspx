<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Activejobs.aspx.vb" Inherits="UserPortal_Activejobs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
      <script src="../Bootstrap5/js/bootstrap.min.js" type="text/javascript"></script>
    <link href="../Bootstrap5/css/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <script src="../Bootstrap5/js/bootstrap.bundle.min.js" type="text/javascript"></script>
  
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.4.0/css/all.min.css"  />
    <style>
    
      .maincontainer
{
    border-radius:8px;
    padding-top:12px;
    margin-top:12px;
    background-color:#fff;
    padding:10px;
   
}
  .line
   {
       content:" ";
       width:100%;
       height:1px;
       background-color:#e1e2e3;
       margin-top:4px;
       margin-bottom:4px;
       
   }
   .hiddencol
   {
       display:none;
       }
    
     .borderTextouter
 {
     font-size:28px;
     font-weight:600;
     color:#15283c;
     }
     .SubjectName
   {
       color:#1ed085;
       text-decoration:none;
       font-weight:400;
    }
.SubjectName:hover
   {
       color:#20a16a;
       text-decoration:none;
       font-weight:400;
    }
    .heading1 h5
{
    font-size:22px;
    color:#15283c;
    font-weight:400;
}
  .backbotton
{
    font-size:22px;
    font-weight:600;
    color:#7c858f;
    
}

.backbotton:hover
{
    color:#15283c;
}
    
    </style>
</head>
<body>
    <form id="form1" runat="server">
     <div class="container-fluid maincontainer">
     <div class="row">
     <div class="col-md-3 ">
     <div class="heading1 d-flex">
     <asp:LinkButton ID="backbotton" class="backbotton" runat="server"><i class="fa-solid fa-arrow-left"></i></asp:LinkButton>
                              &nbsp &nbsp
       <h5><asp:Label ID="JobProfile"  runat="server" Text=""></asp:Label>Active Jobs</h5>

      
     </div>
     </div>
      <div class="col-md-3">
  <h5> </h5>
                 </div>
     <div class="col-md-6 d-flex justify-content-end">

     <table width="70%">
     <tr width="100%">
    
      <td width="30%" align="right">
      <asp:Label ID="Label1" runat="server">Select : &nbsp;</asp:Label>
     </td>
     <td width="70%">
        <asp:DropDownList ID="ddlacademicyear" Autopostback="true" class="form-select" runat="server">
   <asp:ListItem>College</asp:ListItem>
   <asp:ListItem>Alumni</asp:ListItem>
                 </asp:DropDownList>
     </td>
     </tr>
     </table>
     </div>
     </div>

      <div class="line"></div>

     <div class="row">

      <asp:GridView ID="Grdactivejob"  AutoGenerateColumns="False" runat="server" ShowHeaderWhenEmpty="true" 
            Class="table table-bordered mt-4"  >
      
           <Columns>
          
       
         
         <asp:TemplateField HeaderText="S.No.">
        <ItemTemplate>
             <%#Container.DataItemIndex+1 %>
        </ItemTemplate>
       </asp:TemplateField>
             <asp:BoundField HeaderText="Company Id" ItemStyle-CssClass="hiddencol" HeaderStyle-CssClass="hiddencol"  ReadOnly="true" DataField="Companyid"></asp:BoundField>
             <asp:BoundField HeaderText="jobpostId" ItemStyle-CssClass="hiddencol" HeaderStyle-CssClass="hiddencol"   ReadOnly="true" DataField="JobPostId"></asp:BoundField>
              <asp:BoundField HeaderText="PlEligibilityId" ItemStyle-CssClass="hiddencol" HeaderStyle-CssClass="hiddencol"   ReadOnly="true" DataField="PlEligibilityId"></asp:BoundField>
                <asp:BoundField HeaderText="Company Name"  ReadOnly="true" DataField="CompanyName"></asp:BoundField>
                 <asp:BoundField HeaderText="Job Profile"  ReadOnly="true" DataField="JobProfile"></asp:BoundField>
               <asp:BoundField HeaderText="Location"  ReadOnly="true" DataField="VacancyLocation"></asp:BoundField>
          <asp:BoundField HeaderText="Company Id" ItemStyle-CssClass="hiddencol" HeaderStyle-CssClass="hiddencol"  ReadOnly="true" DataField="Applylink"></asp:BoundField>
             <asp:TemplateField HeaderText="Apply">
                   <ItemTemplate>
                       <asp:LinkButton ID="lnkapply"  CommandName="Apply" CssClass="SubjectName"  CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>' runat="server" >Click Here</asp:LinkButton>
                      </ItemTemplate>
        </asp:TemplateField>
            </Columns>
         
          
         </asp:GridView>
         </div>
    </div>
    </form>
</body>
</html>
