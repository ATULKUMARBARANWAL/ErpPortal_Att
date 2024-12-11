<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ProgramSubjectlist.aspx.vb" Inherits="TESTFILES_ProgramSubjectlist" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="../Bootstrap5/css/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <script src="../Bootstrap5/js/bootstrap.min.js" type="text/javascript"></script>
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.4.0/css/all.min.css"  />
   <script src="../LeadJquery/jquery1.min.js" type="text/javascript"></script>
    <style>
        body
        {
        background:#f2f3f5;
    }
   .maincontainer {
    border: 2px solid #fff;
    padding: 10px;
    background-color:#fff;
    border-radius: 6px;
    text-align: left;
   }
   
    .maincontainerm
{
    background-color:#f2f3f5;
    color:#15283c; 
    font-size:16px; 
    width:42px;
    height:41px ;
    border-radius:50%;
    padding-top:8px;
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
   .line
   {
       content:" ";
       width:100%;
       height:1px;
       background-color:#e1e2e3;
       margin-top:4px;
       margin-bottom:8px;
       
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
   #Download
   {
   color:#808080;
   font-size: 22px !important;
   cursor: pointer;
   font-weight: 500;
   border:none;
       }
   
  #Download:hover {
   color:black;
   font-weight: 600;
    } 
 #DownloadSubject
 {
   color:#808080;
   font-size: 22px !important;
   cursor: pointer;
   font-weight: 500;
   border:none;
       }
   
  #DownloadSubject:hover {
   color:black;
   font-weight: 600;
    } 
 #DdownloadUnitName
 {
   color:#808080;
   font-size: 22px !important;
   cursor: pointer;
   font-weight: 500;
   border:none;
       }
   
  #DdownloadUnitName:hover {
   color:black;
   font-weight: 600;
    } 
    #backtoprogram
  {
    font-size:24px;
    font-weight:600;
    color:#7c858f;
}
#backtoprogram:hover
{
    color:#15283c;
}
#backtocourseSubject{
    font-size:24px;
    font-weight:600;
    color:#7c858f;
    
}
#backtocourseSubject:hover
{
    color:#15283c;
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
    .hiddencol
    {
        display:none;
    }
    .ddlSemyear
    {
        border:1px solid gray;
        border-radius:3px;
        
    }
    
    </style>
    <script>
        function SelectheaderCheckboxes(objRef) {

            var GridView = objRef.parentNode.parentNode.parentNode;

            var inputList = GridView.getElementsByTagName("input");

            for (var i = 0; i < inputList.length; i++) {



                var row = inputList[i].parentNode.parentNode;

                if (inputList[i].type == "checkbox" && objRef != inputList[i]) {

                    if (objRef.checked) {


                        row.style.backgroundColor = "#f2f3f5";

                        inputList[i].checked = true;

                    }

                    else {

                        if (row.rowIndex % 2 == 0) {
                        }

                        else {

                            row.style.backgroundColor = "white";

                        }

                        inputList[i].checked = false;

                    }

                }

            }

        }
 </script>

</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:Panel ID="PanelCourseWise" runat="server" Visible="True">
    
     <div class="container-fluid maincontainer mt-2">
                     
                       <div class="row">
     <div class="col-md-12">
       <div class="text-center maincontainerm m-0">
        <asp:LinkButton ID="backbotton" runat="server"><i class="fa-solid fa-arrow-left"></i></asp:LinkButton>
     </div>
     </div>
     </div>
        
                       <div class="row mt-2">
                          <div class="col-md-7">
                              <div class="heading1">
                              <h5>
                                  <asp:Label ID="Lblcoursename" runat="server" Text="N/A"  CssClass="fw-bold"></asp:Label>
                                     (<asp:Label ID="Lblcoursecode" runat="server" Text="N/A" CssClass="fw-bold"></asp:Label>)
                                  <asp:Label ID="Lblcourseid" runat="server" class="hiddencol" Text="N/A"></asp:Label>
                                     </h5>
                               </div>
                             
                          </div>
                          
                          
                          <div class="col-md-5 text-end">
                              <h6><span class="">Total Subject : <span class="">
                              <asp:Label ID="lbltotalsub" runat="server" CssClass="fw-bold" Text="N/A"></asp:Label></span>
                              </span></h6> 
                          </div>
                       </div>

                       <div class="row">
                          <div class="col-md-12">
                            <div class="line">
                            </div>
                          </div>
                       </div>

                       <div class="row mt-1">
                          
                          
                          <div class="col-md-12 d-flex justify-content-end">
                          <table width="50%">
                          <tr width="100%">
                             <td width="50%" align="right">
                                  
                          </td>
                          <td width="15%" align="right">
                           <asp:Label ID="Label2" runat="server" Text="Select: "></asp:Label>&nbsp
                          </td>
                          <td width="15%">
                          <asp:DropDownList ID="Ddlsemyear" AutoPostBack="true"  class="ddlSemyear" Width="100%" runat="server" >
                          
                              </asp:DropDownList>
                          </td>
                          <td width="15%" >
                          &nbsp 
                              <asp:Label ID="Lblsemyear" runat="server" Text="Label"></asp:Label>
                          </td>
                          <td width="5%" align="right">
                          <asp:LinkButton ID="Download" runat="server" CssClass="DownloadExcel"> <i class="fa fa-download fa-2"></i></asp:LinkButton>
                                  
                          </td>
                         
                          </tr>
                          </table>
                              
                          </div>
                         
                       </div>

                       <div class="row mt-2">
                                  <div class="col-md-12 text-end">
                                  </div>
                       </div>

                       <div class="row mt-3">
                          <div class="col-md-12">
                            <asp:GridView ID="grdSubplan" GridLines="Both"  Width="100%"  DataKeyNames="SubjectId"
                           AutoGenerateColumns="False" runat="server" ShowHeaderWhenEmpty="true" CssClass="table table-bordered">
                           <Columns>
                           <asp:TemplateField HeaderText="S.No.">
                           <ItemTemplate>
                           <%#Container.DataItemIndex+1 %>
                           </ItemTemplate>
                          </asp:TemplateField>          
                          <asp:BoundField HeaderText="Leadid" ItemStyle-Width="0px"  DataField="Subjectid" ItemStyle-CssClass="hiddencol" HeaderStyle-CssClass="hiddencol" ReadOnly="true"></asp:BoundField>           
            <asp:BoundField HeaderText="Subject Name"  DataField="Subject" ItemStyle-CssClass="hiddencol" HeaderStyle-CssClass="hiddencol"></asp:BoundField>
                          <asp:TemplateField HeaderText="Subject Name">
                          <ItemTemplate >
                          <asp:LinkButton  Text='<%# Eval("Subject") %>' ID="namelink" runat="server" CommandName="SubjectName"
                          CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>' CssClass="SubjectName"></asp:LinkButton>       
                           </ItemTemplate>
                           </asp:TemplateField>
                           <asp:BoundField HeaderText="Subject Code" DataField="Subjectcode"></asp:BoundField>
                           <asp:BoundField HeaderText="Subject Prefix" DataField="SubPrefix"></asp:BoundField>
                            <asp:BoundField HeaderText="Total Lecture" DataField="Totallecture"></asp:BoundField>
                             <asp:BoundField HeaderText="Total Unit" DataField="TotalUnit"></asp:BoundField>
                           <asp:BoundField HeaderText="Sem/Year" DataField="Semyear"></asp:BoundField>     
                            
                          </Columns>
           
                         </asp:GridView>
                          </div>
                       </div>
                      <div class="row">
                                 <div class="col-md-12 text-end">
                                 <asp:LinkButton ID="LinkButton4" runat="server" Text="" class="btn btnAddProgram text-white pe-3">Save</asp:LinkButton>
                                     <asp:LinkButton ID="btnAddProgram" runat="server" Text="" class="btn btnAddProgram text-white"><i class="fa-solid fa-plus"></i> Map Subject</asp:LinkButton>
                      </div>
                      </div>
                       
                       
    </div>
    </asp:Panel>

    <asp:Panel ID="PanelSubjectList" runat="server" Visible="false">
    <div class="container-fluid maincontainer mt-2">
                     
                      <div class="row">
                       <div class="col-md-12">
                       <div class="text-center maincontainerm m-0">
                       <asp:LinkButton ID="backtoprogram" runat="server"><i class="fa-solid fa-arrow-left"></i></asp:LinkButton>
                      </div>
                     </div>
                    </div>
                   
                       <div class="row">
                          <div class="col-md-12">
                            <div class="line">
                            </div>
                          </div>
                       </div>

                       <div class="row mt-1">
                                              <div class="col-md-12 d-flex justify-content-end">
                          <table width="50%">
                          <tr width="100%">
                          <td width="50%" align="right">
                                  
                          </td>
                          <td width="15%" align="right">
                           <asp:Label ID="Label1" runat="server" Text="Select: "></asp:Label>&nbsp
                          </td>
                          <td width="15%">
                          <asp:DropDownList ID="ddlSemyearsub" runat="server" AutoPostBack="true" Width="100%" CssClass="ddlSemyear">
                          
                              </asp:DropDownList>
                          </td>
                          <td width="15%" >
                          &nbsp 
                              <asp:Label ID="Lblsemyearadd" runat="server" Text="Label"></asp:Label>
                          </td>
                          <td width="5%" align="right">
                         <asp:LinkButton ID="DownloadSubject" runat="server" CssClass="DownloadExcel"> <i class="fa fa-download fa-2"></i></asp:LinkButton>
                                           
                          </td>
                          
                          </tr>
                          </table>
                              
                          </div>

                       </div>


                  

                       <div class="row mt-3">
                          <div class="col-md-12">
                            <asp:GridView ID="grdSubjectList" GridLines="Both"  Width="100%"  DataKeyNames="SubjectId"
                           AutoGenerateColumns="False" runat="server" CellPadding="0" 
                           ShowHeaderWhenEmpty="true" CssClass="table table-bordered bg-white rounded_corners">
                           <Columns>
                              <asp:TemplateField HeaderText="S.No.">
                         <HeaderTemplate>
                         <asp:CheckBox ID="CheckBox1" runat="server"  Onclick="javascript:SelectheaderCheckboxes(this)" Checked/>
                         </HeaderTemplate>
                         <ItemTemplate>
                         <asp:CheckBox ID="btnselect" runat="server"   CommandName ="select" CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>' Checked/>
                         </ItemTemplate>
                         </asp:TemplateField>
                           <asp:TemplateField HeaderText="S.No.">
                           <ItemTemplate>
                           <%#Container.DataItemIndex+1 %>
                           </ItemTemplate>
                          </asp:TemplateField>
        
                           <asp:BoundField HeaderText="Subject Name" DataField="Subject"></asp:BoundField>
                           <asp:BoundField HeaderText="Subject Code" DataField="Subjectcode"></asp:BoundField>
                           <asp:BoundField HeaderText="Subject Prefix" DataField="Subprefix"></asp:BoundField>
                              
                      
                          </Columns>
           
                         </asp:GridView>
                          </div>
                       </div>

                      <div class="row">
                      <div class="col-md-12 text-end">
                           <asp:LinkButton ID="LinkButton1" runat="server" Text="" class="btn btnAddProgram text-white pe-3">Save</asp:LinkButton>
                           <asp:LinkButton ID="LinkButton3" runat="server" Text="" class="btn btnAddProgram text-white" data-bs-toggle="modal" data-bs-target="#myModal"><i class="fa-solid fa-plus"></i> Add Subject</asp:LinkButton>
                      </div>
                      </div>
                       
                       <div class="row">
                          <div class="modal fade" id="myModal" data-bs-backdrop="static" data-bs-keyboard="false">
           <div class="modal-dialog modal-lg">
           <div class="modal-content">

           <div class="modal-header">
            <h5 class="modal-title">Add Subject</h5>
           <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
           </div>

           <div class="modal-body">
                      
                   <div class="form-group row">
                     <div class="col-md-1"></div>
                      <asp:Label ID="lblsubject" class="col-md-4 col-form-label text-md-left labeltext" runat="server">Subject Name :</asp:Label>
                       <div class="col-md-6">
                           <asp:TextBox ID="txtsub" runat="server" class="form-control input-sm" Placeholder=""></asp:TextBox>
                       </div>             
                     </div>

                   <div class="form-group row mt-2">
                        <div class="col-md-1"></div>
                        <asp:Label ID="lblcode" class="col-md-4 col-form-label text-md-left labeltext" runat="server">Subject Code :</asp:Label>
                        <div class="col-md-6">
                           <asp:TextBox ID="txtcode" runat="server" class="form-control input-sm" Placeholder=""></asp:TextBox>
                        </div>          
                        </div>

                   <div class="form-group row mt-2">
                           <div class="col-md-1"></div>
                             <asp:Label ID="lblprefix" class="col-md-4 col-form-label text-md-left labeltext" runat="server"> Prefix :</asp:Label>
                            <div class="col-md-6">
                              <asp:TextBox ID="txtprefix" runat="server" class="form-control input-sm" Placeholder=""></asp:TextBox>
                            </div>         
                          </div>
         
           </div>
               
           <div class="modal-footer">
         
                  <asp:LinkButton ID="btnCancel" class="btn btn-outline-dark mr-auto btncanelg" data-dismiss="modal" runat="server"><i class="fa fa-window-close"></i> Cancel</asp:LinkButton>
                  <asp:Button ID="btnAdd" class="btn communicatesub" Width="20%" runat="server" Text="Add" />
           
           </div>
          
           </div>
           </div>
           </div>
                       </div>
    </div>
    </asp:Panel>

    <asp:Panel ID="PanelUnitNameDescription" runat="server" Visible="false">
    <div class="container-fluid maincontainer mt-2">
                     
                      <div class="row">
                       <div class="col-md-12">
                       <div class="text-center maincontainerm m-0">
                       <asp:LinkButton ID="backtocourseSubject" runat="server" CssClass="backtocourseSubject"><i class="fa-solid fa-arrow-left"></i></asp:LinkButton>
                      </div>
                     </div>
                    </div>
                   
                       <div class="row mt-2 mb-1">
                          <div class="col-md-5">
                              <div class="heading1">
                              <h5><span class="">
                               <asp:Label ID="LblSubname" runat="server" CssClass="fw-bold" Text="N/A"></asp:Label>(<asp:Label
                                   ID="Lblsubcode" runat="server" Text="Label"></asp:Label>)
                                  <asp:Label ID="Lblsubid" runat="server" hidden="true" Text="labrl"></asp:Label>
                              </span></h5> 
                               </div>
                             
                          </div>
                          
                          <div class="col-md-2">
                          </div>
                          <div class="col-md-5 text-end">
                          <table width="100%">
                          <tr width="100%">
                          <td width="39%" align="right">
                           <asp:Label ID="Label7" runat="server" Text="Search : "></asp:Label>
                          </td>
                          <td width="1%"></td>
                          <td width="60%">
                              <asp:TextBox ID="TextBox2" runat="server" CssClass="form-control" Height="35px" Placeholder="Search By Unit Name"></asp:TextBox>
                          </td>
                          </tr>
                          </table>
                          </div>
                       </div>

                       <div class="row">
                          <div class="col-md-12">
                            <div class="line">
                            </div>
                          </div>
                       </div>

                       <div class="row mt-2">
                                  <div class="col-md-12 text-end">
                                  <asp:LinkButton ID="DdownloadUnitName" runat="server" CssClass="DownloadExcel"> <i class="fa fa-download fa-2"></i></asp:LinkButton>
                                  </div>
                       </div>

                       <div class="row mt-3">
                          <div class="col-md-12">
                            <asp:GridView ID="GridUnitDec" GridLines="Both"  Width="100%"  DataKeyNames="Subunitid"
                           AutoGenerateColumns="False" runat="server" CellPadding="0" 
                          
                           ShowHeaderWhenEmpty="true" CssClass="table table-bordered bg-white rounded_corners">
                            <%--ShowFooter="true" AutoGenerateColumns="false" OnRowCreated="Gridview1_RowCreated"--%>
                           <Columns>
                           <asp:TemplateField HeaderText="S.No.">
                           <ItemTemplate>
                           <%#Container.DataItemIndex+1 %>
                           </ItemTemplate>
                          </asp:TemplateField>
        
                           <asp:BoundField HeaderText="UnitName" DataField="UnitName"></asp:BoundField>
                           <asp:BoundField HeaderText="Descripption" DataField="Description"></asp:BoundField>
                           <asp:BoundField HeaderText="Lecture duration" DataField="Timeduration"></asp:BoundField>
                    
                    
                          </Columns>
                        </asp:GridView>
                          </div>
                       </div>
                       <div class="row">
                       <div class="col-md-12 text-end">
                              <asp:LinkButton ID="LinkButton2" runat="server" Text="" class="btn btnaddDecription"><i class="fa-solid fa-plus"></i></asp:LinkButton>
                       </div>
                       </div>
               <asp:Panel ID="pnlsave" visible="false" runat="server">
        
                      <div class="row mt-2">
                      <div class="col-md-12 Justify-content-center">
                           <asp:LinkButton ID="LinkButton5" runat="server" Text="" class="btn btnAddProgram text-white pe-3"><i class="fa-solid fa-plus"></i> Add</asp:LinkButton>
                      </div>
                      </div>
                      </asp:Panel>
                       
                       <div class="row">
                          <div class="modal fade" id="Div1" data-bs-backdrop="static" data-bs-keyboard="false">
                       <div class="modal-dialog modal-lg">
                      <div class="modal-content">
 
                     <div class="modal-header">
                     <h5 class="modal-title">Add Subject</h5>
                     <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                     </div>

                    <div class="modal-body">
                      
                   <div class="form-group row">
                     <div class="col-md-1"></div>
                      <asp:Label ID="Label9" class="col-md-4 col-form-label text-md-left labeltext" runat="server">Subject Name :</asp:Label>
                       <div class="col-md-6">
                           <asp:TextBox ID="TextBox3" runat="server" class="form-control input-sm" Placeholder=""></asp:TextBox>
                       </div>             
                     </div>

                   <div class="form-group row mt-2">
                        <div class="col-md-1"></div>
                        <asp:Label ID="Label10" class="col-md-4 col-form-label text-md-left labeltext" runat="server">Subject Code :</asp:Label>
                        <div class="col-md-6">
                           <asp:TextBox ID="TextBox4" runat="server" class="form-control input-sm" Placeholder=""></asp:TextBox>
                        </div>          
                        </div>

                   <div class="form-group row mt-2">
                           <div class="col-md-1"></div>
                             <asp:Label ID="Label11" class="col-md-4 col-form-label text-md-left labeltext" runat="server"> Prefix :</asp:Label>
                            <div class="col-md-6">
                              <asp:TextBox ID="TextBox5" runat="server" class="form-control input-sm" Placeholder=""></asp:TextBox>
                            </div>         
                          </div>
         
           </div>
               
           <div class="modal-footer">
         
                  <asp:LinkButton ID="LinkButton7" class="btn btn-outline-dark mr-auto btncanelg" data-dismiss="modal" runat="server"><i class="fa fa-window-close"></i> Cancel</asp:LinkButton>
                  <asp:Button ID="Button1" class="btn communicatesub" Width="20%" runat="server" Text="Add" />
           
           </div>
          
           </div>
           </div>
           </div>
                       </div>
    </div>
    </asp:Panel>

    </form>
    <script src="../LeadJquery/table2excel.js" type="text/javascript"></script>
  
<script type="text/javascript">
    $("body").on("click", "#Download", function () {
        $("[id*=grdSubplan]").table2excel({
            filename: "ProgramWiseSubjectList.xls"
        });
    });
</script>
<script type="text/javascript">
    $("body").on("click", "#DownloadSubject", function () {
        $("[id*=grdSubjectList]").table2excel({
            filename: "SubjectList.xls"
        });
    });
</script>

<script type="text/javascript">
    $("body").on("click", "#DdownloadUnitName", function () {
        $("[id*=GridUnitDec]").table2excel({
            filename: "UnitList.xls"
        });
    });
</script>

</body>
</html>
