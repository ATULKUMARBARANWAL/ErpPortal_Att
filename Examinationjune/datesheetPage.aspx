<%@ Page Language="VB" AutoEventWireup="false" CodeFile="datesheetPage.aspx.vb" Inherits="datesheetPage" %>



<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
   
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.4.0/css/all.min.css" integrity="sha512-iecdLmaskl7CVkqkXNQ/ZH/XLlvWZOJyj7Yy7tcenmpD1ypASozpmT/E0iPtmFIB46ZmdtAc9eNBvH0H/ZpiBw==" crossorigin="anonymous" referrerpolicy="no-referrer" />
  <link href="../Bootstrap5/css/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <script src="../Bootstrap5/js/bootstrap.min.js" type="text/javascript"></script>
      <link href="../ExaminationNCSS/datesheetPage.css" rel="stylesheet" type="text/css" />

    <script type = "text/javascript">
        function printFunction() {
            var panel = document.getElementById("<%=pnlPrintGridView.ClientID %>");
            var btn = document.getElementById("<%=pnlGridheaderView.ClientID %>");
            btn.style.display = 'none';
            var printWindow = window.open('', '', 'height=400,width=800,top=100');
            printWindow.document.write('<html><head><title>DIV Contents</title>');
            printWindow.document.write('</head><body >');
            printWindow.document.write(panel.innerHTML);
            printWindow.document.write('</body></html>');
            printWindow.document.close();
            setTimeout(function () {
                printWindow.print();
            }, 500);
            return false;
        }
    </script>
</head>
<style>
  body
        {
            
            overflow-x: hidden;
            background-color:#f2f3f5;
            background-repeat: no-repeat;
            background-size: 100% 100%;
        }
        
        .grass5
{
  background-color:#1ed085;
    border:none;
    color:White;
    text-align:center;
   height: 40px;
   padding-top:7px;
    }
 .grass5:hover
 {
    background-color:#1aad6f;
     box-shadow:0px 1px 5px 1px #dcdcdc;
     border:none;
     color:White;
     
     } 
     
.icongrass
{
   font-size:25px;
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
      width:12%;
   }
   .Submit:hover
   {
       color:White;
       background-color:#1aad6f;
       border:none;
       box-shadow:0px 1px 5px 1px #dcdcdc;
    }
    
 .card-header
 {
     background-color:#152837;
     color:#f2f3f5;
     font-weight:500;
     font-size:22px;
     letter-spacing:1px;
     }
 .labeltext
 {
     color:#15283c;
     font-size:16px;
     font-weight:500;
     letter-spacing:1px;
     
     
 }
 
 .hiddencol
 {
     display:none;     
     }
 @media print
 {
     #headerView
  {
      visibility:hidden;
      
      }
       .maincontainerm
{
   
    color:#15283c; 
    font-size:16px; 
    width:50px;
    height:43px ;
    padding-top:4px;    
  }
     #btnPrint
  {
      visibility:hidden;
      
      }
     }

