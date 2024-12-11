<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Department.aspx.vb" Inherits="Examinationjune_Department" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>

     <link href="../Bootstrap5/css/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <script src="../Bootstrap5/js/bootstrap.min.js" type="text/javascript"></script>
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.9.1/jquery.min.js"></script>
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.4.0/css/all.min.css"  />
    <script type="text/javascript" src="../JavaScript/jquery.min.1.7.2.js"></script>
  <style>
      
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
      
    body
        {
            overflow-x: hidden;
            background-color: #f2f3f5;
            background-repeat: no-repeat;
            background-size: 100% 100%;
        }
     
         body::-webkit-scrollbar {
            display: none;
        }
   .maincontainer {
    border: 2px solid #fff;
    padding: 10px;
    background-color:#fff;
    border-radius: 6px;
    text-align: left;
   }
    .viewprofile
    {
   color:gray;
   font-size: 18px !important;
   cursor: pointer;
   font-weight: 500;
   border:none;
       }
   
  .viewprofile:hover {
   color:black;
   font-weight: 600;
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

 .DownloadExcel
   {
   color:gray;
   font-size: 22px !important;
   cursor: pointer;
   font-weight: 500;
   border:none;
       }
   
 .DownloadExcel:hover {
   color:black;
   font-weight: 600;
    } 
   .CourseName
   {
       color:#1ed085;
       text-decoration:none;
       font-weight:500;
       font-size:20px;
    }
     .CourseName:hover
   {
       color:#20a16a;
       text-decoration:none;
       font-weight:400;
    }
     .CourseName1
   {
       color:#1ed085;
       text-decoration:none;
       font-weight:500;
       
    }
     .CourseName1:hover
   {
       color:#20a16a;
       text-decoration:none;
       font-weight:400;
    }
  .line
   {
       content:" ";
       width:100%;
       height:1px;
       background-color:#e1e2e3;
       margin-top:8px;
       margin-bottom:8px;
       
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
    width:11%;
   }
   .Submit:hover
   {
       color:White;
       background-color:#1aad6f;
       border:none;
       box-shadow:0px 1px 5px 1px #dcdcdc;
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
   margin-left:200px;
    }
    .addsubject:hover
    {
   color:#fff;
   background-color:#15283c;
   text-decoration:none;
    }
.card-header
 {
     background-color:#152837;
     color:#fff;
     font-weight:500;
     font-size:22px;
     letter-spacing:1px;
     }
    .SubjectName
   {
       color:#1ed085;
       text-decoration:none;
       font-weight:400;
       font-size:16px
    }
.SubjectName:hover
   {
       color:#20a16a;
       text-decoration:none;
       font-weight:400;
    }
    .modal-header {
  
  text-align: left;
  font-size: 22px;
  color: #f2f3f5;
  
  background-color: #152837;
  border-bottom: 0px;
  height:50px;
}


  #backbotton
{
    font-size:24px;
    font-weight:600;
    color:#7c858f;
    margin-left:5px;
}
#backbotton:hover
{
    color:#15283c;
}
.heading
{
    font-size:20px;
    color:#15283c;
    font-weight:500;
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


                    row.style.backgroundColor = "white";

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
    <div>
    
    <asp:Panel ID="pnldepartments"  runat="server" Visible="true">
    <div class="container-fluid maincontainer">
     <div class="row">
    <div class="col-md-6">
    <div class="heading1">
      <h5>Department</h5>
    </div>
    </div> 

    <div class="col-md-6 text-end ">
           <asp:LinkButton ID="btnAddDep" runat="server" CssClass="btn addsubject" ><i class="fa fa-plus"></i> Add Department</asp:LinkButton>
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
<asp:GridView ID="grddepartment" GridLines="Both" DataKeyNames="InstitueID" AutoGenerateColumns="False" 
runat="server" CellPadding="0" CssClass="table table-bordered" ShowHeaderWhenEmpty="true">
<Columns>

<asp:TemplateField HeaderText="S.No.">
<ItemTemplate>
<%#Container.DataItemIndex+1 %>
</ItemTemplate>
</asp:TemplateField>
<asp:BoundField HeaderText="Department Id" ItemStyle-CssClass="hiddencol" HeaderStyle-CssClass="hiddencol" DataField="InstitueID"></asp:BoundField>
<asp:BoundField HeaderText="Department" DataField="Institue"></asp:BoundField>

  <asp:TemplateField HeaderText="Branch">
            <ItemTemplate >
             <asp:LinkButton  Text='<%# Eval("Total") %>' ID="namelinkes" runat="server" CommandName="Branch"  CssClass="CourseName" CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>'  ></asp:LinkButton>       
            </ItemTemplate>
        </asp:TemplateField>
                  


            
</Columns>
           
</asp:GridView>
</div>
</div>                                                   
     </div>                     
    </asp:Panel>

     <asp:Panel ID="Pnladddepart"  runat="server" Visible="false">
    <div class="container-fluid maincontainer">
     <div class="row">
   
    <div class="col-md-6">
    <table>
    <tr width="100%">
    <td width="10%">
     <asp:LinkButton ID="btnback" class="backbotton" runat="server"><i class="fa-solid fa-arrow-left"></i></asp:LinkButton>
    </td>
    <td width="5%"></td>
    <td width="85%">
     <div class="heading1">
      <h5>Add Department</h5>
    </div>
    </td>
    </tr>
    </table>
   
    </div> 

    <div class="col-md-6 text-end mt-2">
         
     </div>

   
    </div>
                     
     <div class="row">
    <div class="col-md-12">
    <div class="line">
    </div>
    </div>
    </div>

    <div class="row">
          <div class="col-md-2"></div> 
       <div class="col-md-2 text-end mt-1"> <label>Department Name : </label></div>
                            
                        <div class="col-md-6">
                         <asp:TextBox ID="txtdepartmentname" runat="server" class="form-control" Placeholder="Department Name"></asp:TextBox>
                         </div>  
        
        <div class="col-md-2"></div>            
      </div>

        <div class="row mt-3 mb-1">
         <div class="col-md-12 text-center"> 
                    <asp:Button ID="btnsubmit" class="btn Submit" runat="server" Text="Add" Width="14%"></asp:Button>  
                    <asp:Button ID="btnupdate" class="btn Submit" runat="server" Visible="false" Text="Update" Width="14%"></asp:Button>  
          </div>
          </div>
 
     <div class="row">
<div class="col-md-12">
<asp:GridView ID="Griddepart" GridLines="Both" DataKeyNames="InstitueID" AutoGenerateColumns="False" 
runat="server" CellPadding="0" CssClass="table table-bordered"  OnRowEditing="OnRowEditing" OnRowDeleting="OnRowDeleting" ShowHeaderWhenEmpty="true">
<Columns>

<asp:TemplateField HeaderText="Modify">
                   <ItemTemplate>
                       <asp:LinkButton ID="lnkModify"  CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>' CommandName="Edit"  runat="server" ><i class="fa-solid fa-pen-to-square" style="color: #27aa48;"></i></asp:LinkButton>
                      </ItemTemplate>
                       <%--<EditItemTemplate>
                   <asp:Button ID="btnUpdate" runat="server" Text="Update" CommandName="Update"/>
                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" CommandName="Cancel"/>
                   </EditItemTemplate>--%>
             </asp:TemplateField>

<asp:TemplateField HeaderText="S.No.">
<ItemTemplate>
<%#Container.DataItemIndex+1 %>
</ItemTemplate>
</asp:TemplateField>
<asp:BoundField HeaderText="Department Id" ItemStyle-CssClass="hiddencol" HeaderStyle-CssClass="hiddencol" DataField="InstitueID"></asp:BoundField>
<asp:BoundField HeaderText="Department" ReadOnly="true" DataField="Institue"></asp:BoundField>

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
    </asp:Panel>

    <asp:Panel ID="PnlBranch"  runat="server" Visible="false">
    <div class="container-fluid maincontainer">
     <div class="row">
    <div class="col-md-6">
     <table>
    <tr width="100%">
    <td width="10%">
     <asp:LinkButton ID="btnbakbranch" class="backbotton" runat="server"><i class="fa-solid fa-arrow-left"></i></asp:LinkButton>
    </td>
    <td width="5%"></td>
    <td width="85%">
    <div class="heading1">
      <h5>Branch of <span>( <asp:Label ID="lbldepart" runat="server" Text=""></asp:Label> )</span></h5>
    </div>
    </td>
    </tr>
    </table>
    </div> 

    <div class="col-md-6 text-end">
           <asp:LinkButton ID="lnkbtnaddbranch" runat="server" Visible="false" CssClass="btn addsubject" ><i class="fa fa-plus"></i> Add Branch</asp:LinkButton>
     </div>

   
    </div>
                     
     <div class="row">
    <div class="col-md-12">
    <div class="line">
    </div>
    </div>
    </div>
 
        <div class="row">
          <div class="col-md-2"></div> 
       <div class="col-md-2 text-end mt-1"> <label>Branch Name : </label></div>
                            
                        <div class="col-md-6">
                         <asp:TextBox ID="txtbranch" runat="server" class="form-control" Placeholder="Branch Name"></asp:TextBox>
                         </div>  
        
        <div class="col-md-2"></div>            
      </div>

        <div class="row mt-3 mb-1">
         <div class="col-md-12 text-center"> 
                    <asp:Button ID="btnaddbranch" class="btn Submit" runat="server" Text="Add" Width="14%"></asp:Button>  
                    <asp:Button ID="btnupdatebranch" class="btn Submit" runat="server" Visible="false" Text="Update" Width="14%"></asp:Button>  
          </div>
          </div>

     <div class="row">
<div class="col-md-12">
<asp:GridView ID="Grdbranch" GridLines="Both" DataKeyNames="Branchid" AutoGenerateColumns="False" 
runat="server" CellPadding="0" CssClass="table table-bordered" OnRowEditing="OnRowEditing" OnRowDeleting="OnRowDeleting" ShowHeaderWhenEmpty="true">
<Columns>

<asp:TemplateField HeaderText="Modify">
                   <ItemTemplate>
                       <asp:LinkButton ID="lnkModify"  CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>' CommandName="Edit"  runat="server" ><i class="fa-solid fa-pen-to-square" style="color: #27aa48;"></i></asp:LinkButton>
                      </ItemTemplate>
                       <%--<EditItemTemplate>
                   <asp:Button ID="btnUpdate" runat="server" Text="Update" CommandName="Update"/>
                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" CommandName="Cancel"/>
                   </EditItemTemplate>--%>
             </asp:TemplateField>

<asp:TemplateField HeaderText="S.No.">
<ItemTemplate>
<%#Container.DataItemIndex+1 %>
</ItemTemplate>
</asp:TemplateField>
<asp:BoundField HeaderText="Branch Id" ItemStyle-CssClass="hiddencol" HeaderStyle-CssClass="hiddencol" DataField="BranchID"></asp:BoundField>
<asp:BoundField HeaderText="Branch" ReadOnly="true" DataField="Code"></asp:BoundField>

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
    </asp:Panel>

    </div>
    </form>
</body>
</html>
