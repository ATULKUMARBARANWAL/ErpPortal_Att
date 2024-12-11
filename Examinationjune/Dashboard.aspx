<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Dashboard.aspx.vb" EnableEventValidation = "false" Inherits="ExaminationNEW11_Dashboard" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="../Bootstrap5/css/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <script src="../Bootstrap5/js/bootstrap.min.js" type="text/javascript"></script>
    <link href="../ExaminationNCSS/ExamDahboard1.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.9.1/jquery.min.js"></script>
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.4.0/css/all.min.css"  />
  <script src="../LeadJquery/jquery1.9.1.min.js" type="text/javascript"></script>
<style type = "text/css">
     .Search
 {
     font-weight:500;
     font-size:16px;
    
     }
   .maincontainer {
    border: 2px solid #fff;
    padding: 10px;
    background-color:#fff;
    border-radius: 6px;
    text-align: left;
   }
   .CourseName
   {
       color:#1ed085;
       text-decoration:none;
       font-weight:400;
    }
     .CourseName:hover
   {
       color:#20a16a;
       text-decoration:none;
       font-weight:400;
    }
     .communicatesub
{
    background-color:#1ed085;
    color:#f2f3f5;
    font-weight:500;
    }
 .communicatesub:hover
{
    background-color:#1ed085;
    color:#f2f3f5;
    font-weight:500;
    border:1px solid #1ed085;
    }
  .btncanelg
  {
      margin-left:270px;
      }
    .modal-header {
  
  text-align: left;
  font-size: 22px;
  color: #f2f3f5;
  
  background-color: #152837;
  border-bottom: 0px;
  height:50px;
}

.hidden
{
    display:none;
    }
       .hiddencol1
{
    Display:none;
    }
</style>
</head>
<body>
<form id="form1" runat="server">
  <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
 <div class="container-fluid maincontainer mt-1">
                   
                       <div class="row">
                        <div class="col-md-2">
                              <div class="heading1">
                              
                                  <h5>Exam Dashboard </h5>
                                     
                                      </div>
                              
                                </div>
                              <div class="col-md-4"></div>
                 <div class="col-md-1 text-end" hidden="true">
               
                  <h6 style="padding-top:5px;">Academic Year:</h6>
                       </div>         
                       
                      <div class="col-md-1" hidden="true">
                              <asp:DropDownList ID="ddlacademicyear" Enabled=false autopostback="true" class="form-control" runat="server">
                 </asp:DropDownList>
                  </div>

                 <div class="col-md-6 text-end">
                              <h6><span class="">Academic Year : <span class="">
                              <asp:Label ID="lblacademicyear" runat="server" CssClass="fw-bold" Text="N/A"></asp:Label></span>
                              </span></h6> 
                          </div>
            
                 </div>

                 <div class="row">
                          <div class="col-md-12">
                            <div class="line">
                            </div>
                          </div>
                       </div>

                 <div class="row mt-2">
                 <div class="col-md-6"></div>
                 <div class="col-md-2 mt-2 text-end"> 
                 <asp:Label ID="Label2" runat="server" CssClass="Search" Text="Search :"></asp:Label>
                 </div>
                   <div class="col-md-4 d-flex">
                               

                 <table width="100%">
                 <tr width="100%">
                   <td width="80%">
                        <txtsearchprog:textbox ID="stusearch" class="form-control" runat="server"></txtsearchprog:textbox>
                   </td>
                    <td width="10%"></td>
                   <td width="10%" align="center">
                      <asp:LinkButton ID="Download" runat="server" CssClass="DownloadExcel" > <i class="fa fa-download fa-2"></i></asp:LinkButton>
                    </td>
                 </tr>
              </table>
                 </div> 
                   </div>

                       
                       
                       <div class="row">
                       <div class="col-md-12">
                    <div class="container-fluid">
                                <div class="row">
                                 <div class="col-md-2">
                            <%--  <div class="heading1">
                              
                                  <h5>No of Programs: 
                                      <asp:Label ID="NoofPrograms" runat="server" Text="" CssClass="fw-bold hidden"></asp:Label></h5>
                                      </div>--%>
                              
                                </div>
                                <div class="col-md-4"></div>


                                  <div class="col-md-1"></div>

                                  
                          <div class="col-md-4">
              
          </div>
                                </div>
                                <div class="msg_list_main mt-3">
                          <asp:GridView ID="grdPrograms" GridLines="Both"  Width="100%"  DataKeyNames="classid"
                           AutoGenerateColumns="False" runat="server" CellPadding="0" 
                           ShowHeaderWhenEmpty="true" CssClass="table table-bordered bg-white rounded_corners"  >
                <Columns>
    <asp:TemplateField HeaderText="S.No.">
        <ItemTemplate>
            <%# Container.DataItemIndex + 1 %> <!-- S.No is auto-incremented -->
        </ItemTemplate>
    </asp:TemplateField>

    <asp:BoundField HeaderText="Class Code" DataField="ClassCode"></asp:BoundField>
    
    <asp:BoundField HeaderText="Classid" HeaderStyle-CssClass="hidden" ReadOnly="true" 
                    ItemStyle-Width="0px" ItemStyle-CssClass="hidden" DataField="Classid"></asp:BoundField>
    
    <asp:BoundField HeaderText="Class" DataField="ClassName"></asp:BoundField>


    <asp:TemplateField HeaderText="Total Student">
        <ItemTemplate>
            <asp:LinkButton Text='<%# Eval("TotalStudents") %>' ID="namelinkes" runat="server" 
                            CommandName="Studentlist" CssClass="CourseName" 
                            CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>'></asp:LinkButton>
        </ItemTemplate>
    </asp:TemplateField>

    
