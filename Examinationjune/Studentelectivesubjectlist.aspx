<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Studentelectivesubjectlist.aspx.vb" Inherits="Examinationjune_Studentelectivesubjectlist" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">


<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    
    <link href="../Bootstrap5/css/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <script src="../Bootstrap5/js/bootstrap.min.js" type="text/javascript"></script>
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.4.0/css/all.min.css"  />
    <style type="text/css" >
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
    border:none;
    }
 .communicatesub:hover
{
       color:#fff;
       background-color:#1aad6f;
       border:none;
       box-shadow:0px 1px 5px 1px #dcdcdc;
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
 .addsubject
    {
   color:#15283c;
   font-size: 18px !important;
   cursor: pointer;
   font-weight: 500;
   border:1px solid #15283c; 
   background-color:#fff;
   text-decoration:none;
   text-align :center;
   border-radius:4px;
   padding:3px 10px;
    }
    .addsubject:hover
    {
   color:#fff;
   background-color:#15283c;
   text-decoration:none;
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
 .hiddencol
    {
        display:none;
    }
     .Submit
 {
     font-size: 18px !important;
      font-weight: 500;
      text-align:center;
      cursor: pointer;
      color:#fff;
      background-color:#1ed085;
      border:none;
      padding:3px 10px;
      width:20%;
      
   }
   .Submit:hover
   {
       color:#fff;
       background-color:#1aad6f;
       border:none;
       box-shadow:0px 1px 5px 1px #dcdcdc;
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
                     
         <asp:DropDownList ID="ddlacademicyear" AutoPostBack="true" Visible="false" runat="server">
         <asp:ListItem >2021</asp:ListItem>
         </asp:DropDownList>
        
                       <div class="row mt-2">

                          <div class="col-md-8">
                           <table width="100%">
                    <tr width="100%">
                    <td width="5%">
                              <div class="heading1 d-flex">
                              <asp:LinkButton ID="backbotton" class="backbotton" runat="server"><i class="fa-solid fa-arrow-left"></i></asp:LinkButton>
                              <asp:LinkButton ID="backbotton1" class="backbotton" Visible="false" runat="server"><i class="fa-solid fa-arrow-left"></i></asp:LinkButton>
                              
                               </div>
                   </td>
                   
                    <td width="10%" align="right">
                    Subject:&nbsp
                    </td>
                    <td width="40%">
                    <asp:DropDownList  ID="Ddlprogram" AutoPostBack="true" class="form-select" runat="server">
                    
                                            </asp:DropDownList>
                                     
                    </td>
                    <td width="45%"></td>
                    </tr>
                    </table>
                    
                              
                             
                          </div>
                          
                          
                          <div class="col-md-4 text-end">
                              <h6><span class="">Academic Year : <span class="">
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
                          
                          
                          <div class="col-md-6 d-flex">
                          <table width="50%">
                          <tr width="100%">
                          <td width="30%" align="right">
                          <%-- <asp:Label ID="Label2" runat="server" Text="Filter: "></asp:Label>&nbsp--%>
                          </td>
                          <td width="33%">
                       <%--   <asp:DropDownList ID="Ddlsemyear" AutoPostBack="true" Width="100%"  runat="server" >
                          
                              </asp:DropDownList>--%>
                           <%--   <asp:DropDownList ID="Ddlsemeser" visible="false" AutoPostBack="true" Width="100%"  runat="server" >
                          
                              </asp:DropDownList>--%>
                          </td>
                          <td width="33%" >
                          &nbsp 
                           <%--   <asp:Label ID="Lblsemyear" runat="server" Text="N/A"></asp:Label>--%>
                          </td>
                          
                          
                          </tr>
                          </table>
                            
                          </div>
                          
                          <div class="col-md-6 d-flex justify-content-end ">
                              
                            <table width="70%">
                          <tr width="100%">
                          
                          <td width="100%">
                              <asp:TextBox ID="txtsearch" Placeholder="Search by subject" hidden="true" class="form-control" runat="server"></asp:TextBox>
                              <txtsearchsub:textbox ID="stusearch" class="form-control" runat="server"></txtsearchsub:textbox>

                               <txtsearchsuball:textbox ID="txtsearchforallsubject" class="form-control" Visible="false" runat="server"></txtsearchsuball:textbox>
                          </td>
                          
                          </tr>
                          </table>
                           &nbsp &nbsp <asp:LinkButton ID="Download" runat="server" CssClass="DownloadExcel"> <i class="fa fa-download fa-2"></i></asp:LinkButton>
                          
                          </div> 

                         
                       </div>

                       <div class="row mt-2 hiddencol">
                                  <div class="col-md-6 d-flex ">
                                  <asp:Panel ID="pnlsec" Visible="false" Width="40%" runat="server">
                              
                          <table width="100%">
                          <tr width="100%">
                          <td width="30%" align="right">
                           <asp:Label ID="Label4" runat="server" Text="Section: "></asp:Label>&nbsp
                          </td>
                          <td width="33%">
                          <asp:DropDownList ID="Ddlsection" AutoPostBack="true" Width="100%"  runat="server" >
                          
                              </asp:DropDownList>
                              
                          </td>
                          
                          
                          
                          </tr>
                          </table>
                          </asp:Panel>
                                  </div>
                       </div>
             
             <asp:Panel ID="PnlProgramsubjectlist" Visible="False" runat="server">
                       <div class="row mt-3">
                          <div class="col-md-12">
                              
                              
                          <div class="custom-scrollbar-css">
                          
                            <asp:GridView ID="gridallsubject" GridLines="Both"  Width="100%"  DataKeyNames="Studentid"
                           AutoGenerateColumns="False" runat="server" CellPadding="0" 
                           ShowHeaderWhenEmpty="true" CssClass="table table-bordered" OnRowDeleting="OnRowDeleting">
                           <Columns>
                           <asp:TemplateField HeaderText="S.No.">
                           <ItemTemplate>
                           <%#Container.DataItemIndex+1 %>
                           </ItemTemplate>
                          </asp:TemplateField>          
                          <asp:BoundField HeaderText="Leadid" ItemStyle-Width="0px"  DataField="Studentid" ItemStyle-CssClass="hiddencol" HeaderStyle-CssClass="hiddencol" ReadOnly="true"></asp:BoundField>           
           
              <asp:BoundField HeaderText="Subject Name"  DataField="Student" ItemStyle-CssClass="hiddencol" HeaderStyle-CssClass="hiddencol"></asp:BoundField>
                         <asp:BoundField HeaderText="Admissionno" DataField="Admissionno"></asp:BoundField>
                          
                          <asp:TemplateField HeaderText="Student Name">
                          <ItemTemplate >
                          <asp:LinkButton  Text='<%# Eval("Student") %>' ID="namelink" runat="server" CommandName="SubjectName"
                          CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>' CssClass="SubjectName"></asp:LinkButton>       
                           </ItemTemplate>
                           </asp:TemplateField>
                           <asp:BoundField HeaderText="Father Name" DataField="Fathername"></asp:BoundField>
                          
                            
                           

                             <asp:TemplateField HeaderText="Delete" >
                   <ItemTemplate>
                    <asp:ImageButton ID="lnkDelete"  CommandName="Delete" CausesValidation="False" ImageUrl="../ExaminationNImages/img/url.png"                           
                    CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>'  Width="20px" OnClientClick="return confirm('Really!Do u want to Delete')" runat="server" />
                   </ItemTemplate>                 
             </asp:TemplateField>
                          </Columns>
           
                         </asp:GridView>
                         
                         </div>
                          </div>
                       </div>

                       <div class="row">
                                 <div class="col-md-12 text-end">
                                
                                     <asp:LinkButton ID="btnAddsubject" runat="server" class="addsubject">Add Student</asp:LinkButton>
                      </div>
                      </div>
                      </asp:Panel>

                  <asp:Panel ID="PnlallSubjectlis"  runat="server">
                       <div class="row mt-3">
                       
                          <div class="col-md-12">
                              
                              
                          <div class="custom-scrollbar-css">
                          
                            <asp:GridView ID="GridMapSubject" GridLines="Both"  Width="100%"  DataKeyNames="Studentid"
                           AutoGenerateColumns="False" runat="server" CellPadding="0" 
                           ShowHeaderWhenEmpty="true" CssClass="table table-bordered">
                           <Columns>
                           <asp:TemplateField HeaderText="S.No.">
                         <HeaderTemplate>
                         <asp:CheckBox ID="CheckBox1" runat="server"  Onclick="javascript:SelectheaderCheckboxes(this)" />
                         </HeaderTemplate>
                         <ItemTemplate>
                         <asp:CheckBox ID="btnselect" runat="server" />
                         </ItemTemplate>
                         </asp:TemplateField>
                           <asp:TemplateField HeaderText="S.No.">
                           <ItemTemplate>
                           <%#Container.DataItemIndex+1 %>
                           </ItemTemplate>
                          </asp:TemplateField>          
                         <asp:BoundField HeaderText="Leadid" ItemStyle-Width="0px"  DataField="Studentid" ItemStyle-CssClass="hiddencol" HeaderStyle-CssClass="hiddencol" ReadOnly="true"></asp:BoundField>           
           
              <asp:BoundField HeaderText="Subject Name"  DataField="Student" ItemStyle-CssClass="hiddencol" HeaderStyle-CssClass="hiddencol"></asp:BoundField>
                         <asp:BoundField HeaderText="Admissionno" DataField="Admissionno"></asp:BoundField>
                          
                          <asp:TemplateField HeaderText="Student Name">
                          <ItemTemplate >
                          <asp:LinkButton  Text='<%# Eval("Student") %>' ID="namelink" runat="server" CommandName="SubjectName"
                          CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>' CssClass="SubjectName"></asp:LinkButton>       
                           </ItemTemplate>
                           </asp:TemplateField>
                           <asp:BoundField HeaderText="Father Name" DataField="Fathername"></asp:BoundField>
                            
                          </Columns>
           
                         </asp:GridView>
                         
                         </div>
                          <div class="row">
                          <div class="col-md-2"></div>
                 <div class="col-md-8 d-flex justify-content-center ">
                   <asp:Button ID="btnsavesubject" class="btn Submit" runat="server" Text="Save"></asp:Button>
                 </div>

                                           <div class="col-md-2"></div>
                 </div>
                          </div>
                         
                       </div>

                       <div class="row">
                                 <div class="col-md-12 text-end">
           <%--                          <asp:LinkButton ID="btnmasterSubject" runat="server" class="addsubject" data-bs-toggle="modal" data-bs-target="#myModal" ><i class="fa-solid fa-plus"></i> Subject</asp:LinkButton>
           --%>           </div>
                      </div>
                      <div class="row">
                       <%--   <div class="modal fade" id="myModal" data-bs-backdrop="static" data-bs-keyboard="false">
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
                           <asp:TextBox ID="txtsub" runat="server" class="form-control input-sm" Placeholder="Subject full name"></asp:TextBox>
                       </div>             
                     </div>

                   <div class="form-group row mt-2">
                        <div class="col-md-1"></div>
                        <asp:Label ID="lblcode" class="col-md-4 col-form-label text-md-left labeltext" runat="server">Subject Code :</asp:Label>
                        <div class="col-md-6">
                           <asp:TextBox ID="txtcode" runat="server" class="form-control input-sm" Placeholder="Subject code "></asp:TextBox>
                        </div>          
                        </div>

                   <div class="form-group row mt-2">
                           <div class="col-md-1"></div>
                             <asp:Label ID="lblprefix" class="col-md-4 col-form-label text-md-left labeltext" runat="server"> Prefix :</asp:Label>
                            <div class="col-md-6">
                              <asp:TextBox ID="txtprefix" runat="server" class="form-control input-sm" Placeholder="Subject prefix"></asp:TextBox>
                            </div>         
                          </div>
                          <div class="form-group row mt-2">
                           <div class="col-md-1"></div>
                             <asp:Label ID="Label1" class="col-md-4 col-form-label text-md-left labeltext" runat="server"> Subject type :</asp:Label>
                            <div class="col-md-6">
                                <asp:DropDownList ID="Ddlsubtype" class="form-select" runat="server">
                                <asp:ListItem text="Theory">Theory</asp:ListItem>
                                <asp:ListItem text="Practical">Practical</asp:ListItem>
                                </asp:DropDownList>
                               </div>         
                          </div>
         
           </div>
               
           <div class="modal-footer">
         
           
                  <asp:LinkButton ID="btnCancel" class="btn btn-outline-dark mr-auto btncanelg" data-bs-dismiss="modal" runat="server"><i class="fa fa-window-close"></i> Cancel</asp:LinkButton>
               <asp:Button ID="btnAdd" class="btn communicatesub" Width="15%" runat="server"  Text="Save" />   
           </div>
          
           </div>
           </div>
           </div>--%>
                       </div>

                      </asp:Panel>

                       
                       
    </div>
    </asp:Panel>

    

    

    </form>
</body>
</html>
