<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ExamStructuretoProgram.aspx.vb" Inherits="Templates_ExamStructuretoProgram" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../Bootstrap5/css/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <script src="../Bootstrap5/js/bootstrap.min.js" type="text/javascript"></script>
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.4.0/css/all.min.css"  />
    <style type="text/css" >
    body 
{
    background-color:#f2f3f5;
}
.maincontainer
{
     background-color:#fff;
    border:1px solid #e1e2e3;
    border-radius:8px;
    box-shadow:0 2px 10px 2px #e1e2e3;
    padding-bottom:24px;
}
 .heading1
{
    padding-top:16px;
    }
  .heading1 h3
{
    color:#15283c;
    font-size:24px;
    font-weight:400;
    }
   
     .line
   {
       content:" ";
       width:100%;
       height:1px;
        background-color:#e1e2e3;
        margin-top:16px;
        margin-bottom:32px;
        
       
   }
   .tableform tr td
{
    padding-top:10px;
}
.tableform1 tr td
{
    padding-top:10px;
}
 .Submit
 {
     font-size: 18px !important;
      font-weight: 500;
      
      cursor: pointer;
      color:#fff;
      background-color:#1ed085;
      border:none;
      width:20%;
   }
   .Submit:hover
   {
       color:#fff;
       background-color:#1aad6f;
       border:none;
       box-shadow:0px 1px 5px 1px #dcdcdc;
    }
    .gridview
    {
        margin-top:24px;
    }
    .txtmarks
    {
        width:50%;
    }
    .hiddencol
    {
        display:none;
    }
    .custom-scrollbar-css 
    {
        min-height:0vh;
 max-height:65vh;
}
    .custom-scrollbar-css {
  overflow-y: scroll;
}

/* scrollbar width */
.custom-scrollbar-css::-webkit-scrollbar {
  width: 5px;
}

/* scrollbar track */
.custom-scrollbar-css::-webkit-scrollbar-track {
  background: #eee;
}

/* scrollbar handle */
.custom-scrollbar-css::-webkit-scrollbar-thumb {
  border-radius: 2rem;
  background-color: #00d2ff;
  background-image: linear-gradient(to top, #000 0%, #808080 100%);
}
 
    </style>
</head>
<body>
    <form id="form1" runat="server">
   <div class="container maincontainer">
    <div class="row">
    <div class="col-md-12">
   
      <div class="heading1">
                                    <h3>Exam structure assign to program</h3>
                                 </div>
      <div class="line">
         </div>

         <div class="row">
         <div class="col-md-3">
             <%--<asp:DropDownList ID="Ddlyear" runat="server">
             </asp:DropDownList>--%>
         </div>
          <div class="col-md-6">
          <table class="tableform" width="100%">
                                                <tr width="100%">
                                                <td width="30%">
                                                  Academic Year:
                                                       
                                                </td>
                                                <td width="70%">
                                                
                                                 <div class="form-group">
                                                       
                                                            <asp:DropDownList ID="ddlacademicyear" AutoPostBack="true" class="form-control form-select" runat="server" >
                                                                </asp:DropDownList>
                                                        
                                                    </div>
                                                    </td>
                                                    </tr>
                                                 <tr width="100%">
                                                <td width="30%">
                                                    Program:
                                                        
                                                    </td>
                                                    <td width="70%">
                                                    <div class="form-group">
                                                    <asp:DropDownList ID="ddlprogram" AutoPostBack="true" class="form-control form-select" runat="server" >
                                                            </asp:DropDownList>
                                                            </div> 
                                                    </td> 
                                                    </tr> 
                                                  

                                                    <tr width="100%">
                                                    <td width="30%">
                                                    Exam Structure:
                                                    </td>
                                                    <td width="70%">
                                                    <div class="form-group">
                                                    <asp:DropDownList ID="ddlexamname" AutoPostBack="true" class="form-control form-select" runat="server" >
                                                            </asp:DropDownList>
                                                          
                                                                </div> 
                                                    </td>
                                                    </tr>
                                                    
                                                   
                                                    
                                                    </table>

                                                

                                           </div>
                                           <div class="col-md-3"></div>
                                           </div>
         
        
         <div class="row">
         <div class="col-md-12">
         
         </div>
           <div class="col-md-12 gridview">
           <div class="custom-scrollbar-css">
           
           
               <asp:GridView ID="gridprogramexam" AutoGenerateColumns="False" class="table table-bordered" runat="server">
                  <Columns>
                     <asp:TemplateField HeaderText="S.No.">
                           <ItemTemplate>
                           <%#Container.DataItemIndex+1 %>
                           </ItemTemplate>
                          </asp:TemplateField>
                  <asp:BoundField HeaderText="Structure id" DataField="Structureid" ></asp:BoundField>
                  
                  <asp:BoundField HeaderText="Academic Year" DataField="AcademicYear" ItemStyle-Width="0px" ItemStyle-CssClass="hiddencol" HeaderStyle-CssClass="hiddencol" ReadOnly="true" ></asp:BoundField>
                           
                           <asp:BoundField HeaderText="Exam Name" DataField="Examname"></asp:BoundField>
                           <asp:BoundField HeaderText="Exam Type" DataField="Examtype"></asp:BoundField>
                           <asp:TemplateField HeaderText="Internal/External">
                           <ItemTemplate>
                               <asp:DropDownList ID="DdlInterexternal" class="form-control form-select" runat="server">
                               <asp:ListItem Text="Internal"></asp:ListItem>
                               <asp:ListItem Text="External"></asp:ListItem>
                               </asp:DropDownList>
                           </ItemTemplate>
                          </asp:TemplateField>
                          <asp:TemplateField HeaderText="Marks">
                           <ItemTemplate>
                               <asp:TextBox ID="txtexammarks" class="form-control " placeholder="xx" runat="server"></asp:TextBox>
                           </ItemTemplate>
                          </asp:TemplateField>
                  </Columns>
               </asp:GridView>
           </div>
         </div>
         </div>
         <div class="row">
         
           <div class="col-md-12 d-flex justify-content-center">

           <asp:Button ID="btnsave" class="btn Submit" runat="server" Text="Save"></asp:Button>
                                        
           </div>
         </div>
         </div>
    </div>
    </div>
    </form>
</body>
</html>