<asp:TemplateField HeaderText="Add Section">
    <ItemTemplate>
        <asp:LinkButton ID="addSectionLink" runat="server" CommandName="AddSection" 
                        CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>' 
                        CssClass="CourseName">
            <i class="fa-solid fa-plus"></i>
        </asp:LinkButton>
    </ItemTemplate>
</asp:TemplateField>

<asp:TemplateField HeaderText="Add ClassTeacher">
    <ItemTemplate>
        <asp:LinkButton ID="addClassTeacher" runat="server" CommandName="AddClassTeacher" 
                        CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>' 
                        CssClass="CourseName">
          <i class="fa-solid fa-user-plus"></i>
        </asp:LinkButton>
    </ItemTemplate>
</asp:TemplateField>
   <asp:TemplateField HeaderText="Approve Leave">
    <ItemTemplate>
        <asp:LinkButton ID="approveLeave" runat="server" CommandName="ApproveLeave" 
                        CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>' 
                        CssClass="CourseName">
        <i class="fa-solid fa-person-circle-check"></i>
        </asp:LinkButton>
    </ItemTemplate>
</asp:TemplateField>
    <asp:TemplateField HeaderText="Subject">
        <ItemTemplate>
            <asp:LinkButton ID="namelink1" runat="server" CommandName="Subjectmapping"
                            CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>' 
                            CssClass="CourseName"><i class="fa-solid fa-book"></i></asp:LinkButton>
        </ItemTemplate>
    </asp:TemplateField>
    
    <asp:TemplateField HeaderText="Assign Faculty">
        <ItemTemplate>
            <asp:LinkButton ID="assignlink" runat="server" CommandName="AssignFaculty" 
                            CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>' 
                            CssClass="CourseName"><i class="fa-solid fa-chalkboard-user"></i></asp:LinkButton>
        </ItemTemplate>

    </asp:TemplateField>

    <asp:TemplateField HeaderText="Timetable">
        <ItemTemplate>
            <asp:LinkButton ID="timetbllink" runat="server" CommandName="Timetable" 
                            CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>' 
                            CssClass="CourseName"><i class="fa-solid fa-calendar-days"></i></asp:LinkButton>
        </ItemTemplate>

    </asp:TemplateField>


</Columns>

           
                  </asp:GridView>
                                </div>
                              
                                </div>
                                </div>
                </div>


                   <div class="row" hidden="true">
                       <div class="col-md-12">
                    <div class="container-fluid">
                                <div class="row mt-3">
                                 <div class="col-md-2">
                            <%--  <div class="heading1">
                              
                                  <h5>No of Programs: 
                                      <asp:Label ID="NoofPrograms" runat="server" Text="" CssClass="fw-bold hidden"></asp:Label></h5>
                                      </div>--%>
                              
                                </div>
                                <div class="col-md-4"></div>


                                  <div class="col-md-1"></div>

                                  
                          <div class="col-md-4">
              
          </div>
                                </div>
                                <div class="msg_list_main mt-3">
                          <asp:GridView ID="GridView1" GridLines="Both"  Width="100%"  DataKeyNames="Courseid"
                           AutoGenerateColumns="False" runat="server" CellPadding="0" 
                           ShowHeaderWhenEmpty="true" CssClass="table table-bordered bg-white rounded_corners">
                <Columns>
                  <asp:TemplateField HeaderText="S.No." ItemStyle-HorizontalAlign="Center">
                  <ItemTemplate>
                       <%#Container.DataItemIndex+1 %>
                 </ItemTemplate>
                 </asp:TemplateField>
                 <asp:BoundField HeaderText="Class Code" DataField="CourseCode" ItemStyle-HorizontalAlign="Center"></asp:BoundField>
                 <asp:BoundField HeaderText="Courseid" HeaderStyle-CssClass="hidden" ReadOnly="true" ItemStyle-Width="0px" ItemStyle-CssClass="hidden" DataField="Courseid"></asp:BoundField>
            <asp:BoundField HeaderText="Program" DataField="Course" ItemStyle-HorizontalAlign="Center"></asp:BoundField>
                 
                  <asp:BoundField HeaderText="Sem/Year" DataField="Total Sem/Year" ItemStyle-HorizontalAlign="Center"></asp:BoundField>
                   <asp:BoundField HeaderText="Total student" DataField="Total" ItemStyle-HorizontalAlign="Center"></asp:BoundField>
                
              
            
                </Columns>
           
                  </asp:GridView>
                                </div>
                              
                                </div>
                                </div>
                </div>
</div>
    </form>
   <%-- <script src="../LeadJquery/table2excel.js" type="text/javascript"></script>
  
<script type="text/javascript">
    $("body").on("click", "#", function () {
        $("[id*=grdPrograms]").table2excel({
            filename: "ProgramList.xls"
        });
    });
</script>--%>

</body>
</html>