</style>
<body>
   <form id="form1" runat="server">
    <div class="my-form">
    
       <div class="cotainer-fluid">
        <div class="row justify-content-center mt-2">
            <div class="col-md-12">
        <div class="card">
        <asp:Panel ID="pnlMain" runat="server" Visible="False">
             <div class="card-header">
             <table width="100%">
             <tr width="100%">
             <td width="4%">
               <asp:LinkButton ID="backbotton" class="text-white" runat="server"><i class="fa-solid fa-arrow-left"></i></asp:LinkButton>
             </td>
             <td width="96%">
               <h5 class="enquiry">Datesheet Generate</h5>
             </td>
             </tr>
             </table>
             </div>
           
                           
                             <div class="card-body">
                               

                             <%--   <div class="form-group row mb-2">
                                   <div class="col-md-1"></div>
                                    <asp:Label ID="lblProgLevel" class="col-md-3 col-form-label text-md-left labeltext" runat="server">Program Level :</asp:Label>
                                    <div class="col-md-6">
                                        <asp:DropDownList ID="ddlProgLevel" runat="server" CssClass="form-control input-sm form-select" AutoPostBack="True">
                                        <asp:ListItem>Select Program Level</asp:ListItem>
                                            
                                        
                                        </asp:DropDownList>
                                    </div>
                                </div>--%>
                                
                             
                              
                                <table width="100%">
                                  <tr width="100%">
                                   <td width="3%">
  </td>                              
                                  <td width="12%"align="center"> <asp:Label ID="lblExamN" class="col-md-2 col-form-label text-end labeltext" runat="server">Exam Name :</asp:Label>  </td>
                                  &nbsp; <td width="11%">
                                     <asp:DropDownList ID="ddlExamN" runat="server" AutoPostBack="true" CssClass="form-control input-sm form-select">
                                          </asp:DropDownList></td>
                                          
                                            <td width="13%" align="center">    <asp:Label ID="lblProgram" class="col-md-2 col-form-label text-end  labeltext" runat="server" >Program :</asp:Label></td>
                                    
                                            <td width="29%">  
                                        <asp:DropDownList ID="ddlProgram" runat="server" AutoPostBack="True" CssClass="form-select">
                                           <asp:ListItem>Select Program</asp:ListItem>
                                       </asp:DropDownList></td>
                                      
                                      <td width="15%" align="center">   <asp:Label ID="lblSemYr" class="col-md-2 col-form-label text-end  labeltext" runat="server">Sem/Year :</asp:Label></td>
                                 <td width="7%">    
           <asp:DropDownList ID="ddlSemYr" runat="server"  AutoPostBack="True" CssClass="form-control input-sm form-select">
                                           <asp:ListItem>Select Sem/Year</asp:ListItem>
                                                   </asp:DropDownList>
       </td>
                                 <td width="1%"></td>
                                   </tr>
                                    
                                         </table>

                                
                                <hr />
                                </div>
                       </asp:Panel>
           
      <asp:Panel ID="pnlGridheaderView" runat="server" Visible="False">
                                    
            <div id="headerView" class="card-header ">
            <div class="row">
            <div class="col-md-6">
            <h5 class="enquiry">
            <asp:LinkButton ID="backbuttonview" CssClass="backbutton text-white" runat="server"><i class="fa-solid fa-arrow-left"></i></asp:LinkButton> &nbsp;
           View Datesheet </h5>
            </div>
             <div class="col-md-6 d-flex justify-content-end">
             <Button ID="btnPrint" onclick="printFunction()" style="height:35px;" runat="server">
            <i class="fa fa-print p-1" style="font-size:22px;border:none;"></i></Button>                              
           
            </div>
            </div>
             

          </div>
     
                           
            </asp:Panel>

                                   <asp:Panel ID="pnlPrintGridView" runat="server" Visible="False">
                                   <div class="row ">
             <div class="col-md-12 justify-content-between  d-flex">  
                                   <div>
                                  <label id="lblDuration" runat="server" class="  " style="margin-left:23px;">Duration :</label>
                                    <asp:label id="lblDurationValue" runat="server" class=""></asp:label></div>
                                    <div>
                                       <asp:Label ID="lblExamName" runat="server" class="">Exam Name :</asp:Label>
                                       <asp:Label ID="lblExamNameValue" runat="server" class="" style="margin-right:23px;"></asp:Label>
            </div>
            </div>
            </div>

                <asp:GridView ID="grdGridView" GridLines="None" class="table-bordered mb-2 mt-2 m-auto"
         AutoGenerateColumns="False" runat="server" ShowHeaderWhenEmpty="True" CellPadding="4" 
              ForeColor="#333333" EditRowStyle-BackColor="#6183A8" 
          RowStyle-BorderWidth="2px" style="text-align:left; " Width="100%">
           <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
        <Columns>
        
         <asp:TemplateField HeaderText="S.No.">
        <ItemTemplate>
             <%#Container.DataItemIndex+1 %>
        </ItemTemplate>
       </asp:TemplateField>
       
     <asp:BoundField HeaderText="Program" DataField="Course"></asp:BoundField>
       <asp:BoundField HeaderText="Subject Code" DataField="Subjectcode"></asp:BoundField>
     <asp:BoundField HeaderText="Subject" DataField="Subject"></asp:BoundField>
     <asp:BoundField HeaderText="Semester" DataField="SemYear"></asp:BoundField>
     <asp:BoundField HeaderText="Exam Date" DataField="ExamDate"></asp:BoundField>
     <asp:BoundField HeaderText="Reporting Time" DataField="ReportingTime"></asp:BoundField>
     <asp:BoundField HeaderText="Exam Timing" DataField="ExamTime"></asp:BoundField>
                         
   
          </Columns>
                                      
           
        </asp:GridView>

                                </asp:Panel>
                           
                            <asp:Panel ID="pnlGrid" runat="server" Visible="false">
                          
                                  <table width="100%">
                                  <tr width="100%">
                                 
                                  <td width="15%"align="center"><asp:Label ID="lblReportTime" class="col-md-2  col-form-label text-end labeltext " runat="server">Reporting At :</asp:Label></td>
                                  &nbsp; <td width="13%">
                                       <asp:TextBox ID="txtReportTime" runat="server" class="form-control input-sm " type="Time" Placeholder="9:00am"></asp:TextBox></td>
                                           
                                            <td width="15%" align="center">   <asp:Label ID="lblExamTime" class="col-md-2 col-form-label text-end labeltext" runat="server">Exam Time :</asp:Label></td>
                                    
                                            <td width="13%">  
                                        <asp:TextBox ID="txtExamTime" runat="server" class="form-control input-sm" type="Time" Placeholder="9:00am"></asp:TextBox></td>
                                      
                                      <td width="16%" align="center">  <asp:Label ID="lblExamDuration" class="col-md-2 col-form-label text-end labeltext" runat="server">Exam Duration :</asp:Label></td>
                                 <td width="15%">    
           <asp:DropDownList id="ddlExamDurinsert" class="form-select" runat="server">
       <asp:ListItem >Select Duration</asp:ListItem>
         <asp:ListItem >30 minute</asp:ListItem>
          <asp:ListItem >1 hour</asp:ListItem>
            <asp:ListItem >1 hour 30 minute</asp:ListItem>
              <asp:ListItem >2 hour</asp:ListItem>
               <asp:ListItem >2 hour 30 minute</asp:ListItem>
                 <asp:ListItem >3 hour</asp:ListItem>
                  <asp:ListItem >3 hour 30 minute</asp:ListItem>
        </asp:DropDownList>
       </td>
                                 <td width="3%"></td>
                                   </tr>
                                    
                                         </table>
                            <div class="form-group row">
                                    <div class="col-md-12 text-center">
                                       <asp:Button ID="btnInsert" class="btn Submit mt-3" runat="server" Text="Fill"></asp:Button>
                                     </div>
                                    </div>
                         
                       <div class="row mt-3">
                       <div class="col-md-12">
                       <asp:GridView ID="GridView2" GridLines="Both" class="table table-bordered mt-2"
         AutoGenerateColumns="False" runat="server" ShowHeaderWhenEmpty="true" CellPadding="3" >
        <Columns>
        
         <asp:TemplateField HeaderText="S.No.">
        <ItemTemplate>
             <%#Container.DataItemIndex+1 %>
        </ItemTemplate>
       </asp:TemplateField>

       <asp:BoundField HeaderText="Examsubid" DataField="Examsubid" ItemStyle-CssClass="hiddencol"  HeaderStyle-CssClass="hiddencol"  ReadOnly="true"></asp:BoundField>
       <asp:BoundField HeaderText="Subid" DataField="Subjectid" ItemStyle-CssClass="hiddencol"  HeaderStyle-CssClass="hiddencol"  ReadOnly="true"></asp:BoundField>
       
       <asp:BoundField HeaderText="Subject Code" DataField="Subcode"></asp:BoundField>
         <asp:BoundField HeaderText="Subject"   DataField="Subject"></asp:BoundField>
    <asp:TemplateField HeaderText = "Exam Date" >
            <ItemTemplate>
              <asp:TextBox ID="txtExamDate" runat="server" TextMode="Date"></asp:TextBox>
             </ItemTemplate> 
        </asp:TemplateField>
         
           <asp:TemplateField HeaderText = "Shift" >
            <ItemTemplate>
                <asp:DropDownList ID="ddlShift" runat="server" >
                 <asp:ListItem text="Morning">Morning</asp:ListItem>
                 <asp:ListItem text="Evening">Evening</asp:ListItem>
                 </asp:DropDownList>
            </ItemTemplate> 
        </asp:TemplateField>
        <asp:TemplateField HeaderText = "Reporting Time" >
            <ItemTemplate>
              <asp:TextBox ID="txtReportTime" runat="server" TextMode="Time"></asp:TextBox>
             </ItemTemplate> 
        </asp:TemplateField>

        <asp:TemplateField HeaderText = "Exam Time" >
            <ItemTemplate>
               
                <asp:TextBox ID="txtExamTime" runat="server" TextMode="Time"></asp:TextBox>
             </ItemTemplate> 
        </asp:TemplateField>

        <asp:TemplateField HeaderText="Exam Duration" >
        <ItemTemplate>
        <asp:DropDownList id="ddlExamDuration" runat="server">
         <asp:ListItem >30 minute</asp:ListItem>
          <asp:ListItem >1 hour</asp:ListItem>
            <asp:ListItem >1 hour 30 minute</asp:ListItem>
              <asp:ListItem >2 hour</asp:ListItem>
               <asp:ListItem >2 hour 30 minute</asp:ListItem>
                 <asp:ListItem >3 hour</asp:ListItem>
                  <asp:ListItem >2 hour 30 minute</asp:ListItem>
        </asp:DropDownList>
        </ItemTemplate>
        </asp:TemplateField>

          </Columns>

        </asp:GridView>
                       </div>
                       </div>
                         
 

                                    <div class="form-group row">
                                    <div class="col-md-12 text-center">
                                       <asp:Button ID="btnSave" class="btn Submit m-3" runat="server" Text="Save" ></asp:Button>
                                        <asp:LinkButton ID="lnkbtnView" Visible="True" runat="server"><i class="fa fa-eye mt-2"></i></asp:LinkButton>
                                     </div>
                                    </div>
                                      
                                      </asp:Panel>
                           </div>
                        </div>
                </div>
                    </div>
             </div>
                 
  </form>

  <script type="text/javascript">
      function printFunction() {
          window.print();
          //                               
      }
                                  </script>
</body>
</html>
