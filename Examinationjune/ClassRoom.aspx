<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ClassRoom.aspx.vb" Inherits="ClassRoom" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<link href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.4.0/css/all.min.css" rel="stylesheet" type="text/css" />

    <link href="../Bootstrap5/css/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <script src="../Bootstrap5/js/bootstrap.min.js" type="text/javascript"></script>
   
    <title></title>
    <style>
      body
      {
          background:#f2f3f5;
       }
          .hiddencol
       {
           display:none;
       }
         .Submit
       {
      font-size: 18px !important;
      font-weight: 500;
      height: 40px;
      cursor: pointer;
      color:White;
      background-color:#1ed085;
      border:none;
      width:24%;
   }
   .labeltext
 {
     color:#15283c;
     font-size:18px;
     font-weight:450;
     letter-spacing:1px;
 }
    .Submit:hover
 {
       color:White;
       background-color:#1aad6f;
       border:none;
       box-shadow:0px 1px 5px 1px #dcdcdc;
 }
      .maincontainer 
 {
    border: 2px solid #fff;
    padding: 10px;
    background-color:#fff;
    border-radius: 6px;
    text-align: left;
 }
   .backbtn
 {
       padding-left:20px;
       
 }
   .headingR
 {
   padding-right:60px;       
  }
   
    .maincontainerm
 {
   
    color:#15283c; 
    font-size:16px; 
    width:50px;
    height:43px ;
   padding-top:4px;
 
  }
  #backbotton
 {
    font-size:24px;
    font-weight:600;
    color:#7c858f;
 }
  #backbotton:hover
{
    color:#15283c;
}
  .btnAddProgram
{
    background-color:#1ed085;
    color:#f2f3f5;
    font-weight:500;
    
    }
 .btnAddProgram:hover
{
    background-color:#1ed085;
    color:#f2f3f5;
    font-weight:500;
    border:1px solid #1ed085;
    }
    .modify:hover
    {
        
    color:#f2f3f5;
    font-weight:500;
   
        
        }
        .del:hover
        { 
    color:#000;
    font-weight:500;
    
            
            }
     .btnaddDecription
{
    background-color:#808080;
    color:#f2f3f5;
    font-weight:600;
    font-size:18px;
    text-align:center;
    border-radius:50%;
    }
 .btnaddDecription:hover
{
    background-color:#000;
    color:#f2f3f5;
    font-weight:500;
}
 .hiddencol1
{
        display:none;
}   
</style>
</head>
<body>
    <form id="form1" runat="server">
    <div class="container-fluid maincontainer mt-2">
      <div class="row mb-4">
   <div class="col-md-8">
   <div class="headinL text-center d-flex">
       
        <h4 class="m-0 backbtn"><asp:Label ID="lblheading" runat="server" Text="Create ClassRoom"></asp:Label></h4>
     </div>
    </div>
    <hr class="m-2"/>
       </div>
       <div class="row mb-4">
                      <div class="col-md-3"></div>
                         <asp:Label ID="lblClassRoom" class="col-md-2 labeltext " runat="server">ClassRoom </asp:Label>
                        <div class="col-md-6">
                          <asp:TextBox ID="txtClassRoom" runat="server" Width="400px" class="form-control input-sm" Placeholder="ClassRoom"></asp:TextBox>
                         </div>              
                     </div>
       <div class="row mb-1">
                      <div class="col-md-3"></div>
                         <asp:Label ID="lblDescription" class="col-md-2 labeltext" runat="server">Description </asp:Label>
                        <div class="col-md-6">
                          <asp:TextBox ID="txtDescription" runat="server"  Width="400px" class="form-control input-sm" Placeholder="Description"></asp:TextBox>
                         </div>              
                     </div>
                      <br /> 
                                    
                       <div class="row mt-1">
                          <div class="col-md-1"></div>
                         <div class="col-md-10 text-center ps-5">                                     
                            <asp:Button ID="btnAdd" class="btn Submit" runat="server" Text="Save"></asp:Button> 
                             <asp:Button ID="btnupdate" class="btn Submit" Visible="false" runat="server" Text="Update"></asp:Button>          
                         </div>
                         <div class="col-md-1"></div>
                       </div>
                       <br />

                       <asp:Panel ID="pnlgrid" runat="server" ScrollBars="auto" Visible="true">
            <asp:GridView ID="grdClassRoom"  AutoGenerateColumns="False" AllowPaging="true" OnPageIndexChanging="OnPageIndexChanging" PageSize="10" runat="server" ShowHeaderWhenEmpty="true" DataKeyNames="classid"
          OnRowEditing="OnRowEditing"  OnRowDeleting="OnRowDeleting" Class="table table-bordered"  >
      
           <Columns>
           
           
        <asp:TemplateField HeaderText="Modify">
                   <ItemTemplate>
                       <asp:LinkButton ID="lnkModify" CommandName="Edit"   CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>' runat="server" ><i class="fa-solid fa-pen-to-square modify" style="color: #27aa48;"></i></asp:LinkButton>
                      </ItemTemplate>
                     </asp:TemplateField>
             
           
         <asp:TemplateField HeaderText="S.No.">
        <ItemTemplate>
             <%#Container.DataItemIndex+1 %>
        </ItemTemplate>
       </asp:TemplateField>
                <asp:BoundField HeaderText="Classid" ItemStyle-CssClass="hiddencol1" HeaderStyle-CssClass="hiddencol1" DataField="classid"></asp:BoundField>
                <asp:BoundField HeaderText="ClassRoom" ItemStyle-width="530" ReadOnly="true" DataField="ClassRoom"></asp:BoundField>
                <asp:BoundField HeaderText="Description"  ReadOnly="true" DataField="Description"></asp:BoundField>
                <asp:TemplateField HeaderText="Delete">
                   <ItemTemplate>
                       <asp:LinkButton ID="lnkDelete" CommandName="Delete" CausesValidation="False"                           
                        CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>'  runat="server"><i class=" fa-solid fa-trash-can del" style="color: #808080;"></i></asp:LinkButton>
                   </ItemTemplate>
                  
             </asp:TemplateField>
            </Columns>
         
          
         </asp:GridView>
         </asp:Panel>
         </div>
     </div>
    </form>
</body>
</html>
