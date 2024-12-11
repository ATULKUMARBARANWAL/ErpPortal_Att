<%@ Page Language="VB" EnableEventValidation="false" ValidateRequest="false" AutoEventWireup="false" CodeFile="updateatt.aspx.vb" Inherits="Payroll_updateatt" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Untitled Page</title>    
    <script type="text/javascript" src="../bootstrap-3.3.7-dist/js/jquery-3.2.1.min.js"></script>
       <script type="text/javascript" src="../Scripts/manojshort.js"></script>
      <link href="../Bootstrap5/css/bootstrap.min.css" rel="stylesheet" type="text/css" />
        <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.4.0/css/all.min.css"  />
    <style type="text/css">
        #btnHidden
        {
            visibility: hidden;
        }
        
         .kh
        {
            visibility: hidden;
        }
        
          b
        {
            color: red;
            background-color :Red;
        }
        d
        {
            color: blue;
        }
        
        tb
        {
             font-size:small;
             vertical-align:top;
             border: 1px solid black;
        }
        .tbl
        {
           width:100%
            }
        
        th {
    white-space: nowrap;
}
td {
    white-space: nowrap;
}
        
        
       div {
    padding: 0px 0px 7px 0px;
  font-size: 12px;
   font-family:Arial;
   }
    </style>
        
    <script type="text/javascript">
        function boxDisable(t) {
            //   alert(t);
            if ($('#' + t + '').is(':checked')) {
                $(".tbl td").each(function () {
                    $('.' + t + '').prop('checked', true);
                });

            } else {
                $(".tbl td").each(function () {
                    $('.' + t + '').prop('checked', false);
                });
            }
        };
    </script>

    
    <script type="text/javascript">

        function someFunction() {
            alert("Changed");
        } 

    </script>

      <script type="text/javascript">
          function ddl_changed(ddl) {
              alert(ddl.value);
          }
    </script>
<style>
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
 .backbotton1
{
    font-size:22px;
    font-weight:600;
    color:#fff; 
}

.backbotton1:hover
{
    color:#fff;
}  

 .CourseName
   {
       color:#1ed085;
       text-decoration:none;
       font-weight:500;
    }
     .CourseName:hover
   {
       color:#20a16a;
       text-decoration:none;
       font-weight:400;
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
#Panel2
{
    min-height:400px;
    max-height:400px;
    }  

.Submit
 {
     font-size: 18px !important;
      font-weight: 500;
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
     .line
   {
       content:" ";
       width:100%;
       height:1px;
       background-color:#e1e2e3;
       margin-top:8px;
       margin-bottom:8px;
       
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
   padding:4px 10px;
    }
    .addsubject:hover
    {
   color:#fff;
   background-color:#15283c;
   text-decoration:none;
    } 
 #Panel1
 {
     min-height:500px;
     max-height:500px;
     }
 </style>

</head>
<body>
    <form id="form1" runat="server" autocomplete="off">

    <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
            
  <div class="container-fluid maincontainer">        

  <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
<div class="chh" style="display:none">
<div class="row">
<div class="col-sm-2"></div>
<div class="col-sm-2">College</div>
<div class="col-sm-6">
<ddlcollege:DropDownList ID="ddlcollege"    class="form-control"   width="100%"  AutoPostBack="true" runat="server">
</ddlcollege:DropDownList></div>
<div class="col-sm-2"></div>
</div>

<div class="row mt-2">
<div class="col-sm-2"></div>
<div class="col-sm-2">Department</div>
<div class="col-sm-6">
<ddldepartment:DropDownList ID="ddldepartment"   onchange="someFunction()"  placeholder="select your beverage" class="form-control"  width="100%"  runat="server">
</ddldepartment:DropDownList>
</div>
<div class="col-sm-2"></div>
</div>

<div class="row mt-2">
<div class="col-sm-2"></div>
<div class="col-sm-2">Date</div>
<div class="col-sm-6">
<datetxt:textbox ID="txtDt"   width="150"  class="form-control" AutoPostBack="True" runat="server">
</datetxt:textbox>
</div>
<div class="col-sm-2"></div>
</div>
</div>


     
   


 </ContentTemplate>
  
    </asp:UpdatePanel>

  <asp:UpdatePanel ID="UpdatePanel3" runat="server">
        <ContentTemplate>

        <div class="row">
        <div class="col-md-12">
         <table width="100%">
        <tr width="100%">
        <td width="4%">
        <div class="heading1 d-flex"> 
          <asp:LinkButton ID="btnheadback" class="backbotton" runat="server"><i class="fa-solid fa-arrow-left"></i></asp:LinkButton>
        </div> 
        </td>
        <td width="16%">
            <div class="heading1 mt-1">
                    <h5>Staff Attendance </h5>
            </div>
        </td>
        <td width="30%">
        <asp:DropDownList ID="Ddlprogram" AutoPostBack="true" class="form-select" runat="server">
                               </asp:DropDownList>
        </td>
        <td width="25%"></td>
        <td width="5%">Date :</td>
        <td width="10%">
           <asp:TextBox ID="TxtDate" runat="server" AutoPostBack="true" CssClass="form-control" TextMode="Date"></asp:TextBox>

        </td>
        <td width="10%" align="center">
            <asp:Label ID="Label1" runat="server" >20/12/2023</asp:Label>
        </td>
        </tr>
        </table>
        </div>
        </div>
      
    <asp:Panel ID="Panel1" runat="server" ScrollBars="Auto">
   

      <asp:Literal ID="Literal1" runat="server"></asp:Literal>               
      </asp:Panel>
      <div class="row mt-2">
<div class="col-sm-12 text-center">
<label id="msgsave" runat="server" > </label>
 <asp:Button ID="BtnLoad"   class=" btn Submit" runat="server" Text="Load" Width="60px" Visible="false"/>
           <asp:Button ID="btnsave" runat="server" Text="Insert" CssClass="btn Submit" Width="150px"/>  
</div>
</div>
  </ContentTemplate>
    <Triggers>
            <asp:AsyncPostBackTrigger ControlID="BtnLoad" EventName="Click" />
           
                
        </Triggers>
    </asp:UpdatePanel>

    </div>
    </form>
</body>
</html>
