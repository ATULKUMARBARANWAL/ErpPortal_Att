<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ExProStru.aspx.vb" Inherits="ExProStru" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
  
    <link href="../ExaminationNCSS/ExProStru.css" rel="stylesheet" type="text/css" />
   
    <link href="../Bootstrap5/css/bootstrap.min.css" rel="stylesheet" type="text/css" />

    <link href=" https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.4.0/css/all.min.css" rel="stylesheet" type="text/css">

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
   .Submit:hover
   {
       color:White;
       background-color:#1aad6f;
       border:none;
       box-shadow:0px 1px 5px 1px #dcdcdc;
    }
</style>
</head>
<body>
   <form id="form1" runat="server">
   <div class="container-fluid maincontainer mt-2">
   <div class="row ">
   <div class="col-md-8">
   <div class="headinL text-center d-flex">
        <asp:LinkButton ID="backbotton" class="maincontainerm" runat="server"><i class="fa-solid fa-arrow-left"></i></asp:LinkButton>
        <h4 class="m-0 backbtn"><asp:Label ID="lblheading" runat="server" Text="Program Exam Structure"></asp:Label></h4>
     </div>
    </div>
    <div class="col-md-4">
    <div class="headingR d-flex justify-content-end">
    <h5><asp:Label ID="lblAcademicyr" runat="server" Text="Academic Year : "></asp:Label></h5>&nbsp;&nbsp;&nbsp;
      <h5> <asp:Label ID="lblacedmic" runat="server" class="form-label" Text="Label"></asp:Label></h5> 
  
    </div>
    </div>
     </div>

   <hr class="m-2"/>

   <div class="row m-auto p-0" >
   <div class="col-md-6">
   <div class="ProgLbl text-center d-flex">
   <div class="col-md-1">
   </div>
   <asp:Label id="lblProgram" runat="server" Text="Program :"></asp:Label>&nbsp;&nbsp;&nbsp;
   <asp:Label id="lblFetchProgram" runat="server" Text=""></asp:Label>
   </div>
   </div>
   <div class="col-md-6">
   <div class="AllProgLbl d-flex justify-content-end">
   <asp:CheckBox ID="chckBoxAllProg" AutopostBack="true" runat="server" />  &nbsp;&nbsp;&nbsp;
   <asp:Label ID="lblAllPro" runat="server" Text="All Programs"></asp:Label>
  
   </div>
   </div>
   </div>

   <asp:Panel ID="pnlgrdProg" runat="server" Visible="true">
   <div class="row mt-3">
       <div class="col-md-12">
       <asp:GridView ID="grdProg" GridLines="Both"  DataKeyNames="Courseid"
       AutoGenerateColumns="False" runat="server" ShowHeaderWhenEmpty="true" CssClass="table table-bordered" AllowPaging="true"
       PageSize="10">
          <Columns>
            <asp:TemplateField ItemStyle-Width="0px"  HeaderText="S.No.">
            <ItemTemplate>
            <%#Container.DataItemIndex+1 %>
            </ItemTemplate>
            </asp:TemplateField>          
          
            <asp:BoundField HeaderText="Program Code" ItemStyle-Width="0px"  DataField="Coursecode"   ReadOnly="true"></asp:BoundField>
            <asp:BoundField HeaderText="Program" ItemStyle-Width="0px"  DataField="Course"  ReadOnly="true"></asp:BoundField>
            <asp:TemplateField HeaderText="Exam Structure"  ItemStyle-Width="0px"  HeaderStyle-HorizontalAlign="Center">
            <ItemTemplate>
            <asp:DropDownList ID="ddlStructure" class="form-select" runat="server">
            </asp:DropDownList>
            </ItemTemplate>
            <HeaderStyle HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:BoundField HeaderText="Courseid" ItemStyle-Width="0px"  DataField="Courseid" ItemStyle-CssClass="hiddencol" HeaderStyle-CssClass="hiddencol" ReadOnly="true"></asp:BoundField>
        <asp:BoundField HeaderText="Courseid" ItemStyle-Width="0px"  DataField="Coursesessionid" ItemStyle-CssClass="hiddencol" HeaderStyle-CssClass="hiddencol" ReadOnly="true"></asp:BoundField>
          </Columns>
       </asp:GridView>
       </div>
       </div>
   
   </asp:Panel>

    <asp:Panel ID="pnlgrdAllProg" runat="server" Visible="false">
    
    <div class="row mt-3">
       <div class="col-md-12">
       <asp:GridView ID="grdAllProg" GridLines="Both"  DataKeyNames="Courseid"
       AutoGenerateColumns="False" runat="server" ShowHeaderWhenEmpty="true" CssClass="table table-bordered" AllowPaging="true">
           <Columns>
             <asp:TemplateField HeaderText="S.No.">
             <ItemTemplate>
             <%#Container.DataItemIndex+1 %>
             </ItemTemplate>
             </asp:TemplateField>          
                    
             <asp:BoundField HeaderText="Program Code"  DataField="Coursecode"   ReadOnly="true"></asp:BoundField>
             <asp:BoundField HeaderText="Program"  DataField="Course"  ReadOnly="true"></asp:BoundField>
             <asp:TemplateField HeaderText="Exam Structure"  HeaderStyle-HorizontalAlign="Center">
             <ItemTemplate>
             <asp:DropDownList ID="ddlStructure"  class="form-select" runat="server">
             </asp:DropDownList>
             </ItemTemplate>
             <HeaderStyle HorizontalAlign="Center" />
             </asp:TemplateField>
            
             <asp:BoundField HeaderText="Courseid"  DataField="Courseid" ItemStyle-CssClass="hiddencol" HeaderStyle-CssClass="hiddencol" ReadOnly="true"></asp:BoundField>
          <asp:BoundField HeaderText="Courseid" ItemStyle-Width="0px"  DataField="Coursesessionid" ItemStyle-CssClass="hiddencol" HeaderStyle-CssClass="hiddencol" ReadOnly="true"></asp:BoundField>
       
          </Columns>
        </asp:GridView>
        </div>
        </div>
        </asp:Panel>

        <div class="row mt-2">
                          <div class="col-md-1"></div>
                         <div class="col-md-10 text-center ps-5">                                     
                            <asp:Button ID="btnAdd" class="btn Submit" runat="server" Text="Add"></asp:Button> 
                                      
                         </div>
                         <div class="col-md-1"></div>
                       </div>
</div>
</form>
</body>
</html>