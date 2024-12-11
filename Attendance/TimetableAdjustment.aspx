<%@ Page Language="VB" AutoEventWireup="false" CodeFile="TimetableAdjustment.aspx.vb" EnableEventValidation="false" Inherits="Attendance_TimetableAdjustment" %>

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
   .DownloadExcel
   {
   color:#808080;
   font-size: 22px !important;
   cursor: pointer;
   font-weight: 500;
   border:none;
       }
   
  .DownloadExcel:hover {
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
    .heading1
{
    padding-top:2px;
    }
  .heading1 h4
{
    color:#15283c;
    font-size:22px;
    font-weight:600;
   
    }
     .subheading h4
    {
        color:#15283c;
    font-size:20px;
    font-weight:500;
    margin-bottom:12px;
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
                              <asp:LinkButton ID="backbotton" class="backbotton" Visible="false" runat="server"><i class="fa-solid fa-arrow-left"></i></asp:LinkButton>
                              <asp:LinkButton ID="backbotton1" class="backbotton" Visible="false" runat="server"><i class="fa-solid fa-arrow-left"></i></asp:LinkButton> 
                                <asp:LinkButton ID="backbotton3" class="backbotton" Visible="false" runat="server"><i class="fa-solid fa-arrow-left"></i></asp:LinkButton>
                              
                               </div>
                   </td>
                   
                    <td width="50%" >
           <div class="heading1">
   <h4>TimeTable Adjustment</h4>
   </div>
                    </td>
                    
                    <td width="45%"></td>
                    </tr>
                    </table>
                    
                              
                             
                          </div>
                          
                          
                          <div class="col-md-4 heading1 text-end" hidden="true">
                              <h4><span class="">Academic Year : <span class="">
                              <asp:Label ID="lbltotalsub" runat="server" Text="N/A"></asp:Label></span>
                              </span></h4> 
                          </div>
                          </div> 
                  

                       <div class="row">
                          <div class="col-md-12">
                            <div class="line">
                            </div>
                          </div>
                       </div>


                         <asp:Panel ID="PnlAbsentfacltylist" Visible="True" runat="server">

                       <div class="row mt-1">
                          
                          
                          <div class="col-md-6 d-flex">
                          
                            <div class="subheading">
                            <h4>
                             Faculty Absent List
                            </h4>
                            </div>
                          </div>
                          
                          <div class="col-md-6 d-flex justify-content-end ">
                          
                           &nbsp &nbsp <asp:LinkButton ID="Download" runat="server" CssClass="DownloadExcel"> <i class="fa fa-download fa-2"></i></asp:LinkButton>
                          
                          </div> 

                         
                       </div>

                       <div class="row mt-2">
                                  <div class="col-md-12 text-end">

                                  </div>
                       </div>
             
           
                       <div class="row mt-3">
                          <div class="col-md-12">
                              
                              
                          <div class="custom-scrollbar-css">
                          
                            <asp:GridView ID="gridAbsentFaculty" GridLines="Both" 
                           AutoGenerateColumns="False" runat="server" CellPadding="0" 
                           ShowHeaderWhenEmpty="true" CssClass="table table-bordered">
                           <Columns>
                           <asp:TemplateField HeaderText="S.No.">
                           <ItemTemplate>
                           <%#Container.DataItemIndex+1 %>
                           </ItemTemplate>
                          </asp:TemplateField>          
                          <asp:BoundField HeaderText="Attend id" HeaderStyle-CssClass="hiddencol" ItemStyle-CssClass="hiddencol" DataField="attid" ></asp:BoundField>           
           
              <asp:BoundField HeaderText="Department id" HeaderStyle-CssClass="hiddencol" ItemStyle-CssClass="hiddencol"  DataField="DEPARTMENTID" ></asp:BoundField>
                            <asp:BoundField HeaderText="Emp Id" HeaderStyle-CssClass="hiddencol" ItemStyle-CssClass="hiddencol" DataField="Empid" ></asp:BoundField>
                         <asp:BoundField HeaderText="Total Employee" DataField="Employee"></asp:BoundField>
                        <asp:BoundField HeaderText="Status" DataField="P/A"></asp:BoundField>
                          
                          <asp:TemplateField HeaderText="View Lecture">
                          <ItemTemplate >
                          <asp:LinkButton   ID="namelink" runat="server" CommandName="Viewlecture"
                          CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>' CssClass="SubjectName"><i class="fa-solid fa-eye"></i></asp:LinkButton>       
                           </ItemTemplate>
                           </asp:TemplateField>

                           <asp:TemplateField HeaderText="Status">
                          <ItemTemplate >
                          <asp:LinkButton   ID="namel" runat="server" CommandName="Timetle" Readonly="True"
                          CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>' CssClass="SubjectName" OnClientClick="Return false;"><i class="fa-solid fa-check"></i></asp:LinkButton>       
                           </ItemTemplate>
                           </asp:TemplateField>    
                            
                          </Columns>
           
                         </asp:GridView>
                         
                         </div>
                          </div>
                       </div>

                       
                      </asp:Panel>
                    
                     
                       <asp:Panel ID="panellecture" Visible="false" runat="server">

                         <div class="row mt-1">
                          
                          
                          <div class="col-md-6 d-flex">
                          
                            <div class="subheading">
                            <h4>
                                <asp:Label ID="lblday" runat="server" Text="Label"></asp:Label>
                            </h4>
                            </div>
                          </div>
                          
                          <div class="col-md-6 d-flex justify-content-end ">
                          
                           &nbsp &nbsp <asp:LinkButton ID="LinkButton1" runat="server" CssClass="DownloadExcel"> <i class="fa fa-download fa-2"></i></asp:LinkButton>
                          
                          </div> 

                         
                       </div>

                       <div class="row mt-2">
                                  <div class="col-md-12 text-end">

                                  </div>
                       </div>

                       <div class="row mt-3">
                          <div class="col-md-12">
                              
                              
                          <div class="custom-scrollbar-css">
                          
                            <asp:GridView ID="grdtodaylecture" GridLines="Both" 
                           AutoGenerateColumns="False" runat="server" CellPadding="0" 
                           ShowHeaderWhenEmpty="true" CssClass="table table-bordered">
                           <Columns>
                           <asp:TemplateField HeaderText="S.No.">
                           <ItemTemplate>
                           <%#Container.DataItemIndex+1 %>
                           </ItemTemplate>
                          </asp:TemplateField>          
                          <asp:BoundField HeaderText="Period"  DataField="Prd" ></asp:BoundField>           
            <asp:BoundField HeaderText="Timetableid" HeaderStyle-CssClass="hiddencol" ItemStyle-CssClass="hiddencol"  DataField="Timetableid" ></asp:BoundField>   
              <asp:BoundField HeaderText="Subject"  DataField="Subject" ></asp:BoundField>
                            <asp:BoundField HeaderText="Section"  DataField="Code" ></asp:BoundField>
                         <asp:BoundField HeaderText="Program" DataField="Course"></asp:BoundField>
                        <asp:BoundField HeaderText="ClassRoom" DataField="ClassRoom"></asp:BoundField>
                          
                          <asp:TemplateField HeaderText="Availability">
                          <ItemTemplate >
                          <asp:LinkButton   ID="namelink" runat="server" CommandName="Adjustment"
                          CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>' CssClass="SubjectName"><i class="fa-solid fa-plus"></i></asp:LinkButton>       
                           </ItemTemplate>
                           </asp:TemplateField>

                         <asp:BoundField HeaderText="Teacher" DataField="usernamefull"></asp:BoundField>
                          
                            
                          </Columns>
           
                         </asp:GridView>
                         
                         </div>
                          </div>
                       </div>

                       
                      </asp:Panel> 



                        <asp:Panel ID="paneladjustment" Visible="false" runat="server">

                         <div class="row mt-1">
                          
                          
                          <div class="col-md-1">
                          
                            
                                <asp:Label ID="Label1" runat="server" Text="Subject :"></asp:Label>
                            
                          </div>

                           <div class="col-md-4 ">
                               <asp:DropDownList ID="Ddlsubject" AutoPostBack="true" class="form-select" runat="server">
                               </asp:DropDownList>
                           </div>

                              <div class="col-md-1">
                              </div>

                          
                          <div class="col-md-6 d-flex justify-content-end ">
                          
                           &nbsp &nbsp <asp:LinkButton ID="LinkButton2" runat="server" CssClass="DownloadExcel"> <i class="fa fa-download fa-2"></i></asp:LinkButton>
                          
                          </div> 

                         
                       </div>

                       <div class="row mt-2">
                                  <div class="col-md-12 text-end">

                                  </div>
                       </div>

                       <div class="row mt-3">
                          <div class="col-md-12">
                              
                              
                          <div class="custom-scrollbar-css">
                          
                            <asp:GridView ID="grdapply" GridLines="Both" 
                           AutoGenerateColumns="False" runat="server" CellPadding="0" 
                           ShowHeaderWhenEmpty="true" CssClass="table table-bordered">
                           <Columns>
                           <asp:TemplateField HeaderText="S.No.">
                           <ItemTemplate >
                           <%#Container.DataItemIndex+1 %>
                           </ItemTemplate>
                          </asp:TemplateField> 
                          <asp:BoundField ItemStyle-Width="20px" HeaderStyle-CssClass="hiddencol" ItemStyle-CssClass="hiddencol" DataField="TeacherID" HtmlEncode="false" 
                HeaderText="Facultyid" > 
                <ItemStyle Width="20px"></ItemStyle>
            </asp:BoundField>        
                         <asp:BoundField ItemStyle-Width="120px" DataField="usernamefull" HtmlEncode="false" 
                HeaderText="Faculty" >
<ItemStyle Width="120px"></ItemStyle>
            </asp:BoundField>
            <asp:BoundField ItemStyle-Width="70px" DataField="1" HtmlEncode="false" 
                HeaderText="I" >
<ItemStyle Width="70px"></ItemStyle>
            </asp:BoundField>
            <asp:BoundField ItemStyle-Width="70px" DataField="2" HtmlEncode="false" 
                HeaderText="II" >
<ItemStyle Width="70px"></ItemStyle>
            </asp:BoundField>
            <asp:BoundField ItemStyle-Width="70px" DataField="3" HtmlEncode="false" 
                HeaderText="III" >
<ItemStyle Width="70px"></ItemStyle>
            </asp:BoundField>
            <asp:BoundField ItemStyle-Width="70px" DataField="4" HtmlEncode="false" 
                HeaderText="IV" >
<ItemStyle Width="70px"></ItemStyle>
            </asp:BoundField>
            <asp:BoundField ItemStyle-Width="70px" DataField="5" HtmlEncode="false" 
                HeaderText="V" >
<ItemStyle Width="70px"></ItemStyle>
            </asp:BoundField>
            <asp:BoundField ItemStyle-Width="70px" DataField="6" HtmlEncode="false" 
                HeaderText="VI" >
<ItemStyle Width="70px"></ItemStyle>
            </asp:BoundField>
            <asp:BoundField ItemStyle-Width="70px" DataField="7" HtmlEncode="false" 
                HeaderText="VII" >
<ItemStyle Width="70px"></ItemStyle>
            </asp:BoundField>
            <asp:BoundField ItemStyle-Width="70px" DataField="8" HtmlEncode="false" 
                HeaderText="VIII" >
<ItemStyle Width="70px"></ItemStyle>
            </asp:BoundField>
            <asp:BoundField ItemStyle-Width="70px" DataField="9" HtmlEncode="false" 
                HeaderText="IX" >
<ItemStyle Width="70px"></ItemStyle>
            </asp:BoundField>
            <asp:BoundField ItemStyle-Width="70px" DataField="10" HtmlEncode="false" 
                HeaderText="X" >
<ItemStyle Width="70px"></ItemStyle>
            </asp:BoundField>
            <asp:BoundField ItemStyle-Width="70px" DataField="11" HtmlEncode="false" 
                HeaderText="XI" >
<ItemStyle Width="70px"></ItemStyle>
            </asp:BoundField>
                          
                          <asp:TemplateField HeaderText="Apply">
                          <ItemTemplate >
                          <asp:LinkButton   ID="namelink" runat="server" CommandName="Apply"
                          CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>' text="Apply" ></asp:LinkButton>       
                           </ItemTemplate>
                           </asp:TemplateField>

                          
                            
                          </Columns>
           
                         </asp:GridView>
                         
                         </div>
                          </div>
                       </div>

                       
                      </asp:Panel> 

                       
                       
    </div>
    </asp:Panel>

    

    

    </form>
</body>
</html>
