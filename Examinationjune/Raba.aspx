<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Raba.aspx.vb" Inherits="Reports_Academicrpt_Raba" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <!-- Bootstrap -->
    <link href="../../AR/vendors/bootstrap/dist/css/bootstrap.min.css" rel="stylesheet">
    <!-- Font Awesome -->
    <link href="../../AR/vendors/font-awesome/css/font-awesome.min.css" rel="stylesheet">
    <!-- NProgress -->
    <link href="../../AR/vendors/nprogress/nprogress.css" rel="stylesheet">
    <!-- iCheck -->
    <link href="../../AR/vendors/iCheck/skins/flat/green.css" rel="stylesheet">
    <!-- bootstrap-progressbar -->
    <link href="../../AR/vendors/bootstrap-progressbar/css/bootstrap-progressbar-3.3.4.min.css"
        rel="stylesheet">
    <!-- JQVMap -->
    <link href="../../AR/vendors/jqvmap/dist/jqvmap.min.css" rel="stylesheet" />
    <!-- bootstrap-daterangepicker -->
    <link href="../../AR/vendors/bootstrap-daterangepicker/daterangepicker.css" rel="stylesheet">
    <!-- Custom Theme Style -->
    <link href="../../AR/build/css/custom.min.css" rel="stylesheet">
   
