<%@ Page Language="VB" AutoEventWireup="false" CodeFile="MISdashboard.aspx.vb" Inherits="StudentMis_MISdashboard" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="../Bootstrap5/css/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <script src="../Bootstrap5/js/bootstrap.min.js" type="text/javascript"></script>
    <link href="../ExaminationNCSS/ExamDahboard1.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.9.1/jquery.min.js"></script>
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.4.0/css/all.min.css"  />

<style>
   .maincontainer {
    border: 2px solid #fff;
    padding: 10px;
    background-color:#fff;
    border-radius: 6px;
    text-align: left;
   }
   .CourseName
   {
       font-size:18px;
       color:#1ed085;
       text-decoration:none;
       font-weight:400;
    }
     .CourseName:hover
   {
       color:#20a16a;
       text-decoration:none;
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
.hidden
{
    display:none;
    }
     .gridicon
   {
       color:#1ed085;
       text-decoration:none;
       font-size:20px;
       font-weight:400;
    }
.gridicon:hover
   {
       color:#1aad6f;
       text-decoration:none;
       font-weight:400;
    }
    .hiddencol
    {
        display:none;
    }
</style>
</head>

<body>
<form id="form2" runat="server">
  <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
 <div class="container-fluid maincontainer mt-1">
                   
                       <div class="row">
                        <div class="col-md-3">
                              <div class="heading1">
                              
                                  <h5>Student MIS
                                     </h5> 
                                      </div>
                              
                                </div>
                     
                              
                 <div class="col-md-5"></div>
                   <div class="col-md-2 text-end">
                                <h5 style="padding-top:5px;">Academic Year:</h5>
                                </div>
                         <div class="col-md-2">
                              <asp:DropDownList ID="ddlacademicyear" autopostback="true" class="form-select" runat="server">
                 </asp:DropDownList>
                 </div>
                               
                     
                   </div>
                       <div class="row">
                          <div class="col-md-12">
                            <div class="line">
                            </div>
                          </div>
                       </div>
                       
                       <div class="row">
                       <div class="col-md-12">
                   
                                <div class="row mt-3">
                                 <div class="col-md-2">
                            <%--  <div class="heading1">
                              
                                  <h5>No of Programs: 
                                      <asp:Label ID="NoofPrograms" runat="server" Text="" CssClass="fw-bold hidden"></asp:Label></h5>
                                      </div>--%>
                              
                                </div>
                                <div class="col-md-4"></div>


                                  <div class="col-md-1"></div>

                                  <div class="col-md-1 mt-1 text-end"> <asp:Label ID="Label2" runat="server" Text="Search :"></asp:Label></div>
                          <div class="col-md-4">
              <table width="100%">
                 <tr width="100%">
                   <td width="90%">
                       <asp:TextBox ID="txtSearchSubject" runat="server" AutoPostBack="true" CssClass="form-control" Placeholder="Search by Program Name"></asp:TextBox>
                   </td>
                   <td width="10%" align="center">
                      <asp:LinkButton ID="Download" runat="server" CssClass="DownloadExcel"> <i class="fa fa-download fa-2"></i></asp:LinkButton>
                    </td>
                 </tr>
              </table>
          </div>
                                </div>
                                <div class="custom-scrollbar-css mt-3">
                          <asp:GridView ID="grdPrograms" GridLines="Both"  Width="100%"  
                           AutoGenerateColumns="False" runat="server" CellPadding="0" 
                           ShowHeaderWhenEmpty="true" CssClass="table table-bordered ">
                <Columns>
                   <asp:TemplateField HeaderText="S.No.">
                  <ItemTemplate>
                       <%#Container.DataItemIndex+1 %>
                 </ItemTemplate>
                 </asp:TemplateField>
                 <asp:BoundField HeaderText="Courseid" DataField="Courseid" ItemStyle-Width="0px" ItemStyle-CssClass="hiddencol" HeaderStyle-CssClass="hiddencol" ReadOnly="true"></asp:BoundField>
                                  <asp:BoundField HeaderText="Crssesnid" DataField="Coursesessionid" ItemStyle-Width="0px"   ItemStyle-CssClass="hiddencol" HeaderStyle-CssClass="hiddencol" ReadOnly="true"></asp:BoundField>
               <asp:BoundField HeaderText="Program Code" DataField="Coursecode"></asp:BoundField>               
               <asp:BoundField HeaderText="Program Name" DataField="Course"></asp:BoundField>
               <asp:BoundField HeaderText="Duration" DataField="Coursetype"></asp:BoundField>
 
               <asp:TemplateField HeaderText="Total student">
            <ItemTemplate >
             <asp:LinkButton  Text='<%# Eval("Total") %>' ID="namelink" runat="server" CommandName="Studentlist"  CssClass="CourseName" CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>'  ></asp:LinkButton>       
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Id Card">
            <ItemTemplate >
             <asp:LinkButton ID="namelink1" runat="server" CommandName="Idcard"  CssClass="gridicon" CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>'  >
             <i class="fa-solid fa-address-card"></i></asp:LinkButton>       
            </ItemTemplate>
        </asp:TemplateField>
               
                </Columns>
           
                  </asp:GridView>
                                </div>
                               
                                </div>
                </div>
                </div> 
    </form>
    <script src="../LeadJquery/table2excel.js" type="text/javascript"></script>
  
<script type="text/javascript">
    $("body").on("click", "#Download", function () {
        $("[id*=grdPrograms]").table2excel({
            filename: "ProgramList.xls"
        });
    });
</script>

   
</body>
</html>