</head>
<body>
    <form id="form1" runat="server">
   
 <%--  <asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>--%>


    <script type="text/javascript">

        var tableToExcel = (function () {
            var uri = 'data:application/vnd.ms-excel;base64,'
    , template = '<html xmlns:o="urn:schemas-microsoft-com:office:office" xmlns:x="urn:schemas-microsoft-com:office:excel" xmlns="http://www.w3.org/TR/REC-html40"><head><!--[if gte mso 9]><xml><x:ExcelWorkbook><x:ExcelWorksheets><x:ExcelWorksheet><x:Name>{worksheet}</x:Name><x:WorksheetOptions><x:DisplayGridlines/></x:WorksheetOptions></x:ExcelWorksheet></x:ExcelWorksheets></x:ExcelWorkbook></xml><![endif]--></head><body><table>{table}</table></body></html>'
    , base64 = function (s) { return window.btoa(unescape(encodeURIComponent(s))) }
    , format = function (s, c) { return s.replace(/{(\w+)}/g, function (m, p) { return c[p]; }) }
            return function (table, name) {
                if (!table.nodeType) table = document.getElementById(table)
                var ctx = { worksheet: name || 'Worksheet', table: table.innerHTML }
                window.location.href = uri + base64(format(template, ctx))
            }
        })()
    </script>


    <div class="container body">
        <div class="main_container">
            <div class="right_col" role="main">
                <div class="">
                    <div class="page-title">
                        <asp:ScriptManager ID="ScriptManager1" runat="server">
                        </asp:ScriptManager>
                         <div class="title_left">
                            <h2>
                              Attendance Sheet
                            </h2>
                        </div>
                        <div class="clearfix">
                        </div>
                  
                                <div class="row">

                                             <asp:Panel ID="pnlstudent"  runat="server">

                                         <div class="col-md-12 col-sm-12 col-xs-12">
                                        <div class="x_panel">
                                       
                                            <div class="x_content">
                                                <div class="form-horizontal form-label-left">
                                               

                                                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                                    <ContentTemplate>
                                                    
                                                     <div class="form-group">
                                                        <label class="control-label col-md-2 col-sm-2 col-xs-2">
                                                         Exam Name
                                                        </label>
                                                        <div class="col-md-10 col-sm-10 col-xs-10">
                                                            <asp:DropDownList ID="ddlexamname" AutoPostBack="true" class="form-control" runat="server"
                                                                Height="35px">
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>
                                               


                                                     <div class="form-group">
                                                        <label class="control-label col-md-2 col-sm-2 col-xs-2">
                                                            Faculty
                                                        </label>
                                                        <div class="col-md-10 col-sm-10 col-xs-10">
                                                            <asp:DropDownList ID="ddlcollege" AutoPostBack="true" class="form-control" runat="server"
                                                                Height="35px">
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>
                                               
                                                    <div class="form-group">
                                                          <label class="control-label col-md-2 col-sm-2 col-xs-2">
                                                            Level of Studies</label>
                                                         <div class="col-md-10 col-sm-10 col-xs-10">
                                                            <asp:DropDownList ID="ddlcoursetype" AutoPostBack="true" class="form-control" runat="server"
                                                                Height="35px">
                                                            </asp:DropDownList>
                                                          
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                       <label class="control-label col-md-2 col-sm-2 col-xs-2">
                                                            Course</label>
                                                       <div class="col-md-10 col-sm-10 col-xs-10">
                                                            <asp:DropDownList ID="ddlcourse" AutoPostBack="true" class="form-control" runat="server"
                                                                Height="35px">
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>



                                                      <div class="form-group">
                                                       <label class="control-label col-md-2 col-sm-2 col-xs-2">
                                                            Sem </label>
                                                       <div class="col-md-10 col-sm-10 col-xs-10">
                                                            <ddlsem:DropDownList ID="ddlsem" AutoPostBack="true" class="form-control" runat="server"
                                                                Height="35px">
                                                            </ddlsem:DropDownList>
                                                        </div>
                                                    </div>


                                                <%--  
                                                    <div class="form-group">
                                                       <label class="control-label col-md-2 col-sm-2 col-xs-2">
                                                            </label>
                                                       <div class="col-md-10 col-sm-10 col-xs-10">
                                                        
                                                                <asp:RadioButtonList ID="rblstudentlist"  class="form-control" RepeatDirection="Horizontal" runat="server">
                                                                <asp:ListItem  Text="Not Apply" Value="0" ></asp:ListItem>
                                                                <asp:ListItem  Text="Exam From Pending" Value="1" ></asp:ListItem>
                                                                <asp:ListItem  Text="Exam From Approved" Value="2" ></asp:ListItem>
                                                                 <asp:ListItem  Selected="True" Text="All List" Value="3" ></asp:ListItem>
                                                                </asp:RadioButtonList>
                                                                
                                                        </div>
                                                    </div>--%>
                                                                </ContentTemplate>
                                                    </asp:UpdatePanel>

                                                           <div class="form-group">
                                                       <label class="control-label col-md-2 col-sm-2 col-xs-2">
                                                            </label>
                                                       <div class="col-md-10 col-sm-10 col-xs-10">
                                                           <asp:Button ID="btnload" runat="server" Text="Load" />
                                                            <asp:Button ID="btn_cgpa" runat="server" Text="RABA" />
                                                            <asp:Button ID="btnfail" runat="server" Text="Practical-Foil" />
                                                             <asp:Button ID="btnpratcialattendance" runat="server" Text="Practical-Attendance Sheet" />
                                                             <asp:Button ID="btnTheoryattendance" runat="server" Text="Theory-Attendance Sheet" />
                                                             <img alt="" onclick="tableToExcel('grdstudent', 'W3C Example Table')" value="Export to Excel"
                    src="../../img/ExcelImage.jpg" />
                                                        </div>

                                                         <div class="col-md-10 col-sm-10 col-xs-10">
                                                             <asp:Button ID="btnfailnew" runat="server" Text="Practical-Foil New" />
                                                             <asp:Button ID="btnpratcialattendancenew" runat="server" Text="Practical-Attendance Sheet New" />
                                                             <asp:Button ID="btnTheoryattendancenew" runat="server" Text="Theory-Attendance Sheet New" />
                                                             <img alt="" onclick="tableToExcel('grdstudent', 'W3C Example Table')" value="Export to Excel"
                    src="../../img/ExcelImage.jpg" />
                                                        </div>


                                                    </div>


                                                    <div class="form-group">
                                                        <asp:GridView ID="grdstudent" GridLines="Both"  Width="100%"  DataKeyNames="studentid" AutoGenerateColumns="False" runat="server" CellPadding="0" 
                                                            ForeColor="#333333" >
        <Columns>

         <asp:TemplateField HeaderText="S.No.">
        <ItemTemplate>
             <%#Container.DataItemIndex+1 %>
        </ItemTemplate>
    </asp:TemplateField>

     <asp:TemplateField HeaderText="Sel">
                                <ItemTemplate>
                                    <asp:CheckBox ID="chkselect"   runat="server" />
                                </ItemTemplate>
                                <HeaderTemplate>
                                    <asp:CheckBox ID="chkall" runat="server" AutoPostBack="true" OnCheckedChanged="Change_student" />
                                </HeaderTemplate>
                            </asp:TemplateField>

                <asp:BoundField HeaderText="Faculty" DataField="Faculty"></asp:BoundField>
                <asp:BoundField HeaderText="Course" DataField="Course"></asp:BoundField>
               <asp:BoundField HeaderText="Sem" DataField="Sem"></asp:BoundField>
              <asp:BoundField HeaderText="Roll No" DataField="Rollnoid"></asp:BoundField>
                    <asp:BoundField HeaderText="Student Type" DataField="Studenttype"></asp:BoundField>
                <asp:BoundField HeaderText="Admission No." DataField="AdmissionNo"></asp:BoundField>
                 <asp:BoundField HeaderText="Enrollment No." DataField="EnrollmentNo"></asp:BoundField>
                 <asp:BoundField HeaderText="student" DataField="student"></asp:BoundField>

                 
                    <asp:BoundField HeaderText="Father Name" DataField="FatherName"></asp:BoundField>


   <%--   <asp:BoundField HeaderText="Status" DataField="Status"></asp:BoundField>--%>
    
                                        
        </Columns>
            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
            <EditRowStyle BackColor="#999999" />
            <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
            <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
            <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
            <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
            <SortedAscendingCellStyle BackColor="#E9E7E2" />
            <SortedAscendingHeaderStyle BackColor="#506C8C" />
            <SortedDescendingCellStyle BackColor="#FFFDF8" />
            <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
        </asp:GridView>
                                                   </div>
                                                                                                       
                                               </div>
                                            </div>
                                        </div>
                                    </div>
                                    </asp:Panel>



                     <asp:Panel ID="pnlprint"   Visible="false" runat="server">
                            <div class="row">
                                   <div class="col-md-12 col-sm-12 col-xs-12">
                                        <div class="x_panel">
                                           
                                            <div class="x_content">
                                                <div class="form-horizontal form-label-left">
                                              <asp:Button ID="btn_back_print" runat="server" Text="Back" />
                                                      <div class="ln_solid">     </div>

                                                    <div class="form-group">
                                                      <asp:Button ID="bntdocprint" runat="server" Text="Print" />
                
                    <div id="dvReport">
                     <asp:Panel ID="pnl1" runat="server">
                      <asp:Literal ID="Literal1" runat="server"></asp:Literal>
                  
                      </asp:Panel>
                    </div>
             </div>                
     
                                                
                                                   
                                                                                                

                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                   
                                </div>
                                    </asp:Panel> 

                    

 

                          
                                </div>
                          <%--  </ContentTemplate>
                        </asp:UpdatePanel>--%>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <br />
    <br />
<br />
<br />
<br />
<br />
<br />
<br />
<br />
<br />
<br />
<br />
    <br />
    ...

      
 
    <script src="../AR/vendors/jquery/dist/jquery.min.js"></script>
   <!-- Custom Theme Scripts -->
    <script src="../AR/build/js/custom.min.js"></script>
    </form>
</body>
</html>
